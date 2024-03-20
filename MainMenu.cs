using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessExperiment
{
    public partial class MainMenu : UserControl
    {
        public MainMenu(string displayText)
        {
            InitializeComponent();
            displayTextLabel.Text = displayText;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new GameScreen());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
