using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GUI.Properties;
using IART_A3.StateRepresentation;
using Point = IART_A3.StateRepresentation.Point;

namespace GUI
{
    public partial class BuilderForm : Form
    {
        private readonly HashSet<Button> _selectedButtons = new HashSet<Button>();

        private readonly Problem _problem;
        private readonly int _sizePx;
        private readonly StartForm _startForm;

        private void RemoveTerrainPoint(Point point)
        {
            _problem.Lakes.Remove(point);
            _problem.Highways.Remove(point);
            foreach (var lot in _problem.Lots)
                lot.Value.Terrain.Remove(point);
        }

        private bool IntersectsWith(Button btn1, Button btn2)
        {
            if (btn1 == btn2)
                return false;

            var bounds1 = btn1.Bounds;
            var bounds2 = btn1.Bounds;
            bounds1.Inflate(_sizePx / 2, -_sizePx / 2);
            bounds2.Inflate(-_sizePx / 2, _sizePx / 2);

            return btn2.Bounds.IntersectsWith(bounds1) || btn2.Bounds.IntersectsWith(bounds2);
        }

        private void ToggleButton(Button button)
        {
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
            return _selectedButtons.Count <= 1 ||
                _selectedButtons.All(btn1 => _selectedButtons.Any(button => IntersectsWith(button, btn1)));
        }

        public BuilderForm(StartForm startForm, Problem problem)
        {
            InitializeComponent();

            _startForm = startForm;
            _problem = problem;

            _sizePx = (int)(550.0 / _problem.Size);

            for (var i = 0; i < _problem.Size; ++i)
            {
                for (var j = 0; j < _problem.Size; ++j)
                {
                    var btn = new Button
                    {
                        Name = "btn" + i + j,
                        Bounds = new Rectangle(_sizePx * i, _sizePx * j, _sizePx, _sizePx),
                        BackColor = Color.Gray,
                        ForeColor = Color.Gray,
                        Margin = new Padding(0),
                        FlatStyle = FlatStyle.Flat,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        TabStop = false,
                        Tag = new Point(i, j)
                    };

                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += (sender, args) => ToggleButton((Button) sender);

                    gridPanel.Controls.Add(btn);
                }
            }
        }

        private void highwayApplyButton_Click(object sender, EventArgs e)
        {
            foreach (var btn in _selectedButtons)
            {
                btn.BackColor = Color.SlateGray;
                btn.ForeColor = Color.SlateGray;
                btn.BackgroundImage = new Bitmap(Resources.AsphaltTexture);

                var point = (Point) btn.Tag;
                RemoveTerrainPoint(point);
                _problem.Highways.Add(point);
            }

            _selectedButtons.Clear();
        }

        private void waterApplyButton_Click(object sender, EventArgs e)
        {
            foreach (var btn in _selectedButtons)
            {
                btn.BackColor = Color.DodgerBlue;
                btn.ForeColor = Color.DodgerBlue;
                btn.BackgroundImage = new Bitmap(Resources.WaterTexture);

                var point = (Point)btn.Tag;
                RemoveTerrainPoint(point);
                _problem.Lakes.Add(point);
            }

            _selectedButtons.Clear();
        }

        private void lotApplyButton_Click(object sender, EventArgs e)
        {
            if (_selectedButtons.Count == 0)
            {
                MessageBox.Show(Resources.NoTerrainSelectedStr,
                    Resources.ErrorStr, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var name = lotNameTextBox.Text;
            if (_problem.Lots.ContainsKey(name))
            {
                MessageBox.Show(Resources.DuplicateLotNameStr,
                    Resources.ErrorStr, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var poorSoil = poorSoilCheckBox.Checked;
            var price = Convert.ToDouble(priceNumericUpDown.Value);

            Bitmap img = null;
            var steepType = SteepType.Flat;
            if (flatRadioButton.Checked)
            {
                img = poorSoil ? Resources.SandFlatTexture : Resources.DirtFlatTexture;
                steepType = SteepType.Flat;
            }
            else if (moderatelySteepRadioButton.Checked)
            {
                img = poorSoil
                    ? Resources.SandModeratelySteepTexture
                    : Resources.DirtModeratelySteepTexture;
                steepType = SteepType.ModeratelySteep;
            }
            else if (steepRadioButton.Checked)
            {
                img = poorSoil ? Resources.SandSteepTexture : Resources.DirtSteepTexture;
                steepType = SteepType.Steep;
            }
            else if (verySteepRadioButton.Checked)
            {
                img = poorSoil ? Resources.SandVerySteepTexture : Resources.DirtVerySteepTexture;
                steepType = SteepType.VerySteep;
            }

            if (img == null)
            {
                MessageBox.Show(Resources.UnknownSteepTypeStr,
                    Resources.ErrorStr, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var terrain = new HashSet<Point>();

            foreach (var btn in _selectedButtons)
            {
                btn.BackColor = poorSoil ? Color.SandyBrown : Color.Brown;
                btn.ForeColor = poorSoil ? Color.SandyBrown : Color.Brown;
                btn.BackgroundImage = img;

                var point = (Point)btn.Tag;
                RemoveTerrainPoint(point);
                terrain.Add(point);
            }

            _selectedButtons.Clear();

            var lot = new Lot
            {
                Price = price,
                PoorSoil = poorSoil,
                Steep = steepType,
                Terrain = terrain
            };

            _problem.Lots.Add(name, lot);
            lotsDataGridView.Rows.Add(name, lot.Price, lot.PoorSoil, lot.Steep.ToString());
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

            _problem.Landuses = landuses;

            landusesDataGridView.Rows.Clear();
            foreach (var landuse in landuses)
                landusesDataGridView.Rows.Add(landuse.Key, landuse.Value.Type.ToString());
        }

        private void BuilderForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _startForm.Show();
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
