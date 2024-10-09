using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 f = new Form1(this);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 f = new Form2(this);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            Form3 f = new Form3(this);
            f.Show();
        }
    }
}
