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

    enum ProjectionMode { Perspective, Other};
    enum DrawingMode { EdgesOnly, Zbuff, InvisibleFacesCut, InvisFacesCutZBUFF };
    enum LightingMode { Disable, Guro, Phong};
    enum TexturingMode { Disable, ShowAllTextures };

    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class Form1 : Form 
    {
        /// <summary>
        /// Многогранник
        /// </summary>
        Polyhedron cur_polyhedron;
        /// <summary>
        /// Список всех многогранников на сцене
        /// </summary>
        List<Polyhedron> all_polyhedrons;


        Graphics g;
        Graphics g2;

        Pen pen = new Pen(Color.Black, 2);
        List<Color> colors;
        private Random random = new Random();
        private Vector3 lightPosition = new Vector3(100, 100, 1000);


        Camera camera;


        // Mods
        ProjectionMode proj_mode;
        DrawingMode draw_mode;
        LightingMode light_mode;
        TexturingMode textur_mode;


        /// <summary>
        /// Инициализация формы
        /// </summary>
        public Form1() 
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            g2 = pictureBox3.CreateGraphics();
            camera = new Camera(pictureBox3.Width, pictureBox3.Height);
            colors = new List<Color>{ };
            all_polyhedrons = new List<Polyhedron> { };


            SetStartSelectorsSettings();

            RedrawCamryField();

            RedrawField();
        }

        private void SetStartSelectorsSettings()
        {
            //comboBox1.SelectedIndex = 0;
            projectionModeSelector.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            DrawModeSelector.SelectedIndex = 0;
            lightningComboBox.SelectedIndex = 0;
            textur_mode = TexturingMode.Disable;
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
                all_polyhedrons.Add(cur_polyhedron);
                colors.Add(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
                Redraw();
            }
        }

        private void Redraw()
        {
            RedrawField();
            RedrawCamryField();
        }

        /// <summary>
        /// Проверка значения в текстовом полес числом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            {
                cur_polyhedron = all_polyhedrons[objects_list.SelectedIndex];
            }
                
        }

        /// <summary>Выбор типа проекции</summary>
        private void projectionMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (projectionModeSelector.SelectedIndex != -1)
                proj_mode = (ProjectionMode)projectionModeSelector.SelectedIndex;
            Redraw();
        }
        /// <summary>Выбор типа рисования</summary>
        private void DrawModeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DrawModeSelector.SelectedIndex != -1)
                draw_mode = (DrawingMode)DrawModeSelector.SelectedIndex;
            Redraw();
        }
        /// <summary>Выбор типа освещения</summary>
        private void lightningComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lightningComboBox.SelectedIndex != -1)
                light_mode = (LightingMode)lightningComboBox.SelectedIndex;
            Redraw();
        }

        private void textureCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (textureCheckBox.Checked)
                textur_mode = TexturingMode.ShowAllTextures;
            else
                textur_mode = TexturingMode.Disable;
            Redraw();
        }
    }
}