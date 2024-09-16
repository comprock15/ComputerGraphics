using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastBitmap;

namespace LAB2
{
    public partial class Form3 : Form
    {
        MainForm menu;
        Image img;
        public Form3(MainForm menu)
        {
            InitializeComponent();
            this.menu = menu;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menu.Show();
            Close();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            menu.Show();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            button_saveFile.Enabled = true;
            trackBar_Hue.Enabled = true;
            trackBar_Saturation.Enabled = true;
            trackBar_Value.Enabled = true;

            img = pictureBox1.Image;
        }

        private void button_saveFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            pictureBox1.Image.Save(saveFileDialog1.FileName);
        }

        private void button_loadFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void trackBar_Hue_Scroll(object sender, EventArgs e)
        {
            textBox_Hue.Text = trackBar_Hue.Value.ToString();
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            pictureBox1.Image = bmp.Select(color => {
                double h, s, v;
                RGBtoHSV(color, out h, out s, out v);
                var c = HSVtoRGB(trackBar_Hue.Value, s, v);
                return c;
            });
        }

        private void trackBar_Saturation_Scroll(object sender, EventArgs e)
        {
            textBox_Saturation.Text = trackBar_Saturation.Value.ToString();
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            pictureBox1.Image = bmp.Select(color => {
                double h, s, v;
                RGBtoHSV(color, out h, out s, out v);
                var c = HSVtoRGB(h, trackBar_Saturation.Value / 100.0, v); 
                return c;
            });
        }

        private void trackBar_Value_Scroll(object sender, EventArgs e)
        {
            textBox_Value.Text = trackBar_Value.Value.ToString();
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            pictureBox1.Image = bmp.Select(color => {
                double h, s, v;
                RGBtoHSV(color, out h, out s, out v);
                var c = HSVtoRGB(h, s, trackBar_Value.Value / 100.0);
                return c;
            });
        }

        private void RGBtoHSV(Color pix, out double hue, out double sat, out double val)
        {
            double R = pix.R / 255.0;
     
            double G = pix.G / 255.0;
            double B = pix.B / 255.0;

            var max = Math.Max(R, Math.Max(G, B));
            var min = Math.Min(R, Math.Min(G, B));

            var d = max - min;

            if (max == min)
                hue = 0;
            else if (max == R)
            {
                if (G >= B)
                    hue = 60 / d * (G - B) + 0;
                else 
                    hue = 60 / d * (G - B) + 360;
            }
            else if (max == G)
                hue = 60 / d * (B - R) + 120;
            else //if (max == B)
                hue = 60 / d * (R - G) + 240;
            
            if (max == 0)
                sat = 0;
            else
                sat = 1 - (min / max);

            val = max;
        }

        private Color HSVtoRGB(double hue, double sat, double val)
        {
            double r, g, b;

            var f = hue / 60.0 - Math.Floor(hue / 60.0);
            var p = val * (1 - sat);
            var q = val * (1 - f * sat);
            var t = val * (1 - (1 - f) * sat);

            switch (Math.Floor(hue / 60.0) % 6)
            {
                case 0:
                    r = val; g = t; b = p;
                    break;
                case 1:
                    r = q; g = val; b = p;
                    break;
                case 2:
                    r = p; g = val; b = t;
                    break;
                case 3:
                    r = p; g = q; b = val;
                    break;
                case 4:
                    r = t; g = p; b = val;
                    break;
                default:
                    r = val; g = p; b = q;
                    break;
            }

            //var c = val * sat;
            //var x = c * (1 - Math.Abs((hue / 60) % 2 - 1));
            //var m = val - c;

            //if (hue < 60)
            //{
            //    r = c; g = x; b = 0;
            //}
            //else if (hue < 120)
            //{
            //    r = x; g = c; b = 0;
            //}
            //else if(hue < 180)
            //{
            //    r = 0; g = c; b = x;
            //}
            //else if(hue < 240)
            //{
            //    r = 0; g = x; b = c;
            //}
            //else if(hue < 300)
            //{
            //    r = x; g = 0; b = c;
            //}
            //else
            //{
            //    r = c; g = 0; b = x;
            //}

            //r = (r + m);
            //g = (g + m);
            //b = (b + m);

            return Color.FromArgb(255, (int)(r*255), (int)(g*255), (int)(b*255));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = img;
        }
    }
}
