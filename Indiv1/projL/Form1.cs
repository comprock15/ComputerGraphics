using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace projL
{
    public partial class Form1 : Form
    {
        List<Point> points;
        
        SolidBrush b;
        List<Point> shell;
        Graphics g;
        Random rand;

        public Form1()
        {
            InitializeComponent();


            g = pictureBox1.CreateGraphics();
            b = new SolidBrush(Color.Black);
            
            points = new List<Point>() { };
            shell = new List<Point>() { };
            
            rand = new Random();
            numericUpDown1.Maximum = Width-5;
            numericUpDown2.Maximum = Height - 5;
        }

        

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            points.Add(new Point(e.X, e.Y));
            g.FillEllipse(b, e.X - 5, e.Y - 5, 10, 10);
        }

        private void button_addRandomPoint_Click(object sender, EventArgs e)
        {
            for (decimal i = 0; i < numericUpDown_countRandomP.Value; i++)
            {
                points.Add(new Point(rand.Next(3, pictureBox1.Width - 5), rand.Next(3, pictureBox1.Height- 5)));
                g.FillEllipse(b, points[points.Count - 1].X - 5, points[points.Count - 1].Y - 5, 10, 10);
            }
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            points.Clear();
            g.Clear(Color.White);
            shell.Clear();
            
        }

        private void button_addPointByCoords_Click(object sender, EventArgs e)
        {
            
            points.Add(new Point((int)numericUpDown1.Value, (int)numericUpDown2.Value));
            g.FillEllipse(b, points[points.Count - 1].X - 5, points[points.Count - 1].Y - 5, 10, 10);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
           // g.Dispose();
            g = pictureBox1.CreateGraphics();
            RedrawImage();
            numericUpDown1.Maximum = Width - 5;
            numericUpDown2.Maximum = Height - 5;
        }

        private void RedrawImage()
        {
            for (int i = 0; i < points.Count; i++)
            {
                g.FillEllipse(b, points[i].X - 3, points[i].Y - 3, 10, 10);
            }
        }

        

        private void button_createShell_Click(object sender, EventArgs e)
        {
            shell.Clear();
            if (points.Count > 2)
            { 
                MakeShell();
                DrawShell();
            }
            
        }

        private void DrawShell()
        {
            g.Clear(Color.White);
            
            Pen p = new Pen(Color.Blue, 3);
            for (int i = 0; i < shell.Count-1; i++ )
            {
                g.DrawLine(p, shell[i], shell[i + 1]);
            }
            RedrawImage();
        }

        private void MakeShell()
        {
            // сортируем от самой левой до самой правой точки 
            points = points.OrderBy(p => p.X).ToList();

            var p_l = points.First();
            var p_r = points.Last();
            var y = (int x) => { return (x - p_l.X) * (p_r.Y - p_l.Y) / (float)(p_r.X - p_l.X) + p_l.Y; };

            List<Point> points_up = new List<Point>();
            List<Point> points_bot = new List<Point>();

            // сортируем точки относительно прямой между самой левой и самой правой точками
            for (int i = 1; i < points.Count - 1; i++)
            {
                var y_cur = y(points[i].X);
                if (y_cur > points[i].Y)
                    points_up.Add(points[i]);
                else
                    points_bot.Add(points[i]);
            }

            

            MakeShell_Recur(ref points_up, p_l, p_r, true);

            var shell1 = shell.OrderBy(p => p.X).ToList();
            shell.Clear();
            
            MakeShell_Recur(ref points_bot, p_l, p_r, false);

            var shell2 = shell.OrderByDescending(p => p.X).ToList();

            shell.Clear();

            shell.Add(p_l);
            foreach (var p in shell1)
                shell.Add(p);
            shell.Add(p_r);
            foreach (var p in shell2)
                shell.Add(p);
            shell.Add(p_l);
        }

        private void MakeShell_Recur(ref List<Point> pnts, Point p_l , Point p_r, bool IsUp)
        {
            if (pnts.Count == 0) return;

            Point choose_p = pnts[0];
            double dist_choose_p = DistanceFromPointToLine( p_r, p_l, choose_p);
            double cur_dist;
            //находим самую удалённую и самую правую точку
            for (int i = 1; i < pnts.Count; i++)
            {
                cur_dist = DistanceFromPointToLine(p_r, p_l, pnts[i]);
                if (cur_dist >= dist_choose_p)
                {
                    if (cur_dist == dist_choose_p)
                    {
                        if (choose_p.X < pnts[i].X)
                        {
                            choose_p = pnts[i];
                            dist_choose_p = cur_dist;
                        }
                    }
                    else
                    {
                        choose_p = pnts[i];
                        dist_choose_p = cur_dist;
                    }
                }
            }

            //удаляем все точки внутри треугольника
            pnts.RemoveAll(p => IsPointInTriangle(p_l, p_r, choose_p, p));


            List<Point> pnts_l = new List<Point>();
            List<Point> pnts_r = new List<Point>();
            
            // уравнение новой прямой для сравнения
            var x = (int y) => { return (y - choose_p.Y) * (p_r.X - choose_p.X) / (double)(p_r.Y - choose_p.Y) + choose_p.X; }; 
            
            //делим оставшиеся на левые и правые относительно одной из новых прямых
            for (int i = 0; i < pnts.Count; i++)
            {
                var x_cur = x(pnts[i].Y);
                if (x_cur < pnts[i].X)
                    pnts_r.Add(pnts[i]);
                else
                    pnts_l.Add(pnts[i]);
            }

            //находим новые граничные точки если они есть
            MakeShell_Recur(ref pnts_r, choose_p, p_r, true);
            shell.Add(choose_p);
            MakeShell_Recur(ref pnts_l, p_l, choose_p, true); 
        }

        public bool IsPointInTriangle(Point p1, Point p2, Point p3, Point cur_p)
        {
            int a = (p1.X - cur_p.X) * (p2.Y - p1.Y) - (p2.X - p1.X) * (p1.Y - cur_p.Y);
            int b = (p2.X - cur_p.X) * (p3.Y - p2.Y) - (p3.X - p2.X) * (p2.Y - cur_p.Y);
            int c = (p3.X - cur_p.X) * (p1.Y - p3.Y) - (p1.X - p3.X) * (p3.Y - cur_p.Y);
            return ((a >= 0 && b >= 0 && c >= 0) || (a <= 0 && b <= 0 && c <= 0));
        }

        //private void MakeShell()
        //{
        //    // сортируем от самой левой до самой правой точки 
        //    points = points.OrderBy(p => p.X).ToList();

        //    var p_l = points.First();
        //    var p_r = points.Last();
        //    var y = (int x) => { return (x - p_l.X) * (p_r.Y - p_l.Y) / (float)(p_r.X - p_l.X) + p_l.Y; };

        //    // сортируем точки относительно прямой между самой левой и самой правой точками
        //    for (int i = 1; i < points.Count - 1; i++)
        //    {
        //        var y_cur = y(points[i].X);
        //        if (y_cur > points[i].Y)
        //            points_up.Add(points[i]);
        //        else
        //            points_bot.Add(points[i]);
        //    }
        //    shell.Add(p_l);
        //    shell.Add(p_r);
            

        //    while (points_up.Count != 0)
        //    {
        //        Point choose_p = points_up[0];
        //        double dist_choose_p = DistanceFromPointToLine(p_l, p_r, choose_p);
        //        double cur_dist;
        //        //поиск самой удалённой и самой правой точки
        //        foreach (var p in points_up)
        //        {
        //            cur_dist = DistanceFromPointToLine(p_l, p_r, p);
        //            if (cur_dist >= dist_choose_p)
        //            {
        //                if (cur_dist == dist_choose_p)
        //                {
        //                    if (choose_p.X < p.X)
        //                    {
        //                        choose_p = p;
        //                        dist_choose_p = cur_dist;
        //                    }
        //                }
        //                else
        //                {
        //                    choose_p = p;
        //                    dist_choose_p = cur_dist;
        //                }
        //            }
        //        }

        //        // Добавление точки в оболочку 
        //        shell.Add(choose_p);
        //        points_up.Remove(choose_p);

        //        //Удаление внутренних точек из списка рассмотрения
        //        shell = shell.OrderBy(p => p.X).ToList();
        //        shell.Add(p_l);
        //        points_up.RemoveAll(p => IsPointInShell(p));
                
        //        shell.RemoveAt(shell.Count - 1);
        //    }

        //    shell = shell.OrderBy(p => p.X).ToList();
            
        //    //shell.Add(p_l);

        //    List<Point> shell2 = new List<Point>(shell);
        //    shell.Clear();
        //    shell.Add(p_l);
        //    shell.Add(p_r);

        //    while (points_bot.Count != 0)
        //    {
        //        Point choose_p = points_bot[0];
        //        double dist_choose_p = DistanceFromPointToLine(p_l, p_r, choose_p);
        //        double cur_dist;
        //        //поиск самой удалённой и самой правой точки
        //        foreach (var p in points_bot)
        //        {
        //            cur_dist = DistanceFromPointToLine(p_l, p_r, p);
        //            if (cur_dist >= dist_choose_p)
        //            {
        //                if (cur_dist == dist_choose_p)
        //                {
        //                    if (choose_p.X < p.X)
        //                    {
        //                        choose_p = p;
        //                        dist_choose_p = cur_dist;
        //                    }
        //                }
        //                else
        //                {
        //                    choose_p = p;
        //                    dist_choose_p = cur_dist;
        //                }
        //            }
        //        }

        //        // Добавление точки в оболочку 
        //        shell.Add(choose_p);
        //        points_bot.Remove(choose_p);

        //        //Удаление внутренних точек из списка рассмотрения
        //        shell = shell.OrderBy(p => p.X).ToList();
        //        shell.Add(p_l);
        //        points_bot.RemoveAll(p => IsPointInShell(p));
        //        shell.RemoveAt(shell.Count - 1);
        //    }

        //    var shell1 = shell.OrderByDescending(p => p.X).ToList();
        //    shell.Clear();
        //    foreach (var p in shell1)
        //        shell.Add(p);
        //    foreach (var p in shell2)
        //        shell.Add(p);
        //}
        private double DistanceFromPointToLine(Point p1, Point p2, Point p_cur)
        {
            var dy = p2.Y - p1.Y;
            var dx = p2.X - p1.X;
            return Math.Abs(dy * p_cur.X - dx * p_cur.Y + p2.X * p1.Y - p2.Y * p1.X) / Math.Sqrt(dy * dy + dx * dx);
        }

        //private bool IsPointInShell(Point point)
        //{
        //    Point ray_point = new Point(Width + 10, point.Y);
        //    int res = 0;

        //    for (int i = 0; i < shell.Count - 1; i++)
        //    {
        //        var p1 = shell[i];
        //        var p2 = shell[i + 1];

        //        //res += (((a < c) ? c : a) <= ((b < d) ? b : d)) ? 1 : 0;

        //        float denom = (p2.X - p1.X) * (ray_point.Y - point.Y) - (p2.Y - p1.Y) * (ray_point.X - point.X);

        //        if (denom != 0)  // Не параллельные линии
        //        {
        //            float ua = ((ray_point.X - point.X) * (p1.Y - point.Y) - (ray_point.Y - point.Y) * (p1.X - point.X)) / denom;
        //            float ub = ((p2.X - p1.X) * (p1.Y - point.Y) - (p2.Y - p1.Y) * (p1.X - point.X)) / denom;

        //            if (!(ua < 0 || ua > 1 || ub < 0 || ub > 1)) // Нет пересечения на отрезках
        //                res += 1; 
        //        }
        //    }
        //    return res != 0 && res % 2 != 0;
        //}

        //private bool IsPointInShell(Point point)
        //{
        //    bool isLeft = false;
        //    bool isRight = false;
        //    int verticesCount = shell.Count;

        //    for (int i = 0; i < verticesCount - 1; i++)
        //    {
        //        var p1 = shell[i];
        //        var p2 = shell[i + 1];
        //        if (shell[i] != shell[i+1])
        //        {
        //            var x = (point.Y - p2.Y) * (p1.X - p2.X) / (float)(p1.Y - p2.Y) + p2.X;
        //            if (p2.Y < p1.Y) //проверка направления ребра
        //            {
        //                if (point.X > x)
        //                    isRight = true;
        //                //{ textBox2.Text = "Точка (" + point.X + ", " + point.Y + ") справа от ребра"; }
        //                else
        //                    isLeft = true;
        //                //{ textBox2.Text = "Точка (" + point.X + ", " + point.Y + ") слева от ребра"; }
        //            }
        //            else
        //            {
        //                if (point.X > x) isLeft = true;
        //                //{ textBox2.Text = "Точка (" + point.X + ", " + point.Y + ") слева от ребра"; }
        //                else isRight = true;
        //                //{ textBox2.Text = "Точка (" + point.X + ", " + point.Y + ") справа от ребра"; }
        //            }
        //        }
        //        if (isLeft == isRight)
        //            return false;
        //    }
        //    return true;
        //}

        //private void MakeShellWithAnim()
        //{
        //    // сортируем от самой левой до самой правой точки 
        //    points = points.OrderBy(p => p.X).ToList();




        //    var p_l = points.First();
        //    var p_r = points.Last();
        //    var y = (int x) => { return (x - p_l.X) * (p_r.Y - p_l.Y) / (float)(p_r.X - p_l.X) + p_l.Y; };

        //    // сортируем точки относительно прямой между самой левой и самой правой точками
        //    for (int i = 1; i < points.Count - 1; i++)
        //    {
        //        var y_cur = y(points[i].X);
        //        if (y_cur > points[i].Y)
        //            points_up.Add(points[i]);
        //        else
        //            points_bot.Add(points[i]);
        //    }
        //    shell.Add(p_l);
        //    shell.Add(p_r);


        //    while (points_up.Count != 0)
        //    {
        //        Point choose_p = points_up[0];
        //        double dist_choose_p = DistanceFromPointToLine(p_l, p_r, choose_p);
        //        double cur_dist;
        //        //поиск самой удалённой и самой правой точки
        //        foreach (var p in points_up)
        //        {
        //            cur_dist = DistanceFromPointToLine(p_l, p_r, p);
        //            if (cur_dist >= dist_choose_p)
        //            {
        //                if (cur_dist == dist_choose_p)
        //                {
        //                    if (choose_p.X < p.X)
        //                    {
        //                        choose_p = p;
        //                        dist_choose_p = cur_dist;
        //                    }
        //                }
        //                else
        //                {
        //                    choose_p = p;
        //                    dist_choose_p = cur_dist;
        //                }
        //            }
        //        }

        //        // Добавление точки в оболочку 
        //        shell.Add(choose_p);
        //        points_up.Remove(choose_p);

        //        //Удаление внутренних точек из списка рассмотрения
        //        shell = shell.OrderBy(p => p.X).ToList();
        //        shell.Add(p_l);
        //        DrawShell();
        //        points_up.RemoveAll(p => IsPointInShell(p));

        //        shell.RemoveAt(shell.Count - 1);

               
        //        System.Threading.Thread.Sleep(1500);
        //    }

        //    shell = shell.OrderBy(p => p.X).ToList();

        //    //shell.Add(p_l);

        //    List<Point> shell2 = new List<Point>(shell);
        //    shell.Clear();
        //    shell.Add(p_l);
        //    shell.Add(p_r);

        //    while (points_bot.Count != 0)
        //    {
        //        Point choose_p = points_bot[0];
        //        double dist_choose_p = DistanceFromPointToLine(p_l, p_r, choose_p);
        //        double cur_dist;
        //        //поиск самой удалённой и самой правой точки
        //        foreach (var p in points_bot)
        //        {
        //            cur_dist = DistanceFromPointToLine(p_l, p_r, p);
        //            if (cur_dist >= dist_choose_p)
        //            {
        //                if (cur_dist == dist_choose_p)
        //                {
        //                    if (choose_p.X < p.X)
        //                    {
        //                        choose_p = p;
        //                        dist_choose_p = cur_dist;
        //                    }
        //                }
        //                else
        //                {
        //                    choose_p = p;
        //                    dist_choose_p = cur_dist;
        //                }
        //            }
        //        }

        //        // Добавление точки в оболочку 
        //        shell.Add(choose_p);
        //        points_bot.Remove(choose_p);

        //        //Удаление внутренних точек из списка рассмотрения
        //        shell = shell.OrderBy(p => p.X).ToList();
        //        shell.Add(p_l);
        //        points_bot.RemoveAll(p => IsPointInShell(p));



        //        shell.RemoveAt(shell.Count - 1);

        //        DrawShell();
        //        System.Threading.Thread.Sleep(1500);

        //    }

        //    var shell1 = shell.OrderByDescending(p => p.X).ToList();
        //    shell.Clear();
        //    foreach (var p in shell1)
        //        shell.Add(p);
        //    foreach (var p in shell2)
        //        shell.Add(p);
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            shell.Clear();
            //MakeShellWithAnim();
            DrawShell();
        }
    }
}
