using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        private readonly HashSet<Button> _selectedButtons = new HashSet<Button>();

        private const int size = 55;
        private const int rows = 10;
        private const int cols = 10;

        private bool IntersectsWith(Button btn1, Button btn2)
        {
            if (btn1 == btn2)
                return false;

            var bounds1 = btn1.Bounds;
            var bounds2 = btn1.Bounds;
            bounds1.Inflate(size / 2, -size / 2);
            bounds2.Inflate(-size / 2, size / 2);

            return btn2.Bounds.IntersectsWith(bounds1) || btn2.Bounds.IntersectsWith(bounds2);
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

            gridPanel.Size = new Size(size * rows, size * cols);
            gridPanel.Location = new Point((ClientSize.Width - gridPanel.Size.Width) / 2, (ClientSize.Height - gridPanel.Size.Height) / 2);

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
                        FlatStyle = FlatStyle.Flat
                    };

                    btn.Click += (sender, args) =>
                    {
                        var button = (Button) sender;

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
                    };

                    gridPanel.Controls.Add(btn);
                }
            }
        }
    }
}
