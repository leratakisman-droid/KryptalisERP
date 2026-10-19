using KryptalisERP.Forms;
using KryptalisERP.Helpers; // Για DashboardChartHelper
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using Microsoft.Extensions.Configuration;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class MainForm : Form
    {
        private readonly string _connStr = Program.Configuration?.GetConnectionString("DefaultConnection")
            ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP;User Id=sa;Password=epsilonsa;TrustServerCertificate=True;";

        // Colors for Charts
        private static readonly SKColor ColorGold = new SKColor(212, 175, 55);
        private static readonly SKColor ColorGreen = new SKColor(46, 204, 113);
        private static readonly SKColor ColorBlue = new SKColor(52, 152, 219);
        private static readonly SKColor ColorRed = new SKColor(231, 76, 60);
        private static readonly SKColor ColorYellow = new SKColor(241, 196, 15);
        private static readonly SKColor ColorPurple = new SKColor(155, 89, 182);
        private static readonly SKColor ColorOrange = new SKColor(230, 126, 34);
        private static readonly SKColor ColorGray = new SKColor(149, 165, 166);

        public MainForm()
        {
            InitializeComponent();

            // 1. Load User Info & Setup Events
            LoadUserInfo();
            SetupSimpleNavigation();

            // 2. Bind Logout separately as it's not a module
            if (btnLogout != null) btnLogout.Click += btnLogout_Click;

            this.Load += MainForm_Load;
        }

        // ============================================
        // NAVIGATION LOGIC (SIMPLE & DIRECT)
        // ============================================

        /// <summary>
        /// Συνδέει τα κουμπιά απευθείας με τις υπάρχουσες φόρμες ή List Views.
        /// </summary>
        private void SetupSimpleNavigation()
        {
            // --- ROW 1 & 2: EXISTING FORMS (Direct Open) ---

            // Πελάτες -> CustomerForm
            BindButton(btnClients, () => ShowFormSafe(() => new CustomerForm()));

            // Προμηθευτές -> SuppliersForm
            BindButton(btnSuppliers, () => ShowFormSafe(() => new SuppliersForm()));

            // Απόθεση Είδη -> ItemsForm
            BindButton(btnWarehouse, () => ShowFormSafe(() => new ItemsForm()));

            // Πωλητές -> EmployeesForm (Υποθέτουμε ότι υπάρχει)
            BindButton(btnEmployees, () => ShowFormSafe(() => new EmployeesForm()));

            // Ρυθμίσεις -> NewSettingsForm
            BindButton(btnSettings, () => ShowFormSafe(() => new NewSettingsForm()));

            // myDATA -> MyDataDashboardForm
            BindButton(btnMyDataDash, () => ShowFormSafe(() => new MyDataDashboardForm()));


            // --- DOCUMENT LISTS (Via DocumentListViewer + Engine Config) ---

            // Πωλήσεις -> Sales Invoice List
            BindButton(btnSales, () => OpenDocumentList("Sales"));

            // Αγορές -> Purchase Invoice List
            BindButton(btnPurchases, () => OpenDocumentList("Purchases"));

            // Εισπράξεις/Πληρωμές -> Finance List
            BindButton(btnPayments, () => OpenDocumentList("Finance"));

            // Δελτία -> Delivery Notes List
            BindButton(btnDeliveryNotes, () => OpenDocumentList("DeliveryNotes"));


            // --- ROW 3: NEW MODULES (Fixed Connections) ---

            // 1. Κινήσεις Αποθήκης -> WarehouseManagerForm
            BindButton(btnStockMovements, () => ShowFormSafe(() => new WarehouseManagerForm()));

            // 2. Προσφορές Πωλήσεων -> QuotationListForm OR Fallback to DocList
            BindButton(btnQuotations, () =>
            {
                // Προσπάθησε να ανοίξεις την ειδική φόρμα λίστας αν υπάρχει
                var frmType = Type.GetType("KryptalisERP.QuotationListForm, KryptalisERP");
                if (frmType != null)
                {
                    ShowFormSafe(() => Activator.CreateInstance(frmType) as Form);
                }
                else
                {
                    // Fallback: Άνοιξε γενική λίστα εγγράφων φίλτρου 'Quotes'
                    OpenDocumentList("Quotes");
                }
            });

            // 3. Παραγγελίες Πωλητών -> PurchaseOrderListForm OR Fallback
            BindButton(btnPurchaseOrders, () =>
            {
                var frmType = Type.GetType("KryptalisERP.PurchaseOrderListForm, KryptalisERP");
                if (frmType != null)
                {
                    ShowFormSafe(() => Activator.CreateInstance(frmType) as Form);
                }
                else
                {
                    OpenDocumentList("PurchaseOrders");
                }
            });

            // 4. Προσφορές Αγορών -> PurchaseQuotationListForm OR Placeholder
            BindButton(btnPurchaseQuotations, () =>
            {
                var frmType = Type.GetType("KryptalisERP.PurchaseQuotationListForm, KryptalisERP");
                if (frmType != null)
                {
                    ShowFormSafe(() => Activator.CreateInstance(frmType) as Form);
                }
                else
                {
                    MessageBox.Show("Φόρμα Προσφορών Αγορών δεν έχει δημιουργηθεί ακόμα.\n\nΔημιούργησε το 'PurchaseQuotationListForm.cs' για ενεργοποίηση.", "Info");
                }
            });

            // 5. Έργα -> ProjectListForm OR Placeholder
            BindButton(btnProjects, () =>
            {
                var frmType = Type.GetType("KryptalisERP.ProjectListForm, KryptalisERP");
                if (frmType != null)
                {
                    ShowFormSafe(() => Activator.CreateInstance(frmType) as Form);
                }
                else
                {
                    MessageBox.Show("Φόρμα Έργων δεν έχει δημιουργηθεί ακόμα.\n\nΔημιούργησε το 'ProjectListForm.cs' για ενεργοποίηση.", "Info");
                }
            });
        }

        // ============================================
        // HELPERS
        // ============================================

        private void BindButton(Button btn, Action action)
        {
            if (btn != null)
            {
                // Καθαρισμός παλιών handlers για ασφάλεια
                btn.Click -= AnyClickHandler;

                // Σύνδεση νέου handler
                btn.Click += (s, e) =>
                {
                    try
                    {
                        action();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Σφάλμα εκτέλεσης ενέργειας:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
            }
        }

        // Dummy handler to allow removal above
        private void AnyClickHandler(object sender, EventArgs e) { }

        private void ShowFormSafe(Func<Form> formFactory)
        {
            try
            {
                var frm = formFactory();
                if (frm != null)
                {
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Σφάλμα άνοιγμα φόρμας:\n{ex.Message}", "Error");
            }
        }

        private void OpenDocumentList(string moduleName)
        {
            try
            {
                // We still need the Engine JUST to get the Config (ModuleName -> Table/Type mapping)
                var engine = new Engines.GenericFormEngine(_connStr);
                var mod = engine.GetModule(moduleName);

                if (mod == null)
                {
                    MessageBox.Show($"Το module '{moduleName}' δεν βρέθηκε στη βάση AppModules.");
                    return;
                }

                using (var lst = new DocumentListViewer(mod))
                {
                    lst.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Σφάλμα λίστας εγγράφων '{moduleName}':\n{ex.Message}", "Error");
            }
        }

        // ============================================
        // USER INFO & LOAD
        // ============================================

        private void LoadUserInfo()
        {
            if (lblWelcome != null) lblWelcome.Text = $"Καλώς ήρθες, {Program.CurrentUserName}!";
            if (lblRole != null) lblRole.Text = $"{Program.CurrentUserRole} | {Program.CurrentCompanyName} | {Program.CurrentBranchName}";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BuildDashboardCharts();
        }

        // ============================================
        // DASHBOARD CHARTS ENGINE
        // ============================================

        private void BuildDashboardCharts()
        {
            this.SuspendLayout();

            // Ensure chartsPanel exists and clear old content
            if (this.Controls.ContainsKey("chartsPanel") && chartsPanel != null)
            {
                chartsPanel.Controls.Clear();
            }
            else
            {
                chartsPanel = new Panel { Name = "chartsPanel", Dock = DockStyle.Fill, Padding = new Padding(10), BackColor = Color.FromArgb(241, 245, 249) };
                this.Controls.Add(chartsPanel);
            }

            var tbl = new TableLayoutPanel
            {
                Name = "tblCharts",
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 2,
                BackColor = Color.Transparent,
                Padding = new Padding(8)
            };

            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            try
            {
                tbl.Controls.Add(CreateMonthlySalesPanel(), 0, 0);
                tbl.Controls.Add(CreateCashFlowPanel(), 1, 0);
                tbl.Controls.Add(CreateInventoryPanel(), 2, 0);
                tbl.Controls.Add(CreateTopProductsPanel(), 0, 1);
                tbl.Controls.Add(CreateTopCustomerPanel(), 1, 1);
                tbl.Controls.Add(CreateDailySalesPanel(), 2, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα δημιουργίας γραφημάτων:\n" + ex.Message);
            }

            chartsPanel.Controls.Add(tbl);
            this.ResumeLayout();
        }

        private Panel CreateChartPanel(string title, Control chartControl, Color headerColor)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(0),
                Margin = new Padding(4),
                BorderStyle = BorderStyle.FixedSingle
            };

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 35,
                BackColor = headerColor,
                Padding = new Padding(10, 8, 0, 0)
            };

            var lbl = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(250, 25)
            };
            header.Controls.Add(lbl);
            panel.Controls.Add(header);

            chartControl.Dock = DockStyle.Fill;
            panel.Controls.Add(chartControl);

            return panel;
        }

        private Panel CreateMonthlySalesPanel()
        {
            var data = DashboardChartHelper.GetMonthlySales();
            var chart = new CartesianChart
            {
                Series = new ISeries[]
                {
                    new LineSeries<double> { Name = $"{DateTime.Now.Year}", Values = data.current, Stroke = new SolidColorPaint(ColorGold) { StrokeThickness = 3 }, Fill = new SolidColorPaint(new SKColor(212, 175, 55, 30)), GeometrySize = 8 },
                    new LineSeries<double> { Name = $"{DateTime.Now.Year - 1}", Values = data.previous, Stroke = new SolidColorPaint(ColorGray) { StrokeThickness = 2 } }
                },
                XAxes = new[] { new Axis { Labels = data.labels, LabelsRotation = 0 } },
                YAxes = new[] { new Axis { Name = "€" } },
                LegendPosition = LegendPosition.Top
            };
            return CreateChartPanel("📈 Πωλήσεις ανά Μήνα", chart, Color.FromArgb(212, 175, 55));
        }

        private Panel CreateCashFlowPanel()
        {
            var data = DashboardChartHelper.GetCashFlow();
            var chart = new CartesianChart
            {
                Series = new ISeries[]
                {
                    new ColumnSeries<double> { Name = "Εισπράξεις", Values = data.income, Fill = new SolidColorPaint(ColorGreen), MaxBarWidth = 50 },
                    new ColumnSeries<double> { Name = "Πληρωμές", Values = data.expenses, Fill = new SolidColorPaint(ColorRed), MaxBarWidth = 50 }
                },
                XAxes = new[] { new Axis { Labels = data.labels } },
                YAxes = new[] { new Axis { Name = "€" } },
                LegendPosition = LegendPosition.Top
            };
            return CreateChartPanel("💰 Εισπράξεις vs Πληρωμές", chart, Color.FromArgb(46, 204, 113));
        }

        private Panel CreateInventoryPanel()
        {
            var data = DashboardChartHelper.GetInventoryLevels();
            var series = new List<ISeries>();

            for (int i = 0; i < Math.Min(data.names.Count, 10); i++)
            {
                SKColor color = data.statusColors[i] == 0 ? ColorGreen : (data.statusColors[i] == 1 ? ColorYellow : ColorRed);
                series.Add(new ColumnSeries<double> { Values = new double[] { data.stock[i] }, Name = data.names[i], Fill = new SolidColorPaint(color), MaxBarWidth = 40 });
            }

            var chart = new CartesianChart
            {
                Series = series.ToArray(),
                XAxes = new[] { new Axis { Labels = data.names, LabelsRotation = -45 } },
                YAxes = new[] { new Axis { Name = "Τεμάχια" } }
            };
            return CreateChartPanel("📦 Αποθήκη - Επίπεδα Στοκ", chart, Color.FromArgb(52, 152, 219));
        }

        private Panel CreateTopProductsPanel()
        {
            var data = DashboardChartHelper.GetTop10Products();
            var chart = new CartesianChart
            {
                Series = new ISeries[] { new ColumnSeries<double> { Name = "Πωλήσεις", Values = data.quantities, Fill = new SolidColorPaint(ColorBlue), MaxBarWidth = 50 } },
                XAxes = new[] { new Axis { Labels = data.names, LabelsRotation = -45 } },
                YAxes = new[] { new Axis { Name = "Τεμάχια" } }
            };
            return CreateChartPanel("🏆 Top 10 Προϊόντα", chart, Color.FromArgb(52, 152, 219));
        }

        private Panel CreateTopCustomerPanel()
        {
            var data = DashboardChartHelper.GetTop10Clients();
            var chart = new CartesianChart
            {
                Series = new ISeries[] { new ColumnSeries<double> { Name = "Πωλήσεις", Values = data.totals, Fill = new SolidColorPaint(ColorPurple), MaxBarWidth = 50 } },
                XAxes = new[] { new Axis { Labels = data.names, LabelsRotation = -30 } },
                YAxes = new[] { new Axis { Name = "€" } }
            };
            return CreateChartPanel("👥 Top 10 Πελάτες", chart, Color.FromArgb(155, 89, 182));
        }

        private Panel CreateDailySalesPanel()
        {
            var data = DashboardChartHelper.GetDailySales();
            var chart = new CartesianChart
            {
                Series = new ISeries[] { new LineSeries<double> { Name = "Πωλήσεις", Values = data.dailySales, Fill = new SolidColorPaint(new SKColor(52, 152, 219, 60)), Stroke = new SolidColorPaint(ColorBlue) { StrokeThickness = 2 }, GeometrySize = 5 } },
                XAxes = new[] { new Axis { Labels = data.labels } },
                YAxes = new[] { new Axis { Name = "€" } }
            };
            return CreateChartPanel("📅 Ημερήσιες Πωλήσεις", chart, Color.FromArgb(230, 126, 34));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Θέλεις να αποσυνδεθείς;", "Επιβεβαίωση", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
        }
    }
}