﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IART_A3.StateRepresentation;

namespace GUI
{
    public partial class Form1 : Form
    {
        private readonly HashSet<Button> _selectedButtons = new HashSet<Button>();
        private double _centroidX;
        private double _centroidY;

        private const int size = 55;
        private const int rows = 10;
        private const int cols = 10;

        private static bool IntersectsWith(Button btn1, Button btn2)
        {
            if (btn1 == btn2)
                return false;

            var bounds1 = btn1.Bounds;
            var bounds2 = btn1.Bounds;
            bounds1.Inflate(size / 2, -size / 2);
            bounds2.Inflate(-size / 2, size / 2);

            return btn2.Bounds.IntersectsWith(bounds1) || btn2.Bounds.IntersectsWith(bounds2);
        }

        private void ToggleButton(Button button)
        {
            if (_selectedButtons.Count == 0)
                _centroidX = _centroidY = -1;
            else
            {
                _centroidX = _selectedButtons.Average(btn => btn.Location.X + size/2);
                _centroidY = _selectedButtons.Average(btn => btn.Location.Y - size/2);
            }

            if (_selectedButtons.Add(button))
            {
                if (!ValidArea())
                {
                    _selectedButtons.Remove(button);
                }
                else
                {
                    button.BackColor = Color.Green;
                    button.ForeColor = Color.Green;
                }
            }
            else
            {
                _selectedButtons.Remove(button);

                if (!ValidArea())
                {
                    _selectedButtons.Add(button);
                }
                else
                {
                    button.BackColor = Color.Gray;
                    button.ForeColor = Color.Gray;
                }
            }
        }

        private bool ValidArea()
        {
            if (_selectedButtons.Count <= 1)
                return true;

            /*
            var minX = _selectedButtons.Min(button => button.Location.X);
            var minY = _selectedButtons.Min(button => button.Location.Y);
            var maxX = _selectedButtons.Max(button => button.Location.X) + size;
            var maxY = _selectedButtons.Max(button => button.Location.Y) + size;

            var rectangularArea = (maxX - minX) * (maxY - minY);

            var realArea = _selectedButtons.Sum(btn => size * size);

            return rectangularArea == realArea;
            */

            return _selectedButtons.Count <= 1 ||
                _selectedButtons.All(btn1 => _selectedButtons.Any(button => IntersectsWith(button, btn1)));
        }

        public Form1()
        {
            InitializeComponent();

            //gridPanel.Size = new Size(size * rows, size * cols);
            //gridPanel.Location = new Point((ClientSize.Width - gridPanel.Size.Width) / 2, (ClientSize.Height - gridPanel.Size.Height) / 2);

            for (var i = 0; i < rows; ++i)
            {
                for (var j = 0; j < cols; ++j)
                {
                    var btn = new Button
                    {
                        Name = "btn" + i + j,
                        Bounds = new Rectangle(size*i, size*j, size, size),
                        BackColor = Color.Gray,
                        ForeColor = Color.Gray,
                        Margin = new Padding(0),
                        FlatStyle = FlatStyle.Flat,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        TabStop = false
                    };

                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += (sender, args) => ToggleButton((Button) sender);

                    gridPanel.Controls.Add(btn);
                }
            }
        }

        private void highwayApplyButton_Click(object sender, System.EventArgs e)
        {
            foreach (var btn in _selectedButtons)
            {
                btn.BackColor = Color.SlateGray;
                btn.ForeColor = Color.SlateGray;
                btn.BackgroundImage = new Bitmap(Properties.Resources.AsphaltTexture);
            }

            _selectedButtons.Clear();
        }

        private void waterApplyButton_Click(object sender, System.EventArgs e)
        {
            foreach (var btn in _selectedButtons)
            {
                btn.BackColor = Color.DodgerBlue;
                btn.ForeColor = Color.DodgerBlue;
                btn.BackgroundImage = new Bitmap(Properties.Resources.WaterTexture);
            }

            _selectedButtons.Clear();
        }

        private void lotApplyButton_Click(object sender, System.EventArgs e)
        {
            Bitmap img = null;
            var name = lotNameTextBox.Text;
            var poorSoil = poorSoilCheckBox.Checked;
            var price = Convert.ToDouble(priceNumericUpDown.Value);
            var distanceLake = 0.0;
            var distanceHighway = 0.0;
            var steepType = SteepType.Flat;

            if (flatRadioButton.Checked)
            {
                img = poorSoil ? Properties.Resources.SandFlatTexture : Properties.Resources.DirtFlatTexture;
                steepType = SteepType.Flat;
            }
            else if (moderatelySteepRadioButton.Checked)
            {
                img = poorSoil
                    ? Properties.Resources.SandModeratelySteepTexture
                    : Properties.Resources.DirtModeratelySteepTexture;
                steepType = SteepType.ModeratelySteep;
            }
            else if (steepRadioButton.Checked)
            {
                img = poorSoil ? Properties.Resources.SandSteepTexture : Properties.Resources.DirtSteepTexture;
                steepType = SteepType.Steep;
            }
            else if (verySteepRadioButton.Checked)
            {
                img = poorSoil ? Properties.Resources.SandVerySteepTexture : Properties.Resources.DirtVerySteepTexture;
                steepType = SteepType.VerySteep;
            }

            if (img == null)
                return;

            foreach (var btn in _selectedButtons)
            {
                btn.BackColor = poorSoil ? Color.SandyBrown : Color.Brown;
                btn.ForeColor = poorSoil ? Color.SandyBrown : Color.Brown;
                btn.BackgroundImage = img;
            }

            _selectedButtons.Clear();

            var lot = new Lot
            {
                Price = price,
                DistanceLake = distanceLake,
                DistanceHighway = distanceHighway,
                PoorSoil = poorSoil,
                Steep = steepType
            };

            lotsDataGridView.Rows.Add(name, lot.Price, lot.DistanceLake, lot.DistanceHighway, lot.PoorSoil,
                lot.Steep.ToString());
        }

        private void landusesApplyButton_Click(object sender, EventArgs e)
        {
            var recreationalCount = recreationalNumericUpDown.Value;
            var apartmentsCount = apartmentsNumericUpDown.Value;
            var housingComplexCount = housingComplexNumericUpDown.Value;
            var dumpCount = dumpNumericUpDown.Value;
            var cemeteryCount = cemeteryNumericUpDown.Value;

            var landuses = new Dictionary<string, Landuse>();

            for (var i = 0; i < recreationalCount; ++i)
                landuses.Add("recreational" + i, new Landuse { Type = LanduseType.Recreational });
            for (var i = 0; i < apartmentsCount; ++i)
                landuses.Add("apartment" + i, new Landuse { Type = LanduseType.Apartments });
            for (var i = 0; i < housingComplexCount; ++i)
                landuses.Add("house" + i, new Landuse { Type = LanduseType.HousingComplex });
            for (var i = 0; i < dumpCount; ++i)
                landuses.Add("dump" + i, new Landuse { Type = LanduseType.Dump });
            for (var i = 0; i < cemeteryCount; ++i)
                landuses.Add("cemetery" + i, new Landuse { Type = LanduseType.Cemetery });

            landusesDataGridView.Rows.Clear();
            foreach (var landuse in landuses)
                landusesDataGridView.Rows.Add(landuse.Key, landuse.Value.Type.ToString());
        }

        /*
        private void landuseApplyButton_Click(object sender, System.EventArgs e)
        {
            Bitmap img = null;

            if (recreationalRadioButton.Checked)
                img = Properties.Resources.Recreational;
            else if (apartmentsRadioButton.Checked)
                img = Properties.Resources.Apartments;
            else if (housingComplexRadioButton.Checked)
                img = Properties.Resources.HousingComplex;
            else if (dumpRadioButton.Checked)
                img = Properties.Resources.Dump;
            else if (cemetryRadioButton.Checked)
                img = Properties.Resources.Cemetery;

            if (img == null)
                return;

            foreach (var btn in _selectedButtons)
            {
                btn.BackColor = Color.Yellow;
                btn.ForeColor = Color.Yellow;

                btn.BackgroundImage = img;
            }

            _selectedButtons.Clear();
        }*/
    }
}
