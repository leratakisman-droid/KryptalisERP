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
    public partial class ItemSearchForm : Form
    {
        private readonly string _connStr;

        // ✅ Προσθήκη των properties που ζητούνται από τα άλλα forms
        public dynamic SelectedItem { get; private set; }
        public List<dynamic> SelectedItems { get; private set; }

        public ItemSearchForm()
        {
            InitializeComponent();
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            this.Load += ItemSearchForm_Load;
        }

        private void ItemSearchForm_Load(object sender, EventArgs e)
        {
            CreateColumns();
            LoadItems("");
        }

        private void CreateColumns()
        {
            dgvItems.Columns.Clear();
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κωδικός", DataPropertyName = "Κωδικός", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Όνομα", DataPropertyName = "Όνομα", Width = 250 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Κατηγορία", DataPropertyName = "Κατηγορία", Width = 120 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Τιμή", DataPropertyName = "Τιμή", Width = 80 });
        }

        private void LoadItems(string search)
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                conn.Open();
                string query = @"SELECT TOP 100 Id, ISNULL(Code, ISNULL(ItemCode, '')) AS [Κωδικός], 
                                ISNULL(Name, ISNULL(ItemName, ISNULL(Description, ''))) AS [Όνομα], 
                                ISNULL(Category, '') AS [Κατηγορία], ISNULL(SalePrice, 0) AS [Τιμή]
                                FROM Items WHERE IsActive = 1 AND (Name LIKE @Search OR Code LIKE @Search OR ItemCode LIKE @Search)
                                ORDER BY Name";

                var results = conn.Query(query, new { Search = $"%{search}%" }).ToList();
                dgvItems.DataSource = results;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadItems(txtSearch.Text.Trim());
        }

        private void dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvItems.Rows[e.RowIndex].DataBoundItem;
                SelectedItem = row;
                SelectedItems = new List<dynamic> { row };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow != null)
            {
                var row = dgvItems.CurrentRow.DataBoundItem;
                SelectedItem = row;
                SelectedItems = new List<dynamic> { row };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}