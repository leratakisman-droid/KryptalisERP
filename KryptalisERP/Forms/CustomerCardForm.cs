using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class CustomerCardForm : Form
    {
        private int _customerId;
        private Label lblName, lblAfm, lblAddress, lblPhone, lblEmail, lblDoy;
        private Label lblTotalSales, lblTotalReceipts, lblBalance, lblCreditLimit;
        private DataGridView dgvInvoices, dgvReceipts;

        public CustomerCardForm(int customerId) { _customerId = customerId; InitializeUI(); this.Load += (s, e) => LoadAll(); }

        private void InitializeUI()
        {
            this.Text = "👤 Καρτέλα Πελάτη";
            this.Size = new Size(1400, 850);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);

            var pnlHeader = new Panel { Dock = DockStyle.Top, Height = 95, BackColor = Color.White };
            lblName = new Label { Location = new Point(20, 10), AutoSize = true, Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = Color.FromArgb(13, 110, 253) }; pnlHeader.Controls.Add(lblName);
            lblAfm = new Label { Location = new Point(20, 42), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) }; pnlHeader.Controls.Add(lblAfm);
            lblDoy = new Label { Location = new Point(200, 42), AutoSize = true, Font = new Font("Segoe UI", 10F), ForeColor = Color.Gray }; pnlHeader.Controls.Add(lblDoy);
            lblAddress = new Label { Location = new Point(20, 66), AutoSize = true, Font = new Font("Segoe UI", 9.5F), ForeColor = Color.Gray }; pnlHeader.Controls.Add(lblAddress);
            lblPhone = new Label { Location = new Point(500, 42), AutoSize = true, Font = new Font("Segoe UI", 10F) }; pnlHeader.Controls.Add(lblPhone);
            lblEmail = new Label { Location = new Point(500, 66), AutoSize = true, Font = new Font("Segoe UI", 9.5F), ForeColor = Color.FromArgb(52, 152, 219) }; pnlHeader.Controls.Add(lblEmail);
            this.Controls.Add(pnlHeader);

            var pnlStats = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = Color.Transparent };
            lblTotalSales = CreateStatCard(pnlStats, "📈 ΠΩΛΗΣΕΙΣ", "0,00 €", 10, Color.FromArgb(52, 152, 219), Color.FromArgb(207, 226, 255));
            lblTotalReceipts = CreateStatCard(pnlStats, "💰 ΕΙΣΠΡΑΞΕΙΣ", "0,00 €", 350, Color.FromArgb(46, 204, 113), Color.FromArgb(209, 231, 221));
            lblBalance = CreateStatCard(pnlStats, "⚠️ ΥΠΟΛΟΙΠΟ", "0,00 €", 690, Color.FromArgb(231, 76, 60), Color.FromArgb(248, 215, 218));
            lblCreditLimit = CreateStatCard(pnlStats, "🏦 ΠΙΣΤ.ΟΡΙΟ", "0,00 €", 1030, Color.FromArgb(155, 89, 182), Color.FromArgb(235, 222, 240));
            this.Controls.Add(pnlStats);

            var pnlRecTitle = new Panel { Dock = DockStyle.Bottom, Height = 30, BackColor = Color.FromArgb(209, 231, 221) };
            pnlRecTitle.Controls.Add(new Label { Text = "💰 ΙΣΤΟΡΙΚΟ ΕΙΣΠΡΑΞΕΩΝ", Dock = DockStyle.Left, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) });
            dgvReceipts = new DataGridView { Dock = DockStyle.Bottom, Height = 200, AutoGenerateColumns = false, ReadOnly = true, RowHeadersVisible = false, BackgroundColor = Color.White, Font = new Font("Segoe UI", 9.5F), RowTemplate = { Height = 28 } };
            dgvReceipts.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvReceipts.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReceiptNumber", HeaderText = "Αριθμός", Width = 150 });
            dgvReceipts.Columns.Add(new DataGridViewTextBoxColumn { Name = "ReceiptDate", HeaderText = "Ημερομηνία", Width = 110 });
            dgvReceipts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Amount", HeaderText = "Ποσό €", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvReceipts.Columns.Add(new DataGridViewTextBoxColumn { Name = "PaymentMethod", HeaderText = "Τρόπος", Width = 150 });
            dgvReceipts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Notes", HeaderText = "Σημειώσεις", Width = 300 });

            var pnlInvTitle = new Panel { Dock = DockStyle.Top, Height = 30, BackColor = Color.FromArgb(207, 226, 255) };
            pnlInvTitle.Controls.Add(new Label { Text = "📋 ΙΣΤΟΡΙΚΟ ΤΙΜΟΛΟΓΙΩΝ", Dock = DockStyle.Left, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) });
            dgvInvoices = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false, ReadOnly = true, RowHeadersVisible = false, BackgroundColor = Color.White, Font = new Font("Segoe UI", 9.5F), RowTemplate = { Height = 28 } };
            dgvInvoices.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(13, 110, 253), ForeColor = Color.White, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocNumber", HeaderText = "Αριθμός", Width = 120 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocDate", HeaderText = "Ημερομηνία", Width = 100 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "NetValue", HeaderText = "Καθαρή €", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "VatValue", HeaderText = "ΦΠΑ €", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "GrandTotal", HeaderText = "Αξία €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Allocated", HeaderText = "Εισπραχθέν €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Balance", HeaderText = "Υπόλοιπο €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(231, 76, 60) } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "MyData", HeaderText = "myDATA", Width = 80 });
            dgvInvoices.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) { try { using (var f = new SalesInvoiceForm(Convert.ToInt32(dgvInvoices.CurrentRow.Cells["Id"].Value))) f.ShowDialog(); LoadAll(); } catch (Exception ex) { MessageBox.Show(ex.Message); } } };
            dgvInvoices.CellFormatting += (s, e) => { if (e.RowIndex < 0) return; decimal b = 0; decimal.TryParse(dgvInvoices.Rows[e.RowIndex].Cells["Balance"].Value?.ToString(), out b); if (b <= 0.009m) dgvInvoices.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(235, 245, 235); };

            this.Controls.Add(dgvReceipts);
            this.Controls.Add(pnlRecTitle);
            this.Controls.Add(pnlInvTitle);
            this.Controls.Add(dgvInvoices);
            dgvInvoices.BringToFront();
        }

        private Label CreateStatCard(Panel p, string t, string v, int x, Color f, Color b)
        {
            var card = new Panel { Location = new Point(x, 5), Size = new Size(330, 75), BackColor = b };
            card.Controls.Add(new Label { Text = t, Location = new Point(10, 8), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.Gray });
            var lbl = new Label { Text = v, Location = new Point(10, 32), AutoSize = true, Font = new Font("Segoe UI", 17F, FontStyle.Bold), ForeColor = f };
            card.Controls.Add(lbl); p.Controls.Add(card); return lbl;
        }

        private void LoadAll() { LoadInfo(); LoadStats(); LoadInvoices(); LoadReceipts(); }

        private void LoadInfo()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var c = conn.QueryFirstOrDefault("SELECT * FROM Customers WHERE Id=@Id", new { Id = _customerId });
                if (c == null) return;
                lblName.Text = c.Epwnymia ?? "";
                lblAfm.Text = $"ΑΦΜ: {c.Afm ?? "-"}";
                lblDoy.Text = $"ΔΟΥ: {c.DOY ?? "-"}";
                lblAddress.Text = $"{c.Dieuthinsi ?? ""}, {c.Poli ?? ""} {c.TK ?? ""}";
                lblPhone.Text = $"📞 {c.Tilefono ?? "-"}";
                lblEmail.Text = $"✉️ {c.Email ?? "-"}";
                lblCreditLimit.Text = $"{(c.CreditLimit ?? 0):N2} €";
            }
            catch { }
        }

        private void LoadStats()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                // ✅ ΣΩΣΤΟ: Receipts.Amount + έλεγχος IsCancelled
                decimal sales = conn.ExecuteScalar<decimal?>("SELECT ISNULL(SUM(GrandTotal),0) FROM SalesInvoices WHERE ClientId=@Id AND IsActive=1", new { Id = _customerId }) ?? 0;
                decimal receipts = conn.ExecuteScalar<decimal?>("SELECT ISNULL(SUM(Amount),0) FROM Receipts WHERE CustomerId=@Id AND ISNULL(IsCancelled,0)=0", new { Id = _customerId }) ?? 0;
                lblTotalSales.Text = $"{sales:N2} €";
                lblTotalReceipts.Text = $"{receipts:N2} €";
                lblBalance.Text = $"{sales - receipts:N2} €";
            }
            catch { }
        }

        private void LoadInvoices()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                // ✅ ΣΩΣΤΟ: ReceiptAllocations.AllocatedAmount αντί Amount
                var inv = conn.Query(@"
                    SELECT si.Id, si.DocNumber, si.DocDate, si.NetValue, si.VatValue, si.GrandTotal,
                        ISNULL((SELECT SUM(ra.AllocatedAmount) FROM ReceiptAllocations ra WHERE ra.InvoiceId=si.Id),0) AS Allocated,
                        si.GrandTotal - ISNULL((SELECT SUM(ra.AllocatedAmount) FROM ReceiptAllocations ra WHERE ra.InvoiceId=si.Id),0) AS Balance,
                        CASE WHEN si.MyDataUID IS NOT NULL THEN '✅' ELSE '⏳' END AS MyData
                    FROM SalesInvoices si WHERE si.ClientId=@Id ORDER BY si.DocDate DESC", new { Id = _customerId }).ToList();
                dgvInvoices.DataSource = inv;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadReceipts()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                // ✅ ΣΩΣΤΟ: Receipts.Amount + ReceiptNumber + PaymentMethod (text)
                var rec = conn.Query(@"
                    SELECT ReceiptNumber, ReceiptDate, Amount, PaymentMethod, Notes
                    FROM Receipts WHERE CustomerId=@Id AND ISNULL(IsCancelled,0)=0 ORDER BY ReceiptDate DESC", new { Id = _customerId }).ToList();
                dgvReceipts.DataSource = rec;
            }
            catch { }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}