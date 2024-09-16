using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LAB2 {
    public partial class Form2 : Form {
        private readonly MainForm menu;
        Bitmap source;

        public Form2(MainForm menu) {
            InitializeComponent();
            this.menu = menu;
        }

        private void Button1_Click(object sender, EventArgs e) {
            menu.Show();
            Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e) => Application.OpenForms["MainForm"]?.Show();

        private void ButtonLoad_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png" };

            if (ofd.ShowDialog() == DialogResult.OK) {
                labelProcessing.Text = "Обработка…";
                labelProcessing.Refresh();

                source = new Bitmap(ofd.FileName);
                pictureBoxSource.Image = source;

                pictureBoxR.Image = ExtractChannel(source, 'R');
                pictureBoxG.Image = ExtractChannel(source, 'G');
                pictureBoxB.Image = ExtractChannel(source, 'B');
                
                BuildHistogram(source, 'R', pictureBoxHistR);
                BuildHistogram(source, 'G', pictureBoxHistG);
                BuildHistogram(source, 'B', pictureBoxHistB);

                labelProcessing.Text = "Готово!";
            }
        }
                
        private Bitmap ExtractChannel(Bitmap source, char channel) {
            Bitmap channeled = new Bitmap(source.Width, source.Height);

            for (int y = 0; y < source.Height; ++y) {
                for (int x = 0; x < source.Width; ++x) {
                    Color original = source.GetPixel(x, y);
                    Color extracted = Color.Black;

                    switch (channel) {
                        case 'R':
                            extracted = Color.FromArgb(original.R, 0, 0); 
                            break;
                        case 'G':
                            extracted = Color.FromArgb(0, original.G, 0); 
                            break;
                        case 'B':
                            extracted = Color.FromArgb(0, 0, original.B); 
                            break;
                    }
                    channeled.SetPixel(x, y, extracted);
                }
            }
            return channeled;
        }
                
        private void BuildHistogram(Bitmap image, char channel, PictureBox pb) {
            int[] hist = new int[256];
            for (int y = 0; y < image.Height; ++y) {
                for (int x = 0; x < image.Width; ++x) {
                    Color color = image.GetPixel(x, y);
                    int value = 0;

                    switch (channel) {
                        case 'R':
                            value = color.R;
                            break;
                        case 'G':
                            value = color.G;
                            break;
                        case 'B':
                            value = color.B;
                            break;
                    }
                    ++hist[value];
                }
            }

            Bitmap histImage = new Bitmap(256, 100);
            using (Graphics g = Graphics.FromImage(histImage)) {
                g.Clear(Color.White);
                int max = hist.Max();

                for (int i = 0; i < hist.Length; ++i) {
                    int height = (int)(hist[i] / (float)max * 100);
                    g.DrawLine(Pens.Black, i, 100, i, 100 - height);
                }
            }
            pb.Image = histImage;
        }
    }
}
