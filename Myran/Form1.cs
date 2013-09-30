using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myran
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.board1.Columns = 22;
            //this.board1.Rows = 22;

            ((Bitmap)this.board1.Image).SetPixel(0, 3, Color.FromArgb(100, 100, 100));

            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < numericUpDownUpdates.Value; i++)
                this.board1.Update();
        }

        private void board1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDownSpeed_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDownSpeed.Value;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            board1.Reset((int)numericUpDownSquareSize.Value);
        }
    }
}
