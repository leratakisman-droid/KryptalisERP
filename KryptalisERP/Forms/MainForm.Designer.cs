using System;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            // ========================================
            // 1. DECLARE & INSTANTIATE ALL CONTROLS
            // ========================================
            this.headerPanel = new Panel();
            this.lblLogo = new Label();
            this.lblWelcome = new Label();
            this.lblRole = new Label();
            this.btnLogout = new Button();

            this.buttonsPanel = new Panel();

            this.chartsPanel = new Panel();
            this.lblChartsTitle = new Label();

            // Row 1
            this.btnSales = new Button();
            this.btnPurchases = new Button();
            this.btnClients = new Button();
            this.btnSuppliers = new Button();

            // Row 2
            this.btnWarehouse = new Button();
            this.btnPayments = new Button();
            this.btnEmployees = new Button();
            this.btnSettings = new Button();

            // Row 3
            this.btnStockMovements = new Button();
            this.btnQuotations = new Button();
            this.btnPurchaseOrders = new Button();
            this.btnPurchaseQuotations = new Button();
            this.btnDeliveryNotes = new Button();
            this.btnProjects = new Button();
            this.btnMyDataDash = new Button();

            this.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.chartsPanel.SuspendLayout();

            // ========================================
            // === HEADER PANEL SETUP ===
            // ========================================
            this.headerPanel.BackColor = Color.FromArgb(20, 50, 80);
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 90;
            this.headerPanel.Name = "headerPanel";

            this.lblLogo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.FromArgb(212, 175, 55);
            this.lblLogo.Location = new Point(20, 10);
            this.lblLogo.Size = new Size(280, 40);
            this.lblLogo.Text = "KRYPTALIS ERP";
            this.lblLogo.TextAlign = ContentAlignment.MiddleLeft;

            this.lblWelcome.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblWelcome.ForeColor = Color.White;
            this.lblWelcome.Location = new Point(20, 45);
            this.lblWelcome.Size = new Size(400, 30);
            this.lblWelcome.Text = "Καλώς ήρθες";
            this.lblWelcome.TextAlign = ContentAlignment.MiddleLeft;

            this.lblRole.Font = new Font("Segoe UI", 10F);
            this.lblRole.ForeColor = Color.FromArgb(180, 200, 220);
            this.lblRole.Location = new Point(420, 50);
            this.lblRole.Size = new Size(400, 25);
            this.lblRole.Text = "Ρόλος | Εταιρεία | Υποκατάστημα";
            this.lblRole.TextAlign = ContentAlignment.MiddleLeft;

            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.BackColor = Color.FromArgb(231, 76, 60);
            this.btnLogout.ForeColor = Color.White;
            this.btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLogout.Location = new Point(950, 25);
            this.btnLogout.Size = new Size(140, 40);
            this.btnLogout.Text = "🚪 Αποσύνδεση";
            this.btnLogout.Cursor = Cursors.Hand;
            // NOTE: Event attached in .cs file

            this.headerPanel.Controls.Add(this.lblLogo);
            this.headerPanel.Controls.Add(this.lblWelcome);
            this.headerPanel.Controls.Add(this.lblRole);
            this.headerPanel.Controls.Add(this.btnLogout);

            // ========================================
            // === BUTTONS PANEL SETUP ===
            // ========================================
            this.buttonsPanel.BackColor = Color.FromArgb(241, 245, 249);
            this.buttonsPanel.Dock = DockStyle.Top;
            this.buttonsPanel.Height = 225;
            this.buttonsPanel.Name = "buttonsPanel";

            int btnWidth = 170;
            int btnHeight = 55;
            int gapX = 15;
            int gapY = 12;
            int startX = 20;
            int startY = 10;

            // --- ROW 1 ---
            SetupRoundedButton(btnSales, "💰 Πωλήσεις", startX, startY, btnWidth, btnHeight, Color.FromArgb(46, 204, 113));
            SetupRoundedButton(btnPurchases, "🛒 Αγορές", startX + (btnWidth + gapX), startY, btnWidth, btnHeight, Color.FromArgb(52, 152, 219));
            SetupRoundedButton(btnClients, "👥 Πελάτες", startX + (btnWidth + gapX) * 2, startY, btnWidth, btnHeight, Color.FromArgb(52, 152, 219));
            SetupRoundedButton(btnSuppliers, "🏭 Προμηθευτές", startX + (btnWidth + gapX) * 3, startY, btnWidth, btnHeight, Color.FromArgb(52, 152, 219));

            // --- ROW 2 ---
            int row2Y = startY + btnHeight + gapY;
            SetupRoundedButton(btnWarehouse, "📦 Αποθήκη (Είδη)", startX, row2Y, btnWidth, btnHeight, Color.FromArgb(52, 152, 219));
            SetupRoundedButton(btnPayments, "💳 Εισπράξεις/Πληρ.", startX + (btnWidth + gapX), row2Y, btnWidth, btnHeight, Color.FromArgb(155, 89, 182));
            SetupRoundedButton(btnEmployees, "👨‍💼 Πωλητές", startX + (btnWidth + gapX) * 2, row2Y, btnWidth, btnHeight, Color.FromArgb(52, 152, 219));
            SetupRoundedButton(btnSettings, "⚙️ Ρυθμίσεις", startX + (btnWidth + gapX) * 3, row2Y, btnWidth, btnHeight, Color.FromArgb(127, 140, 141));

            // --- ROW 3 (Narrower to fit 7) ---
            int row3Y = row2Y + btnHeight + gapY;
            int narrowBtnWidth = 150;
            int narrowGap = 10;

            SetupRoundedButton(btnStockMovements, "🔄 Κινήσεις", startX, row3Y, narrowBtnWidth, btnHeight, Color.FromArgb(230, 126, 34));
            SetupRoundedButton(btnQuotations, "📄 Προσφορές", startX + (narrowBtnWidth + narrowGap), row3Y, narrowBtnWidth, btnHeight, Color.FromArgb(13, 110, 253));
            SetupRoundedButton(btnPurchaseOrders, "📝 Παραγγελίες", startX + (narrowBtnWidth + narrowGap) * 2, row3Y, narrowBtnWidth, btnHeight, Color.FromArgb(230, 126, 34));
            SetupRoundedButton(btnPurchaseQuotations, "🏭 Προσφ. Αγ.", startX + (narrowBtnWidth + narrowGap) * 3, row3Y, narrowBtnWidth, btnHeight, Color.FromArgb(230, 126, 34));
            SetupRoundedButton(btnDeliveryNotes, "📦 Δελτία", startX + (narrowBtnWidth + narrowGap) * 4, row3Y, narrowBtnWidth, btnHeight, Color.FromArgb(25, 135, 84));
            SetupRoundedButton(btnProjects, "📂 Έργα", startX + (narrowBtnWidth + narrowGap) * 5, row3Y, narrowBtnWidth, btnHeight, Color.FromArgb(142, 68, 173));
            SetupRoundedButton(btnMyDataDash, "🏛️ myDATA", startX + (narrowBtnWidth + narrowGap) * 6, row3Y, narrowBtnWidth, btnHeight, Color.FromArgb(52, 73, 94));

            // Add Controls to Panel
            this.buttonsPanel.Controls.AddRange(new Control[] {
                btnSales, btnPurchases, btnClients, btnSuppliers,
                btnWarehouse, btnPayments, btnEmployees, btnSettings,
                btnStockMovements, btnQuotations, btnPurchaseOrders, btnPurchaseQuotations,
                btnDeliveryNotes, btnProjects, btnMyDataDash
            });

            // ========================================
            // === CHARTS PANEL SETUP ===
            // ========================================
            this.chartsPanel.BackColor = Color.FromArgb(241, 245, 249);
            this.chartsPanel.Dock = DockStyle.Fill;
            this.chartsPanel.Name = "chartsPanel";

            this.lblChartsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblChartsTitle.ForeColor = Color.FromArgb(44, 62, 80);
            this.lblChartsTitle.Location = new Point(20, 15);
            this.lblChartsTitle.Size = new Size(300, 30);
            this.lblChartsTitle.Text = "📊 Γραφήματα & Στατιστικά";
            this.lblChartsTitle.TextAlign = ContentAlignment.MiddleLeft;

            this.chartsPanel.Controls.Add(this.lblChartsTitle);

            // ========================================
            // === FORM FINALIZATION ===
            // ========================================
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.ClientSize = new Size(1100, 700);

            this.Controls.Add(this.chartsPanel);
            this.Controls.Add(this.buttonsPanel);
            this.Controls.Add(this.headerPanel);

            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "KRYPTALIS ERP - Dashboard";

            this.headerPanel.ResumeLayout(false);
            this.buttonsPanel.ResumeLayout(false);
            this.chartsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void SetupRoundedButton(Button btn, string text, int x, int y, int width, int height, Color color)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Location = new Point(x, y);
            btn.Size = new Size(width, height);
            btn.Text = text;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.UseVisualStyleBackColor = false;

            int radius = height / 2;
            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                path.AddArc(width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
                path.AddArc(width - radius * 2, height - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddArc(0, height - radius * 2, radius * 2, radius * 2, 90, 90);
                path.CloseFigure();
                btn.Region = new Region(path);
            }

            var originalColor = color;
            btn.MouseEnter += (s, e) => { if (!btn.IsDisposed) btn.BackColor = ControlPaint.Light(originalColor, 0.2f); };
            btn.MouseLeave += (s, e) => { if (!btn.IsDisposed) btn.BackColor = originalColor; };
        }
        #endregion

        // === FIELD DECLARATIONS ===
        private Panel headerPanel;
        private Panel buttonsPanel;
        private Panel chartsPanel;
        private Label lblChartsTitle;
        private Label lblLogo;
        private Label lblWelcome;
        private Label lblRole;
        private Button btnLogout;

        private Button btnSales;
        private Button btnPurchases;
        private Button btnClients;
        private Button btnSuppliers;
        private Button btnWarehouse;
        private Button btnPayments;
        private Button btnEmployees;
        private Button btnSettings;
        private Button btnStockMovements;
        private Button btnQuotations;
        private Button btnPurchaseOrders;
        private Button btnPurchaseQuotations;
        private Button btnDeliveryNotes;
        private Button btnProjects;
        private Button btnMyDataDash;
    }
}