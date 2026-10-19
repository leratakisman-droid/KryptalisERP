using KryptalisERP.Helpers;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KryptalisERP
{
    public partial class DashboardChartsForm : Form
    {
        private static readonly SKColor ColorGold = new SKColor(212, 175, 55);
        private static readonly SKColor ColorGreen = new SKColor(46, 204, 113);
        private static readonly SKColor ColorBlue = new SKColor(52, 152, 219);
        private static readonly SKColor ColorRed = new SKColor(231, 76, 60);
        private static readonly SKColor ColorYellow = new SKColor(241, 196, 15);
        private static readonly SKColor ColorPurple = new SKColor(155, 89, 182);
        private static readonly SKColor ColorOrange = new SKColor(230, 126, 34);
        private static readonly SKColor ColorGray = new SKColor(149, 165, 166);

        private readonly SKColor[] Palette = new[]
        {
            ColorGold, ColorGreen, ColorBlue, ColorRed, ColorYellow,
            ColorPurple, ColorOrange, ColorGray,
            new SKColor(26, 188, 156), new SKColor(52, 73, 94)
        };

        public DashboardChartsForm()
        {
            InitializeComponent();
            this.Text = "📊 KRYPTALIS ERP - Dashboard";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(241, 245, 249);

            BuildDashboard();
        }

        private void BuildDashboard()
        {
            this.SuspendLayout();
            this.Controls.Clear();

            // Header
            var lblHeader = new Label
            {
                Text = "📊 Dashboard Στατιστικών",
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                ForeColor = Color.FromArgb(212, 175, 55),
                Location = new Point(20, 15),
                AutoSize = true
            };
            this.Controls.Add(lblHeader);

            // TableLayoutPanel για 4 γραμμές x 2 στήλες
            var tableLayoutPanel = new TableLayoutPanel
            {
                Location = new Point(20, 70),
                Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 100),
                ColumnCount = 2,
                RowCount = 4,
                Dock = DockStyle.None
            };

            // Ορισμός στηλών (50% - 50%)
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            // Ορισμός γραμμών (25% κάθε μία)
            for (int i = 0; i < 4; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            }

            // Γραμμή 1: Πωλήσεις ανά Μήνα + Τρόποι Πληρωμής
            tableLayoutPanel.Controls.Add(CreateMonthlySalesChart(), 0, 0);
            tableLayoutPanel.Controls.Add(CreatePaymentDistributionChart(), 1, 0);

            // Γραμμή 2: Top 10 Πελάτες + Top 10 Προϊόντα
            tableLayoutPanel.Controls.Add(CreateTop10CustomerChart(), 0, 1);
            tableLayoutPanel.Controls.Add(CreateTop10ProductsChart(), 1, 1);

            // Γραμμή 3: Εισπράξεις + Αποθήκη
            tableLayoutPanel.Controls.Add(CreateCashFlowChart(), 0, 2);
            tableLayoutPanel.Controls.Add(CreateInventoryChart(), 1, 2);

            // Γραμμή 4: Πωλητές + Ημερήσιες Πωλήσεις
            tableLayoutPanel.Controls.Add(CreateSalesBySellerChart(), 0, 3);
            tableLayoutPanel.Controls.Add(CreateDailySalesChart(), 1, 3);

            this.Controls.Add(tableLayoutPanel);
            this.ResumeLayout();
        }

        private Panel CreateMonthlySalesChart()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };
            var lbl = new Label { Text = "📈 Πωλήσεις ανά Μήνα", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Location = new Point(15, 8), AutoSize = true };
            panel.Controls.Add(lbl);

            var data = DashboardChartHelper.GetMonthlySales();
            var chart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                Series = new ISeries[]
                {
                    new LineSeries<double> { Name = $"{DateTime.Now.Year}", Values = data.current, Stroke = new SolidColorPaint(ColorGold) { StrokeThickness = 3 } },
                    new LineSeries<double> { Name = $"{DateTime.Now.Year - 1}", Values = data.previous, Stroke = new SolidColorPaint(ColorGray) { StrokeThickness = 2 } }
                },
                XAxes = new[] { new Axis { Labels = data.labels } },
                YAxes = new[] { new Axis { Name = "€" } },
                LegendPosition = LegendPosition.Top
            };
            panel.Controls.Add(chart);
            return panel;
        }

        private Panel CreatePaymentDistributionChart()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };
            var lbl = new Label { Text = "🥧 Τρόποι Πληρωμής", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Location = new Point(15, 8), AutoSize = true };
            panel.Controls.Add(lbl);

            var data = DashboardChartHelper.GetPaymentMethodDistribution();
            var series = new List<ISeries>();
            for (int i = 0; i < data.names.Count; i++)
                series.Add(new PieSeries<double> { Values = new double[] { data.values[i] }, Name = data.names[i], Fill = new SolidColorPaint(Palette[i % Palette.Length]) });

            var chart = new PieChart
            {
                Dock = DockStyle.Fill,
                Series = series.ToArray(),
                LegendPosition = LegendPosition.Right,
                InitialRotation = -90
            };
            panel.Controls.Add(chart);
            return panel;
        }

        private Panel CreateTop10CustomerChart()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };
            var lbl = new Label { Text = "🏆 Top 10 Πελάτες", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Location = new Point(15, 8), AutoSize = true };
            panel.Controls.Add(lbl);

            var data = DashboardChartHelper.GetTop10Clients();
            var chart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                Series = new ISeries[] { new ColumnSeries<double> { Name = "Πωλήσεις", Values = data.totals, Fill = new SolidColorPaint(ColorBlue) } },
                XAxes = new[] { new Axis { Labels = data.names, LabelsRotation = -30 } },
                YAxes = new[] { new Axis { Name = "€" } }
            };
            panel.Controls.Add(chart);
            return panel;
        }

        private Panel CreateTop10ProductsChart()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };
            var lbl = new Label { Text = "📦 Top 10 Προϊόντα", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Location = new Point(15, 8), AutoSize = true };
            panel.Controls.Add(lbl);

            var data = DashboardChartHelper.GetTop10Products();
            var chart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                Series = new ISeries[] { new ColumnSeries<double> { Name = "Ποσότητα", Values = data.quantities, Fill = new SolidColorPaint(ColorGreen) } },
                XAxes = new[] { new Axis { Labels = data.names, LabelsRotation = -30 } },
                YAxes = new[] { new Axis { Name = "Τεμάχια" } }
            };
            panel.Controls.Add(chart);
            return panel;
        }

        private Panel CreateCashFlowChart()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };
            var lbl = new Label { Text = "💰 Εισπράξεις", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Location = new Point(15, 8), AutoSize = true };
            panel.Controls.Add(lbl);

            var data = DashboardChartHelper.GetCashFlow();
            var chart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                Series = new ISeries[]
                {
                    new ColumnSeries<double> { Name = "Εισπράξεις", Values = data.income, Fill = new SolidColorPaint(ColorGreen) },
                    new ColumnSeries<double> { Name = "Πληρωμές", Values = data.expenses, Fill = new SolidColorPaint(ColorRed) }
                },
                XAxes = new[] { new Axis { Labels = data.labels } },
                YAxes = new[] { new Axis { Name = "€" } },
                LegendPosition = LegendPosition.Top
            };
            panel.Controls.Add(chart);
            return panel;
        }

        private Panel CreateInventoryChart()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };
            var lbl = new Label { Text = "📊 Αποθήκη", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Location = new Point(15, 8), AutoSize = true };
            panel.Controls.Add(lbl);

            var data = DashboardChartHelper.GetInventoryLevels();
            var series = new List<ISeries>();
            for (int i = 0; i < data.names.Count; i++)
            {
                SKColor color = data.statusColors[i] == 0 ? ColorGreen : (data.statusColors[i] == 1 ? ColorYellow : ColorRed);
                series.Add(new ColumnSeries<double> { Values = new double[] { data.stock[i] }, Name = data.names[i], Fill = new SolidColorPaint(color) });
            }

            var chart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                Series = series.ToArray(),
                XAxes = new[] { new Axis { Labels = data.names, LabelsRotation = -45 } },
                YAxes = new[] { new Axis { Name = "Τεμάχια" } }
            };
            panel.Controls.Add(chart);
            return panel;
        }

        private Panel CreateSalesBySellerChart()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };
            var lbl = new Label { Text = "👥 Πωλητές", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Location = new Point(15, 8), AutoSize = true };
            panel.Controls.Add(lbl);

            var data = DashboardChartHelper.GetSalesBySeller();
            var series = new List<ISeries>();
            for (int i = 0; i < data.names.Count; i++)
                series.Add(new PieSeries<double> { Values = new double[] { data.totals[i] }, Name = data.names[i], Fill = new SolidColorPaint(Palette[i % Palette.Length]) });

            var chart = new PieChart
            {
                Dock = DockStyle.Fill,
                Series = series.ToArray(),
                LegendPosition = LegendPosition.Right,
                InitialRotation = -90
            };
            panel.Controls.Add(chart);
            return panel;
        }

        private Panel CreateDailySalesChart()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };
            var lbl = new Label { Text = "📅 Ημερήσιες Πωλήσεις", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Location = new Point(15, 8), AutoSize = true };
            panel.Controls.Add(lbl);

            var data = DashboardChartHelper.GetDailySales();
            var chart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                Series = new ISeries[] { new LineSeries<double> { Name = "Πωλήσεις", Values = data.dailySales, Fill = new SolidColorPaint(new SKColor(52, 152, 219, 60)), Stroke = new SolidColorPaint(ColorBlue) { StrokeThickness = 2 } } },
                XAxes = new[] { new Axis { Labels = data.labels } },
                YAxes = new[] { new Axis { Name = "€" } }
            };
            panel.Controls.Add(chart);
            return panel;
        }
    }
}