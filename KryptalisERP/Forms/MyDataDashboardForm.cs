using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class MyDataDashboardForm : Form
    {
        private DataGridView dgvDocs;
        private ComboBox cmbFilterStatus, cmbFilterType;
        private DateTimePicker dtpFrom, dtpTo;
        private TextBox txtSearch;
        private Button btnRefresh, btnRetrySelected, btnViewErrors, btnClose;
        private Label lblStatsTotal, lblStatsSuccess, lblStatsFailed, lblStatsPending;
        private Panel pnlStats;

        public MyDataDashboardForm()
        {
            InitializeUI();
            this.Load += (s, e) => LoadData();
        }

        private void InitializeUI()
        {
            this.Text = "🏛️ myDATA Dashboard - Διαβιβάσεις ΑΑΔΕ";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1000, 600);

            // --- HEADER STATS ---
            pnlStats = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.White, Padding = new Padding(15) };

            lblStatsTotal = CreateStatCard(pnlStats, "Σύνολο Εγγραφών", 15, Color.FromArgb(52, 152, 219), "0");
            lblStatsSuccess = CreateStatCard(pnlStats, "✅ Επιτυχημένες", 220, Color.FromArgb(46, 204, 113), "0");
            lblStatsFailed = CreateStatCard(pnlStats, "❌ Αποτυχημένες", 425, Color.FromArgb(231, 76, 60), "0");
            lblStatsPending = CreateStatCard(pnlStats, "⏳ Σε Εκκρεμότητα", 630, Color.FromArgb(241, 196, 15), "0");

            this.Controls.Add(pnlStats);

            // --- FILTERS ---
            var pnlFilters = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.White, Padding = new Padding(15, 8, 15, 8) };
            int lx = 15;

            pnlFilters.Controls.Add(new Label { Text = "Κατάσταση:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 70;
            cmbFilterStatus = new ComboBox { Location = new Point(lx, 8), Size = new Size(150, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbFilterStatus.Items.AddRange(new object[] { "Όλες", "SUCCESS", "FAILED", "PENDING", "CANCELLED", "ERROR" });
            cmbFilterStatus.SelectedIndex = 0;
            cmbFilterStatus.SelectedIndexChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(cmbFilterStatus); lx += 160;

            pnlFilters.Controls.Add(new Label { Text = "Τύπος:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 50;
            cmbFilterType = new ComboBox { Location = new Point(lx, 8), Size = new Size(180, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbFilterType.Items.AddRange(new object[] { "Όλοι", "SALES_INVOICE", "PURCHASE_INVOICE" });
            cmbFilterType.SelectedIndex = 0;
            cmbFilterType.SelectedIndexChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(cmbFilterType); lx += 190;

            pnlFilters.Controls.Add(new Label { Text = "Από:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 35;
            dtpFrom = new DateTimePicker { Location = new Point(lx, 8), Size = new Size(110, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            dtpFrom.Value = DateTime.Now.AddDays(-30);
            dtpFrom.ValueChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(dtpFrom); lx += 120;

            pnlFilters.Controls.Add(new Label { Text = "Έως:", Location = new Point(lx, 12), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) }); lx += 35;
            dtpTo = new DateTimePicker { Location = new Point(lx, 8), Size = new Size(110, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            dtpTo.ValueChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(dtpTo); lx += 120;

            txtSearch = new TextBox { Location = new Point(lx, 8), Size = new Size(200, 26), Font = new Font("Segoe UI", 9.5F) };
            txtSearch.PlaceholderText = "UID, Mark ή Message...";
            txtSearch.TextChanged += (s, e) => LoadData();
            pnlFilters.Controls.Add(txtSearch);

            this.Controls.Add(pnlFilters);

            // --- GRID ---
            dgvDocs = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 9.5F),
                RowTemplate = { Height = 35 },
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            dgvDocs.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "CreatedAt", HeaderText = "Δημιουργία", Width = 120 });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "InvoiceType", HeaderText = "Τύπος", Width = 120 });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocumentId", HeaderText = "Doc Id", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "UID", HeaderText = "UID (ΑΑΔΕ)", Width = 180 });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "MARK", HeaderText = "MARK (ΑΑΔΕ)", Width = 180 });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "TransmissionStatus", HeaderText = "Κατάσταση", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) } });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "ErrorMessage", HeaderText = "Μήνυμα / Λάθος", Width = 300 });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "RetryCount", HeaderText = "Re-trys", Width = 60, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            dgvDocs.CellFormatting += DgvDocs_CellFormatting;
            dgvDocs.CellDoubleClick += (s, e) => ShowDetails(e.RowIndex);

            this.Controls.Add(dgvDocs);

            // --- FOOTER BUTTONS ---
            var pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(15, 10, 15, 10) };

            btnRefresh = CreateButton("🔄 Ανανέωση", 15, 10, 120, 40, Color.FromArgb(108, 117, 125));
            btnRefresh.Click += (s, e) => LoadData();
            pnlFooter.Controls.Add(btnRefresh);

            btnRetrySelected = CreateButton("♻️ Επαναποστολή Επιλεγμένου", 145, 10, 200, 40, Color.FromArgb(230, 126, 34));
            btnRetrySelected.Click += (s, e) => RetrySelected();
            pnlFooter.Controls.Add(btnRetrySelected);

            btnViewErrors = CreateButton("🐞 Προβολή Σφαλμάτων", 355, 10, 150, 40, Color.FromArgb(231, 76, 60));
            btnViewErrors.Click += (s, e) => ViewErrorsPopup();
            pnlFooter.Controls.Add(btnViewErrors);

            btnClose = CreateButton("❌ Κλείσιμο", 1250, 10, 110, 40, Color.FromArgb(149, 165, 166));
            btnClose.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnClose);

            this.Controls.Add(pnlFooter);
        }

        private Label CreateStatCard(Panel parent, string title, int x, Color color, string value)
        {
            var card = new Panel { Location = new Point(x, 10), Size = new Size(180, 80), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            card.Controls.Add(new Label { Text = title, Location = new Point(10, 10), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.Gray });
            var valLbl = new Label { Text = value, Location = new Point(10, 35), AutoSize = true, Font = new Font("Segoe UI", 18F, FontStyle.Bold), ForeColor = color };
            card.Controls.Add(valLbl);
            parent.Controls.Add(card);
            return valLbl;
        }

        private Button CreateButton(string text, int x, int y, int w, int h, Color c)
            => new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), FlatAppearance = { BorderSize = 0 } };

        private void DgvDocs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvDocs.Columns["TransmissionStatus"].Index) return;

            string status = e.Value?.ToString() ?? "";
            switch (status.ToUpper())
            {
                case "SUCCESS":
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.BackColor = Color.FromArgb(209, 231, 221);
                    break;
                case "FAILED":
                case "ERROR":
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.BackColor = Color.FromArgb(248, 215, 218);
                    break;
                case "PENDING":
                    e.CellStyle.ForeColor = Color.DarkOrange;
                    e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    break;
                case "CANCELLED":
                    e.CellStyle.ForeColor = Color.Gray;
                    e.CellStyle.BackColor = Color.FromArgb(233, 236, 239);
                    break;
            }
        }

        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                string sql = @"SELECT * FROM MyDataDocuments WHERE 1=1";
                if (cmbFilterStatus.SelectedIndex > 0) sql += " AND TransmissionStatus = @St";
                if (cmbFilterType.SelectedIndex > 0) sql += " AND InvoiceType = @Ty";
                sql += " AND CreatedAt >= @From AND CreatedAt <= @To";

                string search = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(search))
                    sql += " AND (UID LIKE @S OR MARK LIKE @S OR ErrorMessage LIKE @S)";

                sql += " ORDER BY CreatedAt DESC";

                var paramsObj = new
                {
                    St = cmbFilterStatus.SelectedItem.ToString(),
                    Ty = cmbFilterType.SelectedItem.ToString(),
                    From = dtpFrom.Value.Date,
                    To = dtpTo.Value.Date.AddDays(1),
                    S = $"%{search}%"
                };

                var data = conn.Query(sql, paramsObj).ToList();
                dgvDocs.DataSource = data;

                // Υπολογισμός Στατιστικών
                lblStatsTotal.Text = data.Count.ToString();
                lblStatsSuccess.Text = data.Count(x => x.TransmissionStatus == "SUCCESS").ToString();
                lblStatsFailed.Text = data.Count(x => x.TransmissionStatus == "FAILED" || x.TransmissionStatus == "ERROR").ToString();
                lblStatsPending.Text = data.Count(x => x.TransmissionStatus == "PENDING").ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης:\n" + ex.Message);
            }
        }

        private void ShowDetails(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvDocs.Rows.Count) return;
            var row = dgvDocs.Rows[rowIndex];
            string msg = row.Cells["ErrorMessage"].Value?.ToString() ?? "";
            string resp = row.Cells["ResponseXml"].Value?.ToString() ?? "";

            MessageBox.Show($"UID: {row.Cells["UID"].Value}\nMark: {row.Cells["MARK"].Value}\n\nΛάθος/Mήνυμα:\n{msg}\n\nResponse XML:\n{resp.Substring(0, Math.Min(resp.Length, 500))}...", "Λεπτομέρειες");
        }

        private async void RetrySelected()
        {
            if (dgvDocs.CurrentRow == null) { MessageBox.Show("Επίλεξε εγγραφή!"); return; }

            int docId = Convert.ToInt32(dgvDocs.CurrentRow.Cells["DocumentId"].Value);
            string type = dgvDocs.CurrentRow.Cells["InvoiceType"].Value?.ToString();

            Cursor = Cursors.WaitCursor;
            Services.MyDataResult res = null;

            if (type == "SALES_INVOICE")
                res = await Services.MyDataService.SendSalesInvoiceAsync(docId);
            else if (type == "PURCHASE_INVOICE")
                res = await Services.MyDataService.SendPurchaseInvoiceAsync(docId);
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Άγνωστος τύπος εγγράφου.");
                return;
            }

            Cursor = Cursors.Default;

            if (res.Success) MessageBox.Show("✅ Η επαναποστολή πέτυχε!");
            else MessageBox.Show($"❌ Απέτυχε: {res.ErrorMessage}");

            LoadData();
        }

        private void ViewErrorsPopup()
        {
            if (dgvDocs.CurrentRow == null) { MessageBox.Show("Επίλεξε πρώτα ένα έγγραφο στη λίστα!"); return; }
            int docId = Convert.ToInt32(dgvDocs.CurrentRow.Cells["Id"].Value);

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var errors = conn.Query(@"
                    SELECT ErrorCode, ErrorMessage, ErrorCategory, Severity, OccurredAt 
                    FROM MyDataErrors 
                    WHERE MyDataDocumentId = @DocId 
                    ORDER BY OccurredAt DESC", new { DocId = docId }).ToList();

                if (errors.Count == 0)
                {
                    MessageBox.Show("Δεν βρέθηκαν καταγεγραμμένα σφάλματα για αυτό το έγγραφο.", "Πληροφορία");
                    return;
                }

                var popup = new Form { Text = "🐞 Λίστα Σφαλμάτων", Size = new Size(800, 400), StartPosition = FormStartPosition.CenterParent };
                var dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = false };

                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "ErrorCode", HeaderText = "Code", Width = 100 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "ErrorCategory", HeaderText = "Category", Width = 100 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Severity", HeaderText = "Sev", Width = 60 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "ErrorMessage", HeaderText = "Message", Width = 400 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "OccurredAt", HeaderText = "Time", Width = 120 });

                dgv.DataSource = errors;
                popup.Controls.Add(dgv);
                popup.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}