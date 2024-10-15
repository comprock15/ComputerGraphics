using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Lab5 {
    public partial class Form1 : Form {
        MainForm menu;
        LSystem lSystem;
        string filePath = "F_Koch.txt";

        public Form1(MainForm menu) {
            InitializeComponent();
            this.menu = menu;
        }

        private void Form1_Load(object sender, EventArgs e) {
            LoadLSystemFromFile(filePath);
            cmbFractals.SelectedIndex = 0;
        }

        private void LoadLSystemFromFile(string path) {
            try {
                lSystem = LSystem.FromFile(path);
                Invalidate();
            }
            catch (Exception ex) {
                MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            if (lSystem != null) {
                int width = ClientSize.Width;
                int height = ClientSize.Height;

                PointF startPoint = new PointF(width / 20, height * 3 / 4);

                float scale = Math.Min(width, height) / 75f;

                LSystemDrawer drawer = new LSystemDrawer(e.Graphics, lSystem.Angle);
                drawer.Draw(lSystem.Generate(), startPoint, scale, lSystem.InitialDirection);
            }
        }

        private void cmbFractals_SelectedIndexChanged(object sender, EventArgs e) {
            switch (cmbFractals.SelectedIndex) {
                case 0: filePath = "F_Koch.txt"; break;
                case 1: filePath = "F_Island_Koch.txt"; break;
                case 2: filePath = "F_Serpinski_Carpet.txt"; break;
                case 3: filePath = "F_Serpinski_Arrowhead.txt"; break;
                case 4: filePath = "F_Hilbert_Curve.txt"; break;
                case 5: filePath = "F_Harter-Haythaway.txt"; break;
                case 6: filePath = "F_Gosper_Hex.txt"; break;
            }
            LoadLSystemFromFile(filePath);
        }

        private void Form1_Resize(object sender, EventArgs e) => Invalidate();

        private void btnBack_Click(object sender, EventArgs e) => Close();

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) => menu.Show();
    }

    public class LSystem {
        public string Axiom { get; private set; }
        public float Angle { get; private set; }
        public float InitialDirection { get; private set; }
        private Dictionary<char, string> rules = new Dictionary<char, string>();
        private int Iterations;

        public static LSystem FromFile(string path) {
            var lines = File.ReadAllLines(path);
            if (lines.Length < 1) throw new Exception("Файл пуст.");

            var initialLine = lines[0].Split();
            if (initialLine.Length < 2) throw new Exception("Некорректный формат первой строки.");

            var axiom = initialLine[0];
            var angle = float.Parse(initialLine[1]);
            var initialDirection = initialLine.Length >= 3 ? float.Parse(initialLine[2]) : 0f;
            var iterations = initialLine.Length == 4 ? int.Parse(initialLine[3]) : 5;

            var lSystem = new LSystem(axiom, angle, initialDirection, iterations);

            for (int i = 1; i < lines.Length; i++) {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                var index = lines[i].IndexOf("->");
                if (index == -1) throw new Exception($"Некорректное правило в строке {i + 1}: {lines[i]}");

                var symbol = lines[i][0];
                var result = lines[i].Substring(index + 2).Trim();

                lSystem.AddRule(symbol, result);
            }

            return lSystem;
        }

        public LSystem(string axiom, float angle, float initialDirection, int iterations = 5) {
            Axiom = axiom;
            Angle = angle;
            InitialDirection = initialDirection;
            Iterations = iterations;
        }

        public void AddRule(char symbol, string result) => rules[symbol] = result;

        public string Generate() {
            string current = Axiom;
            for (int i = 0; i < Iterations; i++) {
                StringBuilder next = new StringBuilder();
                foreach (char c in current)
                    next.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
                current = next.ToString();
            }
            return current;
        }

        private string ApplyRules(string input) {
            var output = new List<char>();

            foreach (var symbol in input) {
                if (rules.ContainsKey(symbol))
                    output.AddRange(rules[symbol]);
                else
                    output.Add(symbol);
            }

            return new string(output.ToArray());
        }
    }

    public class LSystemDrawer {
        private Graphics graphics;
        private float angleStep;
        private Random random;

        public LSystemDrawer(Graphics graphics, float angleStep) {
            this.graphics = graphics;
            this.angleStep = angleStep;
            random = new Random();
        }

        public void Draw(string commands, PointF start, float scale, float initialDirection, float randomAngleRange = 0) {
            Stack<Tuple<PointF, float>> positionStack = new Stack<Tuple<PointF, float>>();
            var currentPosition = start;
            var currentDirection = initialDirection;

            foreach (var command in commands) {
                switch (command) {
                    case 'F':
                        var newPosition = MoveForward(currentPosition, currentDirection, scale);
                        graphics.DrawLine(Pens.Black, currentPosition, newPosition);
                        currentPosition = newPosition;
                        break;

                    case '+':
                        currentDirection += angleStep + GetRandomAngle(randomAngleRange);
                        break;

                    case '-':
                        currentDirection -= angleStep + GetRandomAngle(randomAngleRange);
                        break;

                    case '[':
                        positionStack.Push(Tuple.Create(currentPosition, currentDirection));
                        break;

                    case ']':
                        var savedState = positionStack.Pop();
                        currentPosition = savedState.Item1;
                        currentDirection = savedState.Item2;
                        break;
                }
            }
        }

        private PointF MoveForward(PointF point, float direction, float distance) {
            var rad = direction * Math.PI / 180;
            var newX = point.X + (float)(Math.Cos(rad) * distance);
            var newY = point.Y + (float)(Math.Sin(rad) * distance);
            return new PointF(newX, newY);
        }

        private float GetRandomAngle(float range) {
            if (range == 0) return 0;
            return (float)(random.NextDouble() * range * 2 - range);
        }
    }
}