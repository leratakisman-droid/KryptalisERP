using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP.Services
{
    public static class ThemeManager
    {
        // ✅ ΑΝΟΙΧΤΟΧΡΩΜΗ ΠΑΛΕΤΑ
        public static readonly Color PrimaryBg = Color.FromArgb(240, 245, 250);
        public static readonly Color SecondaryBg = Color.FromArgb(255, 255, 255);
        public static readonly Color CardBg = Color.FromArgb(255, 255, 255);
        public static readonly Color HeaderBg = Color.FromArgb(20, 50, 80);
        public static readonly Color AccentGold = Color.FromArgb(212, 175, 55);
        public static readonly Color AccentBlue = Color.FromArgb(52, 152, 219);
        public static readonly Color AccentGreen = Color.FromArgb(46, 204, 113);
        public static readonly Color AccentRed = Color.FromArgb(231, 76, 60);
        public static readonly Color AccentPurple = Color.FromArgb(155, 89, 182);
        public static readonly Color TextPrimary = Color.FromArgb(44, 62, 80);
        public static readonly Color TextSecondary = Color.FromArgb(127, 140, 141);
        public static readonly Color BorderColor = Color.FromArgb(220, 230, 240);

        public static void ApplyToForm(Form form)
        {
            ApplyToCurrentForm(form);
        }

        public static void ApplyToCurrentForm(Form form)
        {
            if (form == null) return;

            form.BackColor = PrimaryBg;
            form.ForeColor = TextPrimary;

            // Εφαρμογή γραμματοσειράς μόνο αν δεν έχει οριστεί ήδη διαφορετική
            if (form.Font.Name != "Segoe UI")
                form.Font = new Font("Segoe UI", 10F);

            ApplyThemeRecursive(form.Controls);
        }

        private static void ApplyThemeRecursive(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                ApplyToControl(ctrl);
                if (ctrl.HasChildren)
                {
                    ApplyThemeRecursive(ctrl.Controls);
                }
            }
        }

        private static void ApplyToControl(Control ctrl)
        {
            switch (ctrl)
            {
                case Button btn:
                    ApplyButton(btn);
                    break;
                case ComboBox cmb:
                    ApplyComboBox(cmb);
                    break;
                case TextBox txt:
                    ApplyTextBox(txt);
                    break;
                case Label lbl:
                    ApplyLabel(lbl);
                    break;
                case DataGridView dgv:
                    ApplyDataGridView(dgv);
                    break;
                case GroupBox grp:
                    ApplyGroupBox(grp);
                    break;
                case TabControl tc:
                    ApplyTabControl(tc);
                    break;
                case Panel pnl:
                    if (pnl.BackColor != HeaderBg && pnl.BackColor != Color.FromArgb(241, 245, 249))
                    {
                        pnl.BackColor = SecondaryBg;
                    }
                    break;
                case CheckBox chk:
                    chk.ForeColor = TextPrimary;
                    if (chk.Font.Name != "Segoe UI") chk.Font = new Font("Segoe UI", 10F);
                    break;
                case RadioButton rb:
                    rb.ForeColor = TextPrimary;
                    if (rb.Font.Name != "Segoe UI") rb.Font = new Font("Segoe UI", 10F);
                    break;
                case NumericUpDown num:
                    num.BackColor = SecondaryBg;
                    num.ForeColor = TextPrimary;
                    break;
                case DateTimePicker dtp:
                    dtp.BackColor = SecondaryBg;
                    dtp.ForeColor = TextPrimary;
                    break;
            }
        }

        public static void ApplyButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            if (btn.Font.Name != "Segoe UI") btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;

            // Κράτα special χρώματα, αλλιώς βάλε Gold
            if (btn.BackColor != AccentGreen && btn.BackColor != AccentRed &&
                btn.BackColor != AccentPurple && btn.BackColor != AccentBlue && btn.BackColor != Color.Gray)
            {
                btn.BackColor = AccentGold;
            }
        }

        public static void ApplyButton(Button btn, Color bgColor)
        {
            btn.BackColor = bgColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            if (btn.Font.Name != "Segoe UI") btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        public static void ApplyComboBox(ComboBox cmb)
        {
            cmb.BackColor = SecondaryBg;
            cmb.ForeColor = TextPrimary;
            cmb.FlatStyle = FlatStyle.Flat;
            if (cmb.Font.Name != "Segoe UI") cmb.Font = new Font("Segoe UI", 9F);
        }

        public static void ApplyTextBox(TextBox txt)
        {
            txt.BackColor = SecondaryBg;
            txt.ForeColor = TextPrimary;
            txt.BorderStyle = BorderStyle.FixedSingle;
            if (txt.Font.Name != "Segoe UI") txt.Font = new Font("Segoe UI", 10F);
        }

        private static void ApplyLabel(Label lbl)
        {
            if (lbl.ForeColor != AccentGold && lbl.ForeColor != Color.White && lbl.ForeColor != AccentRed && lbl.ForeColor != HeaderBg)
            {
                lbl.ForeColor = TextPrimary;
            }
            if (lbl.Font.Name != "Segoe UI")
            {
                lbl.Font = new Font("Segoe UI", lbl.Font.Size, lbl.Font.Style);
            }
        }

        public static void ApplyDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = PrimaryBg;
            dgv.ForeColor = TextPrimary;
            dgv.GridColor = BorderColor;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = HeaderBg;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.BackColor = SecondaryBg;
            dgv.DefaultCellStyle.ForeColor = TextPrimary;
            dgv.DefaultCellStyle.SelectionBackColor = AccentBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.RowTemplate.Height = 35;
        }

        public static void ApplyGroupBox(GroupBox grp)
        {
            grp.BackColor = SecondaryBg;
            grp.ForeColor = HeaderBg;
            if (grp.Font.Name != "Segoe UI") grp.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        public static void ApplyTabControl(TabControl tc)
        {
            tc.BackColor = SecondaryBg;
            tc.ForeColor = TextPrimary;
            if (tc.Font.Name != "Segoe UI") tc.Font = new Font("Segoe UI", 10F);
        }

        public static Dictionary<string, string> Themes => new Dictionary<string, string>
        {
            { "Light Professional", "Light" },
            { "Dark Blue", "DarkBlue" }
        };

        public static void ApplyTheme(string themeName)
        {
            foreach (Form form in Application.OpenForms)
            {
                ApplyToCurrentForm(form);
            }
        }

        public static string GetSavedTheme()
        {
            return "Light";
        }
    }
}