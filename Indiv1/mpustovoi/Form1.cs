namespace mpustovoi {
    /// <summary>
    /// �����.
    /// </summary>
    public partial class Form1 : Form {
        /// <summary>
        /// ������ ���� �����.
        /// </summary>
        private List<PointF> points = [];
        /// <summary>
        /// �������� �������� � ������� ������.
        /// </summary>
        private List<PointF> convexHull = [];

        /// <summary>
        /// ������������� �����.
        /// </summary>
        public Form1() => InitializeComponent();

        /// <summary>
        /// ������������� ����� ����� ��� ���������� ��������.
        /// </summary>
        /// <param name="sender">�������� �������.</param>
        /// <param name="e">��������� �������.</param>
        private void OnMouseClick(object sender, MouseEventArgs e) {
            PointF newPoint = new(e.X, e.Y);
            points.Add(newPoint);
            UpdateConvexHull();
            Invalidate();
        }

        /// <summary>
        /// ����������� ��������.
        /// </summary>
        private void UpdateConvexHull() {
            if (points.Count < 3) {
                convexHull = new List<PointF>(points);
                return;
            }

            convexHull = FindConvexHull(ref points);
        }

        /// <summary>
        /// ��������� �������� �������� ��� ������ �����
        /// ���������� ����������� ������ � ������� �������.
        /// </summary>
        /// <param name="points">������ �����.</param>
        /// <returns>�������� � ������� ������.</returns>
        private static List<PointF> FindConvexHull(ref List<PointF> points) {
            List<PointF> hull = [];

            List<PointF> sortedPoints = [.. points.OrderBy(p => p.X).ThenBy(p => p.Y)]; // ���������� ������� ������

            foreach (var point in sortedPoints) { // ������ ������� ��������
                while (hull.Count >= 2 && CrossProduct(hull[^2], hull[^1], point) <= 0)
                    hull.RemoveAt(hull.Count - 1); // �������� ���������, �� ���������� ������ �������
                hull.Add(point);
            }

            int lowerHullCount = hull.Count; // ������� ������� ��������
            for (int i = sortedPoints.Count - 2; i >= 0; --i) {
                PointF point = sortedPoints[i];
                while (hull.Count > lowerHullCount && CrossProduct(hull[^2], hull[^1], point) <= 0)
                    hull.RemoveAt(hull.Count - 1); // 
                hull.Add(point);
            }

            hull.RemoveAt(hull.Count - 1); // �������� ���������

            return hull;
        }


        /// <summary>
        /// ��������� ��������� ������������ ��� ����������� ��������.
        /// </summary>
        /// <param name="p1">������ �����.</param>
        /// <param name="p2">������ �����.</param>
        /// <param name="p3">������ �����.</param>
        /// <returns>��������� ������������.
        /// ������ 0 � ������ ������� �������, 
        /// ������ 0 � �� ������� �������
        /// ����� 0 � ����������� ��� �� ����� ������.
        /// </returns>
        private static float CrossProduct(PointF p1, PointF p2, PointF p3) => (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);

        /// <summary>
        /// ������������ ����� � �������� �������� �� �����. 
        /// ����� �������� � ���� ������, � �������� �������� � � ���� �������������� � �������������� ��������.
        /// </summary>
        /// <param name="sender">�������� �������.</param>
        /// <param name="e">��������� �������.</param>
        private void OnPaint(object sender, PaintEventArgs e) {
            var g = e.Graphics;

            if (convexHull.Count > 1) {
                SolidBrush semiTransparentBrush = new(Color.FromArgb(100, Color.Red));
                g.FillPolygon(semiTransparentBrush, convexHull.ToArray()); // ������� �������������� ��������
            }

            foreach (var point in points) // ��������� �����
                g.FillEllipse(Brushes.Black, point.X - 5, point.Y - 5, 10, 10);

            if (convexHull.Count > 1) {
                Pen thickPen = new(Color.Red, 3);
                g.DrawPolygon(thickPen, convexHull.ToArray()); // ��������� �������� ��������
            }
        }


        /// <summary>
        /// ������� �����, ������ ��� �����.
        /// </summary>
        /// <param name="sender">�������� �������.</param>
        /// <param name="e">��������� �������.</param>
        private void ClearButtonClick(object sender, EventArgs e) {
            points.Clear();
            convexHull.Clear();
            Invalidate();
        }

        /// <summary>
        /// ������� ��������� ����� � �������������� ��������.
        /// </summary>
        /// <param name="sender">�������� �������.</param>
        /// <param name="e">��������� �������.</param>
        private void UndoButtonClick(object sender, EventArgs e) {
            if (points.Count > 0) {
                points.RemoveAt(points.Count - 1);
                UpdateConvexHull();
                Invalidate();
            }
        }
    }
}