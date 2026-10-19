using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemSerialNumbersForm : Form
    {
        private readonly int _itemId;
        private readonly string _connStr;
        private int _editingId = 0;
        private List<dynamic> _allData = new List<dynamic>();
        private List<dynamic> _filteredData = new List<dynamic>();

        public ItemSerialNumbersForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemSerialNumbersForm_Load;
        }

        private void ItemSerialNumbersForm_Load(object sender, EventArgs e)
        {
            cmbStatus.Items.AddRange(new object[] { "Διαθέσιμο", "Πωλημένο", "Σε Επισκευή", "Απενεργοποιημένο" });
            cmbStatus.SelectedIndex = 0;
            LoadLots();
            LoadSerials();
        }

        private void LoadLots()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var lots = conn.Query(@"
                    SELECT Id, ISNULL(LotNumber, 'Χωρίς Κωδικό') AS LotNumber
                    FROM Lots WHERE ItemId = @Id ORDER BY LotNumber",
                    new { Id = _itemId }).ToList();

                var lotList = new List<LotItem>();
                lotList.Add(new LotItem { Id = 0, DisplayName = "-- Χωρίς Παρτίδα --" });

                foreach (var lot in lots)
                    lotList.Add(new LotItem { Id = lot.Id, DisplayName = lot.LotNumber });

                cmbLotId.DisplayMember = "DisplayName";
                cmbLotId.ValueMember = "Id";
                cmbLotId.DataSource = lotList;
                cmbLotId.SelectedIndex = 0;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα φόρτωσης παρτίδων: " + ex.Message); }
        }

        private void LoadSerials()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                _allData = conn.Query(@"
                    SELECT Id, SerialNumber AS [Σειριακός], LotId AS [Παρτίδα],
                           WarrantyStart AS [Εγγύηση Από], WarrantyEnd AS [Εγγύηση Έως],
                           Status AS [Κατάσταση], Notes AS [Σημειώσεις]
                    FROM SerialNumbers WHERE ItemId = @Id
                    ORDER BY SerialNumber",
                    new { Id = _itemId }).ToList();

                _filteredData = _allData.ToList();
                ApplyFilter();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        // ✅ ΦΙΛΤΡΑΡΙΣΜΑ & ΑΝΑΖΗΤΗΣΗ
        private void ApplyFilter()
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            var filtered = _allData.AsEnumerable();

            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(r =>
                    r.Σειριακός?.ToString().ToLower().Contains(searchText) == true ||
                    r.Παρτίδα?.ToString().Contains(searchText) == true ||
                    r.Κατάσταση?.ToString().ToLower().Contains(searchText) == true ||
                    r.Σημειώσεις?.ToString().ToLower().Contains(searchText) == true
                );
            }

            _filteredData = filtered.ToList();
            bindingSource.DataSource = _filteredData;
            dgvSerials.DataSource = bindingSource;

            UpdateRecordInfo();
        }

        private void UpdateRecordInfo()
        {
            int currentPosition = bindingSource.Position + 1;
            int totalCount = _filteredData.Count;
            lblRecordInfo.Text = $"{currentPosition} / {totalCount}";
        }

        // ✅ ΑΝΑΖΗΤΗΣΗ
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ApplyFilter();
                e.Handled = true;
            }
        }

        // ✅ ΠΛΟΗΓΗΣΗ
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (bindingSource.Count > 0)
            {
                bindingSource.MoveFirst();
                UpdateRecordInfo();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (bindingSource.Position > 0)
            {
                bindingSource.MovePrevious();
                UpdateRecordInfo();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (bindingSource.Position < bindingSource.Count - 1)
            {
                bindingSource.MoveNext();
                UpdateRecordInfo();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (bindingSource.Count > 0)
            {
                bindingSource.MoveLast();
                UpdateRecordInfo();
            }
        }

        private void dgvSerials_SelectionChanged(object sender, EventArgs e)
        {
            UpdateRecordInfo();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSerialNumber.Text))
            {
                MessageBox.Show("⚠️ Ο Σειριακός Αριθμός είναι υποχρεωτικός!", "Προσοχή");
                txtSerialNumber.Focus();
                return;
            }

            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();

                int? lotId = cmbLotId.SelectedValue != null && Convert.ToInt32(cmbLotId.SelectedValue) > 0
                    ? Convert.ToInt32(cmbLotId.SelectedValue)
                    : (int?)null;

                DateTime? warrantyStart = dtpWarrantyStart.Checked ? dtpWarrantyStart.Value : (DateTime?)null;
                DateTime? warrantyEnd = dtpWarrantyEnd.Checked ? dtpWarrantyEnd.Value : (DateTime?)null;

                if (_editingId > 0)
                {
                    conn.Execute(@"UPDATE SerialNumbers SET 
                        SerialNumber=@Serial, LotId=@Lot, 
                        WarrantyStart=@WarrantyStart, WarrantyEnd=@WarrantyEnd,
                        Status=@Status, Notes=@Notes, UpdatedAt=GETDATE()
                        WHERE Id=@Id AND ItemId=@ItemId", new
                    {
                        Id = _editingId,
                        ItemId = _itemId,
                        Serial = txtSerialNumber.Text.Trim(),
                        Lot = lotId,
                        WarrantyStart = warrantyStart,
                        WarrantyEnd = warrantyEnd,
                        Status = cmbStatus.Text.Trim(),
                        Notes = txtNotes.Text.Trim()
                    });
                    MessageBox.Show("✅ Ενημερώθηκε επιτυχώς!", "Επιτυχία");
                }
                else
                {
                    conn.Execute(@"INSERT INTO SerialNumbers (ItemId, SerialNumber, LotId, 
                        WarrantyStart, WarrantyEnd, Status, Notes, CreatedAt) 
                        VALUES (@ItemId, @Serial, @Lot, @WarrantyStart, @WarrantyEnd, @Status, @Notes, GETDATE())", new
                    {
                        ItemId = _itemId,
                        Serial = txtSerialNumber.Text.Trim(),
                        Lot = lotId,
                        WarrantyStart = warrantyStart,
                        WarrantyEnd = warrantyEnd,
                        Status = cmbStatus.Text.Trim(),
                        Notes = txtNotes.Text.Trim()
                    });
                    MessageBox.Show("✅ Προστέθηκε επιτυχώς!", "Επιτυχία");
                }

                ClearFields();
                LoadSerials();
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSerials.CurrentRow == null)
            {
                MessageBox.Show("⚠️ Επίλεξε μια εγγραφή από τη λίστα!", "Προσοχή");
                return;
            }

            var row = dgvSerials.CurrentRow;
            _editingId = Convert.ToInt32(row.Cells["Id"].Value);

            txtSerialNumber.Text = row.Cells["Σειριακός"].Value?.ToString() ?? "";
            txtNotes.Text = row.Cells["Σημειώσεις"].Value?.ToString() ?? "";

            var lotId = row.Cells["Παρτίδα"].Value;
            if (lotId != null && lotId != DBNull.Value)
                cmbLotId.SelectedValue = Convert.ToInt32(lotId);
            else
                cmbLotId.SelectedIndex = 0;

            var warrantyStart = row.Cells["Εγγύηση Από"].Value;
            var warrantyEnd = row.Cells["Εγγύηση Έως"].Value;

            if (warrantyStart != null && warrantyStart != DBNull.Value)
            {
                dtpWarrantyStart.Value = Convert.ToDateTime(warrantyStart);
                dtpWarrantyStart.Checked = true;
            }
            else
                dtpWarrantyStart.Checked = false;

            if (warrantyEnd != null && warrantyEnd != DBNull.Value)
            {
                dtpWarrantyEnd.Value = Convert.ToDateTime(warrantyEnd);
                dtpWarrantyEnd.Checked = true;
            }
            else
                dtpWarrantyEnd.Checked = false;

            var status = row.Cells["Κατάσταση"].Value?.ToString() ?? "";
            if (cmbStatus.Items.Contains(status))
                cmbStatus.SelectedItem = status;

            btnAdd.Text = "💾 Αποθήκευση";
            btnCancel.Visible = true;

            txtSerialNumber.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            _editingId = 0;
            txtSerialNumber.Clear();
            txtNotes.Clear();
            cmbLotId.SelectedIndex = 0;
            dtpWarrantyStart.Checked = false;
            dtpWarrantyEnd.Checked = false;
            cmbStatus.SelectedIndex = 0;
            btnAdd.Text = " Προσθήκη";
            btnCancel.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSerials.CurrentRow == null)
            {
                MessageBox.Show("⚠️ Επίλεξε μια εγγραφή!", "Προσοχή");
                return;
            }
            var id = dgvSerials.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;

            if (MessageBox.Show("Είσαι σίγουρος ότι θέλεις να διαγράψεις αυτόν τον σειριακό αριθμό;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(_connStr);
                    conn.Execute("DELETE FROM SerialNumbers WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                    MessageBox.Show("✅ Διαγράφηκε!");
                    ClearFields();
                    LoadSerials();
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void dgvSerials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEdit_Click(sender, EventArgs.Empty);
        }
    }

    public class LotItem
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
    }
}