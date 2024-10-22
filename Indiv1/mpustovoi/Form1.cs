namespace mpustovoi {
    /// <summary>
    /// Форма.
    /// </summary>
    public partial class Form1 : Form {
        /// <summary>
        /// Список всех точек.
        /// </summary>
        private List<PointF> points = [];
        /// <summary>
        /// Выпуклая оболочка в порядке обхода.
        /// </summary>
        private List<PointF> convexHull = [];

        /// <summary>
        /// Инициализация формы.
        /// </summary>
        public Form1() => InitializeComponent();

        /// <summary>
        /// Установливает новую точку для построения оболочки.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void OnMouseClick(object sender, MouseEventArgs e) {
            PointF newPoint = new(e.X, e.Y);
            points.Add(newPoint);
            UpdateConvexHull();
            Invalidate();
        }

        /// <summary>
        /// Перерисовка оболочки.
        /// </summary>
        private void UpdateConvexHull() {
            if (points.Count < 3) {
                convexHull = new List<PointF>(points);
                return;
            }

            convexHull = FindConvexHull(ref points);
        }

        /// <summary>
        /// Вычисляет выпуклую оболочку для набора точек
        /// поочерёдным построением нижней и верхней цепочек.
        /// </summary>
        /// <param name="points">Список точек.</param>
        /// <returns>Оболочка в порядке обхода.</returns>
        private static List<PointF> FindConvexHull(ref List<PointF> points) {
            List<PointF> hull = [];

            List<PointF> sortedPoints = [.. points.OrderBy(p => p.X).ThenBy(p => p.Y)]; // сортировка порядка обхода

            foreach (var point in sortedPoints) { // нижняя цепочка оболочки
                while (hull.Count >= 2 && CrossProduct(hull[^2], hull[^1], point) <= 0)
                    hull.RemoveAt(hull.Count - 1); // удаление последней, не образующей правый поворот
                hull.Add(point);
            }

            int lowerHullCount = hull.Count; // верхняя цепочка оболочки
            for (int i = sortedPoints.Count - 2; i >= 0; --i) {
                PointF point = sortedPoints[i];
                while (hull.Count > lowerHullCount && CrossProduct(hull[^2], hull[^1], point) <= 0)
                    hull.RemoveAt(hull.Count - 1); // 
                hull.Add(point);
            }

            hull.RemoveAt(hull.Count - 1); // удаление дубликата

            return hull;
        }


        /// <summary>
        /// Вычисляет векторное произведение для направления поворота.
        /// </summary>
        /// <param name="p1">Первая точка.</param>
        /// <param name="p2">Вторая точка.</param>
        /// <param name="p3">Третья точка.</param>
        /// <returns>Векторное произведение.
        /// Меньше 0 — против часовой стрелки, 
        /// Больше 0 — по часовой стрелке
        /// Равно 0 — параллельны или на одной прямой.
        /// </returns>
        private static float CrossProduct(PointF p1, PointF p2, PointF p3) => (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);

        /// <summary>
        /// Отрисовывает точки и выпуклую оболочку на форме. 
        /// Точки рисуются в виде кругов, а выпуклая оболочка — в виде многоугольника с полупрозрачной заливкой.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void OnPaint(object sender, PaintEventArgs e) {
            var g = e.Graphics;

            if (convexHull.Count > 1) {
                SolidBrush semiTransparentBrush = new(Color.FromArgb(100, Color.Red));
                g.FillPolygon(semiTransparentBrush, convexHull.ToArray()); // заливка полупрозрачной оболочки
            }

            foreach (var point in points) // рисование точек
                g.FillEllipse(Brushes.Black, point.X - 5, point.Y - 5, 10, 10);

            if (convexHull.Count > 1) {
                Pen thickPen = new(Color.Red, 3);
                g.DrawPolygon(thickPen, convexHull.ToArray()); // рисование выпуклой оболочки
            }
        }


        /// <summary>
        /// Очищает форму, удаляя все точки.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void ClearButtonClick(object sender, EventArgs e) {
            points.Clear();
            convexHull.Clear();
            Invalidate();
        }

        /// <summary>
        /// Удаляет последнюю точку и перерисовывает оболочку.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void UndoButtonClick(object sender, EventArgs e) {
            if (points.Count > 0) {
                points.RemoveAt(points.Count - 1);
                UpdateConvexHull();
                Invalidate();
            }
        }
    }
}