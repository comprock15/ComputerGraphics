﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB3
{
    public partial class Form3 : Form
    {
        MainForm menu;
        public Form3(MainForm menu)
        {
            InitializeComponent();
            this.menu = menu;
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            menu.Show();
        }
    }
}
