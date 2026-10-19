using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class DeliveryNoteListForm : Form
    {
        private DataGridView dgvNotes;
        private TextBox txtSearch;
        private ComboBox cmbStatus, cmbCustomer;
        private DateTimePicker dtpFrom, dtpTo;
        private Button btnNew, btnEdit, btnDelete, btnRefresh, btnClose;
        private Label lblTotalRecords;

        public DeliveryNoteListForm()
        {
            InitializeUI();
            this.Load += DeliveryNoteListForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "📦 Λίστα Δελτίων Αποστολής";
            this.Size = new Size(1400, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1000, 600);

            // --- TOOLBAR ---
            var pnlToolbar = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.White, Padding = new Padding(10) };

            btnNew = CreateButton("➕ Νέο Δελτίο", 10, 8, 140, 34, Color.FromArgb(46, 204, 113));
            btnNew.Click += (s, e) => OpenForm(null);
            pnlToolbar.Controls.Add(btnNew);

            btnEdit = CreateButton("✏️ Επεξεργασία", 160, 8, 130, 34, Color.FromArgb(52, 152, 219));
            btnEdit.Click += (s, e) => EditSelected();
            pnlToolbar.Controls.Add(btnEdit);

            btnDelete = CreateButton("🗑️ Ακύρωση", 300, 8, 120, 34, Color.FromArgb(231, 76, 60));
            btnDelete.Click += (s, e) => CancelSelected();
            pnlToolbar.Controls.Add(btnDelete);

            btnRefresh = CreateButton("🔄 Ανανέωση", 430, 8, 110, 34, Color.FromArgb(108, 117, 125));
            btnRefresh.Click += (s, e) => LoadData();
            pnlToolbar.Controls.Add(btnRefresh);

            btnClose = CreateButton("❌ Κλείσιμο", 550, 8, 100, 34, Color.FromArgb(149, 165, 166));
            btnClose.Click += (s, e) => this.Close();
            pnlToolbar.Controls.Add(btnClose);

            this.Controls.Add(pnlToolbar);

            // --- FILTERS ---
            var pnlFilters = new Panel { Dock = DockStyle.Top, Height = 40, BackColor = Color.White, Padding = new Padding(10) };
            int lx = 10;

            pnlFilters.Controls.Add(new Label { Text = "Κατάσταση:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 80;
            cmbStatus = new ComboBox { Location = new Point(lx, 8), Size = new Size(150, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbStatus.Items.AddRange(new object[] { "Όλες", "DRAFT", "SENT_TO_MYDATA", "ACCEPTED", "REJECTED", "CANCELLED" });
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(cmbStatus); lx += 160;

            pnlFilters.Controls.Add(new Label { Text = "Πελάτης:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 60;
            cmbCustomer = new ComboBox { Location = new Point(lx, 8), Size = new Size(200, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbCustomer.SelectedIndexChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(cmbCustomer); lx += 210;

            pnlFilters.Controls.Add(new Label { Text = "Από:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 35;
            dtpFrom = new DateTimePicker { Location = new Point(lx, 8), Size = new Size(110, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            dtpFrom.Value = DateTime.Now.AddMonths(-1);
            dtpFrom.ValueChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(dtpFrom); lx += 120;

            pnlFilters.Controls.Add(new Label { Text = "Έως:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 35;
            dtpTo = new DateTimePicker { Location = new Point(lx, 8), Size = new Size(110, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            dtpTo.ValueChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(dtpTo); lx += 120;

            txtSearch = new TextBox { Location = new Point(lx, 8), Size = new Size(200, 26), Font = new Font("Segoe UI", 9.5F) };
            txtSearch.PlaceholderText = "Αριθμός ή Πελάτης...";
            txtSearch.TextChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(txtSearch);

            this.Controls.Add(pnlFilters);

            // --- GRID ---
            dgvNotes = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 10F),
                RowTemplate = { Height = 32 },
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false
            };

            dgvNotes.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dgvNotes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });
            dgvNotes.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocNumber", HeaderText = "Αριθμός", Width = 120, DataPropertyName = "DocNumber" });
            dgvNotes.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocDate", HeaderText = "Ημερομηνία", Width = 100, DataPropertyName = "DocDate" });
            dgvNotes.Columns.Add(new DataGridViewTextBoxColumn { Name = "CustomerName", HeaderText = "Πελάτης", Width = 250, DataPropertyName = "CustomerName" });
            dgvNotes.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalQty", HeaderText = "Ποσότητα", Width = 80, DataPropertyName = "TotalQty", DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvNotes.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalWeight", HeaderText = "Βάρος kg", Width = 80, DataPropertyName = "TotalWeight", DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvNotes.Columns.Add(new DataGridViewTextBoxColumn { Name = "TransportCo", HeaderText = "Μεταφορική", Width = 150, DataPropertyName = "TransportCo" });
            dgvNotes.Columns.Add(new DataGridViewTextBoxColumn { Name = "StatusDisplay", HeaderText = "Κατάσταση", Width = 120, DataPropertyName = "StatusDisplay", DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) } });

            dgvNotes.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditSelected(); };
            dgvNotes.CellFormatting += DgvNotes_CellFormatting;

            this.Controls.Add(dgvNotes);

            // --- STATUS BAR ---
            var pnlStatus = new Panel { Dock = DockStyle.Bottom, Height = 30, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(10, 5, 10, 5) };
            lblTotalRecords = new Label { Text = "Εγγραφές: 0", Location = new Point(10, 5), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.Gray };
            pnlStatus.Controls.Add(lblTotalRecords);
            this.Controls.Add(pnlStatus);
        }

        private Button CreateButton(string text, int x, int y, int w, int h, Color color)
            => new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };

        private void DeliveryNoteListForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadData();
        }

        private void LoadCustomers()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var c = conn.Query("SELECT Id, Epwnymia FROM Customers WHERE Energo = 1 ORDER BY Epwnymia").ToList();
                cmbCustomer.Items.Clear();
                cmbCustomer.Items.Add("Όλοι");
                foreach (var item in c) cmbCustomer.Items.Add(item.Epwnymia);
                cmbCustomer.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                string sql = @"
                    SELECT 
                        dn.Id, 
                        dn.DocNumber, 
                        dn.DocDate, 
                        c.Epwnymia AS CustomerName, 
                        dn.TotalQty, 
                        dn.TotalWeight, 
                        dn.TransportCompany AS TransportCo,
                        CASE dn.Status 
                            WHEN 'DRAFT' THEN '📝 Πρόχειρο'
                            WHEN 'SENT_TO_MYDATA' THEN '🏛️ Στάλθηκε'
                            WHEN 'ACCEPTED' THEN '✅ Εγκρίθηκε'
                            WHEN 'REJECTED' THEN '❌ Απορρίφθηκε'
                            WHEN 'CANCELLED' THEN '❌ Ακυρωμένο'
                            ELSE dn.Status 
                        END AS StatusDisplay,
                        dn.Status as RawStatus
                    FROM DeliveryNotes dn 
                    LEFT JOIN Customers c ON dn.CustomerId = c.Id 
                    WHERE 1=1";

                if (cmbStatus.SelectedIndex > 0)
                    sql += " AND dn.Status = @St";

                if (cmbCustomer.SelectedIndex > 0)
                    sql += " AND c.Epwnymia = @Cust";

                sql += " AND dn.DocDate BETWEEN @From AND @To";

                string search = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(search))
                    sql += " AND (dn.DocNumber LIKE @S OR c.Epwnymia LIKE @S)";

                sql += " ORDER BY dn.DocDate DESC, dn.Id DESC";

                var data = conn.Query(sql, new
                {
                    St = cmbStatus.SelectedIndex > 0 ? cmbStatus.SelectedItem.ToString() : null,
                    Cust = cmbCustomer.SelectedIndex > 0 ? cmbCustomer.SelectedItem.ToString() : null,
                    From = dtpFrom.Value.Date,
                    To = dtpTo.Value.Date.AddDays(1),
                    S = $"%{search}%"
                }).ToList();

                dgvNotes.DataSource = data;
                lblTotalRecords.Text = $"Εγγραφές: {data.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvNotes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvNotes.Columns["StatusDisplay"].Index) return;

            string status = e.Value?.ToString();
            if (status.Contains("Πρόχειρο")) e.CellStyle.ForeColor = Color.Gray;
            else if (status.Contains("Στάλθηκε")) e.CellStyle.ForeColor = Color.FromArgb(13, 110, 253);
            else if (status.Contains("Εγκρίθηκε")) e.CellStyle.ForeColor = Color.FromArgb(25, 135, 84);
            else if (status.Contains("Απορρίφθηκε") || status.Contains("Ακυρωμένο")) e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
        }

        private void OpenForm(int? id)
        {
            try
            {
                using (var f = new DeliveryNoteForm(id))
                    f.ShowDialog();
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void EditSelected()
        {
            if (dgvNotes.CurrentRow == null) { MessageBox.Show("Επίλεξε δελτίο!"); return; }
            OpenForm(Convert.ToInt32(dgvNotes.CurrentRow.Cells["Id"].Value));
        }

        private void CancelSelected()
        {
            if (dgvNotes.CurrentRow == null) { MessageBox.Show("Επίλεξε δελτίο!"); return; }

            string rawStatus = dgvNotes.CurrentRow.Cells["RawStatus"].Value?.ToString();
            if (rawStatus == "CANCELLED" || rawStatus == "ACCEPTED")
            {
                MessageBox.Show("Δεν μπορείτε να ακυρώσετε ήδη εγκριμένα ή ακυρωμένα δελτία.", "Πληροφορία");
                return;
            }

            int id = Convert.ToInt32(dgvNotes.CurrentRow.Cells["Id"].Value);
            if (MessageBox.Show($"Ακύρωση Δελτίου '{dgvNotes.CurrentRow.Cells["DocNumber"].Value}';\n\nΘα αποκατασταθεί το στοκ;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(GetConnectionString());
                    conn.Open();
                    using var tran = conn.BeginTransaction();

                    // 1. Restore Stock
                    var lines = conn.Query("SELECT ItemId, QuantityShipped FROM DeliveryNoteLines WHERE DeliveryNoteId = @Id", new { Id = id }, tran).ToList();
                    foreach (var l in lines)
                    {
                        conn.Execute(@"UPDATE StockVariants SET AvailableQty = AvailableQty + @Qty, Quantity = Quantity + @Qty 
                                       WHERE ItemVariantId IN (SELECT Id FROM ItemVariants WHERE ItemId = @ItemId)",
                                       new { Qty = l.QuantityShipped, ItemId = l.ItemId }, tran);

                        conn.Execute("UPDATE Items SET Stock = Stock + @Qty WHERE Id = @ItemId", new { Qty = l.QuantityShipped, ItemId = l.ItemId }, tran);
                    }

                    // 2. Update Status
                    conn.Execute("UPDATE DeliveryNotes SET Status = 'CANCELLED', UpdatedAt = GETDATE() WHERE Id = @Id", new { Id = id }, tran);

                    tran.Commit();
                    LoadData();
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα ακύρωσης:\n" + ex.Message); }
            }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}