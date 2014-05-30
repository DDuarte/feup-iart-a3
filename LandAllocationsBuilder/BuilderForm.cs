using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using LandAllocationBuilder.Properties;
using LandAllocationsLib.SearchAlgorithms;
using LandAllocationsLib.StateRepresentation;
using Point = LandAllocationsLib.StateRepresentation.Point;

namespace LandAllocationBuilder
{
    public partial class BuilderForm : Form
    {
        private HashSet<Button> _selectedButtons = new HashSet<Button>();
        private Button[,] _buttons;

        private Problem _problem;
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
            _buttons = new Button[_problem.Size, _problem.Size];

            algorithmComboBox.SelectedIndex = 0;

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
                    _buttons[i, j] = btn;
                }
            }

            foreach (var point in _problem.Highways)
                AddHighway(point);

            foreach (var point in _problem.Lakes)
                AddLake(point);

            foreach (var lot in _problem.Lots)
                AddLot(lot.Key, lot.Value);

            SetLanduses(_problem.Landuses);

            constraintsTextBox.Text = string.Empty;
            foreach (var constraint in _problem.HardConstraints)
                constraintsTextBox.Text += constraint.Value + Environment.NewLine;
            foreach (var constraint in _problem.SoftConstraints)
                constraintsTextBox.Text += constraint.Value + Environment.NewLine;

            if (_problem.ProblemResult != null)
            {
                foreach (var landuseAllocation in _problem.ProblemResult.LanduseAllocations)
                {
                    AddLandAllocation(landuseAllocation.Item1, landuseAllocation.Item2);
                }
            }
        }

        private void AddHighway(Point point)
        {
            var btn = _buttons[point.X, point.Y];
            btn.BackColor = Color.SlateGray;
            btn.ForeColor = Color.SlateGray;
            btn.BackgroundImage = Resources.AsphaltTexture;
        }

        private void AddLake(Point point)
        {
            var btn = _buttons[point.X, point.Y];
            btn.BackColor = Color.DodgerBlue;
            btn.ForeColor = Color.DodgerBlue;
            btn.BackgroundImage = Resources.WaterTexture;
        }

        private void highwayApplyButton_Click(object sender, EventArgs e)
        {
            foreach (var btn in _selectedButtons)
            {
                var point = (Point) btn.Tag;
                AddHighway(point);
                RemoveTerrainPoint(point);
                _problem.Highways.Add(point);
            }

            _selectedButtons.Clear();
        }

        private void waterApplyButton_Click(object sender, EventArgs e)
        {
            foreach (var btn in _selectedButtons)
            {
                var point = (Point) btn.Tag;
                AddLake(point);
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

            SteepType steepType;
            if (flatRadioButton.Checked)
                steepType = SteepType.Flat;
            else if (moderatelySteepRadioButton.Checked)
                steepType = SteepType.ModeratelySteep;
            else if (steepRadioButton.Checked)
                steepType = SteepType.Steep;
            else if (verySteepRadioButton.Checked)
                steepType = SteepType.VerySteep;
            else
            {
                MessageBox.Show(Resources.UnknownSteepTypeStr,
                    Resources.ErrorStr, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var terrain = new HashSet<Point>();

            foreach (var btn in _selectedButtons)
            {
                var point = (Point) btn.Tag;
                terrain.Add(point);
                RemoveTerrainPoint(point);
            }

            _selectedButtons.Clear();

            var lot = new Lot
            {
                Price = price,
                PoorSoil = poorSoil,
                Steep = steepType,
                Terrain = terrain
            };

            AddLot(name, lot);
            _problem.Lots.Add(name, lot);
        }

        private readonly Dictionary<SteepType, Tuple<Bitmap, Bitmap>> _steepImages = new Dictionary
            <SteepType, Tuple<Bitmap, Bitmap>>
        {
            {SteepType.Flat, Tuple.Create(Resources.SandFlatTexture, Resources.DirtFlatTexture)},
            {SteepType.ModeratelySteep, Tuple.Create(Resources.SandModeratelySteepTexture, Resources.DirtModeratelySteepTexture)},
            {SteepType.Steep, Tuple.Create(Resources.SandSteepTexture, Resources.DirtSteepTexture)},
            {SteepType.VerySteep, Tuple.Create(Resources.SandVerySteepTexture, Resources.DirtVerySteepTexture)},
        };

        private void AddLot(string name, Lot lot)
        {
            foreach (var point in lot.Terrain)
            {
                var btn = _buttons[point.X, point.Y];
                btn.BackColor = lot.PoorSoil ? Color.SandyBrown : Color.Brown;
                btn.ForeColor = lot.PoorSoil ? Color.SandyBrown : Color.Brown;
                btn.BackgroundImage = lot.PoorSoil ? _steepImages[lot.Steep].Item1 : _steepImages[lot.Steep].Item2;
            }

            lotsDataGridView.Rows.Add(name, lot.Size, lot.Price, lot.PoorSoil, lot.Steep.ToString());
        }

        private void RemoveLot(string name)
        {
            var lot = _problem.Lots[name];

            foreach (var point in lot.Terrain)
            {
                var btn = _buttons[point.X, point.Y];
                btn.BackColor = Color.Gray;
                btn.ForeColor = Color.Gray;
                btn.BackgroundImage = null;
            }

            _problem.Lots.Remove(name);
        }

        private void SetLanduses(Dictionary<string, Landuse> landuses)
        {
            _problem.Landuses = landuses;

            landusesDataGridView.Rows.Clear();
            foreach (var landuse in landuses)
                landusesDataGridView.Rows.Add(landuse.Key, landuse.Value.Type.ToString());
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

            SetLanduses(landuses);
        }

        private void BuilderForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _startForm.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            var name = saveFileDialog.FileName;

            try
            {
                _problem.WriteJson(name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Resources.ErrorStr,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool _algoRunning;
        private Thread _algoThread;

        private void runAlgorithmButton_Click(object sender, EventArgs e)
        {
            if (_algoRunning)
            {
                _algoThread.Abort();
                _algoRunning = false;
                runAlgorithmButton.Text = "Run Algorithm";
            }

            var algorithms = new Dictionary<string, Type>
            {
                {"A*", typeof (AStarSearchAlgorithm)},
                {"Greedy", typeof (GreedySearchAlgorithm)},
                {"UniformCost", typeof (UniformCostAlgorithm)},
                {"BreadthFirst", typeof (BreadthFirstSearchAlgorithm)},
                {"Bruteforce", typeof (BruteforceSearchAlgorithm)},
                {"DepthFirst", typeof (DepthFirstSearchAlgorithm)}
            };

            var algorithmName = (string) algorithmComboBox.SelectedItem;

            var algorithmType = algorithms[algorithmName];
            var algorithm = (SearchAlgorithm)Activator.CreateInstance(algorithmType, _problem);

            _problem.UpdateConstraintsTable();

            _algoRunning = true;
            runAlgorithmButton.Text = "Stop";
            _algoThread = new Thread(() =>
            {
                try
                {
                    _algoRunning = true;
                    _problem.ProblemResult = algorithm.Search();
                    _algoRunning = false;

                    Invoke(new Action(() =>
                    {
                        algorithmNameTextBox.Text = _problem.ProblemResult.AlgorithmName;
                        timeTextBox.Text = _problem.ProblemResult.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture);
                        iterTextBox.Text = _problem.ProblemResult.Iterations.ToString(CultureInfo.InvariantCulture);
                        solutionTextBox.Text = LanduseAllocations.ToString(_problem.ProblemResult.LanduseAllocations);
                        costTextBox.Text =
                            _problem.ProblemResult.Cost.ToString(CultureInfo.InvariantCulture);

                        foreach (var landuseAllocation in _problem.ProblemResult.LanduseAllocations)
                        {
                            AddLandAllocation(landuseAllocation.Item1, landuseAllocation.Item2);
                        }

                        tabControl2.SelectTab(resultTabPage);
                    }));
                }
                catch (ThreadAbortException) { }

                Invoke(new Action(() =>
                {
                    _algoRunning = false;
                    runAlgorithmButton.Text = "Run Algorithm";
                }));
            });
            _algoThread.Start();
        }

        private void compileConstraintsButton_Click(object sender, EventArgs e)
        {
            _problem.HardConstraints.Clear();
            _problem.SoftConstraints.Clear();

            var lines = constraintsTextBox.Text.Split('\n');
            foreach (var line in lines.Where(line => !string.IsNullOrWhiteSpace(line)))
            {
                try
                {
                    if (!_problem.AddConstraint(line))
                        throw new Exception("Failed at parsing.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.ErrorStr, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            constraintsTextBox.Text = string.Empty;

            foreach (var constraint in _problem.HardConstraints)
                constraintsTextBox.Text += constraint.Value + Environment.NewLine;
            foreach (var constraint in _problem.SoftConstraints)
                constraintsTextBox.Text += constraint.Value + Environment.NewLine;
        }

        private void helpConstraintButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(null, @"Hard Constraints:
Size - H [<landuse>] size <op> <threshold>
Distance - H [<landuse>] distance(<place>) <op> <threshold>
Steepness - H [<landuse>] steep [<steep>]
Soil Type - H [<landuse>] soil <soil_type>

Soft Constraints:
Size - S(<base_cost>) [<landuse>] size <op> <threshold>
Distance - S(<base_cost>) [<landuse>] distance(<place>) <op> <threshold>
Steepness - S(<base_cost>) [<landuse>] steep [<steep>]
Soil Type - S(<base_cost>) [<landuse>] soil <soil_type>

landuse: one or more of Recreational, Apartments, HousingComplex, Dump, Cemetery
steep: one or more of Flat, ModeratelySteep, Steep, VerySteep
op: > or <
place: Lake or Highway
threshold: distance or size in kilometers (1 square = 1km^2)
base_cost: initial cost of violating a soft constraint",
                "Constraints Syntax", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void AddLandAllocation(string landuse, string lot)
        {
            var img = _landuseImages[_problem.Landuses[landuse].Type];
            foreach (var point in _problem.Lots[lot].Terrain)
            {
                var btn = _buttons[point.X, point.Y];
                btn.BackColor = Color.Yellow;
                btn.ForeColor = Color.Yellow;

                btn.BackgroundImage = img;
            }
            
        }

        private readonly Dictionary<LanduseType, Bitmap> _landuseImages = new Dictionary<LanduseType, Bitmap>
        {
            { LanduseType.Recreational, Resources.Recreational },
            { LanduseType.Apartments, Resources.Apartments },
            { LanduseType.HousingComplex, Resources.HousingComplex },
            { LanduseType.Dump, Resources.Dump },
            { LanduseType.Cemetery, Resources.Cemetery }
        };

        private void lotsDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var name = (string) e.Row.Cells[0].Value;
            RemoveLot(name);
        }
    }
}
