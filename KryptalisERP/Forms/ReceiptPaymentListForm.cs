using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class ReceiptPaymentListForm : Form
    {
        private DataGridView dgvMovements;
        private ComboBox cmbType, cmbCounterparty, cmbPaymentMethod;
        private DateTimePicker dtpFrom, dtpTo;
        private TextBox txtSearch, txtMinAmount, txtMaxAmount;
        private CheckBox chkGroupByMonth;
        private Button btnNew, btnEdit, btnDelete, btnPrint, btnRefresh, btnClose;
        private Label lblTotalRecords, lblTotalAmount, lblReceipts, lblPayments, lblBalance;

        public ReceiptPaymentListForm()
        {
            InitializeUI();
            this.Load += ReceiptPaymentListForm_Load;
        }

        private void InitializeUI()
        {
            this.Text = "💰 Λίστα Εισπράξεων / Πληρωμών";
            this.Size = new Size(1600, 850);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(1300, 700);

            // ============================================
            // 1. TOOLBAR - ΚΟΥΜΠΙΑ
            // ============================================
            var pnlToolbar = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = Color.White, Padding = new Padding(15, 8, 15, 8), Margin = new Padding(0, 0, 0, 3) };

            btnNew = CreateButton("➕ Νέα Κίνηση", 10, 8, 140, 38, Color.FromArgb(46, 204, 113));
            btnNew.Click += (s, e) => OpenMovementForm();
            pnlToolbar.Controls.Add(btnNew);

            btnEdit = CreateButton("✏️ Επεξεργασία", 160, 8, 130, 38, Color.FromArgb(52, 152, 219));
            btnEdit.Click += (s, e) => EditSelected();
            pnlToolbar.Controls.Add(btnEdit);

            btnDelete = CreateButton("🗑️ Διαγραφή", 300, 8, 120, 38, Color.FromArgb(231, 76, 60));
            btnDelete.Click += (s, e) => DeleteSelected();
            pnlToolbar.Controls.Add(btnDelete);

            btnPrint = CreateButton("🖨️ Εκτύπωση", 430, 8, 120, 38, Color.FromArgb(155, 89, 182));
            btnPrint.Click += (s, e) => MessageBox.Show("Εκτύπωση - Υπό υλοποίηση", "Πληροφορία");
            pnlToolbar.Controls.Add(btnPrint);

            btnRefresh = CreateButton("🔄 Ανανέωση", 560, 8, 120, 38, Color.FromArgb(108, 117, 125));
            btnRefresh.Click += (s, e) => LoadMovements();
            pnlToolbar.Controls.Add(btnRefresh);

            btnClose = CreateButton("❌ Κλείσιμο", 690, 8, 110, 38, Color.FromArgb(149, 165, 166));
            btnClose.Click += (s, e) => this.Close();
            pnlToolbar.Controls.Add(btnClose);

            chkGroupByMonth = new CheckBox { Text = "📅 Ομαδοποίηση ανά Μήνα", Location = new Point(820, 15), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            chkGroupByMonth.CheckedChanged += (s, e) => LoadMovements();
            pnlToolbar.Controls.Add(chkGroupByMonth);

            this.Controls.Add(pnlToolbar);

            // ============================================
            // 2. ΦΙΛΤΡΑ
            // ============================================
            var pnlFilters = new Panel { Dock = DockStyle.Top, Height = 85, BackColor = Color.White, Padding = new Padding(15, 8, 15, 8), Margin = new Padding(0, 0, 0, 3) };
            pnlFilters.Controls.Add(new Label { Text = "🔍 ΦΙΛΤΡΑ", Location = new Point(10, 3), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219) });

            // Γραμμή 1: Τύπος + Ημερομηνίες + Ποσά
            int y1 = 25, lx = 10;

            AddFilterLabel(pnlFilters, "Τύπος:", lx, y1);
            cmbType = new ComboBox { Location = new Point(lx + 50, y1), Size = new Size(130, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbType.Items.AddRange(new object[] { "Όλες", "Μόνο Εισπράξεις", "Μόνο Πληρωμές" });
            cmbType.SelectedIndex = 0;
            cmbType.SelectedIndexChanged += (s, e) => { LoadCounterparties(); LoadMovements(); };
            pnlFilters.Controls.Add(cmbType);
            lx += 50 + 130 + 12;

            AddFilterLabel(pnlFilters, "Από:", lx, y1);
            dtpFrom = new DateTimePicker { Location = new Point(lx + 35, y1), Size = new Size(120, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            dtpFrom.Value = DateTime.Now.AddMonths(-6);
            dtpFrom.ValueChanged += (s, e) => LoadMovements();
            pnlFilters.Controls.Add(dtpFrom);
            lx += 35 + 120 + 12;

            AddFilterLabel(pnlFilters, "Έως:", lx, y1);
            dtpTo = new DateTimePicker { Location = new Point(lx + 35, y1), Size = new Size(120, 26), Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9.5F) };
            dtpTo.ValueChanged += (s, e) => LoadMovements();
            pnlFilters.Controls.Add(dtpTo);
            lx += 35 + 120 + 12;

            AddFilterLabel(pnlFilters, "Ποσό Από:", lx, y1);
            txtMinAmount = new TextBox { Location = new Point(lx + 65, y1), Size = new Size(90, 26), Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle, TextAlign = HorizontalAlignment.Right };
            txtMinAmount.TextChanged += (s, e) => LoadMovements();
            pnlFilters.Controls.Add(txtMinAmount);
            lx += 65 + 90 + 12;

            AddFilterLabel(pnlFilters, "Έως:", lx, y1);
            txtMaxAmount = new TextBox { Location = new Point(lx + 35, y1), Size = new Size(90, 26), Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle, TextAlign = HorizontalAlignment.Right };
            txtMaxAmount.TextChanged += (s, e) => LoadMovements();
            pnlFilters.Controls.Add(txtMaxAmount);

            // Γραμμή 2: Πελάτης/Προμηθευτής + Τρόπος + Αναζήτηση
            int y2 = 55; lx = 10;

            AddFilterLabel(pnlFilters, "Πελάτης/Προμηθ.:", lx, y2);
            cmbCounterparty = new ComboBox { Location = new Point(lx + 115, y2), Size = new Size(230, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbCounterparty.SelectedIndexChanged += (s, e) => LoadMovements();
            pnlFilters.Controls.Add(cmbCounterparty);
            lx += 115 + 230 + 12;

            AddFilterLabel(pnlFilters, "Τρόπος:", lx, y2);
            cmbPaymentMethod = new ComboBox { Location = new Point(lx + 60, y2), Size = new Size(150, 26), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbPaymentMethod.SelectedIndexChanged += (s, e) => LoadMovements();
            pnlFilters.Controls.Add(cmbPaymentMethod);
            lx += 60 + 150 + 12;

            AddFilterLabel(pnlFilters, "🔍:", lx, y2);
            txtSearch = new TextBox { Location = new Point(lx + 25, y2), Size = new Size(250, 26), Font = new Font("Segoe UI", 9.5F), BorderStyle = BorderStyle.FixedSingle };
            txtSearch.TextChanged += (s, e) => LoadMovements();
            pnlFilters.Controls.Add(txtSearch);

            this.Controls.Add(pnlFilters);

            // ============================================
            // 3. GRID
            // ============================================
            dgvMovements = new DataGridView
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
                RowTemplate = { Height = 30 },
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false
            };

            dgvMovements.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(5)
            };

            dgvMovements.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });
            dgvMovements.Columns.Add(new DataGridViewTextBoxColumn { Name = "MovementType", Visible = false }); // "Receipt" ή "Payment"
            dgvMovements.Columns.Add(new DataGridViewTextBoxColumn { Name = "Type", HeaderText = "Τύπος", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvMovements.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocNumber", HeaderText = "Αριθμός", Width = 140 });
            dgvMovements.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocDate", HeaderText = "Ημερομηνία", Width = 110 });
            dgvMovements.Columns.Add(new DataGridViewTextBoxColumn { Name = "Counterparty", HeaderText = "Πελάτης / Προμηθευτής", Width = 280 });
            dgvMovements.Columns.Add(new DataGridViewTextBoxColumn { Name = "Amount", HeaderText = "Ποσό €", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 10F, FontStyle.Bold) } });
            dgvMovements.Columns.Add(new DataGridViewTextBoxColumn { Name = "PaymentMethod", HeaderText = "Τρόπος Πληρωμής", Width = 150 });
            dgvMovements.Columns.Add(new DataGridViewTextBoxColumn { Name = "InvoicesCount", HeaderText = "Τιμολόγια", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvMovements.Columns.Add(new DataGridViewTextBoxColumn { Name = "Notes", HeaderText = "Σημειώσεις", Width = 250 });
            dgvMovements.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Κατάσταση", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            dgvMovements.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditSelected(); };
            dgvMovements.CellFormatting += DgvMovements_CellFormatting;

            // ============================================
            // 4. STATUS BAR (Κάτω μέρος με 5 κάρτες)
            // ============================================
            var pnlStatus = new Panel { Dock = DockStyle.Bottom, Height = 70, BackColor = Color.FromArgb(248, 249, 250), Padding = new Padding(15, 8, 15, 8) };

            lblTotalRecords = CreateStatLabel(pnlStatus, "📊 Κινήσεις:", "0", 15, Color.FromArgb(52, 152, 219));
            lblReceipts = CreateStatLabel(pnlStatus, "💰 Εισπράξεις:", "0,00 €", 250, Color.FromArgb(46, 204, 113));
            lblPayments = CreateStatLabel(pnlStatus, "💸 Πληρωμές:", "0,00 €", 550, Color.FromArgb(231, 76, 60));
            lblBalance = CreateStatLabel(pnlStatus, "⚖️ Ισοζύγιο:", "0,00 €", 850, Color.FromArgb(241, 196, 15));
            lblTotalAmount = CreateStatLabel(pnlStatus, "📈 Συνολική Ροή:", "0,00 €", 1150, Color.FromArgb(155, 89, 182));

            // ✅ ΣΩΣΤΗ ΣΕΙΡΑ DOCKING
            this.Controls.Add(pnlFilters);
            this.Controls.Add(pnlToolbar);
            this.Controls.Add(pnlStatus);
            this.Controls.Add(dgvMovements);
            dgvMovements.BringToFront();
        }

        private void AddFilterLabel(Panel p, string text, int x, int y)
        {
            p.Controls.Add(new Label { Text = text, Location = new Point(x, y + 4), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) });
        }

        private Label CreateStatLabel(Panel parent, string title, string value, int x, Color color)
        {
            var card = new Panel { Location = new Point(x, 8), Size = new Size(220, 52), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            card.Controls.Add(new Label { Text = title, Location = new Point(8, 4), AutoSize = true, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), ForeColor = Color.FromArgb(108, 117, 125) });
            var val = new Label { Text = value, Location = new Point(8, 24), AutoSize = true, Font = new Font("Segoe UI", 13F, FontStyle.Bold), ForeColor = color };
            card.Controls.Add(val);
            parent.Controls.Add(card);
            return val;
        }

        private void DgvMovements_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvMovements.Rows[e.RowIndex];
            string movementType = row.Cells["MovementType"].Value?.ToString();

            // Χρωματισμός στήλης τύπου
            if (e.ColumnIndex == dgvMovements.Columns["Type"].Index)
            {
                if (movementType == "Receipt")
                {
                    e.Value = "💰 Είσπραξη";
                    e.CellStyle.ForeColor = Color.FromArgb(46, 204, 113);
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else
                {
                    e.Value = "💸 Πληρωμή";
                    e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
            }

            // Χρωματισμός ποσού
            if (e.ColumnIndex == dgvMovements.Columns["Amount"].Index)
            {
                e.CellStyle.ForeColor = movementType == "Receipt" ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
            }
        }

        private Button CreateButton(string text, int x, int y, int w, int h, Color color)
        {
            return new Button { Text = text, Location = new Point(x, y), Size = new Size(w, h), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
        }

        private void ReceiptPaymentListForm_Load(object sender, EventArgs e)
        {
            LoadPaymentMethods();
            LoadCounterparties();
            LoadMovements();
        }

        private void LoadPaymentMethods()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var m = conn.Query("SELECT Id, Name FROM PaymentMethods WHERE IsActive = 1 ORDER BY Name").ToList();
                cmbPaymentMethod.Items.Clear();
                cmbPaymentMethod.Items.Add("Όλοι οι Τρόποι");
                foreach (var x in m) cmbPaymentMethod.Items.Add(new FilterItem { Id = x.Id, Name = x.Name });
                cmbPaymentMethod.DisplayMember = "Name";
                cmbPaymentMethod.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadCounterparties()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                cmbCounterparty.Items.Clear();
                cmbCounterparty.Items.Add("Όλοι");

                if (cmbType.SelectedIndex == 0 || cmbType.SelectedIndex == 1) // Εισπράξεις ή Όλα
                {
                    var c = conn.Query("SELECT Id, Epwnymia FROM Customers WHERE Energo = 1 ORDER BY Epwnymia").ToList();
                    foreach (var x in c) cmbCounterparty.Items.Add(new FilterItem { Id = x.Id, Name = x.Epwnymia, IsCustomer = true });
                }

                if (cmbType.SelectedIndex == 0 || cmbType.SelectedIndex == 2) // Πληρωμές ή Όλα
                {
                    var s = conn.Query("SELECT Id, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName").ToList();
                    foreach (var x in s) cmbCounterparty.Items.Add(new FilterItem { Id = x.Id, Name = x.SupplierName, IsCustomer = false });
                }

                cmbCounterparty.DisplayMember = "Name";
                cmbCounterparty.SelectedIndex = 0;
            }
            catch { }
        }

        private class FilterItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsCustomer { get; set; }
            public override string ToString() => Name;
        }

        private void LoadMovements()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var all = new System.Collections.Generic.List<dynamic>();

                decimal minAmt = decimal.TryParse(txtMinAmount.Text, out var mn) ? mn : 0;
                decimal maxAmt = decimal.TryParse(txtMaxAmount.Text, out var mx) && mx > 0 ? mx : decimal.MaxValue;
                string search = txtSearch.Text.Trim();

                // ============================================
                // ΕΙΣΠΡΑΞΕΙΣ
                // ============================================
                if (cmbType.SelectedIndex == 0 || cmbType.SelectedIndex == 1)
                {
                    string sqlR = @"
                        SELECT 
                            'Receipt' AS MovementType,
                            r.Id, r.ReceiptNumber AS DocNumber, r.ReceiptDate AS DocDate,
                            c.Epwnymia AS Counterparty,
                            r.Amount,
                            pm.Name AS PaymentMethod,
                            (SELECT COUNT(*) FROM ReceiptAllocations ra WHERE ra.ReceiptId = r.Id) AS InvoicesCount,
                            r.Notes,
                            r.Status
                        FROM Receipts r
                        LEFT JOIN Customers c ON r.CustomerId = c.Id
                        LEFT JOIN PaymentMethods pm ON r.PaymentMethodId = pm.Id
                        WHERE r.ReceiptDate >= @DateFrom AND r.ReceiptDate <= @DateTo
                        AND r.Amount >= @MinAmt AND r.Amount <= @MaxAmt";

                    var cpFilter = cmbCounterparty.SelectedItem as FilterItem;
                    if (cpFilter != null && cpFilter.IsCustomer)
                        sqlR += " AND r.CustomerId = @CpId";

                    if (cmbPaymentMethod.SelectedIndex > 0)
                    {
                        var pmFilter = cmbPaymentMethod.SelectedItem as FilterItem;
                        if (pmFilter != null) sqlR += " AND r.PaymentMethodId = @PmId";
                    }

                    if (!string.IsNullOrEmpty(search))
                        sqlR += " AND (r.ReceiptNumber LIKE @Search OR c.Epwnymia LIKE @Search OR r.Notes LIKE @Search)";

                    sqlR += " ORDER BY r.ReceiptDate DESC";

                    var receipts = conn.Query(sqlR, new
                    {
                        DateFrom = dtpFrom.Value.Date,
                        DateTo = dtpTo.Value.Date.AddDays(1),
                        MinAmt = minAmt,
                        MaxAmt = maxAmt,
                        CpId = cpFilter?.Id,
                        PmId = (cmbPaymentMethod.SelectedItem as FilterItem)?.Id,
                        Search = $"%{search}%"
                    }).ToList();

                    all.AddRange(receipts);
                }

                // ============================================
                // ΠΛΗΡΩΜΕΣ
                // ============================================
                if (cmbType.SelectedIndex == 0 || cmbType.SelectedIndex == 2)
                {
                    string sqlP = @"
                        SELECT 
                            'Payment' AS MovementType,
                            p.Id, p.PaymentNumber AS DocNumber, p.PaymentDate AS DocDate,
                            s.SupplierName AS Counterparty,
                            p.Amount,
                            pm.Name AS PaymentMethod,
                            (SELECT COUNT(*) FROM PaymentAllocations pa WHERE pa.PaymentId = p.Id) AS InvoicesCount,
                            p.Notes,
                            p.Status
                        FROM Payments p
                        LEFT JOIN Suppliers s ON p.SupplierId = s.Id
                        LEFT JOIN PaymentMethods pm ON p.PaymentMethodId = pm.Id
                        WHERE p.PaymentDate >= @DateFrom AND p.PaymentDate <= @DateTo
                        AND p.Amount >= @MinAmt AND p.Amount <= @MaxAmt";

                    var cpFilter = cmbCounterparty.SelectedItem as FilterItem;
                    if (cpFilter != null && !cpFilter.IsCustomer)
                        sqlP += " AND p.SupplierId = @CpId";

                    if (cmbPaymentMethod.SelectedIndex > 0)
                    {
                        var pmFilter = cmbPaymentMethod.SelectedItem as FilterItem;
                        if (pmFilter != null) sqlP += " AND p.PaymentMethodId = @PmId";
                    }

                    if (!string.IsNullOrEmpty(search))
                        sqlP += " AND (p.PaymentNumber LIKE @Search OR s.SupplierName LIKE @Search OR p.Notes LIKE @Search)";

                    sqlP += " ORDER BY p.PaymentDate DESC";

                    var payments = conn.Query(sqlP, new
                    {
                        DateFrom = dtpFrom.Value.Date,
                        DateTo = dtpTo.Value.Date.AddDays(1),
                        MinAmt = minAmt,
                        MaxAmt = maxAmt,
                        CpId = cpFilter?.Id,
                        PmId = (cmbPaymentMethod.SelectedItem as FilterItem)?.Id,
                        Search = $"%{search}%"
                    }).ToList();

                    all.AddRange(payments);
                }

                // Ταξινόμηση κατά ημερομηνία
                var sorted = all.OrderByDescending(x => x.DocDate).ThenByDescending(x => x.Id).ToList();
                dgvMovements.DataSource = sorted;

                // Υπολογισμός στατιστικών
                int total = sorted.Count;
                decimal totalReceipts = 0, totalPayments = 0;
                foreach (var m in sorted)
                {
                    if (m.MovementType == "Receipt") totalReceipts += m.Amount;
                    else totalPayments += m.Amount;
                }
                decimal balance = totalReceipts - totalPayments;

                lblTotalRecords.Text = $"{total}";
                lblReceipts.Text = $"{totalReceipts:N2} €";
                lblPayments.Text = $"{totalPayments:N2} €";
                lblBalance.Text = $"{balance:N2} €";
                lblTotalAmount.Text = $"{totalReceipts + totalPayments:N2} €";

                // Χρώμα ισοζυγίου
                lblBalance.ForeColor = balance >= 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenMovementForm()
        {
            try { using (var frm = new ReceiptPaymentForm()) frm.ShowDialog(); LoadMovements(); }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private void EditSelected()
        {
            MessageBox.Show("Επεξεργασία κίνησης - Υπό υλοποίηση", "Πληροφορία", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeleteSelected()
        {
            if (dgvMovements.CurrentRow == null) { MessageBox.Show("Επίλεξε μια κίνηση!"); return; }

            string type = dgvMovements.CurrentRow.Cells["MovementType"].Value?.ToString();
            int id = Convert.ToInt32(dgvMovements.CurrentRow.Cells["Id"].Value);
            string docNumber = dgvMovements.CurrentRow.Cells["DocNumber"].Value?.ToString();

            if (MessageBox.Show($"Διαγραφή {docNumber};" + "\n\nΠροσοχή: Θα διαγραφούν και οι κατανομές!", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                if (type == "Receipt")
                {
                    conn.Execute("DELETE FROM ReceiptAllocations WHERE ReceiptId = @Id", new { Id = id });
                    conn.Execute("DELETE FROM Receipts WHERE Id = @Id", new { Id = id });
                }
                else
                {
                    conn.Execute("DELETE FROM PaymentAllocations WHERE PaymentId = @Id", new { Id = id });
                    conn.Execute("DELETE FROM Payments WHERE Id = @Id", new { Id = id });
                }
                MessageBox.Show("✅ Η κίνηση διαγράφηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMovements();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα:\n" + ex.Message); }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}