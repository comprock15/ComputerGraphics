using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form3 : Form
    {
        MainForm menu;
        
        public Form3(MainForm menu)
        {
            InitializeComponent();
            this.menu = menu;            
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            menu.Show();
        }

        private void UpdateImage()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            double x, y;
            
            for (int i = 0; i < pictureBox1.Controls.Count - 1; i+= 3) 
            {
                for (double t = 0.0; t <= 1.0; t += 0.001)
                {
                    x = (1 - t) * pictureBox1.Controls[i].Location.X + t * pictureBox1.Controls[i + 2].Location.X;
                    y = (1 - t) * pictureBox1.Controls[i].Location.Y + t * pictureBox1.Controls[i + 2].Location.Y;
                    x = Math.Min(bmp.Width - 1, Math.Max(0, x + 5));
                    y = Math.Min(bmp.Height - 1, Math.Max(0, y + 5));
                    bmp.SetPixel((int)x, (int)y, Color.Blue);
                }
            }

            if (pictureBox1.Controls.Count > 3)
            {
                for (int i = 1; i < pictureBox1.Controls.Count - 3; i += 3)
                {
                    for (double t = 0.0; t <= 1.0; t += 0.001)
                    {
                        x = pictureBox1.Controls[i].Location.X * (1 - t) * (1 - t) * (1 - t)
                            + 3 * pictureBox1.Controls[i + 1].Location.X * (1 - t) * (1 - t) * t
                            + 3 * pictureBox1.Controls[i + 2].Location.X * (1 - t) * t * t
                            + pictureBox1.Controls[i + 3].Location.X * t * t * t;
                        y = pictureBox1.Controls[i].Location.Y * (1 - t) * (1 - t) * (1 - t)
                            + 3 * pictureBox1.Controls[i + 1].Location.Y * (1 - t) * (1 - t) * t
                            + 3 * pictureBox1.Controls[i + 2].Location.Y * (1 - t) * t * t
                            + pictureBox1.Controls[i + 3].Location.Y * t * t * t;
                        bmp.SetPixel((int)Math.Min(bmp.Width - 1, Math.Max(0, x + 5)), (int)Math.Min(bmp.Height - 1, Math.Max(0, y + 5)), Color.Black);
                    }
                }
            }

            //for (int i = 0; i < bmp.Width; i++)
            //{
            //    bmp.SetPixel(i, 0, Color.Black);
            //    bmp.SetPixel(i, bmp.Height-2, Color.Black);
            //}

            //for (int i = 0; i < bmp.Height; i++)
            //{
            //    bmp.SetPixel(0, i, Color.Black);
            //    bmp.SetPixel(bmp.Width-2, i, Color.Black);
            //}

            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose(); 
            pictureBox1.Image = bmp;
        }
         

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            PictureBox pb = new PictureBox(){
                Location = e.Location,
                Size = new Size(10, 10),
                BackColor = Color.Red,
                BorderStyle = BorderStyle.FixedSingle,
            };
            pb.MouseMove += bearingPointMove;
            
            

            PictureBox pb_s1 = new PictureBox()
            {
                Location = new Point(e.Location.X + 15, e.Location.Y + 15),
                Size = new Size(10, 10),
                Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\circle.png"),
            };
            PictureBox pb_s2 = new PictureBox()
            {
                Location = new Point(e.Location.X - 15, e.Location.Y - 15),
                Size = new Size(10, 10),
                Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\circle.png"),
            };
            pb_s1.MouseMove += managePointMove;
            pb_s2.MouseMove += managePointMove;

            pictureBox1.Controls.Add(pb_s1);
            pictureBox1.Controls.Add(pb);
            pictureBox1.Controls.Add(pb_s2);
            UpdateImage();
        }

        private void bearingPointMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                var ind = pictureBox1.Controls.IndexOf(sender as PictureBox);
                pictureBox1.Controls[ind].Location = new Point(e.X + pictureBox1.Controls[ind].Location.X, e.Y + pictureBox1.Controls[ind].Location.Y);
                pictureBox1.Controls[ind - 1].Location = new Point(e.X + pictureBox1.Controls[ind - 1].Location.X, e.Y + pictureBox1.Controls[ind - 1].Location.Y);
                pictureBox1.Controls[ind + 1].Location = new Point(e.X + pictureBox1.Controls[ind + 1].Location.X, e.Y + pictureBox1.Controls[ind + 1].Location.Y);

                UpdateImage();
            }
            if (e.Button == MouseButtons.Right)
            {
                var ind = pictureBox1.Controls.IndexOf(sender as PictureBox);
                if (ind != -1)
                {
                    pictureBox1.Controls.RemoveAt(ind + 1);
                    pictureBox1.Controls.RemoveAt(ind);
                    pictureBox1.Controls.RemoveAt(ind - 1);
                }
                
                UpdateImage();
            }
            
        }

        private void managePointMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var ind1 = pictureBox1.Controls.IndexOf(sender as PictureBox);
                var ind2 = ind1 % 3 == 0 ? ind1 + 2 : ind1 - 2;
                pictureBox1.Controls[ind2].Location = new Point(- e.X + pictureBox1.Controls[ind2].Location.X, - e.Y + pictureBox1.Controls[ind2].Location.Y);
                pictureBox1.Controls[ind1].Location = new Point(e.X + pictureBox1.Controls[ind1].Location.X, e.Y + pictureBox1.Controls[ind1].Location.Y);
                UpdateImage();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            pictureBox1.Controls.Clear();
            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void Form3_Resize(object sender, EventArgs e)
        {
            UpdateImage();
        }
    }
}
