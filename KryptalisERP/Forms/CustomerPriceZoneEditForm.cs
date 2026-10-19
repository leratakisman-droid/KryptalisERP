using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    // ΣΗΜΕΙΩΣΗ: Χωρίς 'partial' και με ρητή κληρονομία από Form
    public class CustomerPriceZoneEditForm : Form
    {
        private readonly int _customerId;
        private readonly int _priceZoneId;

        private TextBox txtCode;
        private TextBox txtName;
        private TextBox txtDescription;
        private ComboBox cmbCurrency;
        private NumericUpDown numDiscount;
        private CheckBox chkActive;
        private Button btnSave;
        private Button btnCancel;

        public CustomerPriceZoneEditForm(int customerId, int priceZoneId = 0)
        {
            _customerId = customerId;
            _priceZoneId = priceZoneId;

            CreateControls();
            this.Load += CustomerPriceZoneEditForm_Load;
        }

        private void CreateControls()
        {
            this.Text = _priceZoneId > 0 ? "✏️ Επεξεργασία Τιμοκαταλόγου" : "➕ Νέος Τιμοκατάλογος";
            this.Size = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            int y = 20;
            int labelX = 20;
            int controlX = 150;

            // Κωδικός
            var lblCode = new Label { Text = "Κωδικός*:", Location = new System.Drawing.Point(labelX, y), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold) };
            this.Controls.Add(lblCode);
            txtCode = new TextBox { Location = new System.Drawing.Point(controlX, y), Size = new System.Drawing.Size(200, 28), Font = new System.Drawing.Font("Segoe UI", 10F) };
            this.Controls.Add(txtCode);
            y += 45;

            // Όνομα
            var lblName = new Label { Text = "Όνομα*:", Location = new System.Drawing.Point(labelX, y), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold) };
            this.Controls.Add(lblName);
            txtName = new TextBox { Location = new System.Drawing.Point(controlX, y), Size = new System.Drawing.Size(400, 28), Font = new System.Drawing.Font("Segoe UI", 10F) };
            this.Controls.Add(txtName);
            y += 45;

            // Περιγραφή
            var lblDesc = new Label { Text = "Περιγραφή:", Location = new System.Drawing.Point(labelX, y), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold) };
            this.Controls.Add(lblDesc);
            txtDescription = new TextBox { Location = new System.Drawing.Point(controlX, y), Size = new System.Drawing.Size(400, 80), Multiline = true, ScrollBars = ScrollBars.Vertical, Font = new System.Drawing.Font("Segoe UI", 10F) };
            this.Controls.Add(txtDescription);
            y += 100;

            // Νόμισμα
            var lblCurrency = new Label { Text = "Νόμισμα:", Location = new System.Drawing.Point(labelX, y), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold) };
            this.Controls.Add(lblCurrency);
            cmbCurrency = new ComboBox { Location = new System.Drawing.Point(controlX, y), Size = new System.Drawing.Size(100, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new System.Drawing.Font("Segoe UI", 10F) };
            cmbCurrency.Items.AddRange(new object[] { "€", "$", "£" });
            cmbCurrency.SelectedIndex = 0;
            this.Controls.Add(cmbCurrency);
            y += 45;

            // Έκπτωση
            var lblDiscount = new Label { Text = "Έκπτωση (%):", Location = new System.Drawing.Point(labelX, y), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold) };
            this.Controls.Add(lblDiscount);
            numDiscount = new NumericUpDown { Location = new System.Drawing.Point(controlX, y), Size = new System.Drawing.Size(100, 28), DecimalPlaces = 2, Maximum = 100, Font = new System.Drawing.Font("Segoe UI", 10F) };
            this.Controls.Add(numDiscount);
            y += 45;

            // Ενεργό
            chkActive = new CheckBox { Text = "✅ Ενεργό", Location = new System.Drawing.Point(controlX, y), AutoSize = true, Checked = true, Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold) };
            this.Controls.Add(chkActive);
            y += 50;

            // Κουμπιά
            btnSave = new Button { Text = "💾 Αποθήκευση", Location = new System.Drawing.Point(controlX, y), Size = new System.Drawing.Size(140, 40), BackColor = System.Drawing.Color.FromArgb(46, 204, 113), ForeColor = System.Drawing.Color.White, FlatStyle = FlatStyle.Flat, Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold) };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button { Text = "❌ Ακύρωση", Location = new System.Drawing.Point(controlX + 160, y), Size = new System.Drawing.Size(120, 40), BackColor = System.Drawing.Color.FromArgb(127, 140, 141), ForeColor = System.Drawing.Color.White, FlatStyle = FlatStyle.Flat, Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold) };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            this.Controls.Add(btnCancel);
        }

        private void CustomerPriceZoneEditForm_Load(object sender, EventArgs e)
        {
            if (_priceZoneId > 0)
            {
                LoadPriceZoneData();
            }
        }

        private void LoadPriceZoneData()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var zone = conn.QueryFirstOrDefault(@"SELECT * FROM CustomerPriceZones WHERE Id = @Id", new { Id = _priceZoneId });

                if (zone != null)
                {
                    txtCode.Text = zone.Code ?? "";
                    txtName.Text = zone.Name ?? "";
                    txtDescription.Text = zone.Description ?? "";
                    cmbCurrency.Text = zone.Currency ?? "€";
                    numDiscount.Value = zone.DiscountPercentage;
                    chkActive.Checked = zone.IsActive;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("⚠️ Συμπλήρωσε τον Κωδικό!", "Επικύρωση", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("⚠️ Συμπλήρωσε το Όνομα!", "Επικύρωση", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                // Έλεγχος διπλότυπου κωδικού
                string checkQuery = _priceZoneId > 0
                    ? "SELECT COUNT(*) FROM CustomerPriceZones WHERE Code = @Code AND Id != @Id"
                    : "SELECT COUNT(*) FROM CustomerPriceZones WHERE Code = @Code";

                int exists = conn.ExecuteScalar<int>(checkQuery, new { Code = txtCode.Text.Trim(), Id = _priceZoneId });

                if (exists > 0)
                {
                    MessageBox.Show($"❌ Ο κωδικός '{txtCode.Text.Trim()}' υπάρχει ήδη!", "Διπλότυπος Κωδικός", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCode.Focus();
                    txtCode.SelectAll();
                    return;
                }

                // Αποθήκευση
                if (_priceZoneId > 0)
                {
                    conn.Execute(@"UPDATE CustomerPriceZones SET Code = @Code, Name = @Name, Description = @Description, Currency = @Currency, DiscountPercentage = @DiscountPercentage, IsActive = @IsActive, UpdatedAt = GETDATE() WHERE Id = @Id", new
                    {
                        Id = _priceZoneId,
                        Code = txtCode.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        Currency = cmbCurrency.Text,
                        DiscountPercentage = numDiscount.Value,
                        IsActive = chkActive.Checked
                    });
                    MessageBox.Show("✅ Ο τιμοκατάλογος ενημερώθηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    conn.Execute(@"INSERT INTO CustomerPriceZones (CustomerId, Code, Name, Description, Currency, DiscountPercentage, IsActive, CreatedAt, UpdatedAt) VALUES (@CustomerId, @Code, @Name, @Description, @Currency, @DiscountPercentage, @IsActive, GETDATE(), GETDATE())", new
                    {
                        CustomerId = _customerId,
                        Code = txtCode.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        Currency = cmbCurrency.Text,
                        DiscountPercentage = numDiscount.Value,
                        IsActive = chkActive.Checked
                    });
                    MessageBox.Show("✅ Ο τιμοκατάλογος δημιουργήθηκε!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα αποθήκευσης:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private string GetConnectionString()
        {
            string connStr = Program.Configuration?.GetConnectionString("DefaultConnection");
            return string.IsNullOrEmpty(connStr)
                ? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;"
                : connStr;
        }
    }
}