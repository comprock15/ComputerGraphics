using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastBitmap;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LAB2
{
    public partial class Form3 : Form
    {
        MainForm menu;
        System.Drawing.Image img;
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
            pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
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
        private void trackBar_Scroll(object sender, EventArgs e)
        {
            double dH = trackBar_Hue.Value - 360;
            double dS = (trackBar_Saturation.Value -100) / 100.0;
            double dV = (trackBar_Value.Value -100) / 100.0;

            Bitmap bmp = (Bitmap)img.Clone();

            for (int i = 0; i < bmp.Width; ++i)
                for (int j = 0; j < bmp.Height; ++j)
                {
                    // Чтение пикселя
                    var px = bmp.GetPixel(i, j);
                    // Преобразование в HSV
                    var res = RGBtoHSV(px.R / 255.0, px.G / 255.0, px.B / 255.0);

                    // Изменение тона (при превышении 360 зацикливаем)
                    res.H += dH;

                    if (res.H > 360)
                        res.H -= 360;
                    if (res.H < 0)
                        res.H += 360;

                    // Изменение насыщенность (обрезаем при выходе за [0; 1])
                    res.S += dS;
                    res.S = Math.Min(1, res.S);
                    res.S = Math.Max(0, res.S);

                    // Изменение яркости (обрезаем при выходе за [0; 1])
                    res.V += dV;
                    res.V = Math.Min(1, res.V);
                    res.V = Math.Max(0, res.V);

                    // Для вывода на экран и сохранения в файл преобразуем обратно в RGB
                    var outres = HSVtoRGB(res.H, res.S, res.V);

                    bmp.SetPixel(i, j,
                        Color.FromArgb(px.A, (int)(outres.R *255.0), (int)(outres.G *255.0), (int)(outres.B *255.0)));
                }
            pictureBox1.Image = bmp;
        }
        
        private (double H, double S, double V) RGBtoHSV( double R, double G, double B)
        {
            var max = Math.Max(R, Math.Max(G, B));
            var min = Math.Min(R, Math.Min(G, B));

            var d = max - min;

            double hue;
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

            double sat;
            if (max == 0)
                sat = 0;
            else
                sat = 1 - (min / max);

            double val = max;

            return (hue, sat, val);
        }

        private (double R, double G, double B) HSVtoRGB(double hue, double sat, double val)
        {
            double eps = 0.0001;
            Debug.Assert(hue > -eps && hue < 360 + eps);
            Debug.Assert(sat > -eps && sat < 1 + eps);
            Debug.Assert(val > -eps && val < 1 + eps);

            double f = hue / 60.0 - Math.Floor(hue / 60.0);
            var p = val* (1 - sat);
            var q = val * (1 - f * sat);
            var t = val * (1 - (1 - f) * sat);

            switch (Math.Floor(hue / 60.0) % 6)
            {
                case 0:
                    return(val, t, p);
                case 1:
                    return (q, val, p);
                case 2:
                    return (p, val, t);
                case 3:
                    return (p, q, val);
                case 4:
                    return (t, p, val);
                case 5:
                    return (val, p, q);
                default:
                    return (0, 0, 0);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = img;
            trackBar_Hue.Value = 0;
            trackBar_Saturation.Value = 0;
            trackBar_Value.Value = 0;

        }

        private void trackBar_Hue_ValueChanged(object sender, EventArgs e)
        {
            textBox_Hue.Text = (trackBar_Hue.Value / 2).ToString();
        }

        private void trackBar_Saturation_ValueChanged(object sender, EventArgs e)
        {
            textBox_Saturation.Text = (trackBar_Saturation.Value / 2).ToString();
        }

        private void trackBar_Value_ValueChanged(object sender, EventArgs e)
        {
            textBox_Value.Text = (trackBar_Value.Value / 2).ToString();
        }
    }
}
