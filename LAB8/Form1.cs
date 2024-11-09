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

namespace LAB7 {
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
            colors = new List<Color>{ Color.Red, Color.Green, Color.Blue};
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox6.SelectedIndex = 1;
            //objects_list.Items.Add(new Polyhedron(new List<Vertex> { new Vertex(0, 0, 0), new Vertex(100.0, 0, 0)},
            //                                  new List<List<int>> { new List<int>{ 1 }, new List<int>{ } },
            //                                  new List<List<int>> { new List<int> { 0 , 1 } }
            //                                  ));
            //objects_list.Items.Add(new Polyhedron(new List<Vertex> { new Vertex(0, 0, 0), new Vertex(0, 100, 0) },
            //                                  new List<List<int>> { new List<int> { 1 }, new List<int> { } },
            //                                  new List<List<int>> { new List<int> { 0, 1 } }
            //                                  ));
            //objects_list.Items.Add(new Polyhedron(new List<Vertex> { new Vertex(0, 0, 0), new Vertex(0, 0, 100) },
            //                                  new List<List<int>> { new List<int> { 1 }, new List<int> { } },
            //                                  new List<List<int>> { new List<int> { 0, 1 } }
            //                                  ));

            RedrawField();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e) {
            if (g != null)
                g.Dispose();
            g = pictureBox1.CreateGraphics();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
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
                default:
                    break;
            }
            objects_list.Items.Add(cur_polyhedron);
            colors.Add(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
            RedrawField();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) => RedrawField();

        /// <summary>
        /// Отрисовывает поле в зависимости от выбранного типа проекции
        /// </summary>
        public void RedrawField() {
            if (cur_polyhedron == null) return;

            double[,] matrix;

            switch (comboBox2.SelectedIndex) {
                case 0: // перспективная
                    double c = 1000;
                    matrix = new double[,] {
                        { 1, 0, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 0, 0, -1 / c },
                        { 0, 0, 0, 1 }
                    };
                    break;
                case 1: //аксонометрическая - изометрическая
                    var psi = AffineTransformations.DegreesToRadians(45);
                    var phi = AffineTransformations.DegreesToRadians(35);
                    matrix = new double[,] {
                        { Math.Cos(psi), Math.Sin(phi)*Math.Cos(psi), 0, 0 },
                        { 0,             Math.Cos(phi),               0, 0 },
                        { Math.Sin(psi),-Math.Sin(phi)*Math.Cos(psi), 0, 0 },
                        { 0,                                       0, 0, 1 }
                    };
                    break;
                default:
                    return;
            }

            //g.Clear(Color.White);

            switch (comboBox6.SelectedIndex)
            {
                case 0:
                    return;
                case 1:
                    //var ZbuffDraw(matrix);
                    var polyh = objects_list.Items;
                    var bmp = ZBuffer.ZBuff(matrix, objects_list.Items, colors, Width, Height);
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();
                    pictureBox1.Image = bmp;
                    return;
                default:
                    // старый алгоритм 
                    break;
            }

            {


                double[,] cur_m;
                Vertex line_start;
                Vertex line_end;

                //рисование осей координат
                cur_m = AffineTransformations.Multiply(new double[,] { { 50.0, 50.0, 50.0, 1 } }, matrix);
                line_start = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);
                //x
                cur_m = AffineTransformations.Multiply(new double[,] { { 200.0, 50.0, 50.0, 1 } }, matrix);
                line_end = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);
                g.DrawLine(new Pen(Color.Red, 3), (float)line_start.x, (float)line_start.y, (float)line_end.x, (float)line_end.y);
                //y
                cur_m = AffineTransformations.Multiply(new double[,] { { 50.0, 200.0, 50.0, 1 } }, matrix);
                line_end = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);
                g.DrawLine(new Pen(Color.Green, 3), (float)line_start.x, (float)line_start.y, (float)line_end.x, (float)line_end.y);
                //z
                cur_m = AffineTransformations.Multiply(new double[,] { { 50.0, 50.0, 200.0, 1 } }, matrix);
                line_end = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);
                g.DrawLine(new Pen(Color.Blue, 3), (float)line_start.x, (float)line_start.y, (float)line_end.x, (float)line_end.y);



                // рисование полигона
                for (int i = 0; i < cur_polyhedron.vertices.Count; i++)
                {
                    cur_m = AffineTransformations.Multiply(new double[,] {{ cur_polyhedron.vertices[i].x,
                                                                                cur_polyhedron.vertices[i].y,
                                                                                cur_polyhedron.vertices[i].z,
                                                                                1 }}, matrix);
                    //line_start = new Vertex(cur_m[0,0]/ cur_m[0,3], cur_m[0, 1] / cur_m[0, 3], 0);
                    line_start = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);

                    //пробегает по всем граничным точкам и рисует линию
                    for (int j = 0; j < cur_polyhedron.edges[i].Count; j++)
                    {
                        var ind = cur_polyhedron.edges[i][j];
                        cur_m = AffineTransformations.Multiply(new double[,] {{ cur_polyhedron.vertices[ind].x,
                                                                            cur_polyhedron.vertices[ind].y,
                                                                            cur_polyhedron.vertices[ind].z,
                                                                            1 }}, matrix);
                        //line_end = new Vertex(cur_m[0, 0] / cur_m[0, 3], cur_m[0, 1] / cur_m[0, 3], 0);
                        line_end = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);
                        g.DrawLine(p, (float)line_start.x, (float)line_start.y, (float)line_end.x, (float)line_end.y);
                    }
                }

                //Рисование доп прямых:
                if (checkBox8.Checked)
                {
                    double x1, x2, y1, y2;
                    if (double.TryParse(textBox10.Text, out x1) && double.TryParse(textBox8.Text, out y1) &&
                        double.TryParse(textBox13.Text, out x2) && double.TryParse(textBox11.Text, out y2))
                    {
                        g.DrawLine(new Pen(Color.Purple, 3), new Point((int)x1, (int)y1), new Point((int)x2, (int)y2));
                    }
                }
            }
        }

        
        private void button1_Click(object sender, EventArgs e) {
            if (cur_polyhedron == null) return;

            if (checkBox1.Checked)
                MakeTranslation();

            if (checkBox2.Checked)
                MakeRotation();

            if (checkBox3.Checked)
                MakeScaling();

            if (checkBox9.Checked)
                MakeReflection();

            RedrawField();
        }

        void MakeTranslation() {
            double dx, dy, dz = 0.0;
            double.TryParse(textBox1.Text, out dx);
            double.TryParse(textBox2.Text, out dy);
            double.TryParse(textBox3.Text, out dz);

            AffineTransformations.Translation(ref cur_polyhedron, dx, dy, dz);
        }

        void MakeRotation() {
            double angle = 0.0;

            if (checkBox4.Checked) {
                double.TryParse(textBox4.Text, out angle);
                AffineTransformations.RotationAboutXAxis(ref cur_polyhedron, angle);
            }
            if (checkBox5.Checked) {
                double.TryParse(textBox5.Text, out angle);
                AffineTransformations.RotationAboutYAxis(ref cur_polyhedron, angle);
            }
            if (checkBox6.Checked) {
                double.TryParse(textBox6.Text, out angle);
                AffineTransformations.RotationAboutZAxis(ref cur_polyhedron, angle);
            }
            if (checkBox7.Checked) {
                double.TryParse(textBox7.Text, out angle);

                switch (comboBox3.SelectedIndex) {
                    case 0:
                        AffineTransformations.RotateAroundCenter(ref cur_polyhedron, angle, 0);
                        break;
                    case 1:
                        AffineTransformations.RotateAroundCenter(ref cur_polyhedron, angle, 1);
                        break;
                    case 2:
                        AffineTransformations.RotateAroundCenter(ref cur_polyhedron, angle, 2);
                        break;
                }
            }
            if (checkBox8.Checked) {
                double x1, x2, y1, y2, z1, z2;
                if (double.TryParse(textBox10.Text, out x1) && double.TryParse(textBox8.Text, out y1) && double.TryParse(textBox9.Text, out z1) &&
                    double.TryParse(textBox13.Text, out x2) && double.TryParse(textBox11.Text, out y2) && double.TryParse(textBox12.Text, out z2) &&
                    double.TryParse(textBox14.Text, out angle)) {
                    AffineTransformations.RotateAboutLine(ref cur_polyhedron, angle, x1, y1, z1, x2, y2, z2);
                }
            }
        }

        void MakeScaling() {
            double scaleX = 1, scaleY = 1, scaleZ = 1, scale = 1;

            if (checkBox12.Checked)
                double.TryParse(textBox17.Text, out scaleX);
            if (checkBox11.Checked)
                double.TryParse(textBox16.Text, out scaleY);
            if (checkBox10.Checked)
                double.TryParse(textBox15.Text, out scaleZ);
            if (checkBox13.Checked) {
                double.TryParse(textBox18.Text, out scale);
                AffineTransformations.Scale(ref cur_polyhedron, scale);
            }

            AffineTransformations.Scale(ref cur_polyhedron, scaleX, scaleY, scaleZ);
        }

        void MakeReflection() {
            double[,] matrix = new double[4, 4];

            switch (comboBox4.SelectedIndex) {
                case 0:
                    matrix = new double[,] {
                        { 1, 0, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 0, -1, 0 },
                        { 0, 0, 0, 1 }
                    };
                    break;
                case 1:
                    matrix = new double[,] {
                        { -1, 0, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 0, 1 }
                    };
                    break;
                case 2:
                    matrix = new double[,] {
                        { 1, 0, 0, 0 },
                        { 0, -1, 0, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 0, 1 }
                    };
                    break;
            }

            AffineTransformations.Reflect(ref cur_polyhedron, matrix);
        }

        private void textBox_TextChanged(object sender, EventArgs e) {
            double num;
            if (double.TryParse((sender as TextBox).Text, out num) == false)
                (sender as TextBox).BackColor = Color.Red;
            else
                (sender as TextBox).BackColor = Color.White;
        }

        /// <summary>
        /// Таймер для отображения сообщении о сохранении объекта
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
        private void saveStatusTimer_Tick(object sender, EventArgs e) {
            Text = "LAB7";
            saveStatusTimer.Stop();
        }

        /// <summary>
        /// Сохранение многогранника
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
        private void buttonSave_Click(object sender, EventArgs e) {
            if (cur_polyhedron == null) {
                MessageBox.Show("Нет загруженного многогранника.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog { Filter = "OBJ Files|*.obj", DefaultExt = "obj" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        OBJHandler.Save(cur_polyhedron, saveFileDialog.FileName);
                        Text = "LAB7: Файл сохранён успешно.";
                        saveStatusTimer.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Загрузка многогранника
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog { Filter = "OBJ Files|*.obj", DefaultExt = "obj" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        cur_polyhedron = OBJHandler.Load(openFileDialog.FileName);
                        objects_list.Items.Add(cur_polyhedron);
                        colors.Add(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
                        RedrawField();

                        Text = "LAB7: Файл загружен успешно.";
                        saveStatusTimer.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void showPlotButton_Click(object sender, EventArgs e)
        {
            double x0 = (double)numericUpDownX0.Value;
            double x1 = (double)numericUpDownX1.Value;
            double y0 = (double)numericUpDownY0.Value;
            double y1 = (double)numericUpDownY1.Value;

            if (x1 < x0)
            {
                numericUpDownX0.BackColor = Color.Red;
                numericUpDownX1.BackColor = Color.Red;
                return;
            }
            else
            {
                numericUpDownX_ValueChanged(sender, e);
            }

            if (y1 < y0)
            {
                numericUpDownY0.BackColor = Color.Red;
                numericUpDownY1.BackColor = Color.Red;
                return;
            }
            else
            {
                numericUpDownY_ValueChanged(sender, e);
            }

            // Получение графика
            try
            {
                cur_polyhedron = FunctionPlotting.GetPlot(textBoxFunc.Text, x0, x1, y0, y1,
                                                      (int)numericUpDownStep.Value);
                textBoxFunc_TextChanged(sender, e);
                objects_list.Items.Add(cur_polyhedron);
            }
            catch (Exception ex)
            {
                textBoxFunc.BackColor = Color.Red;
                return;
            }

            // Настройка приближения
            double scale = Math.Min(pictureBox1.Width / 2.0 / ((double)numericUpDownX1.Value - (double)numericUpDownX0.Value),
                                    pictureBox1.Height / 2.0 / ((double)numericUpDownY1.Value - (double)numericUpDownY0.Value));
            AffineTransformations.Scale(ref cur_polyhedron, scale);

            // Поворот
            if (plottingCheckBox.Checked)
            {
                AffineTransformations.RotateAroundCenter(ref cur_polyhedron, 70, 0);
                AffineTransformations.RotateAroundCenter(ref cur_polyhedron, 30, 1);
            }

            // Центрирование
            var center = AffineTransformations.CalculateCenterCoords(cur_polyhedron);
            var pbCenter = new double[2] { pictureBox1.Width / 2, pictureBox1.Height / 2 };
            AffineTransformations.Translation(ref cur_polyhedron, pbCenter[0] - center[0, 0], pbCenter[1] - center[0, 1], 0);            RedrawField();
        }

        private void textBoxFunc_TextChanged(object sender, EventArgs e)
        {
            textBoxFunc.BackColor = Color.White;
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownX0.BackColor = Color.White;
            numericUpDownX1.BackColor = Color.White;
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownY0.BackColor = Color.White;
            numericUpDownY1.BackColor = Color.White;
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double x, y, z;
            if (double.TryParse(textBox21.Text, out x) &&
                double.TryParse(textBox20.Text, out y) &&
                double.TryParse(textBox19.Text, out z))
            {
                listBox1.Items.Add("x:" + x.ToString() + 
                                   " y:" + y.ToString() + 
                                   " z:" + z.ToString());
            }
        }

        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex != -1 && listBox1.Items.Count > 1)
            {
                List<Vertex> v = new List<Vertex> { };
                foreach (var item in listBox1.Items)
                {
                    var s = (item as string).Split(' ');
                    v.Add(new Vertex(double.Parse(s[0].Substring(2)), 
                                     double.Parse(s[1].Substring(2)), 
                                     double.Parse(s[2].Substring(2))));
                }
                cur_polyhedron = PolyhedronCollection.MakeRotationFigure(comboBox5.SelectedItem as string, (int)numericUpDown1.Value, v);
                objects_list.Items.Add(cur_polyhedron);
                RedrawField();
            }
        }

        private void objects_list_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            CameraForm cameraForm = new CameraForm(objects_list.Items, 
                                                   new Camera.Point3(-600, 
                                                                     -150,
                                                                     1000),
                                                   new Camera.Point3(0, 0, -1000));
            this.AddOwnedForm(cameraForm);
            cameraForm.Show();
        }
    }
}