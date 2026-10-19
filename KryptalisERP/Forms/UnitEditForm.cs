using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace KryptalisERP
{
    public partial class UnitEditForm : Form
    {
        private readonly int? _unitId;
        private readonly string _connStr;

        public UnitEditForm(int? unitId = null)
        {
            InitializeComponent();
            _unitId = unitId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += UnitEditForm_Load;
        }

        private void UnitEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _unitId.HasValue ? "✏️ Επεξεργασία Μονάδας" : "➕ Νέα Μονάδα";
            cmbType.Items.AddRange(new object[] { "Μήκος", "Βάρος", "Όγκος", "Εμβαδόν", "Ποσότητα", "Χρόνος", "Άλλο" });
            cmbType.SelectedIndex = 4;
            if (_unitId.HasValue) LoadUnitData(_unitId.Value);
        }

        private void LoadUnitData(int id)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var unit = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM UnitsOfMeasure WHERE Id = @Id", new { Id = id });
                if (unit == null) return;

                txtCode.Text = unit.Code ?? "";
                txtName.Text = unit.Name ?? "";
                txtSymbol.Text = unit.Symbol ?? "";
                cmbType.Text = unit.UnitType ?? "Ποσότητα";
                chkIsActive.Checked = unit.IsActive ?? true;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("⚠️ Το Όνομα είναι υποχρεωτικό!", "Προσοχή");
                txtName.Focus();
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                if (_unitId.HasValue)
                {
                    conn.Execute(@"UPDATE UnitsOfMeasure SET Code=@Code, Name=@Name, Symbol=@Symbol, UnitType=@UnitType, IsActive=@IsActive, UpdatedAt=GETDATE() WHERE Id=@Id", new
                    {
                        Id = _unitId.Value,
                        Code = txtCode.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Symbol = txtSymbol.Text.Trim(),
                        UnitType = cmbType.Text.Trim(),
                        IsActive = chkIsActive.Checked
                    });
                }
                else
                {
                    conn.Execute(@"INSERT INTO UnitsOfMeasure (Code, Name, Symbol, UnitType, IsActive, CreatedAt) VALUES (@Code, @Name, @Symbol, @UnitType, @IsActive, GETDATE())", new
                    {
                        Code = txtCode.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Symbol = txtSymbol.Text.Trim(),
                        UnitType = cmbType.Text.Trim(),
                        IsActive = chkIsActive.Checked
                    });
                }

                MessageBox.Show("✅ Αποθηκεύτηκε!", "Επιτυχία");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e) { this.DialogResult = DialogResult.Cancel; this.Close(); }
    }
}