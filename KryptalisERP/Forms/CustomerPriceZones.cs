using System;
using System.Data;
using System.Windows.Forms;
using Dapper;
using Microsoft.Data.SqlClient;

namespace KryptalisERP
{
    public partial class CustomerPriceZones : Form
    {
        private string GetConnectionString() => "Server=.;Database=KryptalisERP;Integrated Security=True;TrustServerCertificate=True;";

        public CustomerPriceZones()
        {
            InitializeComponent();
            this.Load += async (s, e) => await LoadZonesAsync();
        }

        private async Task LoadZonesAsync()
        {
            try
            {
                using var c = new SqlConnection(GetConnectionString());
                var zones = await c.QueryAsync("SELECT Id, Code, Name, Description, DiscountPercentage, Currency FROM CustomerPriceZones ORDER BY Name");
                dgvZones.DataSource = zones.ToList();
                if (dgvZones.Columns.Contains("Id")) dgvZones.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using var f = new CustomerPriceZoneEditForm(0);
            if (f.ShowDialog() == DialogResult.OK) await LoadZonesAsync();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvZones.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvZones.CurrentRow.Cells["Id"].Value);
            using var f = new CustomerPriceZoneEditForm(id);
            if (f.ShowDialog() == DialogResult.OK) await LoadZonesAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvZones.CurrentRow == null) return;
            if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            int id = Convert.ToInt32(dgvZones.CurrentRow.Cells["Id"].Value);
            using var c = new SqlConnection(GetConnectionString());
            await c.ExecuteAsync("DELETE FROM CustomerPriceZones WHERE Id = @Id", new { Id = id });
            await LoadZonesAsync();
        }
    }
}