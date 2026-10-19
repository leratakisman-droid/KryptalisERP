using System;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    /// <summary>
    /// Βοηθητική κλάση που δημιουργεί ΤΥΠΟΠΟΙΗΜΕΝΟ layout σε όλες τις φόρμες
    /// </summary>
    public static class StandardFormLayout
    {
        // ✅ Χρώματα
        public static readonly Color HeaderBg = Color.FromArgb(241, 245, 249);
        public static readonly Color TitleColor = Color.FromArgb(212, 175, 55);  // Χρυσό
        public static readonly Color SubtitleColor = Color.FromArgb(44, 62, 80);
        public static readonly Color GridHeaderBg = Color.FromArgb(20, 50, 80);  // Σκούρο μπλε
        public static readonly Color GridHeaderFg = Color.White;
        public static readonly Color GridRowBg = Color.White;
        public static readonly Color GridSelectionBg = Color.FromArgb(52, 152, 219);
        public static readonly Color ButtonPanelBg = Color.FromArgb(241, 245, 249);

        // ✅ Δημιουργία Header Panel
        public static Panel CreateHeaderPanel(Label lblTitle, Label lblSubtitle)
        {
            Panel pnl = new Panel();
            pnl.BackColor = HeaderBg;
            pnl.Dock = DockStyle.Top;
            pnl.Height = 80;
            pnl.Name = "pnlHeader";

            lblTitle.AutoSize = false;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = TitleColor;
            lblTitle.Location = new Point(20, 10);
            lblTitle.Size = new Size(600, 40);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            lblSubtitle.AutoSize = false;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = SubtitleColor;
            lblSubtitle.Location = new Point(20, 50);
            lblSubtitle.Size = new Size(800, 25);
            lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;

            pnl.Controls.Add(lblTitle);
            pnl.Controls.Add(lblSubtitle);
            return pnl;
        }

        // ✅ Δημιουργία Search Panel
        public static Panel CreateSearchPanel(TextBox txtSearch, Button btnSearch, CheckBox chkExtra = null)
        {
            Panel pnl = new Panel();
            pnl.BackColor = HeaderBg;
            pnl.Dock = DockStyle.Top;
            pnl.Height = 50;
            pnl.Name = "pnlSearch";

            txtSearch.Location = new Point(20, 12);
            txtSearch.Size = new Size(300, 25);
            txtSearch.Font = new Font("Segoe UI", 10F);

            btnSearch.BackColor = Color.FromArgb(212, 175, 55);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.ForeColor = Color.White;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.Location = new Point(330, 10);
            btnSearch.Size = new Size(120, 30);
            btnSearch.Text = "🔍 Αναζήτηση";
            btnSearch.Cursor = Cursors.Hand;

            pnl.Controls.Add(txtSearch);
            pnl.Controls.Add(btnSearch);

            if (chkExtra != null)
            {
                chkExtra.AutoSize = true;
                chkExtra.Location = new Point(460, 15);
                chkExtra.Font = new Font("Segoe UI", 9F);
                pnl.Controls.Add(chkExtra);
            }

            return pnl;
        }

        // ✅ Δημιουργία DataGridView με επαγγελματικό style
        public static DataGridView CreateStyledDataGridView()
        {
            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.BackgroundColor = HeaderBg;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.RowTemplate.Height = 35;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;

            // Headers
            dgv.ColumnHeadersDefaultCellStyle.BackColor = GridHeaderBg;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = GridHeaderFg;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 40;

            // Rows
            dgv.DefaultCellStyle.BackColor = GridRowBg;
            dgv.DefaultCellStyle.ForeColor = SubtitleColor;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgv.DefaultCellStyle.SelectionBackColor = GridSelectionBg;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);

            dgv.GridColor = Color.FromArgb(220, 230, 240);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            return dgv;
        }

        // ✅ Δημιουργία Buttons Panel
        public static Panel CreateButtonsPanel(params Button[] buttons)
        {
            Panel pnl = new Panel();
            pnl.BackColor = ButtonPanelBg;
            pnl.Dock = DockStyle.Bottom;
            pnl.Height = 70;
            pnl.Name = "pnlButtons";

            int btnY = 15;
            int btnX = 20;
            int btnW = 110;
            int btnH = 40;
            int gap = 10;

            for (int i = 0; i < buttons.Length; i++)
            {
                Button btn = buttons[i];
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                btn.Location = new Point(btnX + i * (btnW + gap), btnY);
                btn.Size = new Size(btnW, btnH);
                btn.Cursor = Cursors.Hand;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                pnl.Controls.Add(btn);
            }

            return pnl;
        }

        // ✅ Δημιουργία τυποποιημένου κουμπιού
        public static Button CreateButton(string text, Color bgColor)
        {
            Button btn = new Button();
            btn.BackColor = bgColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Text = text;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            return btn;
        }

        // ✅ Χρώματα κουμπιών
        public static readonly Color BtnGreen = Color.FromArgb(46, 204, 113);    // Νέο
        public static readonly Color BtnBlue = Color.FromArgb(52, 152, 219);     // Edit
        public static readonly Color BtnRed = Color.FromArgb(231, 76, 60);       // Delete
        public static readonly Color BtnPurple = Color.FromArgb(155, 89, 182);   // Αντιγραφή
        public static readonly Color BtnGold = Color.FromArgb(212, 175, 55);     // Excel/Πίσω
        public static readonly Color BtnGray = Color.FromArgb(127, 140, 141);    // Εκτύπωση
    }
}