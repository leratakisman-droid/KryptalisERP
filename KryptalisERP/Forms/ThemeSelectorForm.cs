using KryptalisERP.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP.Forms
{
    public class ThemeSelectorForm : Form
    {
        private ListBox lstThemes;
        private Button btnApply;
        private Button btnClose;
        private Label lblTitle;

        public ThemeSelectorForm()
        {
            this.Text = "🎨 Επιλογή Θέματος Εμφάνισης";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            lblTitle = new Label
            {
                Text = "🎨 Επιλέξτε Θέμα Εμφάνισης",
                Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(212, 175, 55),
                Location = new Point(20, 15),
                AutoSize = true
            };

            lstThemes = new ListBox
            {
                Location = new Point(20, 60),
                Size = new Size(440, 200),
                Font = new Font("Microsoft Sans Serif", 11F)
            };

            foreach (string theme in ThemeManager.Themes.Keys)
            {
                lstThemes.Items.Add(theme);
            }

            string saved = ThemeManager.GetSavedTheme();
            for (int i = 0; i < lstThemes.Items.Count; i++)
            {
                if (lstThemes.Items[i].ToString() == saved)
                {
                    lstThemes.SelectedIndex = i;
                    break;
                }
            }
            if (lstThemes.SelectedIndex == -1) lstThemes.SelectedIndex = 0;

            btnApply = new Button
            {
                Text = "✅ Εφαρμογή",
                Location = new Point(20, 280),
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold)
            };
            btnApply.Click += (s, e) => ApplySelectedTheme();

            btnClose = new Button
            {
                Text = "❌ Κλείσιμο",
                Location = new Point(260, 280),
                Size = new Size(200, 40),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold)
            };
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(lblTitle);
            this.Controls.Add(lstThemes);
            this.Controls.Add(btnApply);
            this.Controls.Add(btnClose);
        }

        private void ApplySelectedTheme()
        {
            if (lstThemes.SelectedItem == null) return;

            string theme = lstThemes.SelectedItem.ToString();
            ThemeManager.ApplyTheme(theme);

            MessageBox.Show($"✅ Το θέμα '{theme}' εφαρμόστηκε σε όλες τις φόρμες!", "Επιτυχία");
            this.Close();
        }
    }
}