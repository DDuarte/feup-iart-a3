using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(Color.Black);
            e.Graphics.DrawGrid(pen, 0, 0, 500, 500, 10, 10);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
