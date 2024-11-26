using LAB9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        Pen p;
        List<Color> colors;
        private Random random = new Random();

        /// <summary>
        /// Инициализация формы
        /// </summary>
        public Form1() {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            p = new Pen(Color.Black, 2);
            colors = new List<Color>{ };
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            
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
            Polyhedron old_polyhedron;
            if (cur_polyhedron != null)
                old_polyhedron = new Polyhedron(cur_polyhedron);
            else
                old_polyhedron = null;

            switch (comboBox1.SelectedIndex) {
                case 0:
                    cur_polyhedron = PolyhedronCollection.MakeTetrahedron();
                    break;
                case 1:
                    cur_polyhedron = PolyhedronCollection.MakeHexahedron();
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