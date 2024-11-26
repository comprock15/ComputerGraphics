using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB9
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Загрузка многогранника
        /// </summary>
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

        /// <summary>
        /// Сохранение многогранника
        /// </summary
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (cur_polyhedron == null)
            {
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
        /// Таймер для отображения сообщении о сохранении объекта
        /// </summary>
        private void saveStatusTimer_Tick(object sender, EventArgs e)
        {
            Text = "LAB9";
            saveStatusTimer.Stop();
        }
    }
}
