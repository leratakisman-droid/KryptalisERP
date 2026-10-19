using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class LotEditForm : Form
    {
        private readonly int _itemId;
        private readonly int _lotId;
        private readonly string _connStr;

        public LotEditForm(int itemId, int lotId = 0)
        {
            InitializeComponent();
            _itemId = itemId;
            _lotId = lotId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += LotEditForm_Load;
        }

        private void LotEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _lotId > 0 ? "✏️ Επεξεργασία Παρτίδας" : "➕ Νέα Παρτίδα";
            dtpManufacture.Value = DateTime.Today;
            dtpExpiry.Value = DateTime.Today.AddYears(1);
            if (_lotId > 0) LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var lot = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Lots WHERE Id = @Id", new { Id = _lotId });
                if (lot == null) return;

                txtLotNumber.Text = lot.LotNumber ?? "";
                dtpManufacture.Value = lot.ManufactureDate ?? DateTime.Today;
                dtpExpiry.Value = lot.ExpiryDate ?? DateTime.Today.AddYears(1);
                txtCountry.Text = lot.CountryOfOrigin ?? "";
                txtNotes.Text = lot.Notes ?? "";
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLotNumber.Text))
            {
                MessageBox.Show("⚠️ Ο Κωδικός Παρτίδας είναι υποχρεωτικός!");
                txtLotNumber.Focus();
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                if (_lotId > 0)
                {
                    conn.Execute(@"UPDATE Lots SET LotNumber=@LotNumber, ManufactureDate=@ManufactureDate, 
                        ExpiryDate=@ExpiryDate, CountryOfOrigin=@CountryOfOrigin, Notes=@Notes, UpdatedAt=GETDATE()
                        WHERE Id=@Id AND ItemId=@ItemId", new
                    {
                        Id = _lotId,
                        ItemId = _itemId,
                        LotNumber = txtLotNumber.Text.Trim(),
                        ManufactureDate = dtpManufacture.Value,
                        ExpiryDate = dtpExpiry.Value,
                        CountryOfOrigin = txtCountry.Text.Trim(),
                        Notes = txtNotes.Text.Trim()
                    });
                }
                else
                {
                    conn.Execute(@"INSERT INTO Lots (ItemId, LotNumber, ManufactureDate, ExpiryDate, CountryOfOrigin, Notes, CreatedAt) 
                        VALUES (@ItemId, @LotNumber, @ManufactureDate, @ExpiryDate, @CountryOfOrigin, @Notes, GETDATE())", new
                    {
                        ItemId = _itemId,
                        LotNumber = txtLotNumber.Text.Trim(),
                        ManufactureDate = dtpManufacture.Value,
                        ExpiryDate = dtpExpiry.Value,
                        CountryOfOrigin = txtCountry.Text.Trim(),
                        Notes = txtNotes.Text.Trim()
                    });
                }

                MessageBox.Show("✅ Αποθηκεύτηκε!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}