using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class SupplierCardForm : Form
    {
        private int _supplierId;
        private Label lblName, lblAfm, lblAddress, lblPhone, lblEmail, lblContact;
        private Label lblTotalPurchases, lblTotalPayments, lblBalance, lblOpenOrders;
        private DataGridView dgvInvoices, dgvPayments;

        public SupplierCardForm(int supplierId) { _supplierId = supplierId; InitializeUI(); this.Load += (s, e) => LoadAll(); }

        private void InitializeUI()
        {
            this.Text = "🏭 Καρτέλα Προμηθευτή";
            this.Size = new Size(1400, 850);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);

            var pnlHeader = new Panel { Dock = DockStyle.Top, Height = 95, BackColor = Color.White };
            lblName = new Label { Location = new Point(20, 10), AutoSize = true, Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = Color.FromArgb(230, 126, 34) }; pnlHeader.Controls.Add(lblName);
            lblAfm = new Label { Location = new Point(20, 42), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) }; pnlHeader.Controls.Add(lblAfm);
            lblContact = new Label { Location = new Point(200, 42), AutoSize = true, Font = new Font("Segoe UI", 10F), ForeColor = Color.Gray }; pnlHeader.Controls.Add(lblContact);
            lblAddress = new Label { Location = new Point(20, 66), AutoSize = true, Font = new Font("Segoe UI", 9.5F), ForeColor = Color.Gray }; pnlHeader.Controls.Add(lblAddress);
            lblPhone = new Label { Location = new Point(500, 42), AutoSize = true, Font = new Font("Segoe UI", 10F) }; pnlHeader.Controls.Add(lblPhone);
            lblEmail = new Label { Location = new Point(500, 66), AutoSize = true, Font = new Font("Segoe UI", 9.5F), ForeColor = Color.FromArgb(52, 152, 219) }; pnlHeader.Controls.Add(lblEmail);
            this.Controls.Add(pnlHeader);

            var pnlStats = new Panel { Dock = DockStyle.Top, Height = 90 };
            lblTotalPurchases = CreateStatCard(pnlStats, "📦 ΑΓΟΡΕΣ", "0,00 €", 10, Color.FromArgb(230, 126, 34), Color.FromArgb(253, 235, 208));
            lblTotalPayments = CreateStatCard(pnlStats, "💸 ΠΛΗΡΩΜΕΣ", "0,00 €", 350, Color.FromArgb(231, 76, 60), Color.FromArgb(248, 215, 218));
            lblBalance = CreateStatCard(pnlStats, "⚠️ ΥΠΟΛΟΙΠΟ", "0,00 €", 690, Color.FromArgb(52, 152, 219), Color.FromArgb(207, 226, 255));
            lblOpenOrders = CreateStatCard(pnlStats, "📋 ΑΝΟΙΧΤΕΣ ΠΑΡΑΓΓ.", "0,00 €", 1030, Color.FromArgb(155, 89, 182), Color.FromArgb(235, 222, 240));
            this.Controls.Add(pnlStats);

            var pnlPayTitle = new Panel { Dock = DockStyle.Bottom, Height = 30, BackColor = Color.FromArgb(248, 215, 218) };
            pnlPayTitle.Controls.Add(new Label { Text = "💸 ΙΣΤΟΡΙΚΟ ΠΛΗΡΩΜΩΝ", Dock = DockStyle.Left, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) });
            dgvPayments = new DataGridView { Dock = DockStyle.Bottom, Height = 200, AutoGenerateColumns = false, ReadOnly = true, RowHeadersVisible = false, BackgroundColor = Color.White, Font = new Font("Segoe UI", 9.5F), RowTemplate = { Height = 28 } };
            dgvPayments.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn { Name = "PaymentNumber", HeaderText = "Αριθμός", Width = 150 });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn { Name = "PaymentDate", HeaderText = "Ημερομηνία", Width = 110 });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalAmount", HeaderText = "Ποσό €", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", ForeColor = Color.FromArgb(231, 76, 60) } });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Κατάσταση", Width = 100 });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn { Name = "Notes", HeaderText = "Σημειώσεις", Width = 300 });

            var pnlInvTitle = new Panel { Dock = DockStyle.Top, Height = 30, BackColor = Color.FromArgb(253, 235, 208) };
            pnlInvTitle.Controls.Add(new Label { Text = "📋 ΙΣΤΟΡΙΚΟ ΑΓΟΡΩΝ", Dock = DockStyle.Left, AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) });
            dgvInvoices = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false, ReadOnly = true, RowHeadersVisible = false, BackgroundColor = Color.White, Font = new Font("Segoe UI", 9.5F), RowTemplate = { Height = 28 } };
            dgvInvoices.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(230, 126, 34), ForeColor = Color.White, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocNumber", HeaderText = "Αριθμός", Width = 120 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "DocDate", HeaderText = "Ημερομηνία", Width = 100 });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "NetValue", HeaderText = "Καθαρή €", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "VatValue", HeaderText = "ΦΠΑ €", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Withholding", HeaderText = "Παρακράτηση €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", ForeColor = Color.FromArgb(231, 76, 60) } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "FinalAmount", HeaderText = "Τελικό €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Paid", HeaderText = "Πληρωμένο €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", ForeColor = Color.FromArgb(46, 204, 113) } });
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Balance", HeaderText = "Υπόλοιπο €", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219) } });
            dgvInvoices.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) { try { using (var f = new PurchaseInvoiceForm(Convert.ToInt32(dgvInvoices.CurrentRow.Cells["Id"].Value))) f.ShowDialog(); LoadAll(); } catch (Exception ex) { MessageBox.Show(ex.Message); } } };
            dgvInvoices.CellFormatting += (s, e) => { if (e.RowIndex < 0) return; decimal b = 0; decimal.TryParse(dgvInvoices.Rows[e.RowIndex].Cells["Balance"].Value?.ToString(), out b); if (b <= 0.009m) dgvInvoices.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(235, 245, 235); };

            this.Controls.Add(dgvPayments);
            this.Controls.Add(pnlPayTitle);
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

        private void LoadAll() { LoadInfo(); LoadStats(); LoadInvoices(); LoadPayments(); }

        private void LoadInfo()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var s = conn.QueryFirstOrDefault("SELECT * FROM Suppliers WHERE Id=@Id", new { Id = _supplierId });
                if (s == null) return;
                lblName.Text = s.SupplierName ?? "";
                lblAfm.Text = $"ΑΦΜ: {s.Afm ?? "-"}";
                lblContact.Text = $"Υπεύθυνος: {s.ContactPerson ?? "-"}";
                lblAddress.Text = $"{s.Address ?? ""}, {s.City ?? ""}";
                lblPhone.Text = $"📞 {s.Phone ?? "-"}";
                lblEmail.Text = $"✉️ {s.Email ?? "-"}";
            }
            catch { }
        }

        private void LoadStats()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                // ✅ ΣΩΣΤΟ: Payments.TotalAmount
                decimal purchases = conn.ExecuteScalar<decimal?>("SELECT ISNULL(SUM(ISNULL(FinalAmount,GrandTotal)),0) FROM PurchaseInvoices WHERE SupplierId=@Id AND IsActive=1", new { Id = _supplierId }) ?? 0;
                decimal payments = conn.ExecuteScalar<decimal?>("SELECT ISNULL(SUM(TotalAmount),0) FROM Payments WHERE SupplierId=@Id AND Status<>'Cancelled'", new { Id = _supplierId }) ?? 0;
                decimal openOrders = conn.ExecuteScalar<decimal?>("SELECT ISNULL(SUM(Total),0) FROM PurchaseOrders WHERE SupplierId=@Id AND Status NOT IN ('Received','Closed','Cancelled')", new { Id = _supplierId }) ?? 0;
                lblTotalPurchases.Text = $"{purchases:N2} €";
                lblTotalPayments.Text = $"{payments:N2} €";
                lblBalance.Text = $"{purchases - payments:N2} €";
                lblOpenOrders.Text = $"{openOrders:N2} €";
            }
            catch { }
        }

        private void LoadInvoices()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                // ✅ ΣΩΣΤΟ: PaymentAllocations.AllocatedAmount (όχι Amount) + DocumentId=InvoiceId
                var inv = conn.Query(@"
                    SELECT pi.Id, pi.DocNumber, pi.DocDate, pi.NetValue, pi.VatValue,
                        ISNULL(pi.WithholdingAmount,0) AS Withholding,
                        ISNULL(pi.FinalAmount, pi.GrandTotal) AS FinalAmount,
                        ISNULL((SELECT SUM(pa.AllocatedAmount) FROM PaymentAllocations pa WHERE pa.DocumentId=pi.Id),0) AS Paid,
                        ISNULL(pi.FinalAmount, pi.GrandTotal) - ISNULL((SELECT SUM(pa.AllocatedAmount) FROM PaymentAllocations pa WHERE pa.DocumentId=pi.Id),0) AS Balance
                    FROM PurchaseInvoices pi WHERE pi.SupplierId=@Id ORDER BY pi.DocDate DESC", new { Id = _supplierId }).ToList();
                dgvInvoices.DataSource = inv;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadPayments()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                // ✅ ΣΩΣΤΟ: Payments.TotalAmount + Reference
                var pay = conn.Query(@"
                    SELECT PaymentNumber, PaymentDate, TotalAmount, Status, Notes
                    FROM Payments WHERE SupplierId=@Id AND Status<>'Cancelled' ORDER BY PaymentDate DESC", new { Id = _supplierId }).ToList();
                dgvPayments.DataSource = pay;
            }
            catch { }
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";
    }
}