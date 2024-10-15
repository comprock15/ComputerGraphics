using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace Lab5 {
    public partial class Form1 : Form {
        MainForm menu;
        LSystem lSystem;
        string filePath = @"..\..\L-systems\F_Koch.txt";

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

                PointF startPoint = new PointF(width / 16, height * 3 / 4);

                float scale = Math.Min(width, height) / 75f;

                LSystemDrawer drawer = new LSystemDrawer(e.Graphics, lSystem.Angle);
                drawer.Draw(lSystem.Generate(), startPoint, scale, lSystem.InitialDirection, lSystem.RandomBranching ? lSystem.Angle / 2 : 0);
            }
        }

        private void cmbFractals_SelectedIndexChanged(object sender, EventArgs e) {
            switch (cmbFractals.SelectedIndex) {
                case 0: filePath = @"..\..\L-systems\F_Koch.txt"; break;
                case 1: filePath = @"..\..\L-systems\F_Island_Koch.txt"; break;
                case 2: filePath = @"..\..\L-systems\F_Serpinski_Carpet.txt"; break;
                case 3: filePath = @"..\..\L-systems\F_Serpinski_Arrowhead.txt"; break;
                case 4: filePath = @"..\..\L-systems\F_Hilbert_Curve.txt"; break;
                case 5: filePath = @"..\..\L-systems\F_Harter-Haythaway.txt"; break;
                case 6: filePath = @"..\..\L-systems\F_Gosper_Hex.txt"; break;
                case 7: filePath = @"..\..\L-systems\T_Bush_1.txt"; break;
                case 8: filePath = @"..\..\L-systems\T_Bush_2.txt"; break;
                case 9: filePath = @"..\..\L-systems\T_Bush_3.txt"; break;
                case 10: filePath = @"..\..\L-systems\Hexagonal_Mosaic.txt"; break;
                case 11: filePath = @"..\..\L-systems\T_Tree_Random.txt"; break;
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
        public bool RandomBranching { get; private set; } = false;

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

            lSystem.RandomBranching = lines.Any(line => line.Contains('@'));

            for (int i = 1; i < lines.Length; ++i) {
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
            for (int i = 0; i < Iterations; ++i) {
                StringBuilder next = new StringBuilder();
                foreach (char c in current)
                    next.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
                current = next.ToString();
            }
            return current;
        }
    }

    public class LSystemDrawer {
        private Graphics graphics;
        private float angleStep;
        private Random random;
        private float randomAngleRange;

        public LSystemDrawer(Graphics graphics, float angleStep) {
            this.graphics = graphics;
            this.angleStep = angleStep;
            random = new Random();
            randomAngleRange = 0;
        }

        public void Draw(string commands, PointF start, float scale, float initialDirection, float randomAngleRange = 0) {
            Stack<Tuple<PointF, float>> positionStack = new Stack<Tuple<PointF, float>>();
            var currentPosition = start;
            var currentDirection = initialDirection;

            this.randomAngleRange = randomAngleRange;

            float initialThickness = randomAngleRange == 0 ? 1f : 10f;
            float thicknessDecayFactor = randomAngleRange > 0 ? 0.99875f : 1.0f;

            foreach (var command in commands) {
                switch (command) {
                    case 'F':
                        float factor = randomAngleRange == 0 ? scale : scale * 0.2f;
                        var newPosition = MoveForward(currentPosition, currentDirection, factor);

                        Color lineColor = GetColor(initialThickness);

                        graphics.DrawLine(new Pen(lineColor, initialThickness), currentPosition, newPosition);

                        currentPosition = newPosition;

                        if (randomAngleRange > 0) {
                            initialThickness *= thicknessDecayFactor;
                            if (initialThickness < 1) initialThickness = 1;
                        }
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

                    case '@':
                        currentDirection += GetRandomAngle(randomAngleRange);
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

        private float GetRandomAngle(float range) => (float)(random.NextDouble() * range * 2 - range);

        private Color GetColor(float thickness) {
            if (randomAngleRange == 0) return Color.Black;

            float normalizedThickness = Math.Max(1f, Math.Min(10f, thickness));

            int red = 48;
            int green = (int)(38 + (183 - 38) * (1 - (normalizedThickness - 1) / 9));
            int blue = (int)(33 + (0 - 33) * (1 - (normalizedThickness - 1) / 9));

            red = Math.Max(0, Math.Min(255, red));
            green = Math.Max(0, Math.Min(255, green));
            blue = Math.Max(0, Math.Min(255, blue));

            return Color.FromArgb(red, green, blue);
        }
    }
}