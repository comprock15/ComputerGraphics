using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LAB9 {
    public partial class Form1 : Form 
    {
        /// <summary> Создание многогранника из списка </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Polyhedron old_polyhedron = null;
            if (cur_polyhedron != null)
                old_polyhedron = new Polyhedron(cur_polyhedron);

            switch (comboBox1.SelectedIndex)
            {
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
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        cur_polyhedron = f.cur_polyhedron;
                    }
                    break;
                case 6:
                    var ff = new AddRotationFigurePolyhedronForm();
                    if (ff.ShowDialog() == DialogResult.OK)
                    {
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

        private void button_delete_obj_Click(object sender, EventArgs e)
        {
            if (objects_list.SelectedIndex == -1)
                return;
            colors.RemoveAt(objects_list.SelectedIndex);
            all_polyhedrons.RemoveAt(objects_list.SelectedIndex);
            objects_list.Items.RemoveAt(objects_list.SelectedIndex);
            Redraw();

        }

        private void objects_list_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (objects_list.SelectedIndex != -1)
            {
                cur_polyhedron = all_polyhedrons[objects_list.SelectedIndex];
            }
        }


        /// <summary>
        /// Загрузка многогранника из файла
        /// </summary>
        private void buttonLoad_Click(object sender, EventArgs e) {
            using (var openFileDialog = new OpenFileDialog { Filter = "OBJ Files|*.obj", DefaultExt = "obj" }) {
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    try {
                        cur_polyhedron = OBJHandler.Load(openFileDialog.FileName);
                        objects_list.Items.Add(cur_polyhedron);
                        all_polyhedrons.Add(cur_polyhedron);
                        colors.Add(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
                        Redraw();

                        Text = "LAB9: Файл загружен успешно.";
                        saveStatusTimer.Start();
                    }
                    catch (Exception ex) {
                        MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Сохранение многогранника
        /// </summary
        private void buttonSave_Click(object sender, EventArgs e) {
            if (cur_polyhedron == null) {
                MessageBox.Show("Нет загруженного многогранника.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog { Filter = "OBJ Files|*.obj", DefaultExt = "obj" }) {
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    try {
                        OBJHandler.Save(cur_polyhedron, saveFileDialog.FileName);
                        Text = "LAB9: Файл сохранён успешно.";
                        saveStatusTimer.Start();
                    }
                    catch (Exception ex) {
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Таймер для отображения сообщении о сохранении объекта
        /// </summary>
        private void saveStatusTimer_Tick(object sender, EventArgs e) {
            Text = "LAB9";
            saveStatusTimer.Stop();
        }
    }
}
