using System;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    // ✅ ΧΩΡΙΣ partial, χωρίς Designer, 100% κώδικας
    public class WarehouseManagerForm : Form
    {
        public WarehouseManagerForm()
        {
            InitializeTabbedUI();
        }

        private void InitializeTabbedUI()
        {
            this.Text = "🏭 Κεντρική Διαχείριση Αποθήκης";
            this.Size = new Size(1350, 850);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.MinimumSize = new Size(1100, 700);

            var tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                Padding = new Point(15, 6)
            };

            // 📦 TAB 1: Μεταφορές (Εδώ μπαίνει η φόρμα που φτιάξαμε)
            var tabTransfer = new TabPage("📦 Μεταφορές Αποθηκών");
            var transferForm = new StockMovementForm(null)
            {
                Dock = DockStyle.Fill,
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                AutoScroll = true
            };
            tabTransfer.Controls.Add(transferForm);
            transferForm.Show(); // ✅ Απαραίτητο για να εμφανιστεί μέσα στο Tab
            tabControl.TabPages.Add(tabTransfer);

            // Στο InitializeTabbedUI(), άλλαξε το tabAdjust:
            var tabAdjust = new TabPage("📉 Διορθώσεις & Απώλειες");
            var adjustmentForm = new StockAdjustmentForm(null)
            {
                Dock = DockStyle.Fill,
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                AutoScroll = true
            };
            tabAdjust.Controls.Add(adjustmentForm);
            adjustmentForm.Show();
            tabControl.TabPages.Add(tabAdjust);

            // Μέσα στο InitializeTabbedUI()

            // 📋 TAB 3: Απογραφές Αποθήκης
            var tabCount = new TabPage("📋 Απογραφές Αποθήκης");
            tabCount.BackColor = Color.White;
            var countForm = new StockCountForm(null)
            {
                Dock = DockStyle.Fill,
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                AutoScroll = true
            };
            tabCount.Controls.Add(countForm);
            countForm.Show();
            tabControl.TabPages.Add(tabCount);

            /// 📜 TAB 4: Ιστορικό Κινήσεων
            var tabHistory = new TabPage("📜 Ιστορικό Κινήσεων");
            tabHistory.BackColor = Color.White;

            var transactionsForm = new StockTransactionsForm()
            {
                Dock = DockStyle.Fill,
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                AutoScroll = true
            };
            tabHistory.Controls.Add(transactionsForm);
            transactionsForm.Show();

            tabControl.TabPages.Add(tabHistory);

            this.Controls.Add(tabControl);
        }
    }
}