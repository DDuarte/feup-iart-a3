using IART_A3.StateRepresentation;

namespace GUI
{
    partial class BuilderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.lotsTabPage = new System.Windows.Forms.TabPage();
            this.lotsDataGridView = new System.Windows.Forms.DataGridView();
            this.landuses2TabPage = new System.Windows.Forms.TabPage();
            this.landusesDataGridView = new System.Windows.Forms.DataGridView();
            this.nameLandusesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.lotTabPage = new System.Windows.Forms.TabPage();
            this.lotApplyButton = new System.Windows.Forms.Button();
            this.lotNameLabel = new System.Windows.Forms.Label();
            this.lotNameTextBox = new System.Windows.Forms.TextBox();
            this.steepGroupBox = new System.Windows.Forms.GroupBox();
            this.verySteepRadioButton = new System.Windows.Forms.RadioButton();
            this.steepRadioButton = new System.Windows.Forms.RadioButton();
            this.moderatelySteepRadioButton = new System.Windows.Forms.RadioButton();
            this.flatRadioButton = new System.Windows.Forms.RadioButton();
            this.poorSoilCheckBox = new System.Windows.Forms.CheckBox();
            this.euroLabel = new System.Windows.Forms.Label();
            this.priceLabel = new System.Windows.Forms.Label();
            this.priceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.highwayTabPage = new System.Windows.Forms.TabPage();
            this.highwayApplyButton = new System.Windows.Forms.Button();
            this.waterTabPage = new System.Windows.Forms.TabPage();
            this.waterApplyButton = new System.Windows.Forms.Button();
            this.landusesTabPage = new System.Windows.Forms.TabPage();
            this.landusesApplyButton = new System.Windows.Forms.Button();
            this.cemeteryLabel = new System.Windows.Forms.Label();
            this.cemeteryNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.dumpLabel = new System.Windows.Forms.Label();
            this.dumpNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.housingComplexLabel = new System.Windows.Forms.Label();
            this.housingComplexNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.apartmentsLabel = new System.Windows.Forms.Label();
            this.apartmentsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.recreationalLabel = new System.Windows.Forms.Label();
            this.recreationalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poorSoilColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.steepColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.lotsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lotsDataGridView)).BeginInit();
            this.landuses2TabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.landusesDataGridView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.lotTabPage.SuspendLayout();
            this.steepGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priceNumericUpDown)).BeginInit();
            this.highwayTabPage.SuspendLayout();
            this.waterTabPage.SuspendLayout();
            this.landusesTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cemeteryNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dumpNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.housingComplexNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.apartmentsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recreationalNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPanel
            // 
            this.gridPanel.Location = new System.Drawing.Point(4, 4);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Size = new System.Drawing.Size(550, 550);
            this.gridPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl2);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(561, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(427, 550);
            this.panel1.TabIndex = 1;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.lotsTabPage);
            this.tabControl2.Controls.Add(this.landuses2TabPage);
            this.tabControl2.Location = new System.Drawing.Point(12, 296);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(412, 225);
            this.tabControl2.TabIndex = 10;
            // 
            // lotsTabPage
            // 
            this.lotsTabPage.Controls.Add(this.lotsDataGridView);
            this.lotsTabPage.Location = new System.Drawing.Point(4, 22);
            this.lotsTabPage.Name = "lotsTabPage";
            this.lotsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.lotsTabPage.Size = new System.Drawing.Size(404, 199);
            this.lotsTabPage.TabIndex = 0;
            this.lotsTabPage.Text = "Lots";
            this.lotsTabPage.UseVisualStyleBackColor = true;
            // 
            // lotsDataGridView
            // 
            this.lotsDataGridView.AllowUserToAddRows = false;
            this.lotsDataGridView.AllowUserToDeleteRows = false;
            this.lotsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lotsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameColumn,
            this.priceColumn,
            this.poorSoilColumn,
            this.steepColumn});
            this.lotsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lotsDataGridView.Location = new System.Drawing.Point(3, 3);
            this.lotsDataGridView.Name = "lotsDataGridView";
            this.lotsDataGridView.ReadOnly = true;
            this.lotsDataGridView.Size = new System.Drawing.Size(398, 193);
            this.lotsDataGridView.TabIndex = 0;
            // 
            // landuses2TabPage
            // 
            this.landuses2TabPage.Controls.Add(this.landusesDataGridView);
            this.landuses2TabPage.Location = new System.Drawing.Point(4, 22);
            this.landuses2TabPage.Name = "landuses2TabPage";
            this.landuses2TabPage.Padding = new System.Windows.Forms.Padding(3);
            this.landuses2TabPage.Size = new System.Drawing.Size(404, 199);
            this.landuses2TabPage.TabIndex = 1;
            this.landuses2TabPage.Text = "Landuses";
            this.landuses2TabPage.UseVisualStyleBackColor = true;
            // 
            // landusesDataGridView
            // 
            this.landusesDataGridView.AllowUserToAddRows = false;
            this.landusesDataGridView.AllowUserToDeleteRows = false;
            this.landusesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.landusesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameLandusesColumn,
            this.typeColumn});
            this.landusesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.landusesDataGridView.Location = new System.Drawing.Point(3, 3);
            this.landusesDataGridView.Name = "landusesDataGridView";
            this.landusesDataGridView.ReadOnly = true;
            this.landusesDataGridView.Size = new System.Drawing.Size(398, 193);
            this.landusesDataGridView.TabIndex = 0;
            // 
            // nameLandusesColumn
            // 
            this.nameLandusesColumn.HeaderText = "Name";
            this.nameLandusesColumn.Name = "nameLandusesColumn";
            this.nameLandusesColumn.ReadOnly = true;
            // 
            // typeColumn
            // 
            this.typeColumn.HeaderText = "Type";
            this.typeColumn.Items.AddRange(new object[] {
            "Recreational",
            "Apartments",
            "HousingComplex",
            "Cemetery",
            "Dump"});
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.ReadOnly = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.lotTabPage);
            this.tabControl1.Controls.Add(this.highwayTabPage);
            this.tabControl1.Controls.Add(this.waterTabPage);
            this.tabControl1.Controls.Add(this.landusesTabPage);
            this.tabControl1.Location = new System.Drawing.Point(8, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(409, 282);
            this.tabControl1.TabIndex = 9;
            // 
            // lotTabPage
            // 
            this.lotTabPage.Controls.Add(this.lotApplyButton);
            this.lotTabPage.Controls.Add(this.lotNameLabel);
            this.lotTabPage.Controls.Add(this.lotNameTextBox);
            this.lotTabPage.Controls.Add(this.steepGroupBox);
            this.lotTabPage.Controls.Add(this.poorSoilCheckBox);
            this.lotTabPage.Controls.Add(this.euroLabel);
            this.lotTabPage.Controls.Add(this.priceLabel);
            this.lotTabPage.Controls.Add(this.priceNumericUpDown);
            this.lotTabPage.Location = new System.Drawing.Point(4, 22);
            this.lotTabPage.Name = "lotTabPage";
            this.lotTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.lotTabPage.Size = new System.Drawing.Size(401, 256);
            this.lotTabPage.TabIndex = 0;
            this.lotTabPage.Text = "Lot";
            this.lotTabPage.UseVisualStyleBackColor = true;
            // 
            // lotApplyButton
            // 
            this.lotApplyButton.Location = new System.Drawing.Point(74, 220);
            this.lotApplyButton.Name = "lotApplyButton";
            this.lotApplyButton.Size = new System.Drawing.Size(75, 23);
            this.lotApplyButton.TabIndex = 8;
            this.lotApplyButton.Text = "Apply";
            this.lotApplyButton.UseVisualStyleBackColor = true;
            this.lotApplyButton.Click += new System.EventHandler(this.lotApplyButton_Click);
            // 
            // lotNameLabel
            // 
            this.lotNameLabel.AutoSize = true;
            this.lotNameLabel.Location = new System.Drawing.Point(6, 7);
            this.lotNameLabel.Name = "lotNameLabel";
            this.lotNameLabel.Size = new System.Drawing.Size(38, 13);
            this.lotNameLabel.TabIndex = 2;
            this.lotNameLabel.Text = "Name:";
            // 
            // lotNameTextBox
            // 
            this.lotNameTextBox.Location = new System.Drawing.Point(50, 4);
            this.lotNameTextBox.Name = "lotNameTextBox";
            this.lotNameTextBox.Size = new System.Drawing.Size(170, 20);
            this.lotNameTextBox.TabIndex = 1;
            // 
            // steepGroupBox
            // 
            this.steepGroupBox.Controls.Add(this.verySteepRadioButton);
            this.steepGroupBox.Controls.Add(this.steepRadioButton);
            this.steepGroupBox.Controls.Add(this.moderatelySteepRadioButton);
            this.steepGroupBox.Controls.Add(this.flatRadioButton);
            this.steepGroupBox.Location = new System.Drawing.Point(9, 78);
            this.steepGroupBox.Name = "steepGroupBox";
            this.steepGroupBox.Size = new System.Drawing.Size(211, 123);
            this.steepGroupBox.TabIndex = 7;
            this.steepGroupBox.TabStop = false;
            this.steepGroupBox.Text = "Steep";
            // 
            // verySteepRadioButton
            // 
            this.verySteepRadioButton.AutoSize = true;
            this.verySteepRadioButton.Location = new System.Drawing.Point(7, 90);
            this.verySteepRadioButton.Name = "verySteepRadioButton";
            this.verySteepRadioButton.Size = new System.Drawing.Size(77, 17);
            this.verySteepRadioButton.TabIndex = 3;
            this.verySteepRadioButton.TabStop = true;
            this.verySteepRadioButton.Tag = IART_A3.StateRepresentation.SteepType.VerySteep;
            this.verySteepRadioButton.Text = "Very Steep";
            this.verySteepRadioButton.UseVisualStyleBackColor = true;
            // 
            // steepRadioButton
            // 
            this.steepRadioButton.AutoSize = true;
            this.steepRadioButton.Location = new System.Drawing.Point(7, 67);
            this.steepRadioButton.Name = "steepRadioButton";
            this.steepRadioButton.Size = new System.Drawing.Size(53, 17);
            this.steepRadioButton.TabIndex = 2;
            this.steepRadioButton.TabStop = true;
            this.steepRadioButton.Tag = IART_A3.StateRepresentation.SteepType.Steep;
            this.steepRadioButton.Text = "Steep";
            this.steepRadioButton.UseVisualStyleBackColor = true;
            // 
            // moderatelySteepRadioButton
            // 
            this.moderatelySteepRadioButton.AutoSize = true;
            this.moderatelySteepRadioButton.Location = new System.Drawing.Point(7, 44);
            this.moderatelySteepRadioButton.Name = "moderatelySteepRadioButton";
            this.moderatelySteepRadioButton.Size = new System.Drawing.Size(108, 17);
            this.moderatelySteepRadioButton.TabIndex = 1;
            this.moderatelySteepRadioButton.TabStop = true;
            this.moderatelySteepRadioButton.Tag = IART_A3.StateRepresentation.SteepType.ModeratelySteep;
            this.moderatelySteepRadioButton.Text = "Moderately Steep";
            this.moderatelySteepRadioButton.UseVisualStyleBackColor = true;
            // 
            // flatRadioButton
            // 
            this.flatRadioButton.AutoSize = true;
            this.flatRadioButton.Location = new System.Drawing.Point(7, 20);
            this.flatRadioButton.Name = "flatRadioButton";
            this.flatRadioButton.Size = new System.Drawing.Size(42, 17);
            this.flatRadioButton.TabIndex = 0;
            this.flatRadioButton.TabStop = true;
            this.flatRadioButton.Tag = IART_A3.StateRepresentation.SteepType.Flat;
            this.flatRadioButton.Text = "Flat";
            this.flatRadioButton.UseVisualStyleBackColor = true;
            // 
            // poorSoilCheckBox
            // 
            this.poorSoilCheckBox.AutoSize = true;
            this.poorSoilCheckBox.Location = new System.Drawing.Point(9, 31);
            this.poorSoilCheckBox.Name = "poorSoilCheckBox";
            this.poorSoilCheckBox.Size = new System.Drawing.Size(68, 17);
            this.poorSoilCheckBox.TabIndex = 3;
            this.poorSoilCheckBox.Text = "Poor Soil";
            this.poorSoilCheckBox.UseVisualStyleBackColor = true;
            // 
            // euroLabel
            // 
            this.euroLabel.AutoSize = true;
            this.euroLabel.Location = new System.Drawing.Point(108, 57);
            this.euroLabel.Name = "euroLabel";
            this.euroLabel.Size = new System.Drawing.Size(25, 13);
            this.euroLabel.TabIndex = 6;
            this.euroLabel.Text = "M €";
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.Location = new System.Drawing.Point(6, 56);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(34, 13);
            this.priceLabel.TabIndex = 5;
            this.priceLabel.Text = "Price:";
            // 
            // priceNumericUpDown
            // 
            this.priceNumericUpDown.DecimalPlaces = 2;
            this.priceNumericUpDown.Location = new System.Drawing.Point(46, 54);
            this.priceNumericUpDown.Name = "priceNumericUpDown";
            this.priceNumericUpDown.Size = new System.Drawing.Size(56, 20);
            this.priceNumericUpDown.TabIndex = 4;
            // 
            // highwayTabPage
            // 
            this.highwayTabPage.Controls.Add(this.highwayApplyButton);
            this.highwayTabPage.Location = new System.Drawing.Point(4, 22);
            this.highwayTabPage.Name = "highwayTabPage";
            this.highwayTabPage.Size = new System.Drawing.Size(401, 256);
            this.highwayTabPage.TabIndex = 2;
            this.highwayTabPage.Text = "Highway";
            this.highwayTabPage.UseVisualStyleBackColor = true;
            // 
            // highwayApplyButton
            // 
            this.highwayApplyButton.Location = new System.Drawing.Point(74, 14);
            this.highwayApplyButton.Name = "highwayApplyButton";
            this.highwayApplyButton.Size = new System.Drawing.Size(75, 23);
            this.highwayApplyButton.TabIndex = 10;
            this.highwayApplyButton.Text = "Apply";
            this.highwayApplyButton.UseVisualStyleBackColor = true;
            this.highwayApplyButton.Click += new System.EventHandler(this.highwayApplyButton_Click);
            // 
            // waterTabPage
            // 
            this.waterTabPage.Controls.Add(this.waterApplyButton);
            this.waterTabPage.Location = new System.Drawing.Point(4, 22);
            this.waterTabPage.Name = "waterTabPage";
            this.waterTabPage.Size = new System.Drawing.Size(401, 256);
            this.waterTabPage.TabIndex = 3;
            this.waterTabPage.Text = "Lake / River";
            this.waterTabPage.UseVisualStyleBackColor = true;
            // 
            // waterApplyButton
            // 
            this.waterApplyButton.Location = new System.Drawing.Point(74, 14);
            this.waterApplyButton.Name = "waterApplyButton";
            this.waterApplyButton.Size = new System.Drawing.Size(75, 23);
            this.waterApplyButton.TabIndex = 11;
            this.waterApplyButton.Text = "Apply";
            this.waterApplyButton.UseVisualStyleBackColor = true;
            this.waterApplyButton.Click += new System.EventHandler(this.waterApplyButton_Click);
            // 
            // landusesTabPage
            // 
            this.landusesTabPage.Controls.Add(this.landusesApplyButton);
            this.landusesTabPage.Controls.Add(this.cemeteryLabel);
            this.landusesTabPage.Controls.Add(this.cemeteryNumericUpDown);
            this.landusesTabPage.Controls.Add(this.dumpLabel);
            this.landusesTabPage.Controls.Add(this.dumpNumericUpDown);
            this.landusesTabPage.Controls.Add(this.housingComplexLabel);
            this.landusesTabPage.Controls.Add(this.housingComplexNumericUpDown);
            this.landusesTabPage.Controls.Add(this.apartmentsLabel);
            this.landusesTabPage.Controls.Add(this.apartmentsNumericUpDown);
            this.landusesTabPage.Controls.Add(this.recreationalLabel);
            this.landusesTabPage.Controls.Add(this.recreationalNumericUpDown);
            this.landusesTabPage.Location = new System.Drawing.Point(4, 22);
            this.landusesTabPage.Name = "landusesTabPage";
            this.landusesTabPage.Size = new System.Drawing.Size(401, 256);
            this.landusesTabPage.TabIndex = 4;
            this.landusesTabPage.Text = "Landuses";
            this.landusesTabPage.UseVisualStyleBackColor = true;
            // 
            // landusesApplyButton
            // 
            this.landusesApplyButton.Location = new System.Drawing.Point(76, 160);
            this.landusesApplyButton.Name = "landusesApplyButton";
            this.landusesApplyButton.Size = new System.Drawing.Size(75, 23);
            this.landusesApplyButton.TabIndex = 11;
            this.landusesApplyButton.Text = "Apply";
            this.landusesApplyButton.UseVisualStyleBackColor = true;
            this.landusesApplyButton.Click += new System.EventHandler(this.landusesApplyButton_Click);
            // 
            // cemeteryLabel
            // 
            this.cemeteryLabel.AutoSize = true;
            this.cemeteryLabel.Location = new System.Drawing.Point(58, 121);
            this.cemeteryLabel.Name = "cemeteryLabel";
            this.cemeteryLabel.Size = new System.Drawing.Size(51, 13);
            this.cemeteryLabel.TabIndex = 9;
            this.cemeteryLabel.Text = "Cemetery";
            // 
            // cemeteryNumericUpDown
            // 
            this.cemeteryNumericUpDown.Location = new System.Drawing.Point(16, 119);
            this.cemeteryNumericUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.cemeteryNumericUpDown.Name = "cemeteryNumericUpDown";
            this.cemeteryNumericUpDown.Size = new System.Drawing.Size(36, 20);
            this.cemeteryNumericUpDown.TabIndex = 8;
            this.cemeteryNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dumpLabel
            // 
            this.dumpLabel.AutoSize = true;
            this.dumpLabel.Location = new System.Drawing.Point(58, 95);
            this.dumpLabel.Name = "dumpLabel";
            this.dumpLabel.Size = new System.Drawing.Size(35, 13);
            this.dumpLabel.TabIndex = 7;
            this.dumpLabel.Text = "Dump";
            // 
            // dumpNumericUpDown
            // 
            this.dumpNumericUpDown.Location = new System.Drawing.Point(16, 93);
            this.dumpNumericUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.dumpNumericUpDown.Name = "dumpNumericUpDown";
            this.dumpNumericUpDown.Size = new System.Drawing.Size(36, 20);
            this.dumpNumericUpDown.TabIndex = 6;
            this.dumpNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // housingComplexLabel
            // 
            this.housingComplexLabel.AutoSize = true;
            this.housingComplexLabel.Location = new System.Drawing.Point(58, 69);
            this.housingComplexLabel.Name = "housingComplexLabel";
            this.housingComplexLabel.Size = new System.Drawing.Size(89, 13);
            this.housingComplexLabel.TabIndex = 5;
            this.housingComplexLabel.Text = "Housing Complex";
            // 
            // housingComplexNumericUpDown
            // 
            this.housingComplexNumericUpDown.Location = new System.Drawing.Point(16, 67);
            this.housingComplexNumericUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.housingComplexNumericUpDown.Name = "housingComplexNumericUpDown";
            this.housingComplexNumericUpDown.Size = new System.Drawing.Size(36, 20);
            this.housingComplexNumericUpDown.TabIndex = 4;
            this.housingComplexNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // apartmentsLabel
            // 
            this.apartmentsLabel.AutoSize = true;
            this.apartmentsLabel.Location = new System.Drawing.Point(58, 43);
            this.apartmentsLabel.Name = "apartmentsLabel";
            this.apartmentsLabel.Size = new System.Drawing.Size(60, 13);
            this.apartmentsLabel.TabIndex = 3;
            this.apartmentsLabel.Text = "Apartments";
            // 
            // apartmentsNumericUpDown
            // 
            this.apartmentsNumericUpDown.Location = new System.Drawing.Point(16, 41);
            this.apartmentsNumericUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.apartmentsNumericUpDown.Name = "apartmentsNumericUpDown";
            this.apartmentsNumericUpDown.Size = new System.Drawing.Size(36, 20);
            this.apartmentsNumericUpDown.TabIndex = 2;
            this.apartmentsNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // recreationalLabel
            // 
            this.recreationalLabel.AutoSize = true;
            this.recreationalLabel.Location = new System.Drawing.Point(58, 17);
            this.recreationalLabel.Name = "recreationalLabel";
            this.recreationalLabel.Size = new System.Drawing.Size(67, 13);
            this.recreationalLabel.TabIndex = 1;
            this.recreationalLabel.Text = "Recreational";
            // 
            // recreationalNumericUpDown
            // 
            this.recreationalNumericUpDown.Location = new System.Drawing.Point(16, 15);
            this.recreationalNumericUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.recreationalNumericUpDown.Name = "recreationalNumericUpDown";
            this.recreationalNumericUpDown.Size = new System.Drawing.Size(36, 20);
            this.recreationalNumericUpDown.TabIndex = 0;
            this.recreationalNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // priceColumn
            // 
            this.priceColumn.HeaderText = "Price";
            this.priceColumn.Name = "priceColumn";
            this.priceColumn.ReadOnly = true;
            // 
            // poorSoilColumn
            // 
            this.poorSoilColumn.HeaderText = "Poor Soil";
            this.poorSoilColumn.Name = "poorSoilColumn";
            this.poorSoilColumn.ReadOnly = true;
            // 
            // steepColumn
            // 
            this.steepColumn.HeaderText = "Steep";
            this.steepColumn.Items.AddRange(new object[] {
            "Flat",
            "ModeratelySteep",
            "Steep",
            "VerySteep"});
            this.steepColumn.Name = "steepColumn";
            this.steepColumn.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gridPanel);
            this.Name = "BuilderForm";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.lotsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lotsDataGridView)).EndInit();
            this.landuses2TabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.landusesDataGridView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.lotTabPage.ResumeLayout(false);
            this.lotTabPage.PerformLayout();
            this.steepGroupBox.ResumeLayout(false);
            this.steepGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priceNumericUpDown)).EndInit();
            this.highwayTabPage.ResumeLayout(false);
            this.waterTabPage.ResumeLayout(false);
            this.landusesTabPage.ResumeLayout(false);
            this.landusesTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cemeteryNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dumpNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.housingComplexNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.apartmentsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recreationalNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel gridPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lotNameLabel;
        private System.Windows.Forms.TextBox lotNameTextBox;
        private System.Windows.Forms.CheckBox poorSoilCheckBox;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.NumericUpDown priceNumericUpDown;
        private System.Windows.Forms.Label euroLabel;
        private System.Windows.Forms.GroupBox steepGroupBox;
        private System.Windows.Forms.RadioButton verySteepRadioButton;
        private System.Windows.Forms.RadioButton steepRadioButton;
        private System.Windows.Forms.RadioButton moderatelySteepRadioButton;
        private System.Windows.Forms.RadioButton flatRadioButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage lotTabPage;
        private System.Windows.Forms.TabPage highwayTabPage;
        private System.Windows.Forms.TabPage waterTabPage;
        private System.Windows.Forms.Button lotApplyButton;
        private System.Windows.Forms.Button highwayApplyButton;
        private System.Windows.Forms.Button waterApplyButton;
        private System.Windows.Forms.TabPage landusesTabPage;
        private System.Windows.Forms.Label recreationalLabel;
        private System.Windows.Forms.NumericUpDown recreationalNumericUpDown;
        private System.Windows.Forms.Label cemeteryLabel;
        private System.Windows.Forms.NumericUpDown cemeteryNumericUpDown;
        private System.Windows.Forms.Label dumpLabel;
        private System.Windows.Forms.NumericUpDown dumpNumericUpDown;
        private System.Windows.Forms.Label housingComplexLabel;
        private System.Windows.Forms.NumericUpDown housingComplexNumericUpDown;
        private System.Windows.Forms.Label apartmentsLabel;
        private System.Windows.Forms.NumericUpDown apartmentsNumericUpDown;
        private System.Windows.Forms.Button landusesApplyButton;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage lotsTabPage;
        private System.Windows.Forms.DataGridView lotsDataGridView;
        private System.Windows.Forms.TabPage landuses2TabPage;
        private System.Windows.Forms.DataGridView landusesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameLandusesColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn poorSoilColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn steepColumn;

    }
}

