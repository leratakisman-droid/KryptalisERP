using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KryptalisERP
{
    public class NewSettingsForm : Form
    {
        private SplitContainer splitMain;
        private TreeView tvCategories;
        private Panel pnlContent;

        // State
        private int _currentCompanyId = 0;
        private int _currentBranchId = 0;

        // Professional Color Palette
        private readonly Color COLOR_GLOBAL = Color.FromArgb(44, 62, 80);       // Dark Slate Blue
        private readonly Color COLOR_ACCOUNTING = Color.FromArgb(211, 84, 0);   // Burnt Orange
        private readonly Color COLOR_MASTER_DATA = Color.FromArgb(22, 160, 133);// Teal/Green
        private readonly Color COLOR_MYDATA = Color.FromArgb(192, 57, 43);      // Deep Red
        private readonly Color COLOR_IO = Color.FromArgb(52, 152, 219);         // Bright Blue
        private readonly Color COLOR_COMPANY = Color.FromArgb(142, 68, 173);    // Purple
        private readonly Color COLOR_BRANCH = Color.FromArgb(230, 126, 34);     // Orange
        private readonly Color COLOR_POS = Color.FromArgb(155, 89, 182);        // Violet
        private readonly Color COLOR_COPY_SERIES = Color.FromArgb(241, 196, 15);// Amber

        // ✅ FIX: Added missing color definition used in Series Grids
        private readonly Color COLOR_SETTING = Color.FromArgb(46, 204, 113);

        private readonly Color COLOR_TEXT_LIGHT = Color.White;
        private readonly Color COLOR_BLACK = Color.Black;

        public NewSettingsForm()
        {
            InitializeUI();
            LoadTreeStructure();

            if (tvCategories.Nodes.Count > 0)
                tvCategories.SelectedNode = tvCategories.Nodes[0];
        }

        private void InitializeUI()
        {
            this.Text = "⚙️ Κεντρικές Ρυθμίσεις ERP (Professional Suite)";
            this.Size = new Size(1400, 850);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.WindowState = FormWindowState.Maximized;

            // ==========================================
            // SPLIT CONTAINER SETUP
            // ==========================================
            splitMain = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 350,
                FixedPanel = FixedPanel.Panel1,
                BackColor = Color.Gainsboro,
                BorderStyle = BorderStyle.None
            };

            // --- LEFT PANEL: Navigation Tree ---
            var pnlLeftHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45,
                BackColor = COLOR_GLOBAL,
                Padding = new Padding(10, 5, 10, 5)
            };
            pnlLeftHeader.Controls.Add(new Label
            {
                Text = "📂 Μενού Ρυθμίσεων",
                ForeColor = COLOR_TEXT_LIGHT,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 12)
            });

            tvCategories = new TreeView
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                HideSelection = false,
                ShowLines = true,
                FullRowSelect = true,
                Indent = 20,
                ItemHeight = 30,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                LineColor = Color.LightGray
            };

            tvCategories.DrawNode += TvCategories_DrawNode;
            tvCategories.AfterSelect += TvCategories_AfterSelect;

            splitMain.Panel1.Controls.Add(tvCategories);
            splitMain.Panel1.Controls.Add(pnlLeftHeader);

            // --- RIGHT PANEL: Content Area ---
            var pnlTopBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            var lblBreadcrumb = new Label
            {
                Name = "lblBreadcrumb",
                Text = "📍 Αρχική",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = COLOR_GLOBAL,
                Location = new Point(10, 15),
                AutoSize = true
            };

            pnlTopBar.Controls.Add(lblBreadcrumb);

            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20),
                AutoScroll = true
            };

            splitMain.Panel2.Controls.Add(pnlContent);
            splitMain.Panel2.Controls.Add(pnlTopBar);

            this.Controls.Add(splitMain);
        }

        // ==========================================
        // TREEVIEW CUSTOM DRAWING (SECTOR COLORS)
        // ==========================================
        private void TvCategories_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            string tag = e.Node.Tag?.ToString() ?? "";
            Color bgColor = Color.White;
            Color textColor = Color.Black;
            bool isSelected = (e.State & TreeNodeStates.Selected) != 0;

            // Sector-Based Coloring
            if (tag == "GLOBAL_ROOT" || tag.StartsWith("VIEW_USERS") || tag.StartsWith("VIEW_EMAIL"))
            {
                bgColor = isSelected ? COLOR_GLOBAL : Color.FromArgb(230, 235, 240);
                textColor = isSelected ? COLOR_TEXT_LIGHT : COLOR_GLOBAL;
            }
            else if (tag == "ACCOUNTING_ROOT" || tag.StartsWith("ACCT_") || tag.StartsWith("VAT_"))
            {
                bgColor = isSelected ? COLOR_ACCOUNTING : Color.FromArgb(255, 240, 230);
                textColor = isSelected ? COLOR_TEXT_LIGHT : COLOR_ACCOUNTING;
            }
            else if (tag == "MASTER_DATA_ROOT" || tag.StartsWith("MD_"))
            {
                bgColor = isSelected ? COLOR_MASTER_DATA : Color.FromArgb(230, 250, 245);
                textColor = isSelected ? COLOR_TEXT_LIGHT : COLOR_MASTER_DATA;
            }
            else if (tag == "MYDATA_ROOT" || tag.StartsWith("MYDATA_"))
            {
                bgColor = isSelected ? COLOR_MYDATA : Color.FromArgb(255, 240, 240);
                textColor = isSelected ? COLOR_TEXT_LIGHT : COLOR_MYDATA;
            }
            else if (tag == "IO_ROOT" || tag.StartsWith("IMPORT_") || tag.StartsWith("EXPORT_"))
            {
                bgColor = isSelected ? COLOR_IO : Color.FromArgb(230, 245, 255);
                textColor = isSelected ? COLOR_TEXT_LIGHT : COLOR_IO;
            }
            else if (tag.StartsWith("COMPANY_") || tag.StartsWith("EDIT_COMPANY_"))
            {
                bgColor = isSelected ? COLOR_COMPANY : Color.FromArgb(245, 230, 250);
                textColor = isSelected ? COLOR_TEXT_LIGHT : COLOR_COMPANY;
            }
            else if (tag.StartsWith("BRANCH_DETAIL_"))
            {
                bgColor = isSelected ? COLOR_BRANCH : Color.FromArgb(255, 245, 230);
                textColor = isSelected ? COLOR_TEXT_LIGHT : COLOR_BRANCH;
            }
            else if (tag.StartsWith("POS_"))
            {
                bgColor = isSelected ? COLOR_POS : Color.FromArgb(245, 230, 250);
                textColor = isSelected ? COLOR_TEXT_LIGHT : COLOR_POS;
            }
            else if (tag.StartsWith("COPY_SERIES_"))
            {
                bgColor = isSelected ? COLOR_COPY_SERIES : Color.FromArgb(255, 245, 200);
                textColor = isSelected ? COLOR_BLACK : COLOR_COPY_SERIES;
            }
            else // Default Leaf Nodes (including SERIES_)
            {
                bgColor = isSelected ? COLOR_SETTING : Color.FromArgb(230, 250, 240);
                textColor = isSelected ? COLOR_TEXT_LIGHT : COLOR_SETTING;
            }

            Rectangle bounds = e.Bounds;
            bounds.Width = tvCategories.ClientSize.Width - e.Node.Level * 20;

            using (SolidBrush brushBg = new SolidBrush(bgColor))
            {
                e.Graphics.FillRectangle(brushBg, bounds);
            }

            TextRenderer.DrawText(e.Graphics, e.Node.Text, tvCategories.Font, bounds, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }

        // ==========================================
        // LOAD STRUCTURE (PROFESSIONAL HIERARCHY)
        // ==========================================
        private void LoadTreeStructure()
        {
            tvCategories.Nodes.Clear();

            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                // --- 1. GLOBAL SETTINGS ---
                var nodeGlobal = new TreeNode("📂 Γενικές Ρυθμίσεις") { Tag = "GLOBAL_ROOT" };
                nodeGlobal.Nodes.Add(new TreeNode("👥 Χρήστες & Ρόλοι") { Tag = "VIEW_USERS" });
                nodeGlobal.Nodes.Add(new TreeNode("📧 Email Server") { Tag = "VIEW_EMAIL" });
                nodeGlobal.Nodes.Add(new TreeNode("🖨️ Εκτυπωτές") { Tag = "VIEW_PRINTERS" });
                nodeGlobal.Nodes.Add(new TreeNode("💾 Backup & Restore") { Tag = "VIEW_BACKUP" });
                tvCategories.Nodes.Add(nodeGlobal);

                // --- 2. ACCOUNTING & TAXES ---
                var nodeAcc = new TreeNode("💼 Λογιστικό Πλάνο & Φορολογικά") { Tag = "ACCOUNTING_ROOT" };
                nodeAcc.Nodes.Add(new TreeNode("📊 Γενικό Λογιστικό Σχέδιο (GL Accounts)") { Tag = "ACCT_GL_MAP" });
                nodeAcc.Nodes.Add(new TreeNode("🧮 Συντελεστές ΦΠΑ (VAT Rates)") { Tag = "VAT_RATES_LIST" });
                nodeAcc.Nodes.Add(new TreeNode("🚫 Απαλλαγές ΦΠΑ (Exemptions)") { Tag = "VAT_EXEMPTIONS" });
                nodeAcc.Nodes.Add(new TreeNode("✂️ Παρακρατήσεις (Withholding)") { Tag = "WITHHOLDING_TAXES" });
                nodeAcc.Nodes.Add(new TreeNode("📅 Λογιστικές Περίοδοι") { Tag = "ACCT_PERIODS" });
                tvCategories.Nodes.Add(nodeAcc);

                // --- 3. MASTER DATA & CATALOGS ---
                var nodeMd = new TreeNode("📦 Master Data & Κατάλογοι") { Tag = "MASTER_DATA_ROOT" };
                nodeMd.Nodes.Add(new TreeNode("📏 Μονάδες Μέτρησης (UoM)") { Tag = "MD_UOM" });
                nodeMd.Nodes.Add(new TreeNode("🏷️ Κατηγορίες Προϊόντων") { Tag = "MD_CATEGORIES" });
                nodeMd.Nodes.Add(new TreeNode("🚛 Μεταφορικές & Οχήματα") { Tag = "MD_TRANSPORT" });
                nodeMd.Nodes.Add(new TreeNode("🌍 Περιοχές / Ζώνες") { Tag = "MD_TERRITORIES" });
                nodeMd.Nodes.Add(new TreeNode("🏦 Τρόποι Πληρωμής") { Tag = "MD_PAYMENT_METHODS" });
                tvCategories.Nodes.Add(nodeMd);

                // --- 4. MYDATA & TAXISNET ---
                var nodeMyData = new TreeNode("🇬🇷 myDATA & TAXISNET") { Tag = "MYDATA_ROOT" };
                nodeMyData.Nodes.Add(new TreeNode("🔑 Πάροχοι (API Credentials)") { Tag = "MYDATA_PROVIDERS" });
                nodeMyData.Nodes.Add(new TreeNode("📋 Mapping Κωδικών ΑΑΔΕ") { Tag = "MYDATA_DOC_TYPES" });
                nodeMyData.Nodes.Add(new TreeNode("🔢 Διαχείριση Σειρών (Global)") { Tag = "MYDATA_SERIES_CONFIG" });
                nodeMyData.Nodes.Add(new TreeNode("⚙️ Γεντικές Παράμετροι Sync") { Tag = "MYDATA_GENERAL" });
                tvCategories.Nodes.Add(nodeMyData);

                // --- 5. IMPORT / EXPORT ---
                var nodeIo = new TreeNode("🔄 Εισαγωγή / Εξαγωγή Δεδομένων") { Tag = "IO_ROOT" };
                nodeIo.Nodes.Add(new TreeNode("📥 Εισαγωγή Πελατών/Προμηθευτών (Excel)") { Tag = "IMPORT_PARTIES" });
                nodeIo.Nodes.Add(new TreeNode("📥 Εισαγωγή Ειδών/Υπηρεσιών (Excel)") { Tag = "IMPORT_ITEMS" });
                nodeIo.Nodes.Add(new TreeNode("📤 Εξαγωγή Τιμολογίων ανά Λογιστικό Σχέδιο") { Tag = "EXPORT_INVOICES_GL" });
                nodeIo.Nodes.Add(new TreeNode("📤 Εξαγωγή Αναφορών Πωλήσεων/Αγορών") { Tag = "EXPORT_SALES_PURCH" });
                tvCategories.Nodes.Add(nodeIo);

                // --- 6. COMPANIES (DYNAMIC WITH BRANCHES & POS) ---
                var companies = conn.Query<CompanyDto>(
                    "SELECT Id, CompanyName FROM Companies ORDER BY CompanyName"
                ).ToList();

                foreach (var comp in companies)
                {
                    var nodeComp = new TreeNode($"🏢 {comp.CompanyName}") { Tag = $"COMPANY_{comp.Id}" };

                    nodeComp.Nodes.Add(new TreeNode("ℹ️ Στοιχεία Εταιρείας") { Tag = $"EDIT_COMPANY_{comp.Id}" });

                    var branches = conn.Query<BranchDto>(
                        @"SELECT Id, BranchName, BranchCode 
                          FROM CompanyBranches 
                          WHERE CompanyId = @Cid AND IsActive = 1 
                          ORDER BY BranchName",
                        new { Cid = comp.Id }
                    ).ToList();

                    if (branches.Any())
                    {
                        var nodeBranchesRoot = new TreeNode("📂 Υποκαταστήματα");

                        foreach (var br in branches)
                        {
                            var nodeBr = new TreeNode($"   🏬 {br.BranchName} ({br.BranchCode})")
                            {
                                Tag = $"BRANCH_DETAIL_{br.Id}_{comp.Id}"
                            };

                            // Settings specific to this Branch
                            nodeBr.Nodes.Add(new TreeNode("⚙️ Ρυθμίσεις Αποθήκης") { Tag = $"WAREHOUSE_CONFIG_{br.Id}" });
                            nodeBr.Nodes.Add(new TreeNode("🔢 Σειρές Παραστατικών (Κατάστημα)") { Tag = $"SERIES_BRANCH_{br.Id}" });

                            // ✅ FIX: Ensure Company ID is included in POS Tag for parsing
                            nodeBr.Nodes.Add(new TreeNode("💳 POS Τερματικά") { Tag = $"POS_TERMINALS_{br.Id}_{comp.Id}" });

                            nodeBr.Nodes.Add(new TreeNode("📋 Αντιγραφή Σειρών από Άλλο Κατάστημα") { Tag = $"COPY_SERIES_FROM_{br.Id}" });

                            nodeBranchesRoot.Nodes.Add(nodeBr);
                        }

                        nodeComp.Nodes.Add(nodeBranchesRoot);
                    }

                    nodeComp.Nodes.Add(new TreeNode("🔢 Σειρές Τιμολογίων (Γενικές)") { Tag = $"SERIES_COMPANY_{comp.Id}" });

                    tvCategories.Nodes.Add(nodeComp);
                }

                tvCategories.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης δομής: " + ex.Message);
            }
        }

        private void TvCategories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string tag = e.Node.Tag?.ToString() ?? "";

            var breadcrumbLabel = splitMain.Panel2.Controls.Cast<Control>()
                .FirstOrDefault(c => c.Name == "lblBreadcrumb") as Label;
            if (breadcrumbLabel != null)
                breadcrumbLabel.Text = $"📍 {e.Node.FullPath.Replace("\\", " > ")}";

            RenderContent(tag);
        }

        // ==========================================
        // RENDER CONTENT LOGIC
        // ==========================================
        private void RenderContent(string tag)
        {
            pnlContent.Controls.Clear();

            switch (tag)
            {
                case "ACCT_GL_MAP":
                    RenderPlaceholder("💼 Γενικό Λογιστικό Σχέδιο", "Εδώ θα διαχειρίζεσαι τους λογαριασμούς εσόδων/εξόδων.");
                    break;
                case "VAT_RATES_LIST":
                    RenderVatRatesList();
                    break;
                case "MD_UOM":
                    RenderUnitsOfMeasure();
                    break;
                case "MYDATA_PROVIDERS":
                    RenderMyDataProvidersSection();
                    break;
                case "MYDATA_DOC_TYPES":
                    RenderMyDataDocTypesSection();
                    break;
                case "IMPORT_PARTIES":
                    RenderImportWizard("Πελάτες & Προμηθευτές");
                    break;
                case "IMPORT_ITEMS":
                    RenderImportWizard("Είδη & Υπηρεσίες");
                    break;
                case "EXPORT_INVOICES_GL":
                    RenderExportReportBuilder("Εξαγωγή Τιμολογίων (Βάση GL)");
                    break;

                case "VIEW_USERS": RenderPlaceholder("👥 Χρήστες", "Διαχείριση χρηστών και δικαιωμάτων."); break;
                case "VIEW_EMAIL": RenderPlaceholder("📧 Email", "Ρυθμίσεις SMTP."); break;
                case "VIEW_PRINTERS": RenderPlaceholder("🖨️ Εκτυπωτές", "Επιλογή προεπιλεγμένου εκτυπωτή."); break;
                case "VIEW_BACKUP": RenderPlaceholder("💾 Backup", "Ρυθμίσεις αντιγράφων ασφαλείας."); break;

                default:
                    if (tag.StartsWith("BRANCH_DETAIL_")) HandleBranchDetail(tag);
                    else if (tag.StartsWith("SERIES_BRANCH_")) HandleSeriesList(tag, true);
                    else if (tag.StartsWith("SERIES_COMPANY_")) HandleSeriesList(tag, false);
                    else if (tag.StartsWith("EDIT_COMPANY_")) OpenCompanyEditor(Convert.ToInt32(tag.Split('_')[2]));
                    else if (tag.StartsWith("POS_TERMINALS_")) HandlePosTerminals(tag);
                    else if (tag.StartsWith("COPY_SERIES_FROM_")) HandleCopySeries(tag);
                    else if (tag == "GLOBAL_ROOT") RenderWelcomeScreen();
                    else RenderPlaceholder(tag, "Αυτή η λειτουργία βρίσκεται υπό ανάπτυξη.");
                    break;
            }
        }

        // ==========================================
        // VIEWS IMPLEMENTATION
        // ==========================================

        private void RenderVatRatesList()
        {
            var dgv = CreateStandardGrid(COLOR_ACCOUNTING);
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ποσοστό (%)", DataPropertyName = "Rate", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Περιγραφή", DataPropertyName = "Description", Width = 200 });
            dgv.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Ενεργό", DataPropertyName = "IsActive", Width = 80 });

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                dgv.DataSource = conn.Query("SELECT Id, Rate, Description, IsActive FROM VatRates ORDER BY Rate DESC").ToList();
            }
            catch { }

            pnlContent.Controls.Add(dgv);
        }

        private void RenderUnitsOfMeasure()
        {
            var dgv = CreateStandardGrid(COLOR_MASTER_DATA);
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Κωδικός", DataPropertyName = "Code", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Όνομα (GR)", DataPropertyName = "NameGR", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Σύμβολο", DataPropertyName = "Symbol", Width = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "AADE Code", DataPropertyName = "AadeCode", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Συντελεστής", DataPropertyName = "ConversionFactor", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                dgv.DataSource = conn.Query("SELECT Id, Code, NameGR, Symbol, AadeCode, ConversionFactor FROM UnitsOfMeasure ORDER BY NameGR").ToList();
            }
            catch (Exception ex) { MessageBox.Show("Error loading UoM: " + ex.Message); }

            pnlContent.Controls.Add(dgv);
        }

        private void RenderImportWizard(string entityType)
        {
            var grp = new GroupBox { Text = $"📥 Εισαγωγή {entityType} από Excel", Location = new Point(20, 20), Size = new Size(600, 300) };

            var btnBrowse = CreateActionBtn("📁 Επίλεξε Αρχείο (.xlsx)", 20, 30, Color.Blue);
            btnBrowse.Click += (s, e) => {
                using (var ofd = new OpenFileDialog { Filter = "Excel Files|*.xlsx;*.xls" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show($"✅ Επιλέχθηκε: {ofd.FileName}\n\nTODO: Έναρξη Wizard αντιστοίχισης στηλών.");
                    }
                }
            };

            var lblInfo = new Label
            {
                Text = "Βήματα:\n1. Επίλεξε αρχείο Excel.\n2. Αντιστοίχισε στήλες Excel με πεδία βάσης.\n3. Έλεγχος διπλοεγγραφών (Duplicates).\n4. Εκτέλεση εισαγωγής.",
                Location = new Point(20, 80),
                Size = new Size(560, 150),
                Font = new Font("Segoe UI", 10F)
            };
            grp.Controls.Add(btnBrowse);
            grp.Controls.Add(lblInfo);
            pnlContent.Controls.Add(grp);
        }

        private void RenderExportReportBuilder(string reportName)
        {
            var grp = new GroupBox { Text = $"📤 {reportName}", Location = new Point(20, 20), Size = new Size(600, 300) };

            var lblFilter = new Label { Text = "Φίλτρα:", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            var dtpFrom = new DateTimePicker { Location = new Point(100, 27), Format = DateTimePickerFormat.Short };
            var dtpTo = new DateTimePicker { Location = new Point(220, 27), Format = DateTimePickerFormat.Short };

            var cmbGlAccount = new ComboBox { Location = new Point(100, 60), Size = new Size(300, 26), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGlAccount.Items.Add("-- Όλοι οι Λογαριασμοί --");

            var btnGenerate = CreateActionBtn("🚀 Δημιουργία Report", 20, 100, Color.Green);
            btnGenerate.Click += (s, e) => MessageBox.Show("TODO: Generation of PDF/Excel based on selected filters and GL mapping.");

            grp.Controls.Add(lblFilter);
            grp.Controls.Add(dtpFrom);
            grp.Controls.Add(dtpTo);
            grp.Controls.Add(new Label { Text = "Λογ. Σχέδιο:", Location = new Point(20, 63), AutoSize = true });
            grp.Controls.Add(cmbGlAccount);
            grp.Controls.Add(btnGenerate);

            pnlContent.Controls.Add(grp);
        }

        private void HandleBranchDetail(string tag)
        {
            var parts = tag.Split('_');
            if (int.TryParse(parts[2], out int bid) && int.TryParse(parts[3], out int cid))
                RenderBranchDashboard(bid, cid);
        }

        private void HandleSeriesList(string tag, bool isBranch)
        {
            if (int.TryParse(tag.Split('_')[2], out int id))
                RenderSeriesList(id, isBranch);
        }

        private void RenderMyDataProvidersSection()
        {
            var btnManage = CreateActionBtn("➕ Άνοιγμα Λίστας Παρόχων...", 20, 20, COLOR_MYDATA);
            btnManage.Click += (s, e) => { using (var f = new ProviderListForm()) f.ShowDialog(); };
            pnlContent.Controls.Add(btnManage);

            var lbl = new Label { Text = "Διαχείριση API Keys για σύνδεση με ΑΑΔΕ ή τρίτους παρόχους.", Location = new Point(20, 70), AutoSize = true };
            pnlContent.Controls.Add(lbl);
        }

        private void RenderMyDataDocTypesSection()
        {
            var dgv = CreateStandardGrid(COLOR_MYDATA);
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Εσωτερικός Τύπος", DataPropertyName = "InternalTypeName", Width = 200 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "AADE Code", DataPropertyName = "AadeCode", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Περιγραφή AADE", DataPropertyName = "AadeDesc", Width = 300 });

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var data = conn.Query(@"
                    SELECT dt.DocTypeName AS InternalTypeName, dt.MyDataCode AS AadeCode, dt.DocTypeCategory AS AadeDesc 
                    FROM DocumentTypes dt WHERE dt.IsActive = 1").ToList();
                dgv.DataSource = data;
            }
            catch { }

            pnlContent.Controls.Add(dgv);
        }

        private void RenderCompaniesList()
        {
            var dgv = CreateStandardGrid(COLOR_GLOBAL);
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Όνομα", DataPropertyName = "CompanyName", Width = 300 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ΑΦΜ", DataPropertyName = "VatNumber", Width = 150 });

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                dgv.DataSource = conn.Query("SELECT Id, CompanyName, VatNumber FROM Companies").ToList();
            }
            catch { }

            pnlContent.Controls.Add(dgv);
        }

        private void RenderBranchDashboard(int branchId, int companyId)
        {
            var lbl = new Label { Text = $"🏬 Dashboard Υποκαταστήματος ID: {branchId}", Location = new Point(20, 20), Font = new Font("Segoe UI", 14F, FontStyle.Bold) };
            pnlContent.Controls.Add(lbl);
        }

        // ✅ FIX: Complete implementation of RenderSeriesList with proper context handling
        private void RenderSeriesList(int contextId, bool isBranchSpecific)
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString());

                // Determine Company ID based on scope
                int compId = _currentCompanyId;
                if (isBranchSpecific)
                {
                    // If branch specific, we need to find the company ID associated with this branch
                    // For simplicity in this demo, assuming _currentCompanyId is set correctly by tree selection
                    // Or fetch it:
                    var branchInfo = conn.QueryFirstOrDefault<dynamic>("SELECT CompanyId FROM CompanyBranches WHERE Id = @Bid", new { Bid = contextId });
                    if (branchInfo != null) compId = Convert.ToInt32(branchInfo.CompanyId);
                }

                string sql = @"
                    SELECT ds.Id, ds.Code AS SeriesCode, ds.Name AS SeriesName, ds.Prefix, ds.NextNumber, ds.NumberLength,
                           dt.DocTypeCode AS TypeCode, 
                           dt.DocTypeName AS TypeName,
                           ds.AadeDocTypeCode, ds.IsAnnualReset, ds.PrefixFormat
                    FROM DocumentSeries ds
                    INNER JOIN DocumentTypes dt ON ds.DocumentTypeId = dt.Id
                    WHERE ds.IsActive = 1 ";

                object paramsObj = null;

                if (isBranchSpecific)
                {
                    sql += " AND ds.BranchId = @CtxId";
                    paramsObj = new { CtxId = contextId };
                }
                else
                {
                    sql += " AND ds.CompanyId = @CtxId AND ds.BranchId IS NULL";
                    paramsObj = new { CtxId = compId };
                }

                var data = conn.Query(sql, paramsObj).ToList();

                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AutoGenerateColumns = false,
                    ReadOnly = true,
                    RowHeadersVisible = false,
                    BackgroundColor = Color.White,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                    {
                        BackColor = COLOR_SETTING,
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                    }
                };

                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Κωδικός Σειράς", DataPropertyName = "SeriesCode", Width = 120 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Όνομα Σειράς", DataPropertyName = "SeriesName", Width = 200 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Τύπος Εγγράφου", DataPropertyName = "TypeName", Width = 250 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "AADE Code", DataPropertyName = "AadeDocTypeCode", Width = 80 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Πρόθεμα", DataPropertyName = "Prefix", Width = 60 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Επόμενος Αρ.", DataPropertyName = "NextNumber", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
                dgv.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Ετήσιο Reset?", DataPropertyName = "IsAnnualReset", Width = 70 });

                dgv.DataSource = data;
                // Μέσα στη RenderSeriesList, μετά το dgv.DataSource = data;

                dgv.CellDoubleClick += (s, e) => {
                    if (e.RowIndex >= 0)
                    {
                        int id = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Id"].Value);
                        using (var frm = new Forms.SeriesConfigForm(id))
                        {
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                // ✅ FIX: Κατασκευάζουμε το tag δυναμικά βάσει των παραμέτρων της μεθόδου
                                string refreshTag = isBranchSpecific ? $"SERIES_BRANCH_{contextId}" : $"SERIES_COMPANY_{_currentCompanyId}";
                                RenderContent(refreshTag);
                            }
                        }
                    }
                };
                // Add buttons ABOVE grid
                var pnlTop = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.White };

                var btnAdd = CreateActionBtn(isBranchSpecific ? "➕ Νέα Σειρά Καταστήματος" : "➕ Νέα Σειρά Εταιρείας", 10, 10, Color.Green);
                btnAdd.Click += (s, e) => OpenSeriesEditor(compId, isBranchSpecific ? contextId : (int?)null, null);

                var btnEdit = CreateActionBtn("✏️ Επεξεργασία", 150, 10, Color.Blue);
                btnEdit.Click += (s, e) => {
                    if (dgv.CurrentRow != null && dgv.CurrentRow.Cells["Id"].Value != null)
                    {
                        int id = Convert.ToInt32(dgv.CurrentRow.Cells["Id"].Value);
                        OpenSeriesEditor(compId, isBranchSpecific ? contextId : (int?)null, id);
                    }
                };

                pnlTop.Controls.Add(btnAdd);
                pnlTop.Controls.Add(btnEdit);

                pnlContent.Controls.Add(dgv);
                pnlContent.Controls.Add(pnlTop);
                pnlContent.Controls.SetChildIndex(dgv, 0); // Move grid below panel
            }
            catch (Exception ex) { MessageBox.Show("Σφάλμα λίστας σειρών: " + ex.Message); }
        }

        // ✅ FIX: Implemented Missing Method OpenSeriesEditor
        private void OpenSeriesEditor(int companyId, int? branchId, int? seriesId)
        {
            // Note: Ensure SeriesEditForm.cs exists in your project
            // If not, comment out the body and show a message
            try
            {
                using (var f = new SeriesEditForm(companyId, branchId, seriesId))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh view
                        string refreshTag = branchId.HasValue ? $"SERIES_BRANCH_{branchId.Value}" : $"SERIES_COMPANY_{companyId}";
                        RenderContent(refreshTag);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open Series Editor. Make sure 'SeriesEditForm.cs' is added to the project.\n\nError: " + ex.Message);
            }
        }

        // --- UPDATED: POS Terminals View Handler ---
        private void HandlePosTerminals(string tag)
        {
            var parts = tag.Split('_');
            if (parts.Length < 4 || !int.TryParse(parts[2], out int branchId) || !int.TryParse(parts[3], out int companyId))
            {
                MessageBox.Show($"Σφάλμα Parsing Tag: '{tag}'");
                return;
            }

            var grp = new GroupBox { Text = "💳 Διαχείριση POS Τερματικών", Location = new Point(20, 20), Size = new Size(800, 450) };

            var btnAdd = CreateActionBtn("➕ Νέο Τερματικό", 20, 30, COLOR_POS);

            btnAdd.Click += (s, e) => {
                try
                {
                    using (var f = new PosTerminalsForm(branchId, companyId))
                    {
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            RenderContent(tag);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening POS Form: " + ex.Message);
                }
            };

            var lblInfo = new Label
            {
                Text = $"Διαχείριση συσκευών για Υποκατάσταση ID: {branchId} (Εταιρεία ID: {companyId})",
                Location = new Point(20, 80),
                Size = new Size(760, 30),
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.Gray
            };
            grp.Controls.Add(btnAdd);
            grp.Controls.Add(lblInfo);

            var dgv = CreateStandardGrid(COLOR_POS);
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50, Visible = false });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Όνομα Συσκευής", DataPropertyName = "TerminalName", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "TID", DataPropertyName = "Tid", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "IP Address", DataPropertyName = "IpAddress", Width = 120 });
            dgv.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Online", DataPropertyName = "IsActive", Width = 60 });

            dgv.CellDoubleClick += (s, e) => {
                if (e.RowIndex >= 0 && dgv.Rows[e.RowIndex].Cells["Id"].Value != null)
                {
                    int termId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Id"].Value);
                    using (var f = new PosTerminalsForm(branchId, companyId, termId))
                    {
                        if (f.ShowDialog() == DialogResult.OK) RenderContent(tag);
                    }
                }
            };

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                dgv.DataSource = conn.Query(@"
                    SELECT Id, TerminalName, Tid, IpAddress, IsActive 
                    FROM POSTerminals 
                    WHERE BranchId = @Bid 
                    ORDER BY TerminalName",
                    new { Bid = branchId }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης λίστας POS: " + ex.Message);
            }

            dgv.Location = new Point(20, 120);
            dgv.Size = new Size(760, 300);
            grp.Controls.Add(dgv);

            pnlContent.Controls.Add(grp);
        }

        // --- NEW: Copy Series Utility ---
        private void HandleCopySeries(string tag)
        {
            if (!int.TryParse(tag.Split('_')[3], out int targetBranchId)) return;

            var grp = new GroupBox { Text = "📋 Αντιγραφή Σειρών Παραστατικών", Location = new Point(20, 20), Size = new Size(600, 300) };

            var lblSource = new Label { Text = "Πηγή Σειρών (Από ποιο κατάστημα):", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            var cmbSourceBranch = new ComboBox { Location = new Point(20, 50), Size = new Size(300, 26), DropDownStyle = ComboBoxStyle.DropDownList };

            try
            {
                using var conn = new SqlConnection(GetConnectionString());
                var branches = conn.Query<BranchDto>("SELECT Id, BranchName FROM CompanyBranches WHERE IsActive = 1 AND Id != @TargetId", new { TargetId = targetBranchId }).ToList();

                foreach (var b in branches)
                {
                    cmbSourceBranch.Items.Add(new { Text = $"{b.BranchName} (ID: {b.Id})", Value = b.Id });
                }

                if (cmbSourceBranch.Items.Count > 0)
                {
                    cmbSourceBranch.DisplayMember = "Text";
                    cmbSourceBranch.ValueMember = "Value";
                }
            }
            catch { }

            var btnCopy = CreateActionBtn("🚀 Εκτέλεση Αντιγραφής", 20, 100, COLOR_COPY_SERIES);
            btnCopy.Click += (s, e) => {
                if (cmbSourceBranch.SelectedItem == null) { MessageBox.Show("Επίλεξε πηγή!"); return; }
                dynamic selectedItem = cmbSourceBranch.SelectedItem;
                int sourceBranchId = Convert.ToInt32(selectedItem.Value);
                MessageBox.Show($"TODO: Copying series from Branch {sourceBranchId} to Branch {targetBranchId}...");
            };

            grp.Controls.Add(lblSource);
            grp.Controls.Add(cmbSourceBranch);
            grp.Controls.Add(btnCopy);

            var lblWarning = new Label
            {
                Text = "⚠️ Προσοχή: Η αντιγραφή θα δημιουργήσει νέες σειρές με τους ίδιους κωδικούς/prefixes στο目标 κατάστημα.",
                Location = new Point(20, 150),
                Size = new Size(560, 60),
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic)
            };
            grp.Controls.Add(lblWarning);

            pnlContent.Controls.Add(grp);
        }

        private void RenderWelcomeScreen()
        {
            var lblTitle = new Label
            {
                Text = "⚙️ Κεντρικό Hub Ρυθμίσεων Kryptalis ERP",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = COLOR_GLOBAL,
                Location = new Point(20, 20),
                AutoSize = true
            };

            var lblDesc = new Label
            {
                Text =
                    "Επέλεξε μια ενότητα από το αριστερό μενού για διαμόρφωση:\n\n" +
                    "• 📂 Γενικές Ρυθμίσεις: Χρήστες, Email, Εκτυπωτές, Backup.\n" +
                    "• 💼 Λογιστικό Πλάνο: Γενικό Λογιστικό Σχέδιο, ΦΠΑ, Απαλλαγές, Περίοδοι.\n" +
                    "• 📦 Master Data: Μονάδες Μέτρησης, Κατηγορίες, Μεταφορές, Τρόποι Πληρωμής.\n" +
                    "• 🇬 myDATA & TAXISNET: Πάροχοι API, Mapping Κωδικών ΑΑΔΕ, Γενικές Παράμετροι.\n" +
                    "• 🔄 Εισαγωγή / Εξαγωγή: Μαζική εισαγωγή δεδομένων από Excel και εξαγωγή αναφορών.\n" +
                    "• 🏢 Διαχείριση Εταιρειών: \n" +
                    "   - Στοιχεία κάθε εταιρείας.\n" +
                    "   - Υποκαταστήματα (Branches).\n" +
                    "   - POS Τερματικά ανά κατάστημα.\n" +
                    "   - Σειρές Παραστατικών & Αντιγραφή Ρυθμίσεων.",

                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.DimGray,
                Location = new Point(20, 70),
                Size = new Size(700, 300),
                TextAlign = ContentAlignment.TopLeft
            };

            pnlContent.Controls.Add(lblTitle);
            pnlContent.Controls.Add(lblDesc);

            var btnExpandAll = CreateActionBtn(" Επέκταση Όλων των Εταιρειών", 20, 380, Color.Gray);
            btnExpandAll.Click += (s, e) => tvCategories.ExpandAll();
            pnlContent.Controls.Add(btnExpandAll);
        }

        private void RenderPlaceholder(string title, string desc)
        {
            var lbl = new Label { Text = $"{title}\n\n{desc}", Location = new Point(20, 20), Font = new Font("Segoe UI", 12F), ForeColor = Color.Gray, AutoSize = true };
            pnlContent.Controls.Add(lbl);
        }

        // ==========================================
        // HELPERS
        // ==========================================

        // ✅ FIX: Updated helper to accept custom header color
        private DataGridView CreateStandardGrid(Color headerColor = default)
        {
            if (headerColor == default) headerColor = COLOR_GLOBAL;

            return new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = headerColor,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                }
            };
        }

        private Button CreateActionBtn(string text, int x, int y, Color color)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(200, 35),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
        }

        private void OpenCompanyEditor(int? id)
        {
            MessageBox.Show($"Άνοιγμα Editor για Εταιρεία ID: {id ?? 0}");
        }

        private string GetConnectionString() => Program.Configuration?.GetConnectionString("DefaultConnection") ?? "Server=DESKTOP-HVVQRS8\\EPSILON12;Database=KryptalisERP; User Id=sa; Password=epsilonsa; TrustServerCertificate=True;";

        public class CompanyDto { public int Id { get; set; } public string CompanyName { get; set; } }
        public class BranchDto { public int Id { get; set; } public string BranchName { get; set; } public string BranchCode { get; set; } }
    }
}