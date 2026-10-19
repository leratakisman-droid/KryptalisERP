using ClosedXML.Excel;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemUnitRelationsForm : Form
    {
        private readonly int _itemId;
        private readonly string _connStr;

        public ItemUnitRelationsForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemUnitRelationsForm_Load;
        }

        private void ItemUnitRelationsForm_Load(object sender, EventArgs e)
        {
            LoadRelations();
            LoadUnits();
            cmbRelationType.Items.AddRange(new object[] { "Πολλαπλασιασμός", "Διαίρεση", "Μετατροπή" });
            cmbRelationType.SelectedIndex = 0;
        }

        private void LoadUnits()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var units = conn.Query("SELECT Id, Name FROM UnitsOfMeasure ORDER BY Name").ToList();
                cmbUnit.DataSource = units;
                cmbUnit.DisplayMember = "Name";
                cmbUnit.ValueMember = "Id";
            }
            catch { }
        }

        private void LoadRelations()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                dgvRelations.DataSource = conn.Query(@"
                    SELECT r.Id, u.Name AS [Μονάδα], r.RelationType AS [Τύπος], 
                           r.Coefficient AS [Συντελεστής], r.Formula AS [Τύπος],
                           CASE WHEN r.IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργή]
                    FROM ItemUnitRelations r
                    LEFT JOIN UnitsOfMeasure u ON r.UnitId = u.Id
                    WHERE r.ItemId = @Id
                    ORDER BY u.Name",
                    new { Id = _itemId }).ToList();
                if (dgvRelations.Columns.Contains("Id")) dgvRelations.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbUnit.SelectedValue == null) { MessageBox.Show("Επίλεξε μονάδα!"); return; }
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute(@"INSERT INTO ItemUnitRelations (ItemId, UnitId, RelationType, Coefficient, Formula, IsActive) 
            VALUES (@ItemId, @UnitId, @Type, @Coeff, @Formula, 1)", new
                {
                    ItemId = _itemId,
                    UnitId = cmbUnit.SelectedValue,
                    Type = cmbRelationType.Text.Trim(),
                    Coeff = numCoefficient.Value,
                    Formula = txtFormula.Text.Trim()
                });
                MessageBox.Show("✅ Προστέθηκε!");
                LoadRelations();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRelations.CurrentRow == null) { MessageBox.Show("Επίλεξε!"); return; }
            var id = dgvRelations.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using var conn = new SqlConnection(_connStr);
                conn.Execute("DELETE FROM ItemUnitRelations WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                LoadRelations();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}