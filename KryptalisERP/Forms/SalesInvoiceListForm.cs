using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
// ✅ Βεβαιώσου ότι έχεις αυτό το using αν το Service είναι σε υπο-namespace
using KryptalisERP.Services;

namespace KryptalisERP
{
    public class SalesInvoiceListForm : Form
    {
        private DataGridView dgvInvoices;
        private TextBox txtSearch;
        private ComboBox cmbDocType, cmbSeries, cmbCustomer, cmbMyDataStatus;
        private DateTimePicker dtpFrom, dtpTo;
        private CheckBox chkShowCancelled;
        private Button btnNew, btnEdit, btnDelete, btnMyData, btnPrint, btnRefresh, btnClose;
        private Label lblTotalRecords, lblTotalAmount;

        public SalesInvoiceListForm()
        {
            InitializeUI();
            this.Load += SalesInvoiceListForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "📋 Λίστα Τιμολογίων Πώλησης";
            this.Size = new Size(1600, 850);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1300, 700);

            // ============================================
            // TOOLBAR
            // ============================================
            var pnlToolbar = new Panel { Dock = DockStyle.Top, Height = 120, BackColor = Color.White, Padding = new Padding(15, 10, 15, 10), Margin = new Padding(0, 0, 0, 5) };

            btnNew = CreateButton("➕ Νέο Τιμολόγιο", 10, 10, 150, 38, Color.FromArgb(46, 204, 113));
            btnNew.Click += (s, e) => OpenInvoiceForm(null);
            pnlToolbar.Controls.Add(btnNew);

            btnEdit = CreateButton("✏️ Επεξεργασία", 170, 10, 130, 38, Color.FromArgb(52, 152, 219));
            btnEdit.Click += (s, e) => EditSelectedInvoice();
            pnlToolbar.Controls.Add(btnEdit);

            btnDelete = CreateButton("🗑️ Διαγραφή", 310, 10, 120, 38, Color.FromArgb(231, 76, 60));
            btnDelete.Click += (s, e) => DeleteSelectedInvoice();
            pnlToolbar.Controls.Add(btnDelete);

            btnMyData = CreateButton("🏛️ Αποστολή", 440, 10, 130, 38, Color.FromArgb(52, 73, 94));
            btnMyData.Click += (s, e) => SendSelectedToMyData();
            pnlToolbar.Controls.Add(btnMyData);

            btnPrint = CreateButton("🖨️ Εκτύπωση", 580, 10, 120, 38, Color.FromArgb(155, 89, 182));
            btnPrint.Click += (s, e) => MessageBox.Show("Εκτύπωση - Υπό υλοποίηση", "Πληροφορία");
            pnlToolbar.Controls.Add(btnPrint);

            btnRefresh = CreateButton("🔄 Ανανέωση", 710, 10, 120, 38, Color.FromArgb(108, 117, 125));
            btnRefresh.Click += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(btnRefresh);

            btnClose = CreateButton("❌ Κλείσιμο", 840, 10, 110, 38, Color.FromArgb(149, 165, 166));
            btnClose.Click += (s, e) => this.Close();
            pnlToolbar.Controls.Add(btnClose);

            // Γραμμή 2: Φίλτρα
            int y2 = 55, lx = 10;

            AddFilterLabel(pnlToolbar, "Από:", lx, y2);
            dtpFrom = new DateTimePicker { Location = new Point(lx + 40, y2), Size = new Size(120, 28), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            dtpFrom.Value = DateTime.Now.AddMonths(-3);
            dtpFrom.ValueChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(dtpFrom);
            lx += 40 + 120 + 15;

            AddFilterLabel(pnlToolbar, "Έως:", lx, y2);
            dtpTo = new DateTimePicker { Location = new Point(lx + 40, y2), Size = new Size(120, 28), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            dtpTo.ValueChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(dtpTo);
            lx += 40 + 120 + 15;

            AddFilterLabel(pnlToolbar, "Τύπος:", lx, y2);
            cmbDocType = new ComboBox { Location = new Point(lx + 50, y2), Size = new Size(170, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbDocType.SelectedIndexChanged += (s, e) => { LoadSeriesFilter(); LoadInvoices(); };
            pnlToolbar.Controls.Add(cmbDocType);
            lx += 50 + 170 + 15;

            AddFilterLabel(pnlToolbar, "Σειρά:", lx, y2);
            cmbSeries = new ComboBox { Location = new Point(lx + 50, y2), Size = new Size(150, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbSeries.SelectedIndexChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(cmbSeries);

            // Γραμμή 3
            int y3 = 88; lx = 10;

            AddFilterLabel(pnlToolbar, "Πελάτης:", lx, y3);
            cmbCustomer = new ComboBox { Location = new Point(lx + 65, y3), Size = new Size(230, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbCustomer.SelectedIndexChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(cmbCustomer);
            lx += 65 + 230 + 15;

            AddFilterLabel(pnlToolbar, "myDATA:", lx, y3);
            cmbMyDataStatus = new ComboBox { Location = new Point(lx + 65, y3), Size = new Size(150, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbMyDataStatus.Items.AddRange(new object[] { "Όλα", "Σταλμένα", "Μη Σταλμένα", "Με Σφάλμα" });
            cmbMyDataStatus.SelectedIndex = 0;
            cmbMyDataStatus.SelectedIndexChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(cmbMyDataStatus);
            lx += 65 + 150 + 15;

            AddFilterLabel(pnlToolbar, "🔍 Αναζήτηση:", lx, y3);
            txtSearch = new TextBox { Location = new Point(lx + 90, y3), Size = new Size(230, 28), Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle };
            txtSearch.TextChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(txtSearch);
            lx += 90 + 230 + 15;

            chkShowCancelled = new CheckBox { Text = "Εμφάνιση Ακυρωμένων", Location = new Point(lx, y3 + 4), AutoSize = true, Font = new Font("Segoe UI", 9F) };
            chkShowCancelled.CheckedChanged += (s, e) => LoadInvoices();
            pnlToolbar.Controls.Add(chkShowCancelled);

            // ============================================
            // STATUS BAR
            // ============================================
            var pnlStatus = new Panel { Dock = DockStyle.Bottom, Height = 35, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(15, 5, 15, 5) };
            lblTotalRecords = new Label { Text = "Εγγραφές: 0", Location = new Point(15, 8), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219) };
            pnlStatus.Controls.Add(lblTotalRecords);
            lblTotalAmount = new Label { Text = "Συνολική Αξία: 0,00 €", Location = new Point(200, 8), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) };
            pnlStatus.Controls.Add(lblTotalAmount);

            // ============================================
            // GRID
            // ============================================
            dgvInvoices = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                EnableHeadersVisualStyles = false,
                Font = new Font("Segoe UI", 10F),
                RowTemplate = { Height = 32 },
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false
            };

            dgvInvoices.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(13, 110, 253),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(5)
            };

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocNumber", DataPropertyName = "DocNumber", HeaderText = "Αριθμός", Width = 120 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocDate", DataPropertyName = "DocDate", HeaderText = "Ημερομηνία", Width = 110 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocTypeName", DataPropertyName = "DocTypeName", HeaderText = "Τύπος", Width = 150 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "SeriesName", DataPropertyName = "SeriesName", HeaderText = "Σειρά", Width = 120 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "CustomerName", DataPropertyName = "CustomerName", HeaderText = "Πελάτης", Width = 250 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "NetValue", DataPropertyName = "NetValue", HeaderText = "Καθαρή €", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "VatValue", DataPropertyName = "VatValue", HeaderText = "ΦΠΑ €", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "GrandTotal", DataPropertyName = "GrandTotal", HeaderText = "Σύνολο €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "MyDataStatus", DataPropertyName = "MyDataStatus", HeaderText = "myDATA", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "IsActive", DataPropertyName = "IsActive", HeaderText = "Κατάσταση", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            dgvInvoices.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditSelectedInvoice(); };
            dgvInvoices.CellFormatting += DgvInvoices_CellFormatting;

            // ============================================
            // ΣΩΣΤΗ ΣΕΙΡΑ ΠΡΟΣΘΗΚΗΣ
            // ============================================
            this.Controls.Add(pnlToolbar);
            this.Controls.Add(pnlStatus);
            this.Controls.Add(dgvInvoices);
            dgvInvoices.BringToFront();
        }

        private void AddFilterLabel(Panel p, string text, int x, int y)
        {
            p.Controls.Add(new Label { Text = text, Location = new Point(x, y + 5), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(33, 37, 41) });
        }

        private void DgvInvoices_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvInvoices.Rows[e.RowIndex];

            string isActive = row.Cells["IsActive"].Value?.ToString();
            if (isActive == "❌ Ακυρωμένο")
            {
                row.DefaultCellStyle.ForeColor = Color.Gray;
                row.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            }

            if (e.ColumnIndex == dgvInvoices.Columns["MyDataStatus"].Index)
            {
                string status = e.Value?.ToString();
                if (status == "✅ Σταλμένο") { e.CellStyle.ForeColor = Color.FromArgb(46, 204, 113); e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold); }
                else if (status == "❌ Σφάλμα") { e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60); e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold); }
                else { e.CellStyle.ForeColor = Color.FromArgb(241, 196, 15); }
            }
        }

        private Button CreateButton(string text, int x, int y, int w, int h, Color color)
        {
            return new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
        }

        private void SalesInvoiceListForm_Load(object sender, EventArgs e)
        {
            LoadDocumentTypes();
            LoadCustomers();
            LoadInvoices();
        }

        private void LoadDocumentTypes()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var types = conn.Query("SELECT Id, Name FROM DocumentTypes WHERE IsSales = 1 AND IsActive = 1 ORDER BY Name").ToList();
                cmbDocType.Items.Clear();
                cmbDocType.Items.Add("Όλοι οι Τύποι");
                foreach (var t in types) cmbDocType.Items.Add(t.Name);
                cmbDocType.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadSeriesFilter()
        {
            cmbSeries.Items.Clear();
            cmbSeries.Items.Add("Όλες οι Σειρές");
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var series = conn.Query("SELECT Id, Name FROM DocumentSeries WHERE IsActive = 1 ORDER BY Name").ToList();
                foreach (var s in series) cmbSeries.Items.Add(s.Name);
            }
            catch { }
            cmbSeries.SelectedIndex = 0;
        }

        private void LoadCustomers()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var customers = conn.Query("SELECT Id, Epwnymia FROM Customers WHERE Energo = 1 ORDER BY Epwnymia").ToList();
                cmbCustomer.Items.Clear();
                cmbCustomer.Items.Add("Όλοι οι Πελάτες");
                foreach (var c in customers) cmbCustomer.Items.Add(c.Epwnymia);
                cmbCustomer.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadInvoices()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                // ✅ ΔΙΟΡΘΩΣΗ SQL: Το MyDataErrors συνδέεται μέσω MyDataDocuments
                string sql = @"
                    SELECT 
                        si.Id, si.DocNumber, si.DocDate,
                        dt.Name AS DocTypeName,
                        ds.Name AS SeriesName,
                        c.Epwnymia AS CustomerName,
                        si.NetValue, si.VatValue, si.GrandTotal,
                        CASE 
                            WHEN md.UID IS NOT NULL THEN '✅ Σταλμένο'
                            WHEN EXISTS (SELECT 1 FROM MyDataErrors me JOIN MyDataDocuments mdd ON me.MyDataDocumentId = mdd.Id WHERE mdd.DocumentId = si.Id AND mdd.InvoiceType = 'SALES_INVOICE') THEN '❌ Σφάλμα'
                            ELSE '⏳ Εκκρεμεί'
                        END AS MyDataStatus,
                        CASE WHEN si.IsActive = 1 THEN '✅ Ενεργό' ELSE '❌ Ακυρωμένο' END AS IsActive
                    FROM SalesInvoices si
                    LEFT JOIN DocumentTypes dt ON si.DocType = dt.Id
                    LEFT JOIN DocumentSeries ds ON si.SeriesId = ds.Id
                    LEFT JOIN Customers c ON si.ClientId = c.Id
                    LEFT JOIN MyDataDocuments md ON md.DocumentId = si.Id AND md.InvoiceType = 'SALES_INVOICE'
                    WHERE si.DocDate >= @DateFrom AND si.DocDate <= @DateTo";

                if (cmbDocType.SelectedIndex > 0) sql += " AND dt.Name = @DocTypeName";
                if (cmbSeries.SelectedIndex > 0) sql += " AND ds.Name = @SeriesName";
                if (cmbCustomer.SelectedIndex > 0) sql += " AND c.Epwnymia = @CustomerName";

                string myDataFilter = cmbMyDataStatus.SelectedItem?.ToString();
                if (myDataFilter == "Σταλμένα") sql += " AND md.UID IS NOT NULL";
                else if (myDataFilter == "Μη Σταλμένα") sql += " AND md.UID IS NULL";
                else if (myDataFilter == "Με Σφάλμα") sql += " AND EXISTS (SELECT 1 FROM MyDataErrors me JOIN MyDataDocuments mdd ON me.MyDataDocumentId = mdd.Id WHERE mdd.DocumentId = si.Id AND mdd.InvoiceType = 'SALES_INVOICE')";

                if (!chkShowCancelled.Checked) sql += " AND si.IsActive = 1";

                string search = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(search)) sql += " AND (si.DocNumber LIKE @Search OR c.Epwnymia LIKE @Search)";

                sql += " ORDER BY si.DocDate DESC, si.Id DESC";

                var invoices = conn.Query(sql, new
                {
                    DateFrom = dtpFrom.Value.Date,
                    DateTo = dtpTo.Value.Date.AddDays(1),
                    DocTypeName = cmbDocType.SelectedItem?.ToString(),
                    SeriesName = cmbSeries.SelectedItem?.ToString(),
                    CustomerName = cmbCustomer.SelectedItem?.ToString(),
                    Search = $"%{search}%"
                }).ToList();

                dgvInvoices.DataSource = invoices;
                lblTotalRecords.Text = $"Εγγραφές: {invoices.Count}";
                lblTotalAmount.Text = $"Συνολική Αξία: {invoices.Sum(i => (decimal?)i.GrandTotal) ?? 0:N2} €";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenInvoiceForm(int? invoiceId)
        {
            try { using (var frm = new SalesInvoiceForm(invoiceId)) frm.ShowDialog(); LoadInvoices(); }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void EditSelectedInvoice()
        {
            if (dgvInvoices.CurrentRow == null) { MessageBox.Show("Επίλεξε ένα τιμολόγιο!"); return; }
            OpenInvoiceForm(Convert.ToInt32(dgvInvoices.CurrentRow.Cells["Id"].Value));
        }

        private void DeleteSelectedInvoice()
        {
            if (dgvInvoices.CurrentRow == null) { MessageBox.Show("Επίλεξε ένα τιμολόγιο!"); return; }
            string docNumber = dgvInvoices.CurrentRow.Cells["DocNumber"].Value?.ToString();
            int invoiceId = Convert.ToInt32(dgvInvoices.CurrentRow.Cells["Id"].Value);

            if (MessageBox.Show($"Ακύρωση τιμολογίου '{docNumber}';", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(GetConnectionString());
                    conn.Execute("UPDATE SalesInvoices SET IsActive = 0 WHERE Id = @Id", new { Id = invoiceId });
                    LoadInvoices();
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
            }
        }

        // ✅ ΔΙΟΡΘΩΣΗ: Προστέθηκε 'async' στη δήλωση της μεθόδου
        private async void SendSelectedToMyData()
        {
            if (dgvInvoices.CurrentRow == null) { MessageBox.Show("Επίλεξε ένα τιμολόγιο!"); return; }

            int invoiceId = Convert.ToInt32(dgvInvoices.CurrentRow.Cells["Id"].Value);
            string status = dgvInvoices.CurrentRow.Cells["MyDataStatus"].Value?.ToString();

            if (status == "✅ Σταλμένο") { MessageBox.Show("Έχει ήδη σταλεί στο myDATA!"); return; }
            if (MessageBox.Show("Αποστολή στο myDATA;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                Cursor = Cursors.WaitCursor;

                // ✅ Κάλεσμα Async μεθόδου
                var result = await MyDataService.SendSalesInvoiceAsync(invoiceId);

                Cursor = Cursors.Default;

                if (result.Success)
                {
                    MessageBox.Show($"✅ Επιτυχία!\nUID: {result.Uid}\nMARK: {result.Mark}", "myDATA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"❌ Αποτυχία:\n{result.ErrorMessage}", "myDATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                LoadInvoices(); // Ανανέωση λίστας για να φαίνεται η νέα κατάσταση
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Σφάλμα:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}