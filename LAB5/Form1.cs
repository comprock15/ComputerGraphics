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
    public partial class Form1 : Form
    {
        MainForm menu;
        public Form1(MainForm menu)
        {
            InitializeComponent();
            this.menu = menu;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            menu.Show();
        }
    }
}
