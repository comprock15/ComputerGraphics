using System;
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
        bool add_points_mode;
        int points_count;
        public Form3(MainForm menu)
        {
            InitializeComponent();
            this.menu = menu;
            add_points_mode = false;
            points_count = 0;
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            menu.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
                pictureBox4.BackColor = colorDialog1.Color;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.BackColor = colorDialog1.Color;
                pictureBox5.BackColor = colorDialog1.Color;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.BackColor = colorDialog1.Color;
                pictureBox6.BackColor = colorDialog1.Color;
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            add_points_mode = true;
            button1.Enabled = false;
            
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (add_points_mode && points_count < 3)
            {
                points_count++;
                switch (points_count)
                {
                    case 1:
                        pictureBox4.Location = new Point(e.X, e.Y);
                        pictureBox4.Visible = true;
                        break;
                    case 2:
                        pictureBox5.Location = new Point(e.X, e.Y);
                        pictureBox5.Visible = true;
                        break;
                    case 3:
                        pictureBox6.Location = new Point(e.X, e.Y);
                        pictureBox6.Visible = true;
                        break;
                }
                if (points_count == 3)
                {
                    add_points_mode = false;
                    button3.Enabled = true;
                }
                    
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            pictureBox4.Visible= false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FillGradientRectangle();
        }

        private void FillGradientRectangle()
        {
            
        }
    }
}
