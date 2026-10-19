using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemRelatedForm : Form
    {
        private readonly int _itemId;
        private readonly string _connStr;

        public ItemRelatedForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemRelatedForm_Load;
        }

        private void ItemRelatedForm_Load(object sender, EventArgs e)
        {
            cmbType.Items.AddRange(new object[] { "Σχετικό", "Αντικαταστάσιμο", "Αξεσουάρ", "Προτεινόμενο", "Συμπληρωματικό" });
            cmbType.SelectedIndex = 0;
            LoadRelated();
        }

        private void LoadRelated()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var related = conn.Query(@"
                    SELECT r.Id, i.Name AS [Είδος], i.Code AS [Κωδικός], r.RelationType AS [Τύπος], r.Notes AS [Σημειώσεις]
                    FROM ItemRelated r LEFT JOIN Items i ON r.RelatedItemId = i.Id
                    WHERE r.ItemId = @Id ORDER BY i.Name",
                    new { Id = _itemId }).ToList();
                dgvRelated.DataSource = related;
                if (dgvRelated.Columns.Contains("Id")) dgvRelated.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using var form = new ItemSearchForm();
            if (form.ShowDialog() == DialogResult.OK && form.SelectedItem != null)
            {
                txtRelatedItemId.Text = form.SelectedItem.Id.ToString();
                txtRelatedName.Text = form.SelectedItem.Όνομα?.ToString() ?? "";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRelatedItemId.Text)) { MessageBox.Show("Επίλεξε είδος!"); return; }
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute(@"INSERT INTO ItemRelated (ItemId, RelatedItemId, RelationType, Notes, CreatedAt) 
                    VALUES (@ItemId, @Related, @Type, @Notes, GETDATE())", new
                {
                    ItemId = _itemId,
                    Related = int.Parse(txtRelatedItemId.Text),
                    Type = cmbType.Text.Trim(),
                    Notes = txtNotes.Text.Trim()
                });
                MessageBox.Show("✅ Προστέθηκε!");
                LoadRelated();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRelated.CurrentRow == null) { MessageBox.Show("Επίλεξε!"); return; }
            var id = dgvRelated.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute("DELETE FROM ItemRelated WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                LoadRelated();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}