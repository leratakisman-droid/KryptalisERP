using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class ItemImagesForm : Form
    {
        private readonly int _itemId;
        private readonly string _connStr;
        private readonly string _imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ItemImages");

        public ItemImagesForm(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
            _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
                ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
            if (!Directory.Exists(_imagePath)) Directory.CreateDirectory(_imagePath);
            this.Load += ItemImagesForm_Load;
        }

        private void ItemImagesForm_Load(object sender, EventArgs e) { LoadImages(); }

        private void LoadImages()
        {
            try
            {
                using var conn = new SqlConnection(_connStr);
                var images = conn.Query(@"
                    SELECT Id, ImageUrl AS [URL], ThumbnailUrl AS [Thumbnail], 
                           IsPrimary AS [Κύρια], SortOrder AS [Σειρά]
                    FROM ItemImages WHERE ItemId = @Id ORDER BY SortOrder",
                    new { Id = _itemId }).ToList();
                dgvImages.DataSource = images;
                if (dgvImages.Columns.Contains("Id")) dgvImages.Columns["Id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using var conn = new SqlConnection(_connStr);
                    foreach (var file in ofd.FileNames)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file)}";
                        var destPath = Path.Combine(_imagePath, fileName);
                        File.Copy(file, destPath);
                        conn.Execute(@"INSERT INTO ItemImages (ItemId, ImageUrl, ThumbnailUrl, IsPrimary, SortOrder, CreatedAt) 
                            VALUES (@ItemId, @Url, @Thumb, @Primary, @Sort, GETDATE())", new
                        {
                            ItemId = _itemId,
                            Url = destPath,
                            Thumb = destPath,
                            Primary = false,
                            Sort = 0
                        });
                    }
                    MessageBox.Show("✅ Προστέθηκαν!");
                    LoadImages();
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvImages.CurrentRow == null) { MessageBox.Show("Επίλεξε!"); return; }
            var id = dgvImages.CurrentRow.Cells["Id"].Value;
            if (id == null || id == DBNull.Value) return;
            if (MessageBox.Show("Διαγραφή;", "Επιβεβαίωση", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using var conn = new SqlConnection(_connStr);
                    var url = conn.QueryFirstOrDefault<string>("SELECT ImageUrl FROM ItemImages WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                    if (!string.IsNullOrEmpty(url) && File.Exists(url)) File.Delete(url);
                    conn.Execute("DELETE FROM ItemImages WHERE Id = @Id", new { Id = Convert.ToInt32(id) });
                    LoadImages();
                }
                catch (Exception ex) { MessageBox.Show("Σφάλμα: " + ex.Message); }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}