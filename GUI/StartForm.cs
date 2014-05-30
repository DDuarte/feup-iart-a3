using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.Properties;
using IART_A3.StateRepresentation;

namespace GUI
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void newButton_Click(object sender, EventArgs e)
        {
            var size = Convert.ToInt32(gridSizeNumericUpDown.Value);

            var builderForm = new BuilderForm(this, new Problem(size));
            Hide();
            builderForm.Show();  
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                Problem problem;
                try
                {
                    problem = Problem.ReadJson(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), Resources.ErrorStr,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var builderForm = new BuilderForm(this, problem);
                Hide();
                builderForm.Show();  
            }
            Console.WriteLine(result); // <-- For debugging use.
        }
    }
}
