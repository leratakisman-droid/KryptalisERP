using Dapper;
using KryptalisERP.Engines; // Για ModuleConfig
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
namespace KryptalisERP.Forms
{
    public partial class DocumentListViewer : Form
    {
        private DataGridView dgvDocs;
        private TextBox txtSearch;
        private ComboBox cmbFilterStatus;
        private Button btnNew, btnEdit, btnDelete;

        private Engines.GenericFormEngine.ModuleConfig _config;
        private string _connStr;

        public DocumentListViewer(Engines.GenericFormEngine.ModuleConfig config)
        {
            _config = config;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";

            InitializeCustomComponents(); // Χρησιμοποιούμε δική μας init αντί για Designer
            LoadData();
        }

        // ============================================
        // UI INITIALIZATION (FIXED DOCKING & LAYOUT)
        // ============================================
        private void InitializeCustomComponents()
        {
            this.Text = $"{_config.DisplayName} - Λίστα Εγγράφων";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 242, 245); // Light Gray BG like main form

            // --- 1. TOOLBAR PANEL (Top) ---
            var pnlToolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(10),
                BorderStyle = BorderStyle.None
            };

            // Search Box
            txtSearch = new TextBox
            {
                Location = new Point(10, 15),
                Size = new Size(200, 26),
                PlaceholderText = "Αναζήτηση Αριθμού...",
                Font = new Font("Segoe UI", 9.5F)
            };
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) LoadData(); };
            pnlToolbar.Controls.Add(txtSearch);

            // Status Filter
            cmbFilterStatus = new ComboBox
            {
                Location = new Point(220, 15),
                Size = new Size(150, 26),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5F)
            };
            cmbFilterStatus.Items.AddRange(new[] { "Όλες", "DRAFT", "ISSUED", "PAID", "CANCELLED" });
            cmbFilterStatus.SelectedIndex = 0;
            cmbFilterStatus.SelectedIndexChanged += (s, e) => LoadData();
            pnlToolbar.Controls.Add(cmbFilterStatus);

            // Buttons
            btnNew = MakeBtn("+ Νέο", 400, Color.Green);
            btnNew.Click += BtnNew_Click;
            pnlToolbar.Controls.Add(btnNew);

            btnEdit = MakeBtn("✏️ Άνοιγμα", 500, Color.Blue);
            btnEdit.Click += BtnEdit_Click;
            pnlToolbar.Controls.Add(btnEdit);

            btnDelete = MakeBtn("❌ Ακύρωση", 620, Color.Red);
            btnDelete.Click += BtnDelete_Click;
            pnlToolbar.Controls.Add(btnDelete);

            this.Controls.Add(pnlToolbar);

            // --- 2. DATA GRID (Fill Remaining Space) ---
            dgvDocs = new DataGridView
            {
                Dock = DockStyle.Fill, // This makes it take all remaining space below toolbar
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                EnableHeadersVisualStyles = false,
                Font = new Font("Segoe UI", 9.5F),
                ColumnHeadersHeight = 35,
                RowTemplate = { Height = 30 },
                AlternatingRowsDefaultCellStyle = { BackColor = Color.FromArgb(248, 249, 250) },
                DefaultCellStyle = { SelectionBackColor = Color.FromArgb(52, 152, 219), SelectionForeColor = Color.White }
            };

            // Style Headers
            dgvDocs.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 152, 219), // Blue Header
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(5)
            };

            // Define Columns Manually for Stability
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "Number", HeaderText = "Αριθμός", Width = 120 });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "IssueDate", HeaderText = "Ημερομηνία", Width = 100 });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "PartyName", HeaderText = "Συναλλασσόμενος", Width = 250 });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "GrandTotal", HeaderText = "Σύνολο €", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvDocs.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Κατάσταση", Width = 100 });

            // Double Click to Edit
            dgvDocs.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) BtnEdit_Click(s, e); };

            this.Controls.Add(dgvDocs);

            // Ensure Z-Order is correct (Grid on top of nothing, Toolbar on top of Grid visually due to Dock Top/Fill logic)
            dgvDocs.BringToFront();
        }

        // ============================================
        // DATA LOGIC
        // ============================================
        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);

                // Determine Category based on Module Name
                string category = _config.ModuleName switch
                {
                    "Sales" => "SALES",
                    "Purchases" => "PURCHASE",
                    "DeliveryNotes" => "INVENTORY",
                    "Finance" => "RECEIPT",
                    "Quotes" => "SALES",
                    "PurchaseOrders" => "PURCHASE",
                    _ => "%"
                };

                string sql = @"
                    SELECT d.Id, d.Number, d.IssueDate, d.GrandTotal, d.Status,
                           ISNULL(c.Epwnymia, s.SupplierName) as PartyName
                    FROM Documents d
                    LEFT JOIN Customers c ON d.CustomerId = c.Id
                    LEFT JOIN Suppliers s ON d.SupplierId = s.Id
                    INNER JOIN DocumentSeries ds ON d.SeriesId = ds.Id
                    WHERE ds.Category LIKE @CatPattern ";

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    sql += " AND (d.Number LIKE @Search OR ISNULL(c.Epwnymia, s.SupplierName) LIKE @Search) ";

                if (cmbFilterStatus.SelectedItem.ToString() != "Όλες")
                    sql += " AND d.Status = @Status ";

                var parameters = new
                {
                    CatPattern = $"%{category}%",
                    Search = $"%{txtSearch.Text}%",
                    Status = cmbFilterStatus.SelectedItem.ToString()
                };

                var data = conn.Query(sql, parameters).ToList();
                dgvDocs.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης δεδομένων:\n" + ex.Message, "Error");
            }
        }

        // ============================================
        // ACTIONS
        // ============================================
        private void BtnNew_Click(object sender, EventArgs e)
        {
            Form childForm = null;
            switch (_config.ModuleName.ToLower())
            {
                case "sales": childForm = new SalesInvoiceForm(); break;
                case "purchases": childForm = new PurchaseInvoiceForm(); break;
                case "deliverynotes": childForm = new DeliveryNoteForm(); break;
                case "finance": childForm = new ReceiptPaymentForm(); break;
                default: MessageBox.Show("Νέα δημιουργία δεν υλοποιήθηκε ακόμα."); return;
            }

            if (childForm != null && childForm.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // Refresh list after save
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDocs.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvDocs.SelectedRows[0].Cells["Id"].Value);

            Form childForm = null;
            switch (_config.ModuleName.ToLower())
            {
                case "sales": childForm = new SalesInvoiceForm(id); break;
                case "purchases": childForm = new PurchaseInvoiceForm(id); break;
                case "deliverynotes": childForm = new DeliveryNoteForm(id); break;
                    // Add others as needed
            }

            if (childForm != null && childForm.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDocs.SelectedRows.Count == 0) return;
            if (MessageBox.Show("Ακύρωση εγγράφου;", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvDocs.SelectedRows[0].Cells["Id"].Value);
                using var conn = new SqlConnection(_connStr);
                conn.Execute("UPDATE Documents SET Status='CANCELLED' WHERE Id=@Id", new { Id = id });
                LoadData();
            }
        }

        private Button MakeBtn(string text, int x, Color color)
        {
            return new Button { Text = text, Location = new Point(x, 12), Size = new Size(100, 30), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
        }
    }
}