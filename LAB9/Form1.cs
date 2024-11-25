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
            comboBox6.SelectedIndex = 1;
            
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

            label32.Text = cur_polyhedron.Center().ToString();

            Bitmap bmp = null;

            switch (comboBox6.SelectedIndex) {
                case 0:
                    bmp = BackfaceCulling.Cull(matrix, objects_list.Items, Width, Height);
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();
                    pictureBox1.Image = bmp;
                    return;
                case 1:
                    //var ZbuffDraw(matrix);
                    var polyh = objects_list.Items;
                    bmp = ZBuffer.ZBuff(matrix, objects_list.Items, Width, Height);
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
                for (int i = 0; i < cur_polyhedron.vertices.Count; i++) {
                    cur_m = AffineTransformations.Multiply(new double[,] {{ cur_polyhedron.vertices[i].x,
                                                                                cur_polyhedron.vertices[i].y,
                                                                                cur_polyhedron.vertices[i].z,
                                                                                1 }}, matrix);
                    //line_start = new Vertex(cur_m[0,0]/ cur_m[0,3], cur_m[0, 1] / cur_m[0, 3], 0);
                    line_start = new Vertex(cur_m[0, 0], cur_m[0, 1], 0);

                    //пробегает по всем граничным точкам и рисует линию
                    for (int j = 0; j < cur_polyhedron.edges[i].Count; j++) {
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
                if (checkBox8.Checked) {
                    double x1, x2, y1, y2;
                    if (double.TryParse(textBox10.Text, out x1) && double.TryParse(textBox8.Text, out y1) &&
                        double.TryParse(textBox13.Text, out x2) && double.TryParse(textBox11.Text, out y2)) {
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
            Text = "LAB9";
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
                        Text = "LAB9: Файл сохранён успешно.";
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

                        Text = "LAB9: Файл загружен успешно.";
                        saveStatusTimer.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}