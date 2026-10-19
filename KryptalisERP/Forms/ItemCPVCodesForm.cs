using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemCPVCodesForm : Form
    {
        private readonly int _itemId;
        private readonly string _connStr;

        public ItemCPVCodesForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemCPVCodesForm_Load;
        }

        private void ItemCPVCodesForm_Load(object sender, EventArgs e)
        {
            LoadCPVs();
            LoadCPVList();
        }

        // Φόρτωση λίστας CPV codes στο ComboBox
        private void LoadCPVList()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var cpvList = conn.Query(@"
                    SELECT Id, Code + ' - ' + Name AS DisplayName, Code, Name
                    FROM CPVCodes 
                    WHERE IsActive = 1 
                    ORDER BY Code").ToList();
                cmbCPVCode.DataSource = cpvList;
                cmbCPVCode.DisplayMember = "DisplayName";
                cmbCPVCode.ValueMember = "Id";
            }
            catch { }
        }

        // Φόρτωση των CPV codes του είδους
        private void LoadCPVs()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                dgvCPVs.DataSource = conn.Query(@"
                    SELECT ic.Id, 
                           c.Code AS [CPV Κωδικός], 
                           c.Name AS [Περιγραφή],
                           c.NameEn AS [Περιγραφή EN],
                           CASE WHEN ic.IsPrimary = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Κύριο],
                           ic.Notes AS [Σημειώσεις]
                    FROM ItemCPVCodes ic
                    LEFT JOIN CPVCodes c ON ic.CPVCodeId = c.Id
                    WHERE ic.ItemId = @Id
                    ORDER BY c.Code",
                    new { Id = _itemId }).ToList();
                if (dgvCPVs.Columns.Contains("Id")) dgvCPVs.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbCPVCode.SelectedValue == null) { MessageBox.Show("Επίλεξε CPV Code!"); return; }
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute(@"INSERT INTO ItemCPVCodes (ItemId, CPVCodeId, IsPrimary, Notes) 
                    VALUES (@ItemId, @CPVId, @Primary, @Notes)", new
                {
                    ItemId = _itemId,
                    CPVId = cmbCPVCode.SelectedValue,
                    Primary = chkIsPrimary.Checked,
                    Notes = txtNotes.Text.Trim()
                });
                MessageBox.Show("✅ Προστέθηκε!");
                LoadCPVs();
                txtNotes.Clear();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCPVs.CurrentRow == null) { MessageBox.Show("Επίλεξε!"); return; }
            var id = dgvCPVs.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute("DELETE FROM ItemCPVCodes WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                LoadCPVs();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}