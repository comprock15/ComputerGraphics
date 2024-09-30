namespace LAB4
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonApplyTransform = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.affineScalePointY = new System.Windows.Forms.NumericUpDown();
            this.affineScalePointX = new System.Windows.Forms.NumericUpDown();
            this.affineRotationPointY = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.affineRotationPointX = new System.Windows.Forms.NumericUpDown();
            this.buttonSetScalePoint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSetRotationPoint = new System.Windows.Forms.Button();
            this.affineScaleCenter = new System.Windows.Forms.NumericUpDown();
            this.affineScalePoint = new System.Windows.Forms.NumericUpDown();
            this.checkBoxScaleCenter = new System.Windows.Forms.CheckBox();
            this.checkBoxScalePoint = new System.Windows.Forms.CheckBox();
            this.affineRotationCenterAngle = new System.Windows.Forms.NumericUpDown();
            this.affineRotationPointAngle = new System.Windows.Forms.NumericUpDown();
            this.checkBoxRotationCenter = new System.Windows.Forms.CheckBox();
            this.checkBoxRotationPoint = new System.Windows.Forms.CheckBox();
            this.checkBoxOffset = new System.Windows.Forms.CheckBox();
            this.affineOffsetDx = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.affineOffsetDy = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.affineScalePointY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineScalePointX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineRotationPointY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineRotationPointX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineScaleCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineScalePoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineRotationCenterAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineRotationPointAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineOffsetDx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineOffsetDy)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(472, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(646, 613);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(12, 12);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(276, 23);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "Очистка сцены";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.buttonApplyTransform);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.affineScalePointY);
            this.groupBox1.Controls.Add(this.affineScalePointX);
            this.groupBox1.Controls.Add(this.affineRotationPointY);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.affineRotationPointX);
            this.groupBox1.Controls.Add(this.buttonSetScalePoint);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonSetRotationPoint);
            this.groupBox1.Controls.Add(this.affineScaleCenter);
            this.groupBox1.Controls.Add(this.affineScalePoint);
            this.groupBox1.Controls.Add(this.checkBoxScaleCenter);
            this.groupBox1.Controls.Add(this.checkBoxScalePoint);
            this.groupBox1.Controls.Add(this.affineRotationCenterAngle);
            this.groupBox1.Controls.Add(this.affineRotationPointAngle);
            this.groupBox1.Controls.Add(this.checkBoxRotationCenter);
            this.groupBox1.Controls.Add(this.checkBoxRotationPoint);
            this.groupBox1.Controls.Add(this.checkBoxOffset);
            this.groupBox1.Controls.Add(this.affineOffsetDx);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.affineOffsetDy);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 264);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Афинные преобразования";
            // 
            // buttonApplyTransform
            // 
            this.buttonApplyTransform.Location = new System.Drawing.Point(6, 228);
            this.buttonApplyTransform.Name = "buttonApplyTransform";
            this.buttonApplyTransform.Size = new System.Drawing.Size(261, 23);
            this.buttonApplyTransform.TabIndex = 14;
            this.buttonApplyTransform.Text = "Применить преобразование";
            this.buttonApplyTransform.UseVisualStyleBackColor = true;
            this.buttonApplyTransform.Click += new System.EventHandler(this.buttonApplyTransform_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(159, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = ")";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = ")";
            // 
            // affineScalePointY
            // 
            this.affineScalePointY.Location = new System.Drawing.Point(113, 141);
            this.affineScalePointY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.affineScalePointY.Name = "affineScalePointY";
            this.affineScalePointY.Size = new System.Drawing.Size(40, 20);
            this.affineScalePointY.TabIndex = 23;
            // 
            // affineScalePointX
            // 
            this.affineScalePointX.Location = new System.Drawing.Point(69, 141);
            this.affineScalePointX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.affineScalePointX.Name = "affineScalePointX";
            this.affineScalePointX.Size = new System.Drawing.Size(38, 20);
            this.affineScalePointX.TabIndex = 22;
            // 
            // affineRotationPointY
            // 
            this.affineRotationPointY.Location = new System.Drawing.Point(113, 76);
            this.affineRotationPointY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.affineRotationPointY.Name = "affineRotationPointY";
            this.affineRotationPointY.Size = new System.Drawing.Size(40, 20);
            this.affineRotationPointY.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "( x , y ) = (";
            // 
            // affineRotationPointX
            // 
            this.affineRotationPointX.Location = new System.Drawing.Point(69, 76);
            this.affineRotationPointX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.affineRotationPointX.Name = "affineRotationPointX";
            this.affineRotationPointX.Size = new System.Drawing.Size(38, 20);
            this.affineRotationPointX.TabIndex = 17;
            // 
            // buttonSetScalePoint
            // 
            this.buttonSetScalePoint.Location = new System.Drawing.Point(175, 138);
            this.buttonSetScalePoint.Name = "buttonSetScalePoint";
            this.buttonSetScalePoint.Size = new System.Drawing.Size(92, 23);
            this.buttonSetScalePoint.TabIndex = 20;
            this.buttonSetScalePoint.Text = "Задать точку...";
            this.buttonSetScalePoint.UseVisualStyleBackColor = true;
            this.buttonSetScalePoint.Click += new System.EventHandler(this.buttonSetScalePoint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "( x , y ) = (";
            // 
            // buttonSetRotationPoint
            // 
            this.buttonSetRotationPoint.Location = new System.Drawing.Point(175, 73);
            this.buttonSetRotationPoint.Name = "buttonSetRotationPoint";
            this.buttonSetRotationPoint.Size = new System.Drawing.Size(92, 23);
            this.buttonSetRotationPoint.TabIndex = 15;
            this.buttonSetRotationPoint.Text = "Задать точку...";
            this.buttonSetRotationPoint.UseVisualStyleBackColor = true;
            this.buttonSetRotationPoint.Click += new System.EventHandler(this.buttonSetRotationPoint_Click);
            // 
            // affineScaleCenter
            // 
            this.affineScaleCenter.DecimalPlaces = 2;
            this.affineScaleCenter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.affineScaleCenter.Location = new System.Drawing.Point(215, 198);
            this.affineScaleCenter.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.affineScaleCenter.Name = "affineScaleCenter";
            this.affineScaleCenter.Size = new System.Drawing.Size(52, 20);
            this.affineScaleCenter.TabIndex = 13;
            // 
            // affineScalePoint
            // 
            this.affineScalePoint.DecimalPlaces = 2;
            this.affineScalePoint.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.affineScalePoint.Location = new System.Drawing.Point(215, 111);
            this.affineScalePoint.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.affineScalePoint.Name = "affineScalePoint";
            this.affineScalePoint.Size = new System.Drawing.Size(52, 20);
            this.affineScalePoint.TabIndex = 12;
            // 
            // checkBoxScaleCenter
            // 
            this.checkBoxScaleCenter.AutoSize = true;
            this.checkBoxScaleCenter.Location = new System.Drawing.Point(6, 192);
            this.checkBoxScaleCenter.Name = "checkBoxScaleCenter";
            this.checkBoxScaleCenter.Size = new System.Drawing.Size(196, 30);
            this.checkBoxScaleCenter.TabIndex = 11;
            this.checkBoxScaleCenter.Text = "Масштабирование относительно \r\nсвоего центра в\r\n";
            this.checkBoxScaleCenter.UseVisualStyleBackColor = true;
            // 
            // checkBoxScalePoint
            // 
            this.checkBoxScalePoint.AutoSize = true;
            this.checkBoxScalePoint.Location = new System.Drawing.Point(6, 105);
            this.checkBoxScalePoint.Name = "checkBoxScalePoint";
            this.checkBoxScalePoint.Size = new System.Drawing.Size(196, 30);
            this.checkBoxScalePoint.TabIndex = 10;
            this.checkBoxScalePoint.Text = "Масштабирование относительно \r\nзаданной точки в";
            this.checkBoxScalePoint.UseVisualStyleBackColor = true;
            // 
            // affineRotationCenterAngle
            // 
            this.affineRotationCenterAngle.DecimalPlaces = 2;
            this.affineRotationCenterAngle.Location = new System.Drawing.Point(215, 168);
            this.affineRotationCenterAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.affineRotationCenterAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.affineRotationCenterAngle.Name = "affineRotationCenterAngle";
            this.affineRotationCenterAngle.Size = new System.Drawing.Size(52, 20);
            this.affineRotationCenterAngle.TabIndex = 9;
            // 
            // affineRotationPointAngle
            // 
            this.affineRotationPointAngle.DecimalPlaces = 2;
            this.affineRotationPointAngle.Location = new System.Drawing.Point(215, 47);
            this.affineRotationPointAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.affineRotationPointAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.affineRotationPointAngle.Name = "affineRotationPointAngle";
            this.affineRotationPointAngle.Size = new System.Drawing.Size(52, 20);
            this.affineRotationPointAngle.TabIndex = 8;
            // 
            // checkBoxRotationCenter
            // 
            this.checkBoxRotationCenter.AutoSize = true;
            this.checkBoxRotationCenter.Location = new System.Drawing.Point(6, 169);
            this.checkBoxRotationCenter.Name = "checkBoxRotationCenter";
            this.checkBoxRotationCenter.Size = new System.Drawing.Size(197, 17);
            this.checkBoxRotationCenter.TabIndex = 7;
            this.checkBoxRotationCenter.Text = "Поворот вокруг своего центра на";
            this.checkBoxRotationCenter.UseVisualStyleBackColor = true;
            // 
            // checkBoxRotationPoint
            // 
            this.checkBoxRotationPoint.AutoSize = true;
            this.checkBoxRotationPoint.Location = new System.Drawing.Point(6, 47);
            this.checkBoxRotationPoint.Name = "checkBoxRotationPoint";
            this.checkBoxRotationPoint.Size = new System.Drawing.Size(203, 17);
            this.checkBoxRotationPoint.TabIndex = 6;
            this.checkBoxRotationPoint.Text = "Поворот вокруг заданной точки на";
            this.checkBoxRotationPoint.UseVisualStyleBackColor = true;
            // 
            // checkBoxOffset
            // 
            this.checkBoxOffset.AutoSize = true;
            this.checkBoxOffset.Location = new System.Drawing.Point(6, 24);
            this.checkBoxOffset.Name = "checkBoxOffset";
            this.checkBoxOffset.Size = new System.Drawing.Size(95, 17);
            this.checkBoxOffset.TabIndex = 5;
            this.checkBoxOffset.Text = "Смещение на";
            this.checkBoxOffset.UseVisualStyleBackColor = true;
            // 
            // affineOffsetDx
            // 
            this.affineOffsetDx.DecimalPlaces = 2;
            this.affineOffsetDx.Location = new System.Drawing.Point(131, 23);
            this.affineOffsetDx.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.affineOffsetDx.Name = "affineOffsetDx";
            this.affineOffsetDx.Size = new System.Drawing.Size(52, 20);
            this.affineOffsetDx.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "dy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "dx";
            // 
            // affineOffsetDy
            // 
            this.affineOffsetDy.DecimalPlaces = 2;
            this.affineOffsetDy.Location = new System.Drawing.Point(215, 23);
            this.affineOffsetDy.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.affineOffsetDy.Name = "affineOffsetDy";
            this.affineOffsetDy.Size = new System.Drawing.Size(52, 20);
            this.affineOffsetDy.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new System.Drawing.Point(12, 311);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 315);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Инструменты";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(31, 82);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(138, 20);
            this.textBox5.TabIndex = 19;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(115, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "нет";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(6, 283);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(136, 17);
            this.radioButton4.TabIndex = 17;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Рисование полигонов";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(139, 219);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "нет";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(32, 219);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "Выбранное ребро:";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(142, 40);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(125, 20);
            this.textBox4.TabIndex = 14;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(28, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Координаты точки:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(188, 128);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(79, 20);
            this.textBox3.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 242);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Ответ:";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(74, 242);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(193, 20);
            this.textBox2.TabIndex = 10;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 172);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Ответ:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Координаты точки:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(74, 169);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(193, 20);
            this.textBox1.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Первое ребро:";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 195);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(220, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "Положение точки относительно ребра";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 108);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(240, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Принадлежит ли заданная точка полигону";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(214, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "Поиск точки пересечения двух ребер";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.Location = new System.Drawing.Point(294, 13);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(172, 613);
            this.treeView1.TabIndex = 16;
            this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(25, 153);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(113, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "Выбранный полигон:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(144, 153);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 13);
            this.label16.TabIndex = 21;
            this.label16.Text = "нет";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 638);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "LAB4";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.affineScalePointY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineScalePointX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineRotationPointY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineRotationPointX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineScaleCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineScalePoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineRotationCenterAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineRotationPointAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineOffsetDx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.affineOffsetDy)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxOffset;
        private System.Windows.Forms.NumericUpDown affineOffsetDx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown affineOffsetDy;
        private System.Windows.Forms.NumericUpDown affineRotationCenterAngle;
        private System.Windows.Forms.NumericUpDown affineRotationPointAngle;
        private System.Windows.Forms.CheckBox checkBoxRotationCenter;
        private System.Windows.Forms.CheckBox checkBoxRotationPoint;
        private System.Windows.Forms.NumericUpDown affineScaleCenter;
        private System.Windows.Forms.NumericUpDown affineScalePoint;
        private System.Windows.Forms.CheckBox checkBoxScaleCenter;
        private System.Windows.Forms.CheckBox checkBoxScalePoint;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown affineScalePointY;
        private System.Windows.Forms.NumericUpDown affineScalePointX;
        private System.Windows.Forms.NumericUpDown affineRotationPointY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown affineRotationPointX;
        private System.Windows.Forms.Button buttonSetScalePoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSetRotationPoint;
        private System.Windows.Forms.Button buttonApplyTransform;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
    }
}

