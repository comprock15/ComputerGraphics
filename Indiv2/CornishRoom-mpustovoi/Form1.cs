using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CornishRoom_mpustovoi {
    /// <summary>
    /// Корнуэльская комната
    /// </summary>
    public partial class Form1 : Form {
        /// <summary>
        /// Изображение сцены
        /// </summary>
        Bitmap bmp = new Bitmap(670, 670);
        /// <summary>
        /// Высота окна
        /// </summary>
        public int height = 670;
        /// <summary>
        /// Ширина окна
        /// </summary>
        public int width = 670;
        /// <summary>
        /// Многогранники на сцене
        /// </summary>
        List<Polyhedron> polyhedrons = new List<Polyhedron>();
        /// <summary>
        /// Пиксели сцены
        /// </summary>
        public Vertor[,] pixels = new Vertor[670, 670];
        /// <summary>
        /// Цвета пикселей сцены
        /// </summary>
        public Color[,] colors = new Color[670, 670];
        /// <summary>
        /// Позиция камеры
        /// </summary>
        public Vertor camera = new Vertor();
        /// <summary>
        /// Источники света на сцене
        /// </summary>
        public List<LightSource> lightSources = new List<LightSource>();

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public Form1() {
            InitializeComponent();
            height = cornishRoomPictureBox.Height;
            width = cornishRoomPictureBox.Width;
        }

        /// <summary>
        /// Перерисовка сцены
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
        private void redrawButton_Click(object sender, EventArgs e) {
            height = cornishRoomPictureBox.Height;
            width = cornishRoomPictureBox.Height;
            bmp = new Bitmap(width, height);
            colors = new Color[width, height];
            pixels = new Vertor[width, height];
            camera = new Vertor();
            lightSources = new List<LightSource>();
            polyhedrons = new List<Polyhedron>();
            RayTrace();
        }

        /// <summary>
        /// Проверка видимости точки
        /// </summary>
        /// <param name="lightSourcePosition">Позиция источника света</param>
        /// <param name="lightIntersectionPosition">Точка пересечения луча с поверхностью</param>
        /// <returns>true, если точка видна; иначе false</returns>
        public bool VisiblePoint(Vertor lightSourcePosition, Vertor lightIntersectionPosition) {
            double maxIntersect = (lightSourcePosition - lightIntersectionPosition).Length();
            Ray ray = new Ray(lightIntersectionPosition, lightSourcePosition);

            bool isVisible = true;
            Parallel.ForEach(polyhedrons, (polyhedron, state) => {
                if (polyhedron.IntersectPolyhedrons(ray, out double intersect, out Vertor normalVector))
                    if (intersect < maxIntersect && 0.001 < intersect) {
                        isVisible = false;
                        state.Break();
                    }
            });
            return isVisible;
        }

        /// <summary>
        /// Расчёт коэффициента освещённости
        /// </summary>
        /// <param name="material">Материал</param>
        /// <param name="materialColor">Цвет материала</param>
        /// <returns>Коэффициент освещённости</returns>
        private Vertor CalculateAmbientCoefficient(double[] material, Color materialColor) {
            Vertor ambientCoefficient = new Vertor(1f, 1f, 1f) * material[2];
            ambientCoefficient.x *= materialColor.R / 255.0;
            ambientCoefficient.y *= materialColor.G / 255.0;
            ambientCoefficient.z *= materialColor.B / 255.0;
            return ambientCoefficient;
        }

        /// <summary>
        /// Трассировка луча
        /// </summary>
        /// <param name="ray">Луч</param>
        /// <param name="iter">Итерации трассировки</param>
        /// <param name="mediumRefraction">Преломление среды</param>
        /// <returns>Цвет</returns>
        public Vertor RayTrace(Ray ray, int iter, double mediumRefraction) {
            if (iter <= 0) return new Vertor(0, 0, 0);
            double closestIntersectPosition = 0;
            Vertor normalVector = null;
            double[] material = new double[5];
            Color materialColor = new Color();
            Vertor resColor = new Vertor(0, 0, 0);
            bool sharp = false;

            Parallel.ForEach(polyhedrons, (polyhedron) => {
                if (polyhedron.IntersectPolyhedrons(ray, out double intersect, out Vertor Normalize))
                    if (intersect < closestIntersectPosition || closestIntersectPosition == 0) {
                        closestIntersectPosition = intersect;
                        normalVector = Normalize;
                        material = polyhedron.material;
                        materialColor = polyhedron.materialColor;
                    }
            });

            if (closestIntersectPosition == 0) return new Vertor(0, 0, 0);

            if (Vertor.Scalar(ray.direction, normalVector) > 0) {
                normalVector *= -1;
                sharp = true;
            }

            Vertor lightIntersectionPosition = ray.position + ray.direction * closestIntersectPosition;

            Parallel.ForEach(lightSources, (light) => {
                resColor += CalculateAmbientCoefficient(material, materialColor);
                double shadingFactor = VisiblePoint(light.position, lightIntersectionPosition) ? 1 : 0.5;
                resColor += light.Shading(lightIntersectionPosition, normalVector, materialColor, material[3]) * shadingFactor;
            });

            if (material[0] > 0)
                resColor = material[0] * RayTrace(ray.Reflect(lightIntersectionPosition, normalVector), iter - 1, mediumRefraction);

            if (material[1] > 0) {
                double refractValue = sharp ? material[4] : 1 / material[4];

                Ray refractedRay = ray.Refract(lightIntersectionPosition, normalVector, material[1], refractValue);
                if (refractedRay != null)
                    resColor = material[1] * RayTrace(refractedRay, iter - 1, material[4]);
            }
            return resColor;
        }

        /// <summary>
        /// Установка зеркальности многогранника
        /// </summary>
        /// <param name="polyhedron">Многогранник</param>
        private void SetPolyhedronSpecularity(Polyhedron polyhedron) {
            if (frontWallSpecularityCheckBox.Checked) polyhedron.edges[1].material[0] = 1;
            if (leftWallSpecularityCheckBox.Checked) polyhedron.edges[4].material[0] = 1;
            if (rightWallSpecularityCheckBox.Checked) polyhedron.edges[3].material[0] = 1;
            if (backWallSpecularityCheckBox.Checked) polyhedron.edges[5].material[0] = 1;
        }

        /// <summary>
        /// Добавление куба на сцену
        /// </summary>
        /// <param name="radius">Размер</param>
        /// <param name="translation">Смещение</param>
        /// <param name="angleX">Поворот по X</param>
        /// <param name="angleY">Поворот по Y</param>
        /// <param name="angleZ">Поворот по Z</param>
        /// <param name="specularity">Зеркальность</param>
        private void AddCube(int radius, Vertor translation, double angleX, double angleY, double angleZ, Color color, bool specularity, bool transparency) {
            Polyhedron cube = Polyhedron.Hexahedron(radius);
            cube.edges = AffineTransformations.Rotate(cube, angleX, angleY, angleZ).edges;
            cube.edges = AffineTransformations.Translation(cube, translation.x, translation.y, translation.z).edges;
            cube.SetMaterial(specularity ? new double[] { 1, 0, 0.1, 0.7, 1.5 } : new double[] { 0, 0, 0.1, 0.7, 1.5 });
            if (transparency)
                cube.SetMaterial(new double[] { 0, 0.7, 0.1, 0.5, 1.05 });
            cube.edges.ForEach(e => e.color = color);
            polyhedrons.Add(cube);
        }

        /// <summary>
        /// Добавление сферы на сцену
        /// </summary>
        /// <param name="position">Позиция</param>
        /// <param name="radius">Радиус</param>
        /// <param name="translation">Смещение</param>
        /// <param name="specularity">Зеркальность</param>
        /// <param name="color">Цвет</param>
        private void AddSphere(double radius, Vertor position, Vertor translation, Color color, bool specularity, bool transparency) {
            Sphere sphere = new Sphere(position, radius);
            sphere.edges = AffineTransformations.Translation(sphere, translation.x, translation.y, translation.z).edges;
            sphere.SetMaterial(specularity ? new double[] { 1, 0, 0, 0.1, 1 } : new double[] { 0, 0, 0.1, 0.4, 1.5 });
            if (transparency)
                sphere.SetMaterial(new double[] { 0f, 0.9f, 0.1f, 0.5f, 1.05f });
            sphere.edges.ForEach(e => e.color = color);
            sphere.materialColor = color;
            polyhedrons.Add(sphere);
        }

        /// <summary>
        /// Создание многогранников не сцене
        /// </summary>
        public void InitPolyhedrons() {
            var cornishRoom = Polyhedron.Hexahedron(12);

            var edgesCount = 5;
            var backEdge = cornishRoom.edges[edgesCount];
            
            var center = new Vertor();
            cornishRoom.edges[edgesCount].vertices.ForEach(v => center += v);

            camera = center / 4 + Vertor.Normalize((backEdge.vertices[1] - backEdge.vertices[0]) * (backEdge.vertices[backEdge.vertices.Count - 1] - backEdge.vertices[0])) * 15;

            cornishRoom.edges[0].color = Color.SandyBrown;
            cornishRoom.edges[1].color = Color.White;
            cornishRoom.edges[2].color = Color.SkyBlue;
            cornishRoom.edges[3].color = Color.Blue;
            cornishRoom.edges[4].color = Color.Red;
            cornishRoom.edges[5].color = Color.AntiqueWhite;
            cornishRoom.edges.ForEach(x => x.material = new double[] { 0, 0, 0.05, 0.7, 1 });

            SetPolyhedronSpecularity(cornishRoom);

            polyhedrons.Add(cornishRoom);

            LightSource ls1 = new LightSource(new Vertor(0, 2, 5));
            //AddCube(1, ls1.position, 0, 0, 0, Color.White, false, true);
            LightSource ls2 = new LightSource(new Vertor());
            try {
                double x = double.Parse(secondLightSourceXPositionTextBox.Text);
                double y = double.Parse(secondLightSourceYPositionTextBox.Text);
                double z = double.Parse(secondLightSourceZPositionTextBox.Text);
                ls2 = new LightSource(new Vertor(x, y, z));
                //AddCube(1, ls2.position, 0, 0, 0, Color.White, false, true);
            }
            catch (FormatException) {
                MessageBox.Show("Некорректные координаты второго источника света.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ArgumentNullException) {
                MessageBox.Show("Пустые координаты второго источника света.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (firstLightSourceCheckBox.Checked) lightSources.Add(ls1);
            if (secondLightSourceCheckBox.Checked) lightSources.Add(ls2);

            AddCube(3, new Vertor(-4, 2, -4), 0, 15, 60, Color.Yellow, firstCubeSpecularityCheckBox.Checked, false);
            AddCube(4, new Vertor(5, -3, 3), 15, 0, 30, Color.Lavender, false, secondCubeTransparencyCheckBox.Checked);

            AddSphere(2, new Vertor(), new Vertor(3, 3, -2), Color.Lime, firstSphereSpecularityCheckBox.Checked, false);
            AddSphere(1.5, new Vertor(), new Vertor(-4.5, -2.5, 0), Color.LightPink, false, secondSphereTransparencyCheckBox.Checked);
        }

        /// <summary>
        /// Вычисление координат пикселей
        /// </summary>
        public void CalcPixels() {
            var vertices = polyhedrons[0].edges[5].vertices;
            Vertor shiftUp = (vertices[1] - vertices[0]) / (width - 1);
            Vertor shiftDown = (vertices[2] - vertices[3]) / (width - 1);

            Parallel.For(0, width, i => {
                Vertor up = vertices[0] + shiftUp * i;
                Vertor down = vertices[3] + shiftDown * i;

                Vertor yShift = (up - down) / (height - 1);
                Vertor d = new Vertor(down);

                for (int j = 0; j < height; ++j) {
                    pixels[i, j] = d;
                    d += yShift;
                }
            });
        }

        /// <summary>
        /// Трассировка лучей
        /// </summary>
        public void RayTrace() {
            InitPolyhedrons();
            CalcPixels();

            Parallel.For(0, width, i => {
                for (int j = 0; j < height; ++j) {
                    Ray r = new Ray(camera, pixels[i, j]) { position = new Vertor(pixels[i, j]) };
                    Vertor color = RayTrace(r, 10, 1);
                    if (color.x > 1.0f || color.y > 1.0f || color.z > 1.0f)
                        color = Vertor.Normalize(color);
                    colors[i, j] = Color.FromArgb((int)(255 * color.x), (int)(255 * color.y), (int)(255 * color.z));
                }
            });

            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; ++j)
                    bmp.SetPixel(i, j, colors[i, j]);
            cornishRoomPictureBox.Image = bmp;
        }
    }
}