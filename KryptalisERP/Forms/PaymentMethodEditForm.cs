using KryptalisERP.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class PaymentMethodEditForm : Form
    {
        private int _id;

        // ✅ ΝΕΑ CONTROLS ΓΙΑ ΣΥΝΔΕΣΗ ΜΕ POS
        private Label lblPosTerminal;
        private ComboBox cmbPosTerminal;

        public PaymentMethodEditForm(int id = 0)
        {
            InitializeComponent();
            _id = id;

            // ✅ Δυναμική προσθήκη του πεδίου POS αν δεν υπάρχει στον Designer
            AddPosTerminalControl();

            // ✅ Φόρτωση της λίστας των POS τερματικών
            LoadPosTerminals();

            if (_id > 0)
            {
                this.Text = "✏️ Επεξεργασία Τρόπου Πληρωμής";
                LoadData();
            }
            else
            {
                this.Text = "➕ Νέος Τρόπος Πληρωμής";
            }
        }

        // ✅ Μέθοδος για δυναμική δημιουργία των controls POS
        private void AddPosTerminalControl()
        {
            int yPos = 150; // Προεπιλεγμένη θέση Y

            // Προσπαθούμε να βρούμε το chkActive ή το txtMyData για να τοποθετήσουμε από κάτω
            if (this.Controls.Find("chkActive", true).Length > 0)
            {
                var chk = this.Controls.Find("chkActive", true)[0];
                yPos = chk.Bottom + 15;
            }
            else if (this.Controls.Find("txtMyData", true).Length > 0)
            {
                var txt = this.Controls.Find("txtMyData", true)[0];
                yPos = txt.Bottom + 15;
            }

            lblPosTerminal = new Label
            {
                Text = "Σύνδεση με POS:",
                Location = new Point(20, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            this.Controls.Add(lblPosTerminal);

            cmbPosTerminal = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(150, yPos - 3),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 9F)
            };
            this.Controls.Add(cmbPosTerminal);

            // Αύξηση ύψους φόρμας αν χρειάζεται
            this.ClientSize = new Size(this.ClientSize.Width, Math.Max(this.ClientSize.Height, yPos + 60));
        }

        // ✅ Φόρτωση λίστας POS από τη βάση
        private void LoadPosTerminals()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT Id, TerminalName FROM PosTerminals WHERE IsActive = 1 ORDER BY TerminalName");

                // Προσθήκη επιλογής "Κανένα"
                DataRow emptyRow = dt.NewRow();
                emptyRow["Id"] = DBNull.Value;
                emptyRow["TerminalName"] = "-- Κανένα (Χειροκίνητη Εισαγωγή) --";
                dt.Rows.InsertAt(emptyRow, 0);

                cmbPosTerminal.DataSource = dt;
                cmbPosTerminal.DisplayMember = "TerminalName";
                cmbPosTerminal.ValueMember = "Id";
                cmbPosTerminal.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης POS: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT * FROM PaymentMethods WHERE Id = @Id", new SqlParameter("@Id", _id));
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    txtCode.Text = r["MethodCode"]?.ToString() ?? "";
                    txtName.Text = r["MethodName"]?.ToString() ?? "";
                    txtMyData.Text = r["MyDataPaymentMethod"]?.ToString() ?? "";
                    chkActive.Checked = Convert.ToBoolean(r["IsActive"]);

                    // ✅ Ενημέρωση του επιλεγμένου POS τερματικού
                    if (r["PosTerminalId"] != DBNull.Value && cmbPosTerminal != null)
                    {
                        cmbPosTerminal.SelectedValue = r["PosTerminalId"];
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()) || string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Συμπλήρωσε Κωδικό και Όνομα!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                // ✅ Λήψη του επιλεγμένου POS Terminal ID
                object posTerminalId = (cmbPosTerminal != null && cmbPosTerminal.SelectedValue != null && cmbPosTerminal.SelectedValue.ToString() != "")
                    ? cmbPosTerminal.SelectedValue
                    : DBNull.Value;

                // ✅ Χρήση inline SQL για να είμαστε 100% σίγουροι ότι θα αποθηκευτεί το PosTerminalId 
                // (χωρίς να χρειαστεί να τροποποιήσεις το Stored Procedure sp_SavePaymentMethod)
                string sql;
                if (_id > 0)
                {
                    sql = @"UPDATE PaymentMethods SET 
                            MethodCode = @Code, 
                            MethodName = @Name, 
                            MyDataPaymentMethod = @MyData, 
                            IsActive = @Active,
                            PosTerminalId = @PosTerminalId
                            WHERE Id = @Id";
                }
                else
                {
                    sql = @"INSERT INTO PaymentMethods (MethodCode, MethodName, MyDataPaymentMethod, IsActive, PosTerminalId) 
                            VALUES (@Code, @Name, @MyData, @Active, @PosTerminalId)";
                }

                DatabaseHelper.ExecuteQuery(sql,
                    new SqlParameter("@Id", _id > 0 ? (object)_id : DBNull.Value),
                    new SqlParameter("@Code", txtCode.Text.Trim()),
                    new SqlParameter("@Name", txtName.Text.Trim()),
                    new SqlParameter("@MyData", string.IsNullOrEmpty(txtMyData.Text.Trim()) ? (object)DBNull.Value : txtMyData.Text.Trim()),
                    new SqlParameter("@Active", chkActive.Checked),
                    new SqlParameter("@PosTerminalId", posTerminalId));

                MessageBox.Show("✅ Ο τρόπος πληρωμής αποθηκεύτηκε επιτυχώς!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}