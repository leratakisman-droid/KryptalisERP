using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class ProviderListForm : Form
    {
        private DataGridView dgvProviders;
        private Button btnNew, btnEdit, btnDelete, btnRefresh, btnClose;
        private Label lblStats;

        public ProviderListForm()
        {
            InitializeUI();
            this.Load += (s, e) => LoadData();
        }

        private void InitializeUI()
        {
            this.Text = "🏢 Διαχείριση Παρόχων (Providers)";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);

            var pnlToolbar = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.White, Padding = new Padding(10) };

            btnNew = CreateBtn("➕ Νέος Πάροχος", 10, 8, 150, 34, Color.Green);
            btnNew.Click += (s, e) => OpenForm(null);

            btnEdit = CreateBtn("✏️ Επεξεργασία", 170, 8, 120, 34, Color.Blue);
            btnEdit.Click += (s, e) => EditSel();

            btnDelete = CreateBtn("🗑️ Διαγραφή", 300, 8, 100, 34, Color.Red);
            btnDelete.Click += (s, e) => DeleteSel();

            btnRefresh = CreateBtn("🔄 Ανανέωση", 410, 8, 100, 34, Color.Gray);
            btnRefresh.Click += (s, e) => LoadData();

            btnClose = CreateBtn("❌ Κλείσιμο", 520, 8, 100, 34, Color.DarkGray);
            btnClose.Click += (s, e) => this.Close();

            pnlToolbar.Controls.AddRange(new Control[] { btnNew, btnEdit, btnDelete, btnRefresh, btnClose });
            this.Controls.Add(pnlToolbar);

            dgvProviders = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 10F)
            };

            // ✅ FIX: Σωστή σύνταξη Font
            dgvProviders.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            dgvProviders.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });
            dgvProviders.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProviderCode", HeaderText = "Κωδικός", Width = 100, DataPropertyName = "ProviderCode" });
            dgvProviders.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProviderName", HeaderText = "Όνομα", Width = 200, DataPropertyName = "ProviderName" });
            dgvProviders.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProviderType", HeaderText = "Τύπος", Width = 150, DataPropertyName = "ProviderType" });
            dgvProviders.Columns.Add(new DataGridViewTextBoxColumn { Name = "Environment", HeaderText = "Peri.", Width = 80, DataPropertyName = "Environment" });
            dgvProviders.Columns.Add(new DataGridViewTextBoxColumn { Name = "IsActive", HeaderText = "Ενεργός", Width = 80, DataPropertyName = "IsActiveDisplay" });
            dgvProviders.Columns.Add(new DataGridViewTextBoxColumn { Name = "UseMyDATA", HeaderText = "myDATA", Width = 80, DataPropertyName = "UseMyDataDisplay" });
            dgvProviders.Columns.Add(new DataGridViewTextBoxColumn { Name = "LastUpdate", HeaderText = "Τελ. Ενημέρωση", Width = 150, DataPropertyName = "UpdatedAt" });

            dgvProviders.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditSel(); };
            this.Controls.Add(dgvProviders);

            var pnlStatus = new Panel { Dock = DockStyle.Bottom, Height = 30, BackColor = Color.LightGray };
            // ✅ FIX: Σωστή σύνταξη Font
            lblStats = new Label { Text = "", Location = new Point(10, 5), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            pnlStatus.Controls.Add(lblStats);
            this.Controls.Add(pnlStatus);
        }

        // ✅ FIX: Σωστή σύνταξη Font στο helper method
        private Button CreateBtn(string t, int x, int y, int w, int h, Color c)
            => new Button { Text = t, Location = new Point(x, y), Size = new Size(w, h), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), FlatAppearance = { BorderSize = 0 } };

        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(GetConnStr());
                var data = conn.Query(@"
                    SELECT Id, ProviderCode, ProviderName, ProviderType, Environment, UpdatedAt,
                           CASE WHEN IsActive=1 THEN '✅' ELSE '❌' END AS IsActiveDisplay,
                           CASE WHEN UseMyDATA=1 THEN 'YES' ELSE 'NO' END AS UseMyDataDisplay
                    FROM Providers ORDER BY IsActive DESC, ProviderName").ToList();

                dgvProviders.DataSource = data;
                lblStats.Text = $"Σύνολο Παρόχων: {data.Count} | Ενεργοί: {data.Count(x => x.IsActiveDisplay == "✅")}";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void OpenForm(int? id)
        {
            try
            {
                using (var f = new ProviderForm(id))
                {
                    if (f.ShowDialog() == DialogResult.OK) LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void EditSel()
        {
            if (dgvProviders.CurrentRow == null) return;
            OpenForm(Convert.ToInt32(dgvProviders.CurrentRow.Cells["Id"].Value));
        }

        private void DeleteSel()
        {
            if (dgvProviders.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvProviders.CurrentRow.Cells["Id"].Value);
            if (MessageBox.Show("Διαγραφή παρόχου;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(GetConnStr());
                    conn.Execute("DELETE FROM Providers WHERE Id=@Id", new { Id = id });
                    LoadData();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private string GetConnStr() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}