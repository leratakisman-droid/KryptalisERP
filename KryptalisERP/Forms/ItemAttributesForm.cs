using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemAttributesForm : Form
    {
        private readonly int _itemId;
        private readonly string _connStr;

        public ItemAttributesForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemAttributesForm_Load;
        }

        private void ItemAttributesForm_Load(object sender, EventArgs e)
        {
            LoadAttributes();
            cmbAttributeType.Items.AddRange(new object[] { "Κείμενο", "Αριθμός", "Ημερομηνία", "Boolean", "Λίστα" });
            cmbAttributeType.SelectedIndex = 0;
        }

        private void LoadAttributes()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                dgvAttributes.DataSource = conn.Query(@"
                    SELECT Id, AttributeName AS [Χαρακτηριστικό], AttributeValue AS [Τιμή], 
                           AttributeCode AS [Κωδικός], AttributeType AS [Τύπος],
                           CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργό]
                    FROM ItemAttributes WHERE ItemId = @Id ORDER BY SortOrder",
                    new { Id = _itemId }).ToList();
                if (dgvAttributes.Columns.Contains("Id")) dgvAttributes.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAttributeName.Text)) { MessageBox.Show("Όνομα υποχρεωτικό!"); return; }
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute(@"INSERT INTO ItemAttributes (ItemId, AttributeName, AttributeValue, AttributeCode, AttributeType, IsActive, CreatedAt) 
                    VALUES (@ItemId, @Name, @Value, @Code, @Type, 1, GETDATE())", new
                {
                    ItemId = _itemId,
                    Name = txtAttributeName.Text.Trim(),
                    Value = txtAttributeValue.Text.Trim(),
                    Code = txtAttributeCode.Text.Trim(),
                    Type = cmbAttributeType.Text.Trim()
                });
                MessageBox.Show("✅ Προστέθηκε!");
                LoadAttributes();
                txtAttributeName.Clear(); txtAttributeValue.Clear(); txtAttributeCode.Clear();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAttributes.CurrentRow == null) { MessageBox.Show("Επίλεξε!"); return; }
            var id = dgvAttributes.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute("DELETE FROM ItemAttributes WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                LoadAttributes();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}