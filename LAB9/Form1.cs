using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LAB9 {

    enum ProjectionMode { Perspective, Other };
    enum DrawingMode { EdgesOnly, InvisFacesCutZBUFF, Zbuff };
    enum LightingMode { Disable, Guro, Phong };
    enum TexturingMode { Disable, ShowAllTextures };

    /// <summary> Главная форма</summary>
    public partial class Form1 : Form {
        /// <summary> Многогранник </summary>
        Polyhedron cur_polyhedron;
        /// <summary> Список всех многогранников на сцене </summary>
        List<Polyhedron> all_polyhedrons;

        List<Color> colors;

        Graphics g;
        //Graphics g2;

        Pen pen = new Pen(Color.Black, 2);

        private Random random = new Random();
        // TODO: Аффинные преобразования для источника света
        private Vector3 lightPosition = new Vector3(100, 100, 1000);
        //private Vector3 lightPosition = new Vector3(-1000, 0, 0);
        Camera camera;
        /// <summary> Текстура </summary>
        Bitmap current_texture;

        // Mods
        ProjectionMode proj_mode;
        DrawingMode draw_mode;
        LightingMode light_mode;
        TexturingMode texturing;

        int p_w, p_h;

        /// <summary> Инициализация формы </summary>
        public Form1() {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            //g2 = pictureBox3.CreateGraphics();
            camera = new Camera(pictureBox1.Width, pictureBox1.Height);
            colors = new List<Color> { };
            all_polyhedrons = new List<Polyhedron> { };
            p_w = pictureBox1.Width;
            p_h = pictureBox1.Height;

            SetStartSelectorsSettings();

            Redraw();
        }

        /// <summary>
        /// Настройка селекторов
        /// </summary>
        private void SetStartSelectorsSettings() {
            //comboBox1.SelectedIndex = 0;
            projectionModeSelector.SelectedIndex = 1;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            DrawModeSelector.SelectedIndex = 0;
            lightningComboBox.SelectedIndex = 0;
            texturing = TexturingMode.Disable;
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e) {
            if (g != null)
                g.Dispose();
            g = pictureBox1.CreateGraphics();
            p_w = pictureBox1.Width;
            p_h = pictureBox1.Height;

            camera.worldCenter = new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2);
        }

        /// <summary>
        /// Перерисовка всех полей 
        /// </summary>
        private void Redraw() {
            //RedrawField();
            UltimateFieldRedraw();
            //RedrawCamryField();
        }

        /// <summary>Проверка значения в текстовом поле с числом</summary>
        private void textBox_TextChanged(object sender, EventArgs e) {
            double num;
            if (double.TryParse((sender as TextBox).Text, out num) == false)
                (sender as TextBox).BackColor = Color.Red;
            else
                (sender as TextBox).BackColor = Color.White;
        }
        /// <summary>Выбор типа проекции</summary>
        private void projectionMode_SelectedIndexChanged(object sender, EventArgs e) {
            if (projectionModeSelector.SelectedIndex != -1)
                proj_mode = (ProjectionMode)projectionModeSelector.SelectedIndex;
            Redraw();
        }
        /// <summary>Выбор типа рисования</summary>
        private void DrawModeSelector_SelectedIndexChanged(object sender, EventArgs e) {
            if (DrawModeSelector.SelectedIndex != -1)
                draw_mode = (DrawingMode)DrawModeSelector.SelectedIndex;
            Redraw();
        }
        /// <summary>Выбор типа освещения</summary>
        private void lightningComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (lightningComboBox.SelectedIndex != -1)
                light_mode = (LightingMode)lightningComboBox.SelectedIndex;
            Redraw();
        }
        /// <summary>Загрузка текстуры</summary>
        private void loadTextureButton_Click(object sender, EventArgs e) {
            using (var openFileDialog = new OpenFileDialog()) {
                openFileDialog.Filter = "Image Files|*.bmp;*.png;*.jpg";
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    try {
                        current_texture = new Bitmap(openFileDialog.FileName);
                        texturePictureBox.Image = current_texture;
                        Text = "LAB9: Текстура успешно загружена";
                        saveStatusTimer.Start();
                    }
                    catch (Exception ex) {
                        MessageBox.Show($"Ошибка при загрузке текстуры: {ex.Message}");
                    }
                }
            }
        }

        /// <summary>Применение текстуры</summary>
        private void applyTextureButton_Click(object sender, EventArgs e) {
            if (current_texture == null) {
                MessageBox.Show("Сначала загрузите текстуру");
                return;
            }
            if (cur_polyhedron == null) {
                MessageBox.Show("Не выбран многогранник для применения текстуры");
                return;
            }
            try {
                cur_polyhedron.texture = current_texture;
                Text = $"LAB9: Текстура применена к {cur_polyhedron.GetName()}";
                saveStatusTimer.Start();
            }
            catch (Exception ex) {
                MessageBox.Show($"Ошибка применения текстуры: {ex.Message}");
            }
            Redraw();
        }

        /// <summary>Выбор типа текстурирования</summary>
        private void enableAllTexturesCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (enableAllTexturesCheckBox.Checked)
                texturing = TexturingMode.ShowAllTextures;
            else
                texturing = TexturingMode.Disable;
            Redraw();
        }
    }
}