using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KryptalisERP
{
    // ============================================
    // CLASSES (Cleaned B2G Fields)
    // ============================================
    public class Customer
    {
        public int Id { get; set; }
        public string Epwnymia { get; set; }
        public string Onoma { get; set; }
        public string Epitheto { get; set; }
        public string Afm { get; set; }
        public string DOY { get; set; }
        public string Tilefono { get; set; }
        public string Kinhto { get; set; }
        public string Email { get; set; }
        public string Istoselida { get; set; }
        public string Dieuthinsi { get; set; }
        public string Poli { get; set; }
        public string Periochi { get; set; }
        public string TK { get; set; }
        public string Xora { get; set; }
        public string Epaggelma { get; set; }
        public string TyposPelati { get; set; }
        public int? VatExemptionId { get; set; }
        public string Tags { get; set; }
        public string Simeiwseis { get; set; }
        public bool Energo { get; set; }

        // ✅ ΜΟΝΟ ΑΥΤΟ ΓΙΑ B2G
        public string AnathetousaArchi { get; set; }

        public bool EinaiKaiPromitheutis { get; set; }
    }

    public class CustomerBranch
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string ManagerName { get; set; }
        public string Doy { get; set; }
        public string VatNumber { get; set; }
        public string BankName { get; set; }
        public string BankIban { get; set; }
        public bool IsMain { get; set; }
        public bool IsActive { get; set; }
    }

    public class CustomerContact
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsDecisionMaker { get; set; }
    }

    public class CustomerActivity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Status { get; set; }
        public string ActivityType { get; set; }
    }

    public class CustomerCreditLimit
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal LimitAmount { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? ApprovedById { get; set; }
        public bool IsBlocked { get; set; }
        public string BlockReason { get; set; }
        public string Notes { get; set; }
    }

    public class CustomerPriceZone
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool IsActive { get; set; }
    }

    // ============================================
    // MAIN FORM CLASS
    // ============================================
    public partial class CustomerEditForm : Form
    {
        private readonly int? _customerId;
        private int _currentCustomerId;

        public CustomerEditForm(int? customerId = null)
        {
            InitializeComponent();
            _customerId = customerId;
            this.Load += CustomerEditForm_Load;
        }

        private string GetConnectionString()
        {
            string connStr = Program.Configuration?.GetConnectionString("DefaultConnection");
            return string.IsNullOrEmpty(connStr)
                ? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;"
                : connStr;
        }

        private void CustomerEditForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                LoadLookups();

                if (_customerId.HasValue && _customerId.Value > 0)
                {
                    _currentCustomerId = _customerId.Value;
                    this.Text = "✏️ Επεξεργασία Πελάτη";
                    LoadCustomerData(_customerId.Value);
                }
                else
                {
                    _currentCustomerId = 0;
                    this.Text = "➕ Νέος Πελάτης";
                    ClearAllControls();
                    chkIsActive.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { this.Cursor = Cursors.Default; }
        }

        private void LoadLookups()
        {
            try
            {
                using var connection = new SqlConnection(GetConnectionString());

                cmbDoy.DataSource = connection.Query<string>("SELECT Name FROM GreekTaxOffices WHERE IsActive = 1 ORDER BY Name").ToList();
                try { cmbTK.DataSource = connection.Query<string>("SELECT TK FROM PostCodes ORDER BY TK").ToList(); } catch { }
                try { cmbCountry.DataSource = connection.Query<string>("SELECT Name FROM Countries ORDER BY Name").ToList(); } catch { }
                try { cmbProfession.DataSource = connection.Query<string>("SELECT Name FROM Professions ORDER BY Name").ToList(); } catch { }

                cmbClientType.Items.Clear();
                cmbClientType.Items.AddRange(new object[] { "Ιδιώτης", "Επιχείρηση", "Δημόσιο", "Μη Κερδοσκοπικός", "Ελεύθερος Επαγγελματίας" });

                try
                {
                    var vatQuery = @"SELECT Id, ISNULL(Name, Description) AS Name FROM VatExemptions ORDER BY Name";
                    var vatOptions = connection.Query(vatQuery).ToList();
                    cmbVatExemption.DataSource = vatOptions;
                    cmbVatExemption.DisplayMember = "Name";
                    cmbVatExemption.ValueMember = "Id";
                }
                catch { cmbVatExemption.Items.Clear(); cmbVatExemption.Items.AddRange(new object[] { "Κανονικό (24%)", "Απαλλαγή (0%)" }); }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης Dropdowns:\n" + ex.Message, "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void LoadCustomerData(int id)
        {
            using var connection = new SqlConnection(GetConnectionString());
            var customer = connection.QueryFirstOrDefault<Customer>("SELECT * FROM Customers WHERE Id = @Id", new { Id = id });
            if (customer == null)
            {
                MessageBox.Show("Ο πελάτης δεν βρέθηκε.", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel; this.Close(); return;
            }

            txtCompany.Text = customer.Epwnymia ?? "";
            txtFirstName.Text = customer.Onoma ?? "";
            txtLastName.Text = customer.Epitheto ?? "";
            txtAfm.Text = customer.Afm ?? "";
            cmbDoy.Text = customer.DOY ?? "";
            txtPhone.Text = customer.Tilefono ?? "";
            txtMobile.Text = customer.Kinhto ?? "";
            txtEmail.Text = customer.Email ?? "";
            txtWebsite.Text = customer.Istoselida ?? "";
            txtAddress.Text = customer.Dieuthinsi ?? "";
            txtCity.Text = customer.Poli ?? "";
            txtArea.Text = customer.Periochi ?? "";
            cmbTK.Text = customer.TK ?? "";
            cmbCountry.Text = customer.Xora ?? "";
            cmbProfession.Text = customer.Epaggelma ?? "";
            cmbClientType.Text = customer.TyposPelati ?? "";
            if (customer.VatExemptionId.HasValue) cmbVatExemption.SelectedValue = customer.VatExemptionId.Value;
            txtTags.Text = customer.Tags ?? "";
            txtNotes.Text = customer.Simeiwseis ?? "";
            chkIsActive.Checked = customer.Energo;

            // ✅ ΜΟΝΟ ΑΥΤΟ ΤΟ ΠΕΔΙΟ
            txtContractingAuthority.Text = customer.AnathetousaArchi ?? "";

            chkIsAlsoSupplier.Checked = customer.EinaiKaiPromitheutis;

            // ΦΟΡΤΩΣΗ SUB-TABS
            LoadBranches(id);
            LoadContacts(id);
            LoadActivities(id);
            LoadCredit(id);
            LoadPriceZones(id);
        }

        private void ClearAllControls()
        {
            txtCompany.Clear(); txtFirstName.Clear(); txtLastName.Clear(); txtAfm.Clear();
            txtPhone.Clear(); txtMobile.Clear(); txtEmail.Clear(); txtWebsite.Clear();
            txtAddress.Clear(); txtCity.Clear(); txtArea.Clear(); txtTags.Clear(); txtNotes.Clear();

            // ✅ Καθαρίζουμε μόνο την Αναθέτουσα Αρχή
            txtContractingAuthority.Clear();

            chkIsActive.Checked = true; chkIsAlsoSupplier.Checked = false;
            cmbDoy.SelectedIndex = -1; cmbTK.SelectedIndex = -1; cmbCountry.SelectedIndex = -1;
            cmbProfession.SelectedIndex = -1; cmbClientType.SelectedIndex = -1; cmbVatExemption.SelectedIndex = -1;
        }

        // ==========================================
        // ✅ ΥΠΟΚΑΤΑΣΤΗΜΑΤΑ (BRANCHES)
        // ==========================================
        private void LoadBranches(int customerId)
        {
            if (dgvBranches == null) return;
            try
            {
                dgvBranches.AutoGenerateColumns = false;
                dgvBranches.DataSource = null;
                dgvBranches.Columns.Clear();

                using var conn = new SqlConnection(GetConnectionString());
                var branches = conn.Query(@"
                    SELECT Id, ISNULL(BranchName, '') AS [Όνομα], ISNULL(BranchCode, '') AS [Κωδικός],
                           ISNULL(Address, '') AS [Διεύθυνση], ISNULL(City, '') AS [Πόλη],
                           ISNULL(Phone, '') AS [Τηλέφωνο], ISNULL(ManagerName, '') AS [Υπεύθυνος],
                           CASE WHEN IsMain = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Κύριο]
                    FROM CustomerBranches WHERE CustomerId = @Id ORDER BY BranchName",
                    new { Id = customerId }).ToList();

                dgvBranches.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
                dgvBranches.Columns.Add(new DataGridViewTextBoxColumn { Name = "Όνομα", DataPropertyName = "Όνομα", HeaderText = "Όνομα", Width = 150 });
                dgvBranches.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", HeaderText = "Κωδικός", Width = 100 });
                dgvBranches.Columns.Add(new DataGridViewTextBoxColumn { Name = "Διεύθυνση", DataPropertyName = "Διεύθυνση", HeaderText = "Διεύθυνση", Width = 200 });
                dgvBranches.Columns.Add(new DataGridViewTextBoxColumn { Name = "Πόλη", DataPropertyName = "Πόλη", HeaderText = "Πόλη", Width = 120 });
                dgvBranches.Columns.Add(new DataGridViewTextBoxColumn { Name = "Τηλέφωνο", DataPropertyName = "Τηλέφωνο", HeaderText = "Τηλέφωνο", Width = 120 });
                dgvBranches.Columns.Add(new DataGridViewTextBoxColumn { Name = "Υπεύθυνος", DataPropertyName = "Υπεύθυνος", HeaderText = "Υπεύθυνος", Width = 120 });
                dgvBranches.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κύριο", DataPropertyName = "Κύριο", HeaderText = "Κύριο", Width = 80 });

                dgvBranches.DataSource = branches;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα Branches: " + ex.Message); }
        }

        private void btnAddBranch_Click(object sender, EventArgs e)
        {
            if (_currentCustomerId == 0)
            {
                MessageBox.Show("⚠️ Πρέπει πρώτα να αποθηκεύσετε τα βασικά στοιχεία πατώντας '💾 Αποθήκευση'!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var form = new CustomerBranchEditForm(_currentCustomerId, 0);
            if (form.ShowDialog() == DialogResult.OK) LoadBranches(_currentCustomerId);
        }

        private void btnEditBranch_Click(object sender, EventArgs e)
        {
            if (dgvBranches == null || dgvBranches.CurrentRow == null) { MessageBox.Show("Επίλεξε υποκατάστημα!"); return; }
            var id = dgvBranches.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            using var form = new CustomerBranchEditForm(_currentCustomerId, Convert.ToInt32(id));
            if (form.ShowDialog() == DialogResult.OK) LoadBranches(_currentCustomerId);
        }

        private void btnDeleteBranch_Click(object sender, EventArgs e)
        {
            if (dgvBranches == null || dgvBranches.CurrentRow == null) { MessageBox.Show("Επίλεξε υποκατάστημα!"); return; }
            var id = dgvBranches.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            if (MessageBox.Show("Διαγραφή υποκαταστήματος;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(GetConnectionString());
                    conn.Execute("DELETE FROM CustomerBranches WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                    MessageBox.Show("✅ Διαγράφηκε!", "Επιτυχία");
                    LoadBranches(_currentCustomerId);
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        // ==========================================
        // ✅ ΕΠΑΦΕΣ (CONTACTS)
        // ==========================================
        private void LoadContacts(int customerId)
        {
            if (dgvContacts == null) return;
            try
            {
                dgvContacts.AutoGenerateColumns = false;
                dgvContacts.DataSource = null;
                dgvContacts.Columns.Clear();

                using var conn = new SqlConnection(GetConnectionString());
                var contacts = conn.Query(@"
                    SELECT Id, ISNULL(FirstName, '') AS [Όνομα], ISNULL(LastName, '') AS [Επώνυμο],
                           ISNULL(Position, '') AS [Θέση], ISNULL(Department, '') AS [Τμήμα],
                           ISNULL(Phone, '') AS [Τηλέφωνο], ISNULL(Mobile, '') AS [Κινητό],
                           ISNULL(Email, '') AS [Email],
                           CASE WHEN IsPrimary = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Κύρια]
                    FROM CustomerContacts WHERE CustomerId = @Id ORDER BY FirstName",
                    new { Id = customerId }).ToList();

                dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
                dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Όνομα", DataPropertyName = "Όνομα", HeaderText = "Όνομα", Width = 120 });
                dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Επώνυμο", DataPropertyName = "Επώνυμο", HeaderText = "Επώνυμο", Width = 120 });
                dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Θέση", DataPropertyName = "Θέση", HeaderText = "Θέση", Width = 120 });
                dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Τμήμα", DataPropertyName = "Τμήμα", HeaderText = "Τμήμα", Width = 100 });
                dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Τηλέφωνο", DataPropertyName = "Τηλέφωνο", HeaderText = "Τηλέφωνο", Width = 120 });
                dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κινητό", DataPropertyName = "Κινητό", HeaderText = "Κινητό", Width = 120 });
                dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", DataPropertyName = "Email", HeaderText = "Email", Width = 150 });
                dgvContacts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κύρια", DataPropertyName = "Κύρια", HeaderText = "Κύρια", Width = 80 });

                dgvContacts.DataSource = contacts;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα Contacts: " + ex.Message); }
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            if (_currentCustomerId == 0)
            {
                MessageBox.Show("⚠️ Πρέπει πρώτα να αποθηκεύσετε τα βασικά στοιχεία πατώντας '💾 Αποθήκευση'!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var form = new CustomerContactEditForm(_currentCustomerId, 0);
            if (form.ShowDialog() == DialogResult.OK) LoadContacts(_currentCustomerId);
        }

        private void btnEditContact_Click(object sender, EventArgs e)
        {
            if (dgvContacts == null || dgvContacts.CurrentRow == null) { MessageBox.Show("Επίλεξε επαφή!"); return; }
            var id = dgvContacts.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            using var form = new CustomerContactEditForm(_currentCustomerId, Convert.ToInt32(id));
            if (form.ShowDialog() == DialogResult.OK) LoadContacts(_currentCustomerId);
        }

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            if (dgvContacts == null || dgvContacts.CurrentRow == null) { MessageBox.Show("Επίλεξε επαφή!"); return; }
            if (MessageBox.Show("Διαγραφή επαφής;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var id = dgvContacts.CurrentRow.Cells["Id"].Value;
                if (id == null || id == DBNull.Value) return;
                try
                {
                    using var conn = new SqlConnection(GetConnectionString());
                    conn.Execute("DELETE FROM CustomerContacts WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                    MessageBox.Show("✅ Διαγράφηκε!");
                    LoadContacts(_currentCustomerId);
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        // ==========================================
        // ✅ ΔΡΑΣΤΗΡΙΟΤΗΤΕΣ (ACTIVITIES)
        // ==========================================
        private void LoadActivities(int customerId)
        {
            if (dgvActivities == null) return;
            try
            {
                dgvActivities.AutoGenerateColumns = false;
                dgvActivities.DataSource = null;
                dgvActivities.Columns.Clear();

                using var conn = new SqlConnection(GetConnectionString());
                var activities = conn.Query(@"
                    SELECT Id, ISNULL(Subject, '') AS [Θέμα], ISNULL(Description, '') AS [Περιγραφή],
                           ActivityDate AS [Ημερομηνία], ISNULL(Status, '') AS [Κατάσταση]
                    FROM CustomerActivities WHERE CustomerId = @Id ORDER BY ActivityDate DESC",
                    new { Id = customerId }).ToList();

                dgvActivities.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
                dgvActivities.Columns.Add(new DataGridViewTextBoxColumn { Name = "Θέμα", DataPropertyName = "Θέμα", HeaderText = "Θέμα", Width = 200 });
                dgvActivities.Columns.Add(new DataGridViewTextBoxColumn { Name = "Περιγραφή", DataPropertyName = "Περιγραφή", HeaderText = "Περιγραφή", Width = 300 });
                dgvActivities.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ημερομηνία", DataPropertyName = "Ημερομηνία", HeaderText = "Ημερομηνία", Width = 120 });
                dgvActivities.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κατάσταση", DataPropertyName = "Κατάσταση", HeaderText = "Κατάσταση", Width = 120 });

                dgvActivities.DataSource = activities;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα Activities: " + ex.Message); }
        }

        private void btnAddActivity_Click(object sender, EventArgs e)
        {
            if (_currentCustomerId == 0)
            {
                MessageBox.Show("⚠️ Πρέπει πρώτα να αποθηκεύσετε τα βασικά στοιχεία πατώντας '💾 Αποθήκευση'!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var form = new CustomerActivityEditForm(_currentCustomerId, 0);
            if (form.ShowDialog() == DialogResult.OK) LoadActivities(_currentCustomerId);
        }

        private void btnEditActivity_Click(object sender, EventArgs e)
        {
            if (dgvActivities == null || dgvActivities.CurrentRow == null) { MessageBox.Show("Επίλεξε δραστηριότητα!"); return; }
            var id = dgvActivities.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            using var form = new CustomerActivityEditForm(_currentCustomerId, Convert.ToInt32(id));
            if (form.ShowDialog() == DialogResult.OK) LoadActivities(_currentCustomerId);
        }

        private void btnDeleteActivity_Click(object sender, EventArgs e)
        {
            if (dgvActivities == null || dgvActivities.CurrentRow == null) { MessageBox.Show("Επίλεξε δραστηριότητα!"); return; }
            if (MessageBox.Show("Διαγραφή δραστηριότητας;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var id = dgvActivities.CurrentRow.Cells["Id"].Value;
                if (id == null || id == DBNull.Value) return;
                try
                {
                    using var conn = new SqlConnection(GetConnectionString());
                    conn.Execute("DELETE FROM CustomerActivities WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                    MessageBox.Show("✅ Διαγράφηκε!");
                    LoadActivities(_currentCustomerId);
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        // ==========================================
        // ✅ ΠΙΣΤΩΤΙΚΑ (CREDIT LIMITS)
        // ==========================================
        private void LoadCredit(int customerId)
        {
            if (dgvCredit == null) return;
            try
            {
                dgvCredit.AutoGenerateColumns = false;
                dgvCredit.DataSource = null;
                dgvCredit.Columns.Clear();

                using var conn = new SqlConnection(GetConnectionString());
                var credit = conn.Query(@"
                    SELECT Id, LimitAmount AS [Όριο], CurrentBalance AS [Υπόλοιπο],
                           ValidFrom AS [Από], ValidTo AS [Έως],
                           CASE WHEN IsBlocked = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Μπλοκαρισμένο]
                    FROM CustomerCreditLimits WHERE CustomerId = @Id",
                    new { Id = customerId }).ToList();

                dgvCredit.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
                dgvCredit.Columns.Add(new DataGridViewTextBoxColumn { Name = "Όριο", DataPropertyName = "Όριο", HeaderText = "Όριο", Width = 120 });
                dgvCredit.Columns.Add(new DataGridViewTextBoxColumn { Name = "Υπόλοιπο", DataPropertyName = "Υπόλοιπο", HeaderText = "Υπόλοιπο", Width = 120 });
                dgvCredit.Columns.Add(new DataGridViewTextBoxColumn { Name = "Από", DataPropertyName = "Από", HeaderText = "Από", Width = 120 });
                dgvCredit.Columns.Add(new DataGridViewTextBoxColumn { Name = "Έως", DataPropertyName = "Έως", HeaderText = "Έως", Width = 120 });
                dgvCredit.Columns.Add(new DataGridViewTextBoxColumn { Name = "Μπλοκαρισμένο", DataPropertyName = "Μπλοκαρισμένο", HeaderText = "Μπλοκαρισμένο", Width = 120 });

                dgvCredit.DataSource = credit;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα Credit: " + ex.Message); }
        }

        private void btnAddCredit_Click(object sender, EventArgs e)
        {
            if (_currentCustomerId == 0)
            {
                MessageBox.Show("⚠️ Πρέπει πρώτα να αποθηκεύσετε τα βασικά στοιχεία πατώντας '💾 Αποθήκευση'!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var form = new CustomerCreditLimitEditForm(_currentCustomerId, 0);
            if (form.ShowDialog() == DialogResult.OK) LoadCredit(_currentCustomerId);
        }

        private void btnEditCredit_Click(object sender, EventArgs e)
        {
            if (dgvCredit == null || dgvCredit.CurrentRow == null) { MessageBox.Show("Επίλεξε πιστωτικό όριο!"); return; }
            var id = dgvCredit.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            using var form = new CustomerCreditLimitEditForm(_currentCustomerId, Convert.ToInt32(id));
            if (form.ShowDialog() == DialogResult.OK) LoadCredit(_currentCustomerId);
        }

        private void btnDeleteCredit_Click(object sender, EventArgs e)
        {
            if (dgvCredit == null || dgvCredit.CurrentRow == null) { MessageBox.Show("Επίλεξε πιστωτικό όριο!"); return; }
            if (MessageBox.Show("Διαγραφή πιστωτικού ορίου;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var id = dgvCredit.CurrentRow.Cells["Id"].Value;
                if (id == null || id == DBNull.Value) return;
                try
                {
                    using var conn = new SqlConnection(GetConnectionString());
                    conn.Execute("DELETE FROM CustomerCreditLimits WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                    MessageBox.Show("✅ Διαγράφηκε!");
                    LoadCredit(_currentCustomerId);
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        // ==========================================
        // ✅ ΤΙΜΟΚΑΤΑΛΟΓΟΙ (PRICE ZONES)
        // ==========================================
        private void LoadPriceZones(int customerId)
        {
            if (dgvPriceZones == null) return;
            try
            {
                dgvPriceZones.AutoGenerateColumns = false;
                dgvPriceZones.DataSource = null;
                dgvPriceZones.Columns.Clear();

                using var conn = new SqlConnection(GetConnectionString());
                var zones = conn.Query(@"
                    SELECT Id, 
                           ISNULL(Code, '') AS [Κωδικός],
                           ISNULL(Name, '') AS [Όνομα],
                           ISNULL(Description, '') AS [Περιγραφή],
                           ISNULL(Currency, '€') AS [Νόμισμα],
                           ISNULL(DiscountPercentage, 0) AS [Έκπτωση %],
                           CASE WHEN IsActive = 1 THEN 'Ναι' ELSE 'Όχι' END AS [Ενεργή]
                    FROM CustomerPriceZones WHERE CustomerId = @Id ORDER BY Code",
                    new { Id = customerId }).ToList();

                dgvPriceZones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
                dgvPriceZones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", HeaderText = "Κωδικός", Width = 120 });
                dgvPriceZones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Όνομα", DataPropertyName = "Όνομα", HeaderText = "Όνομα", Width = 150 });
                dgvPriceZones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Περιγραφή", DataPropertyName = "Περιγραφή", HeaderText = "Περιγραφή", Width = 250 });
                dgvPriceZones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Νόμισμα", DataPropertyName = "Νόμισμα", HeaderText = "Νόμισμα", Width = 80 });
                dgvPriceZones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Έκπτωση %", DataPropertyName = "Έκπτωση %", HeaderText = "Έκπτωση %", Width = 100 });
                dgvPriceZones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ενεργή", DataPropertyName = "Ενεργή", HeaderText = "Ενεργή", Width = 80 });

                dgvPriceZones.DataSource = zones;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα Price Zones: " + ex.Message); }
        }

        private void btnAddPriceZone_Click(object sender, EventArgs e)
        {
            if (_currentCustomerId == 0)
            {
                MessageBox.Show("⚠️ Πρέπει πρώτα να αποθηκεύσετε τα βασικά στοιχεία πατώντας '💾 Αποθήκευση'!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var form = new CustomerPriceZoneEditForm(_currentCustomerId, 0);
            if (form.ShowDialog() == DialogResult.OK) LoadPriceZones(_currentCustomerId);
        }

        private void btnEditPriceZone_Click(object sender, EventArgs e)
        {
            if (dgvPriceZones == null || dgvPriceZones.CurrentRow == null) { MessageBox.Show("Επίλεξε τιμοκατάλογο!"); return; }
            var id = dgvPriceZones.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            using var form = new CustomerPriceZoneEditForm(_currentCustomerId, Convert.ToInt32(id));
            if (form.ShowDialog() == DialogResult.OK) LoadPriceZones(_currentCustomerId);
        }

        private void btnDeletePriceZone_Click(object sender, EventArgs e)
        {
            if (dgvPriceZones == null || dgvPriceZones.CurrentRow == null) { MessageBox.Show("Επίλεξε τιμοκατάλογο!"); return; }
            if (MessageBox.Show("Διαγραφή τιμοκατάλογου;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var id = dgvPriceZones.CurrentRow.Cells["Id"].Value;
                if (id == null || id == DBNull.Value) return;
                try
                {
                    using var conn = new SqlConnection(GetConnectionString());
                    conn.Execute("DELETE FROM CustomerPriceZones WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                    MessageBox.Show("✅ Διαγράφηκε!");
                    LoadPriceZones(_currentCustomerId);
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        // ==========================================
        // ✅ SAVE BUTTON (CLEANED B2G)
        // ==========================================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCompany.Text) && string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("⚠️ Συμπλήρωσε τουλάχιστον την 'Επωνυμία' ή το 'Όνομα'!", "Επικύρωση", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCompany.Focus();
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                using var connection = new SqlConnection(GetConnectionString());
                connection.Open();

                if (_customerId.HasValue && _customerId.Value > 0)
                {
                    // UPDATE
                    connection.Execute(@"UPDATE Customers SET 
                        Epwnymia=@Epwnymia, Onoma=@Onoma, Epitheto=@Epitheto, Afm=@Afm, DOY=@DOY, 
                        Tilefono=@Tilefono, Kinhto=@Kinhto, Email=@Email, Istoselida=@Istoselida, 
                        Dieuthinsi=@Dieuthinsi, Poli=@Poli, Periochi=@Periochi, TK=@TK, Xora=@Xora, 
                        Epaggelma=@Epaggelma, TyposPelati=@TyposPelati, VatExemptionId=@VatExemptionId, 
                        Tags=@Tags, Simeiwseis=@Simeiwseis, Energo=@Energo, 
                        AnathetousaArchi=@AnathetousaArchi, 
                        EinaiKaiPromitheutis=@EinaiKaiPromitheutis, UpdatedAt=GETDATE() 
                        WHERE Id=@Id", new
                    {
                        Id = _customerId.Value,
                        Epwnymia = string.IsNullOrWhiteSpace(txtCompany.Text) ? (object)DBNull.Value : txtCompany.Text.Trim(),
                        Onoma = string.IsNullOrWhiteSpace(txtFirstName.Text) ? (object)DBNull.Value : txtFirstName.Text.Trim(),
                        Epitheto = string.IsNullOrWhiteSpace(txtLastName.Text) ? (object)DBNull.Value : txtLastName.Text.Trim(),
                        Afm = string.IsNullOrWhiteSpace(txtAfm.Text) ? (object)DBNull.Value : txtAfm.Text.Trim(),
                        DOY = string.IsNullOrWhiteSpace(cmbDoy.Text) ? (object)DBNull.Value : cmbDoy.Text.Trim(),
                        Tilefono = string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim(),
                        Kinhto = string.IsNullOrWhiteSpace(txtMobile.Text) ? (object)DBNull.Value : txtMobile.Text.Trim(),
                        Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim(),
                        Istoselida = string.IsNullOrWhiteSpace(txtWebsite.Text) ? (object)DBNull.Value : txtWebsite.Text.Trim(),
                        Dieuthinsi = string.IsNullOrWhiteSpace(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text.Trim(),
                        Poli = string.IsNullOrWhiteSpace(txtCity.Text) ? (object)DBNull.Value : txtCity.Text.Trim(),
                        Periochi = string.IsNullOrWhiteSpace(txtArea.Text) ? (object)DBNull.Value : txtArea.Text.Trim(),
                        TK = string.IsNullOrWhiteSpace(cmbTK.Text) ? (object)DBNull.Value : cmbTK.Text.Trim(),
                        Xora = string.IsNullOrWhiteSpace(cmbCountry.Text) ? (object)DBNull.Value : cmbCountry.Text.Trim(),
                        Epaggelma = string.IsNullOrWhiteSpace(cmbProfession.Text) ? (object)DBNull.Value : cmbProfession.Text.Trim(),
                        TyposPelati = string.IsNullOrWhiteSpace(cmbClientType.Text) ? (object)DBNull.Value : cmbClientType.Text.Trim(),
                        VatExemptionId = cmbVatExemption.SelectedValue as int?,
                        Tags = string.IsNullOrWhiteSpace(txtTags.Text) ? (object)DBNull.Value : txtTags.Text.Trim(),
                        Simeiwseis = string.IsNullOrWhiteSpace(txtNotes.Text) ? (object)DBNull.Value : txtNotes.Text.Trim(),
                        Energo = chkIsActive.Checked,

                        // ✅ ΜΟΝΟ ΑΥΤΟ
                        AnathetousaArchi = string.IsNullOrWhiteSpace(txtContractingAuthority.Text) ? (object)DBNull.Value : txtContractingAuthority.Text.Trim(),

                        EinaiKaiPromitheutis = chkIsAlsoSupplier.Checked
                    });

                    MessageBox.Show("✅ Ο πελάτης ενημερώθηκε επιτυχώς!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // INSERT
                    _currentCustomerId = connection.ExecuteScalar<int>(@"
                        INSERT INTO Customers (Epwnymia, Onoma, Epitheto, Afm, DOY, Tilefono, Kinhto, Email, Istoselida, 
                        Dieuthinsi, Poli, Periochi, TK, Xora, Epaggelma, TyposPelati, VatExemptionId, Tags, Simeiwseis, Energo, 
                        AnathetousaArchi, EinaiKaiPromitheutis, CreatedAt, UpdatedAt)
                        VALUES (
                            NULLIF(@Epwnymia, ''), NULLIF(@Onoma, ''), NULLIF(@Epitheto, ''), NULLIF(@Afm, ''), NULLIF(@DOY, ''), 
                            NULLIF(@Tilefono, ''), NULLIF(@Kinhto, ''), NULLIF(@Email, ''), NULLIF(@Istoselida, ''), 
                            NULLIF(@Dieuthinsi, ''), NULLIF(@Poli, ''), NULLIF(@Periochi, ''), NULLIF(@TK, ''), NULLIF(@Xora, ''), 
                            NULLIF(@Epaggelma, ''), NULLIF(@TyposPelati, ''), @VatExemptionId, NULLIF(@Tags, ''), NULLIF(@Simeiwseis, ''), @Energo, 
                            NULLIF(@AnathetousaArchi, ''), @EinaiKaiPromitheutis, GETDATE(), GETDATE());
                        SELECT CAST(SCOPE_IDENTITY() as int);", new
                    {
                        Epwnymia = txtCompany.Text.Trim(),
                        Onoma = txtFirstName.Text.Trim(),
                        Epitheto = txtLastName.Text.Trim(),
                        Afm = txtAfm.Text.Trim(),
                        DOY = cmbDoy.Text.Trim(),
                        Tilefono = txtPhone.Text.Trim(),
                        Kinhto = txtMobile.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Istoselida = txtWebsite.Text.Trim(),
                        Dieuthinsi = txtAddress.Text.Trim(),
                        Poli = txtCity.Text.Trim(),
                        Periochi = txtArea.Text.Trim(),
                        TK = cmbTK.Text.Trim(),
                        Xora = cmbCountry.Text.Trim(),
                        Epaggelma = cmbProfession.Text.Trim(),
                        TyposPelati = cmbClientType.Text.Trim(),
                        VatExemptionId = cmbVatExemption.SelectedValue as int?,
                        Tags = txtTags.Text.Trim(),
                        Simeiwseis = txtNotes.Text.Trim(),
                        Energo = chkIsActive.Checked,

                        // ✅ ΜΟΝΟ ΑΥΤΟ
                        AnathetousaArchi = txtContractingAuthority.Text.Trim(),

                        EinaiKaiPromitheutis = chkIsAlsoSupplier.Checked
                    });

                    MessageBox.Show($"✅ Ο πελάτης δημιουργήθηκε επιτυχώς με ID: {_currentCustomerId}!", "Επιτυχία", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα κατά την αποθήκευση:\n" + ex.Message, "Σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void chkBlocked_CheckedChanged(object sender, EventArgs e) { }

        private void btnSearchAfm_Click(object sender, EventArgs e)
        {
            string afm = txtAfm.Text.Trim();
            if (string.IsNullOrWhiteSpace(afm)) { MessageBox.Show("Εισάγετε ΑΦΜ."); return; }
            if (afm.Length == 9 && afm.All(char.IsDigit))
            {
                int sum = 0; for (int i = 0; i < 8; i++) sum += (afm[i] - '0') * (1 << (8 - i));
                bool isValid = (sum % 11 % 10) == (afm[8] - '0');
                MessageBox.Show(isValid ? "✅ Έγκυρο ΑΦΜ" : "⚠️ Λανθασμένος έλεγχος ΑΦΜ", "ΑΦΜ", MessageBoxButtons.OK, isValid ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            else MessageBox.Show("❌ Το ΑΦΜ πρέπει να έχει 9 ψηφία.");
        }

        private void btnEditTK_Click(object sender, EventArgs e) { string v = Microsoft.VisualBasic.Interaction.InputBox("Νέο Τ.Κ.:", "Τ.Κ.", cmbTK.Text); if (!string.IsNullOrWhiteSpace(v)) cmbTK.Text = v.Trim(); }
        private void btnEditProfession_Click(object sender, EventArgs e) { string v = Microsoft.VisualBasic.Interaction.InputBox("Νέο Επάγγελμα:", "Επάγγελμα", cmbProfession.Text); if (!string.IsNullOrWhiteSpace(v)) cmbProfession.Text = v.Trim(); }
        private void btnEditDoy_Click(object sender, EventArgs e) { string v = Microsoft.VisualBasic.Interaction.InputBox("Νέα ΔΟΥ:", "ΔΟΥ", cmbDoy.Text); if (!string.IsNullOrWhiteSpace(v)) cmbDoy.Text = v.Trim(); }
        private void btnEditCountry_Click(object sender, EventArgs e) { string v = Microsoft.VisualBasic.Interaction.InputBox("Νέα Χώρα:", "Χώρα", cmbCountry.Text); if (!string.IsNullOrWhiteSpace(v)) cmbCountry.Text = v.Trim(); }
        private void btnEditClientType_Click(object sender, EventArgs e) { string v = Microsoft.VisualBasic.Interaction.InputBox("Νέος Τύπος:", "Τύπος", cmbClientType?.Text ?? ""); if (!string.IsNullOrWhiteSpace(v) && cmbClientType != null) cmbClientType.Text = v.Trim(); }
    }
}