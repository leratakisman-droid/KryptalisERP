using Dapper;
using KryptalisERP.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class SupplierEditForm : Form
    {
        private int _supplierId;
        private readonly string _connStr;

        private List<SupplierContact> _contacts = new();
        private List<SupplierBankAccount> _banks = new();
        private List<SupplierPaymentTerm> _payments = new();
        private List<SupplierItem> _items = new();
        private List<SupplierPriceList> _priceLists = new();

        public SupplierEditForm(int id = 0)
        {
            InitializeComponent();
            _supplierId = id;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += SupplierEditForm_Load;
        }

        private void SupplierEditForm_Load(object sender, EventArgs e)
        {
            this.Text = _supplierId > 0 ? "✏️ Επεξεργασία Προμηθευτή" : "➕ Νέος Προμηθευτής";
            SetupGrids();
            LoadLookups();
            LoadBanks();
            LoadMainData();
            LoadAllTabs();
        }

        private void LoadLookups()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);

                try
                {
                    var doys = conn.Query("SELECT Id, Name FROM GreekTaxOffices WHERE IsActive = 1 ORDER BY Name").ToList();
                    cmbDoy.DataSource = doys;
                    cmbDoy.DisplayMember = "Name";
                    cmbDoy.ValueMember = "Id";
                }
                catch { cmbDoy.Items.Clear(); cmbDoy.Items.AddRange(new[] { "Α ΑΘΗΝΩΝ", "Δ΄ ΑΘΗΝΩΝ", "ΚΕ.ΦΟ.ΔΕ ΑΤΤΙΚΗΣ" }); }
                cmbDoy.DropDownStyle = ComboBoxStyle.DropDown;

                try
                {
                    var countries = conn.Query("SELECT Id, CountryName FROM Countries ORDER BY CountryName").ToList();
                    cmbCountry.DataSource = countries;
                    cmbCountry.DisplayMember = "CountryName";
                    cmbCountry.ValueMember = "Id";
                }
                catch { cmbCountry.Items.Clear(); cmbCountry.Items.Add("Ελλάδα"); }

                try
                {
                    var types = conn.Query("SELECT Id, TypeName FROM SupplierTypes ORDER BY TypeName").ToList();
                    cmbSupplierType.DataSource = types;
                    cmbSupplierType.DisplayMember = "TypeName";
                    cmbSupplierType.ValueMember = "Id";
                }
                catch { cmbSupplierType.Items.Clear(); }

                try
                {
                    var professions = conn.Query("SELECT Id, ProfessionName FROM Professions ORDER BY ProfessionName").ToList();
                    cmbProfession.DataSource = professions;
                    cmbProfession.DisplayMember = "ProfessionName";
                    cmbProfession.ValueMember = "Id";
                }
                catch { cmbProfession.Items.Clear(); }
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message); }
        }

        private void LoadBanks()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var banks = conn.Query("SELECT Id, Name FROM Bank ORDER BY Name").ToList();
                cmbBank.DataSource = banks.ToList(); cmbBank.DisplayMember = "Name"; cmbBank.ValueMember = "Id";
                cmbBank2.DataSource = banks.ToList(); cmbBank2.DisplayMember = "Name"; cmbBank2.ValueMember = "Id";
                cmbBank3.DataSource = banks.ToList(); cmbBank3.DisplayMember = "Name"; cmbBank3.ValueMember = "Id";
            }
            catch
            {
                string[] bankNames = { "Alpha Bank", "Eurobank", "National Bank", "Piraeus Bank" };
                cmbBank.Items.Clear(); cmbBank.Items.AddRange(bankNames);
                cmbBank2.Items.Clear(); cmbBank2.Items.AddRange(bankNames);
                cmbBank3.Items.Clear(); cmbBank3.Items.AddRange(bankNames);
            }
        }

        // ✅ ΧΩΡΙΣ αυτόματη δημιουργία - ΜΟΝΟ με manual αποθήκευση
        private bool EnsureSupplierSaved()
        {
            if (_supplierId > 0) return true;

            MessageBox.Show("⚠️ Πρέπει πρώτα να πατήσεις 'Αποθήκευση' για να δημιουργηθεί ο προμηθευτής!",
                "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void SetupGrids()
        {
            var grids = new[] { dgvContacts, dgvBanks, dgvPayments, dgvItems, dgvPriceLists, dgvBranches };
            foreach (var dgv in grids)
            {
                if (dgv != null)
                {
                    dgv.AutoGenerateColumns = true;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.MultiSelect = false;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.ReadOnly = true;
                }
            }
        }

        private void LoadMainData()
        {
            if (_supplierId == 0) return;
            using var conn = new SqlConnection(_connStr);
            var s = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Suppliers WHERE Id = @Id", new { Id = _supplierId });
            if (s == null) return;

            txtSupplierName.Text = s.SupplierName;
            txtContactPerson.Text = s.ContactPerson;
            txtAfm.Text = s.Afm;

            if (!string.IsNullOrEmpty(s.TaxOffice)) cmbDoy.Text = s.TaxOffice;
            if (!string.IsNullOrEmpty(s.Profession)) cmbProfession.Text = s.Profession;

            txtPhone.Text = s.Phone;
            txtPhone2.Text = s.Phone2;
            txtPhone3.Text = s.Phone3;
            txtIban1.Text = s.Iban1;
            txtIban2.Text = s.Iban2;
            txtIban3.Text = s.Iban3;
            txtEmail.Text = s.Email;
            txtAddress.Text = s.Address;
            txtCity.Text = s.City;
            txtArea.Text = s.Area;

            if (!string.IsNullOrEmpty(s.Country)) cmbCountry.Text = s.Country;

            try
            {
                if (s.BankId != null && cmbBank.DataSource != null) cmbBank.SelectedValue = s.BankId;
                if (s.BankId2 != null && cmbBank2.DataSource != null) cmbBank2.SelectedValue = s.BankId2;
                if (s.BankId3 != null && cmbBank3.DataSource != null) cmbBank3.SelectedValue = s.BankId3;
            }
            catch { }

            if (s.SupplierTypeId != null) cmbSupplierType.SelectedValue = s.SupplierTypeId;
            txtNotes.Text = s.Notes;
            chkIsActive.Checked = s.IsActive;
        }

        private void LoadAllTabs()
        {
            if (_supplierId == 0) { ClearGrids(); return; }
            using var conn = new SqlConnection(_connStr);

            LoadBranches();

            _contacts = conn.Query<SupplierContact>("SELECT * FROM SupplierContacts WHERE SupplierId = @Id", new { Id = _supplierId }).ToList();
            dgvContacts.DataSource = _contacts;
            SetHeaders(dgvContacts, new[] { "FirstName", "LastName", "Position", "Department", "Phone", "Mobile", "Email", "IsPrimary", "IsActive" },
                                   new[] { "Όνομα", "Επώνυμο", "Θέση", "Τμήμα", "Τηλέφωνο", "Κινητό", "Email", "Κύρια", "Ενεργή" });

            _banks = conn.Query<SupplierBankAccount>("SELECT * FROM SupplierBankAccounts WHERE SupplierId = @Id", new { Id = _supplierId }).ToList();
            dgvBanks.DataSource = _banks;
            SetHeaders(dgvBanks, new[] { "AccountNumber", "IBAN", "SWIFT", "Currency", "IsDefault", "IsActive", "Notes" },
                                new[] { "Αρ. Λογ.", "IBAN", "SWIFT", "Νόμισμα", "Προεπιλεγμένος", "Ενεργός", "Σημειώσεις" });

            _payments = conn.Query<SupplierPaymentTerm>("SELECT * FROM SupplierPaymentTerms WHERE SupplierId = @Id", new { Id = _supplierId }).ToList();
            dgvPayments.DataSource = _payments;
            SetHeaders(dgvPayments, new[] { "PaymentTermId", "IsDefault", "Notes" },
                                   new[] { "Όρος Πληρωμής", "Προεπιλεγμένος", "Σημειώσεις" });

            _items = conn.Query<SupplierItem>("SELECT * FROM SupplierItems WHERE SupplierId = @Id", new { Id = _supplierId }).ToList();
            dgvItems.DataSource = _items;
            SetHeaders(dgvItems, new[] { "ItemId", "SupplierCode", "SupplierDescription", "PurchasePrice", "Currency", "MinOrderQty", "LeadTime", "IsDefaultSupplier", "IsActive" },
                                 new[] { "Κωδ. Είδους", "Κωδ. Προμ.", "Περιγραφή", "Τιμή Αγοράς", "Νόμισμα", "Ελάχ. Ποσ.", "Χρόνος Παρ.", "Βασικός Προμ.", "Ενεργό" });

            _priceLists = conn.Query<SupplierPriceList>("SELECT * FROM SupplierPriceLists WHERE SupplierId = @Id", new { Id = _supplierId }).ToList();
            dgvPriceLists.DataSource = _priceLists;
            SetHeaders(dgvPriceLists, new[] { "Code", "Name", "Description", "Currency", "ValidFrom", "ValidTo", "IsActive" },
                                     new[] { "Κωδικός", "Όνομα", "Περιγραφή", "Νόμισμα", "Ισχύς Από", "Ισχύς Έως", "Ενεργή" });

            HideSystemColumns();
        }

        private void LoadBranches()
        {
            if (_supplierId == 0) { dgvBranches.DataSource = null; return; }
            using var conn = new SqlConnection(_connStr);
            var branches = conn.Query(@"
                SELECT Id, BranchName AS [Όνομα], BranchCode AS [Κωδικός], Phone1 AS [Τηλέφωνο], 
                       City AS [Πόλη], TaxOffice AS [ΔΟΥ], IsActive AS [Ενεργό]
                FROM SupplierBranches WHERE SupplierId = @Id ORDER BY BranchName",
                new { Id = _supplierId }).ToList();
            dgvBranches.DataSource = branches;
            if (dgvBranches.Columns.Contains("Id")) dgvBranches.Columns["Id"].Visible = false;
        }

        private void SetHeaders(DataGridView dgv, string[] dbCols, string[] grCols)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                int idx = Array.IndexOf(dbCols, col.Name);
                if (idx >= 0) col.HeaderText = grCols[idx];
            }
        }

        private void HideSystemColumns()
        {
            foreach (DataGridView dgv in new[] { dgvContacts, dgvBanks, dgvPayments, dgvItems, dgvPriceLists, dgvBranches })
            {
                if (dgv == null) continue;
                foreach (string col in new[] { "Id", "SupplierId" })
                    if (dgv.Columns.Contains(col)) dgv.Columns[col].Visible = false;
            }
        }

        private void ClearGrids()
        {
            dgvContacts.DataSource = null; dgvBanks.DataSource = null;
            dgvPayments.DataSource = null; dgvItems.DataSource = null;
            dgvPriceLists.DataSource = null; dgvBranches.DataSource = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("⚠️ Η Επωνυμία είναι υποχρεωτική!");
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();
                using var tran = conn.BeginTransaction();
                int savedId = _supplierId;

                object bankId = cmbBank.SelectedValue ?? DBNull.Value;
                object bankId2 = cmbBank2.SelectedValue ?? DBNull.Value;
                object bankId3 = cmbBank3.SelectedValue ?? DBNull.Value;

                if (_supplierId > 0)
                {
                    conn.Execute(@"UPDATE Suppliers SET 
                        SupplierName=@SupplierName, ContactPerson=@ContactPerson, Afm=@Afm, 
                        TaxOffice=@TaxOffice, Profession=@Profession, 
                        Phone=@Phone, Phone2=@Phone2, Phone3=@Phone3, 
                        Iban1=@Iban1, Iban2=@Iban2, Iban3=@Iban3, 
                        Email=@Email, Address=@Address, City=@City, Area=@Area, Country=@Country, 
                        BankId=@BankId, BankId2=@BankId2, BankId3=@BankId3,
                        SupplierTypeId=@SupplierTypeId, Notes=@Notes, IsActive=@IsActive, UpdatedAt=GETDATE() 
                        WHERE Id=@Id",
                        new
                        {
                            Id = _supplierId,
                            SupplierName = txtSupplierName.Text.Trim(),
                            ContactPerson = txtContactPerson.Text.Trim(),
                            Afm = txtAfm.Text.Trim(),
                            TaxOffice = cmbDoy.Text.Trim(),
                            Profession = cmbProfession.Text.Trim(),
                            Phone = txtPhone.Text.Trim(),
                            Phone2 = txtPhone2.Text.Trim(),
                            Phone3 = txtPhone3.Text.Trim(),
                            Iban1 = txtIban1.Text.Trim(),
                            Iban2 = txtIban2.Text.Trim(),
                            Iban3 = txtIban3.Text.Trim(),
                            Email = txtEmail.Text.Trim(),
                            Address = txtAddress.Text.Trim(),
                            City = txtCity.Text.Trim(),
                            Area = txtArea.Text.Trim(),
                            Country = cmbCountry.Text.Trim(),
                            BankId = bankId,
                            BankId2 = bankId2,
                            BankId3 = bankId3,
                            SupplierTypeId = cmbSupplierType.SelectedValue,
                            Notes = txtNotes.Text.Trim(),
                            IsActive = chkIsActive.Checked
                        }, tran);
                }
                else
                {
                    // ✅ ΕΛΕΓΧΟΣ - Μην δημιουργεί "Νέος Προμηθευτής" αν είναι κενό
                    if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
                    {
                        MessageBox.Show("⚠️ Η Επωνυμία είναι υποχρεωτική!", "Προσοχή", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    savedId = conn.ExecuteScalar<int>(@"INSERT INTO Suppliers 
        (SupplierName, ContactPerson, Afm, TaxOffice, Profession, 
         Phone, Phone2, Phone3, Iban1, Iban2, Iban3, 
         Email, Address, City, Area, Country, 
         BankId, BankId2, BankId3, SupplierTypeId, Notes, IsActive, CreatedAt, UpdatedAt)
        VALUES 
        (@SupplierName, @ContactPerson, @Afm, @TaxOffice, @Profession, 
         @Phone, @Phone2, @Phone3, @Iban1, @Iban2, @Iban3, 
         @Email, @Address, @City, @Area, @Country, 
         @BankId, @BankId2, @BankId3, @SupplierTypeId, @Notes, @IsActive, GETDATE(), GETDATE()); 
        SELECT CAST(SCOPE_IDENTITY() as int);",
                        new
                        {
                            SupplierName = txtSupplierName.Text.Trim(),
                            ContactPerson = txtContactPerson.Text.Trim(),
                            Afm = txtAfm.Text.Trim(),
                            TaxOffice = cmbDoy.Text.Trim(),
                            Profession = cmbProfession.Text.Trim(),
                            Phone = txtPhone.Text.Trim(),
                            Phone2 = txtPhone2.Text.Trim(),
                            Phone3 = txtPhone3.Text.Trim(),
                            Iban1 = txtIban1.Text.Trim(),
                            Iban2 = txtIban2.Text.Trim(),
                            Iban3 = txtIban3.Text.Trim(),
                            Email = txtEmail.Text.Trim(),
                            Address = txtAddress.Text.Trim(),
                            City = txtCity.Text.Trim(),
                            Area = txtArea.Text.Trim(),
                            Country = cmbCountry.Text.Trim(),
                            BankId = cmbBank.SelectedValue ?? DBNull.Value,
                            BankId2 = cmbBank2.SelectedValue ?? DBNull.Value,
                            BankId3 = cmbBank3.SelectedValue ?? DBNull.Value,
                            SupplierTypeId = cmbSupplierType.SelectedValue,
                            Notes = txtNotes.Text.Trim(),
                            IsActive = chkIsActive.Checked
                        }, tran);
                }

                SaveContacts(conn, tran, savedId, _contacts);
                SaveBanks(conn, tran, savedId, _banks);
                SavePayments(conn, tran, savedId, _payments);
                SaveItems(conn, tran, savedId, _items);
                SavePriceLists(conn, tran, savedId, _priceLists);

                tran.Commit();
                _supplierId = savedId;
                MessageBox.Show("✅ Αποθηκεύτηκε!", "Επιτυχία");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void SaveContacts(SqlConnection conn, SqlTransaction tran, int supplierId, List<SupplierContact> data)
        {
            conn.Execute("DELETE FROM SupplierContacts WHERE SupplierId = @Id", new { Id = supplierId }, tran);
            if (data.Any()) conn.Execute(@"INSERT INTO SupplierContacts (SupplierId, FirstName, LastName, Position, Department, Phone, Mobile, Email, IsPrimary, Notes, IsActive) VALUES (@SupplierId, @FirstName, @LastName, @Position, @Department, @Phone, @Mobile, @Email, @IsPrimary, @Notes, @IsActive)", data.Select(c => new { SupplierId = supplierId, c.FirstName, c.LastName, c.Position, c.Department, c.Phone, c.Mobile, c.Email, c.IsPrimary, c.Notes, c.IsActive }), tran);
        }
        private void SaveBanks(SqlConnection conn, SqlTransaction tran, int supplierId, List<SupplierBankAccount> data)
        {
            conn.Execute("DELETE FROM SupplierBankAccounts WHERE SupplierId = @Id", new { Id = supplierId }, tran);
            if (data.Any()) conn.Execute(@"INSERT INTO SupplierBankAccounts (SupplierId, BankId, AccountNumber, IBAN, SWIFT, Currency, IsDefault, IsActive, Notes) VALUES (@SupplierId, @BankId, @AccountNumber, @IBAN, @SWIFT, @Currency, @IsDefault, @IsActive, @Notes)", data.Select(b => new { SupplierId = supplierId, b.BankId, b.AccountNumber, b.IBAN, b.SWIFT, b.Currency, b.IsDefault, b.IsActive, b.Notes }), tran);
        }
        private void SavePayments(SqlConnection conn, SqlTransaction tran, int supplierId, List<SupplierPaymentTerm> data)
        {
            conn.Execute("DELETE FROM SupplierPaymentTerms WHERE SupplierId = @Id", new { Id = supplierId }, tran);
            if (data.Any()) conn.Execute(@"INSERT INTO SupplierPaymentTerms (SupplierId, PaymentTermId, IsDefault, Notes) VALUES (@SupplierId, @PaymentTermId, @IsDefault, @Notes)", data.Select(p => new { SupplierId = supplierId, p.PaymentTermId, p.IsDefault, p.Notes }), tran);
        }
        private void SaveItems(SqlConnection conn, SqlTransaction tran, int supplierId, List<SupplierItem> data)
        {
            conn.Execute("DELETE FROM SupplierItems WHERE SupplierId = @Id", new { Id = supplierId }, tran);
            if (data.Any()) conn.Execute(@"INSERT INTO SupplierItems (SupplierId, ItemId, SupplierCode, SupplierDescription, PurchasePrice, Currency, MinOrderQty, LeadTime, IsDefaultSupplier, LastPurchaseDate, LastPurchasePrice, TotalPurchases, Notes, IsActive) VALUES (@SupplierId, @ItemId, @SupplierCode, @SupplierDescription, @PurchasePrice, @Currency, @MinOrderQty, @LeadTime, @IsDefaultSupplier, @LastPurchaseDate, @LastPurchasePrice, @TotalPurchases, @Notes, @IsActive)", data.Select(i => new { SupplierId = supplierId, i.ItemId, i.SupplierCode, i.SupplierDescription, i.PurchasePrice, i.Currency, i.MinOrderQty, i.LeadTime, i.IsDefaultSupplier, i.LastPurchaseDate, i.LastPurchasePrice, i.TotalPurchases, i.Notes, i.IsActive }), tran);
        }
        private void SavePriceLists(SqlConnection conn, SqlTransaction tran, int supplierId, List<SupplierPriceList> data)
        {
            conn.Execute("DELETE FROM SupplierPriceLists WHERE SupplierId = @Id", new { Id = supplierId }, tran);
            if (data.Any()) conn.Execute(@"INSERT INTO SupplierPriceLists (SupplierId, Code, Name, Description, Currency, ValidFrom, ValidTo, IsActive) VALUES (@SupplierId, @Code, @Name, @Description, @Currency, @ValidFrom, @ValidTo, @IsActive)", data.Select(p => new { SupplierId = supplierId, p.Code, p.Name, p.Description, p.Currency, p.ValidFrom, p.ValidTo, p.IsActive }), tran);
        }

        // --- EVENTS ΚΟΥΜΠΙΩΝ ---
        private void btnAddContact_Click(object sender, EventArgs e) { if (!EnsureSupplierSaved()) return; using var form = new SupplierContactEditForm(_supplierId, 0); if (form.ShowDialog() == DialogResult.OK) LoadAllTabs(); }
        private void btnEditContact_Click(object sender, EventArgs e) { if (dgvContacts.CurrentRow == null) { MessageBox.Show("Επίλεξε μια εγγραφή!"); return; } var id = dgvContacts.CurrentRow.Cells["Id"].Value; if (id == null) return; using var form = new SupplierContactEditForm(_supplierId, Convert.ToInt32(id)); if (form.ShowDialog() == DialogResult.OK) LoadAllTabs(); }
        private void btnDeleteContact_Click(object sender, EventArgs e) { if (dgvContacts.CurrentRow == null) return; var id = dgvContacts.CurrentRow.Cells["Id"].Value; if (id == null) return; if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes) { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM SupplierContacts WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); LoadAllTabs(); } }

        private void btnAddBank_Click(object sender, EventArgs e) { if (!EnsureSupplierSaved()) return; using var form = new SupplierBankAccountEditForm(_supplierId, 0); if (form.ShowDialog() == DialogResult.OK) LoadAllTabs(); }
        private void btnEditBank_Click(object sender, EventArgs e) { if (dgvBanks.CurrentRow == null) return; var id = dgvBanks.CurrentRow.Cells["Id"].Value; if (id == null) return; using var form = new SupplierBankAccountEditForm(_supplierId, Convert.ToInt32(id)); if (form.ShowDialog() == DialogResult.OK) LoadAllTabs(); }
        private void btnDeleteBank_Click(object sender, EventArgs e) { if (dgvBanks.CurrentRow == null) return; var id = dgvBanks.CurrentRow.Cells["Id"].Value; if (id == null) return; if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes) { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM SupplierBankAccounts WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); LoadAllTabs(); } }

        private void btnAddPayment_Click(object sender, EventArgs e) { if (!EnsureSupplierSaved()) return; using var form = new SupplierPaymentTermEditForm(_supplierId, 0); if (form.ShowDialog() == DialogResult.OK) LoadAllTabs(); }
        private void btnEditPayment_Click(object sender, EventArgs e) { if (dgvPayments.CurrentRow == null) return; var id = dgvPayments.CurrentRow.Cells["Id"].Value; if (id == null) return; using var form = new SupplierPaymentTermEditForm(_supplierId, Convert.ToInt32(id)); if (form.ShowDialog() == DialogResult.OK) LoadAllTabs(); }
        private void btnDeletePayment_Click(object sender, EventArgs e) { if (dgvPayments.CurrentRow == null) return; var id = dgvPayments.CurrentRow.Cells["Id"].Value; if (id == null) return; if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes) { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM SupplierPaymentTerms WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); LoadAllTabs(); } }

        private void btnAddItem_Click(object sender, EventArgs e) { if (!EnsureSupplierSaved()) return; using var form = new SupplierItemEditForm(_supplierId, 0); if (form.ShowDialog() == DialogResult.OK) LoadAllTabs(); }
        private void btnEditItem_Click(object sender, EventArgs e) { if (dgvItems.CurrentRow == null) return; var id = dgvItems.CurrentRow.Cells["Id"].Value; if (id == null) return; using var form = new SupplierItemEditForm(_supplierId, Convert.ToInt32(id)); if (form.ShowDialog() == DialogResult.OK) LoadAllTabs(); }
        private void btnDeleteItem_Click(object sender, EventArgs e) { if (dgvItems.CurrentRow == null) return; var id = dgvItems.CurrentRow.Cells["Id"].Value; if (id == null) return; if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes) { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM SupplierItems WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); LoadAllTabs(); } }

        private void btnAddPriceList_Click(object sender, EventArgs e) { if (!EnsureSupplierSaved()) return; using var form = new SupplierPriceListEditForm(_supplierId, 0); if (form.ShowDialog() == DialogResult.OK) LoadAllTabs(); }
        private void btnEditPriceList_Click(object sender, EventArgs e) { if (dgvPriceLists.CurrentRow == null) return; var id = dgvPriceLists.CurrentRow.Cells["Id"].Value; if (id == null) return; using var form = new SupplierPriceListEditForm(_supplierId, Convert.ToInt32(id)); if (form.ShowDialog() == DialogResult.OK) LoadAllTabs(); }
        private void btnDeletePriceList_Click(object sender, EventArgs e) { if (dgvPriceLists.CurrentRow == null) return; var id = dgvPriceLists.CurrentRow.Cells["Id"].Value; if (id == null) return; if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes) { using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM SupplierPriceLists WHERE Id = @Id", new { Id = Convert.ToInt32(id) }); LoadAllTabs(); } }

        private void btnAddBranch_Click(object sender, EventArgs e) { if (!EnsureSupplierSaved()) return; using var form = new SupplierBranchEditForm(_supplierId, 0); if (form.ShowDialog() == DialogResult.OK) LoadBranches(); }
        private void btnEditBranch_Click(object sender, EventArgs e) { if (dgvBranches.CurrentRow == null) return; var rowView = dgvBranches.CurrentRow.DataBoundItem as System.Data.DataRowView; if (rowView == null) return; int branchId = Convert.ToInt32(rowView["Id"]); using var form = new SupplierBranchEditForm(_supplierId, branchId); if (form.ShowDialog() == DialogResult.OK) LoadBranches(); }
        private void btnDeleteBranch_Click(object sender, EventArgs e) { if (dgvBranches.CurrentRow == null) return; if (MessageBox.Show("Διαγραφή υποκαταστήματος;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes) { var rowView = dgvBranches.CurrentRow.DataBoundItem as System.Data.DataRowView; if (rowView == null) return; int branchId = Convert.ToInt32(rowView["Id"]); using var conn = new SqlConnection(_connStr); conn.Execute("DELETE FROM SupplierBranches WHERE Id = @Id", new { Id = branchId }); LoadBranches(); } }

        private void btnValidateAfm_Click(object sender, EventArgs e)
        {
            string afm = txtAfm.Text.Trim();
            if (afm.Length != 9 || !afm.All(char.IsDigit)) { MessageBox.Show("⚠️ Το ΑΦΜ πρέπει να έχει 9 ψηφία!"); txtAfm.Focus(); return; }
            int sum = 0; for (int i = 0; i < 8; i++) sum += int.Parse(afm[i].ToString()) * (int)Math.Pow(2, 8 - i);
            int remainder = sum % 11; int checkDigit = remainder == 0 ? 0 : 11 - remainder;
            if (checkDigit == int.Parse(afm[8].ToString())) MessageBox.Show("✅ Το ΑΦΜ είναι έγκυρο!");
            else MessageBox.Show("❌ Το ΑΦΜ δεν είναι έγκυρο!");
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}