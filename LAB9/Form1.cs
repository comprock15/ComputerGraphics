using LAB9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.FSharp.Core.ByRefKinds;

namespace LAB9 {
    /// <summary>
    /// Форма
    /// </summary>
    public partial class Form1 : Form {
        /// <summary>
        /// Многогранник
        /// </summary>
        Polyhedron cur_polyhedron;
        Graphics g;
        Graphics g2;
        Pen p;
        List<Color> colors;
        private Random random = new Random();
        private Vector3 lightPosition = new Vector3(100, 100, 1000);


        //camry
        Camry camry;
        PointF worldCenter;
        double zScreenNear;
        double zScreenFar;
        double fov;
        double[,] parallelProjectionMatrix;
        double[,] perspectiveProjectionMatrix;


        /// <summary>
        /// Инициализация формы
        /// </summary>
        public Form1() {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g2 = pictureBox3.CreateGraphics();
            camry = new Camry();
            p = new Pen(Color.Black, 2);
            colors = new List<Color>{ };
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;

            lightningComboBox.SelectedIndex = 0;


           


            //camry
            
            worldCenter = new PointF(pictureBox3.Width / 2, pictureBox3.Height / 2);
            zScreenNear = 1;
            zScreenFar = 100;
            fov = 45;
            
            parallelProjectionMatrix = new double[,] { 
                { 1.0 / pictureBox3.Width, 0,                       0,                                 0},
                { 0,                      1.0 / pictureBox3.Height, 0,                                 0},
                { 0,                      0,                       -2.0 / (zScreenFar - zScreenNear), -(zScreenFar + zScreenNear) / (zScreenFar - zScreenNear)},
                { 0,                      0,                        0,                                 1} 
            };

            perspectiveProjectionMatrix = new double[,] {
                { pictureBox3.Height / (Math.Tan(AffineTransformations.DegreesToRadians(fov / 2)) * pictureBox3.Width), 0, 0, 0},
                { 0, 1.0 / Math.Tan(AffineTransformations.DegreesToRadians(fov / 2)), 0, 0},
                { 0, 0, -(zScreenFar + zScreenNear) / (zScreenFar - zScreenNear), -2 * (zScreenFar * zScreenNear) / (zScreenFar - zScreenNear)},
                { 0, 0, -1, 0}
            };

            RedrawCamryField();

            RedrawField();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e) {
            if (g != null)
                g.Dispose();
            g = pictureBox1.CreateGraphics();
        }

        /// <summary>
        /// Создание многогранника из списка
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            Polyhedron old_polyhedron = null;
            if (cur_polyhedron != null)
                old_polyhedron = new Polyhedron(cur_polyhedron);

            switch (comboBox1.SelectedIndex) {
                case 0:
                    cur_polyhedron = PolyhedronCollection.MakeTetrahedron();
                    break;
                case 1:
                    cur_polyhedron = OBJHandler.Load(Path.Combine("..", "..", "..", "LAB7", "Polyhendrons", "cube_scaled.obj"));
                    cur_polyhedron.SetName("Гексаэдр");
                    break;
                case 2:
                    cur_polyhedron = PolyhedronCollection.MakeOctahedron();
                    break;
                case 3:
                    cur_polyhedron = PolyhedronCollection.MakeIcosahedron();
                    break;
                case 4:
                    cur_polyhedron = PolyhedronCollection.MakeDodecahedron();
                    break;
                case 5:
                    var f = new AddFunctionGraphicForm(pictureBox1.Width, pictureBox1.Height);
                    if (f.ShowDialog() == DialogResult.OK) {
                        cur_polyhedron = f.cur_polyhedron;
                    }
                    break;
                case 6:
                    var ff = new AddRotationFigurePolyhedronForm();
                    if (ff.ShowDialog() == DialogResult.OK) {
                        cur_polyhedron = ff.cur_polyhedron;
                    }
                    break;
                default:
                    break;
            }
            if (old_polyhedron != cur_polyhedron)
            {
                objects_list.Items.Add(cur_polyhedron);
                colors.Add(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
                RedrawField();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) => RedrawField();

        private void textBox_TextChanged(object sender, EventArgs e) {
            double num;
            if (double.TryParse((sender as TextBox).Text, out num) == false)
                (sender as TextBox).BackColor = Color.Red;
            else
                (sender as TextBox).BackColor = Color.White;
        }

        private void button_delete_obj_Click(object sender, EventArgs e)
        {
            if (objects_list.SelectedIndex == -1)
                return;
            colors.RemoveAt(objects_list.SelectedIndex);
            objects_list.Items.RemoveAt(objects_list.SelectedIndex);
            RedrawField();
            
        }

        private void objects_list_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (objects_list.SelectedIndex != -1)
                cur_polyhedron = objects_list.SelectedItems[0] as Polyhedron;
        }

        private void createCameraButton_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(objects_list.Items);
            this.AddOwnedForm(cameraForm);
            cameraForm.Show();
        }
    }
}