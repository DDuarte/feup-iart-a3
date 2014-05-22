using IART_A3.StateRepresentation;

namespace GUI
{
    partial class Form1
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
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.lotTabPage.SuspendLayout();
            this.steepGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priceNumericUpDown)).BeginInit();
            this.highwayTabPage.SuspendLayout();
            this.waterTabPage.SuspendLayout();
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
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(561, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 545);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.lotTabPage);
            this.tabControl1.Controls.Add(this.highwayTabPage);
            this.tabControl1.Controls.Add(this.waterTabPage);
            this.tabControl1.Location = new System.Drawing.Point(8, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(234, 388);
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
            this.lotTabPage.Size = new System.Drawing.Size(226, 362);
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
            this.highwayTabPage.Size = new System.Drawing.Size(226, 362);
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
            this.waterTabPage.Size = new System.Drawing.Size(226, 362);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gridPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.lotTabPage.ResumeLayout(false);
            this.lotTabPage.PerformLayout();
            this.steepGroupBox.ResumeLayout(false);
            this.steepGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priceNumericUpDown)).EndInit();
            this.highwayTabPage.ResumeLayout(false);
            this.waterTabPage.ResumeLayout(false);
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

    }
}

