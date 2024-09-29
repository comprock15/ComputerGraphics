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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
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
            this.pictureBox1.Location = new System.Drawing.Point(629, 16);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(861, 734);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(16, 15);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(368, 28);
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
            this.groupBox1.Location = new System.Drawing.Point(16, 50);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(368, 325);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Афинные преобразования";
            // 
            // buttonApplyTransform
            // 
            this.buttonApplyTransform.Location = new System.Drawing.Point(8, 281);
            this.buttonApplyTransform.Margin = new System.Windows.Forms.Padding(4);
            this.buttonApplyTransform.Name = "buttonApplyTransform";
            this.buttonApplyTransform.Size = new System.Drawing.Size(348, 28);
            this.buttonApplyTransform.TabIndex = 14;
            this.buttonApplyTransform.Text = "Применить преобразование";
            this.buttonApplyTransform.UseVisualStyleBackColor = true;
            this.buttonApplyTransform.Click += new System.EventHandler(this.buttonApplyTransform_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(212, 176);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 17);
            this.label5.TabIndex = 24;
            this.label5.Text = ")";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(212, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = ")";
            // 
            // affineScalePointY
            // 
            this.affineScalePointY.Location = new System.Drawing.Point(151, 174);
            this.affineScalePointY.Margin = new System.Windows.Forms.Padding(4);
            this.affineScalePointY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.affineScalePointY.Name = "affineScalePointY";
            this.affineScalePointY.Size = new System.Drawing.Size(53, 22);
            this.affineScalePointY.TabIndex = 23;
            // 
            // affineScalePointX
            // 
            this.affineScalePointX.Location = new System.Drawing.Point(92, 174);
            this.affineScalePointX.Margin = new System.Windows.Forms.Padding(4);
            this.affineScalePointX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.affineScalePointX.Name = "affineScalePointX";
            this.affineScalePointX.Size = new System.Drawing.Size(51, 22);
            this.affineScalePointX.TabIndex = 22;
            // 
            // affineRotationPointY
            // 
            this.affineRotationPointY.Location = new System.Drawing.Point(151, 94);
            this.affineRotationPointY.Margin = new System.Windows.Forms.Padding(4);
            this.affineRotationPointY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.affineRotationPointY.Name = "affineRotationPointY";
            this.affineRotationPointY.Size = new System.Drawing.Size(53, 22);
            this.affineRotationPointY.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 176);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "( x , y ) = (";
            // 
            // affineRotationPointX
            // 
            this.affineRotationPointX.Location = new System.Drawing.Point(92, 94);
            this.affineRotationPointX.Margin = new System.Windows.Forms.Padding(4);
            this.affineRotationPointX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.affineRotationPointX.Name = "affineRotationPointX";
            this.affineRotationPointX.Size = new System.Drawing.Size(51, 22);
            this.affineRotationPointX.TabIndex = 17;
            // 
            // buttonSetScalePoint
            // 
            this.buttonSetScalePoint.Location = new System.Drawing.Point(233, 170);
            this.buttonSetScalePoint.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSetScalePoint.Name = "buttonSetScalePoint";
            this.buttonSetScalePoint.Size = new System.Drawing.Size(123, 28);
            this.buttonSetScalePoint.TabIndex = 20;
            this.buttonSetScalePoint.Text = "Задать точку...";
            this.buttonSetScalePoint.UseVisualStyleBackColor = true;
            this.buttonSetScalePoint.Click += new System.EventHandler(this.buttonSetScalePoint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "( x , y ) = (";
            // 
            // buttonSetRotationPoint
            // 
            this.buttonSetRotationPoint.Location = new System.Drawing.Point(233, 90);
            this.buttonSetRotationPoint.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSetRotationPoint.Name = "buttonSetRotationPoint";
            this.buttonSetRotationPoint.Size = new System.Drawing.Size(123, 28);
            this.buttonSetRotationPoint.TabIndex = 15;
            this.buttonSetRotationPoint.Text = "Задать точку...";
            this.buttonSetRotationPoint.UseVisualStyleBackColor = true;
            this.buttonSetRotationPoint.Click += new System.EventHandler(this.buttonSetRotationPoint_Click);
            // 
            // affineScaleCenter
            // 
            this.affineScaleCenter.Location = new System.Drawing.Point(287, 244);
            this.affineScaleCenter.Margin = new System.Windows.Forms.Padding(4);
            this.affineScaleCenter.Name = "affineScaleCenter";
            this.affineScaleCenter.Size = new System.Drawing.Size(69, 22);
            this.affineScaleCenter.TabIndex = 13;
            // 
            // affineScalePoint
            // 
            this.affineScalePoint.Location = new System.Drawing.Point(287, 137);
            this.affineScalePoint.Margin = new System.Windows.Forms.Padding(4);
            this.affineScalePoint.Name = "affineScalePoint";
            this.affineScalePoint.Size = new System.Drawing.Size(69, 22);
            this.affineScalePoint.TabIndex = 12;
            // 
            // checkBoxScaleCenter
            // 
            this.checkBoxScaleCenter.AutoSize = true;
            this.checkBoxScaleCenter.Location = new System.Drawing.Point(8, 236);
            this.checkBoxScaleCenter.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxScaleCenter.Name = "checkBoxScaleCenter";
            this.checkBoxScaleCenter.Size = new System.Drawing.Size(253, 38);
            this.checkBoxScaleCenter.TabIndex = 11;
            this.checkBoxScaleCenter.Text = "Масштабирование относительно \r\nсвоего центра на";
            this.checkBoxScaleCenter.UseVisualStyleBackColor = true;
            // 
            // checkBoxScalePoint
            // 
            this.checkBoxScalePoint.AutoSize = true;
            this.checkBoxScalePoint.Location = new System.Drawing.Point(8, 129);
            this.checkBoxScalePoint.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxScalePoint.Name = "checkBoxScalePoint";
            this.checkBoxScalePoint.Size = new System.Drawing.Size(253, 38);
            this.checkBoxScalePoint.TabIndex = 10;
            this.checkBoxScalePoint.Text = "Масштабирование относительно \r\nзаданной точки на";
            this.checkBoxScalePoint.UseVisualStyleBackColor = true;
            // 
            // affineRotationCenterAngle
            // 
            this.affineRotationCenterAngle.Location = new System.Drawing.Point(287, 207);
            this.affineRotationCenterAngle.Margin = new System.Windows.Forms.Padding(4);
            this.affineRotationCenterAngle.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.affineRotationCenterAngle.Name = "affineRotationCenterAngle";
            this.affineRotationCenterAngle.Size = new System.Drawing.Size(69, 22);
            this.affineRotationCenterAngle.TabIndex = 9;
            // 
            // affineRotationPointAngle
            // 
            this.affineRotationPointAngle.Location = new System.Drawing.Point(287, 58);
            this.affineRotationPointAngle.Margin = new System.Windows.Forms.Padding(4);
            this.affineRotationPointAngle.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.affineRotationPointAngle.Name = "affineRotationPointAngle";
            this.affineRotationPointAngle.Size = new System.Drawing.Size(69, 22);
            this.affineRotationPointAngle.TabIndex = 8;
            // 
            // checkBoxRotationCenter
            // 
            this.checkBoxRotationCenter.AutoSize = true;
            this.checkBoxRotationCenter.Location = new System.Drawing.Point(8, 208);
            this.checkBoxRotationCenter.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRotationCenter.Name = "checkBoxRotationCenter";
            this.checkBoxRotationCenter.Size = new System.Drawing.Size(250, 21);
            this.checkBoxRotationCenter.TabIndex = 7;
            this.checkBoxRotationCenter.Text = "Поворот вокруг своего центра на";
            this.checkBoxRotationCenter.UseVisualStyleBackColor = true;
            // 
            // checkBoxRotationPoint
            // 
            this.checkBoxRotationPoint.AutoSize = true;
            this.checkBoxRotationPoint.Location = new System.Drawing.Point(8, 58);
            this.checkBoxRotationPoint.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRotationPoint.Name = "checkBoxRotationPoint";
            this.checkBoxRotationPoint.Size = new System.Drawing.Size(261, 21);
            this.checkBoxRotationPoint.TabIndex = 6;
            this.checkBoxRotationPoint.Text = "Поворот вокруг заданной точки на";
            this.checkBoxRotationPoint.UseVisualStyleBackColor = true;
            // 
            // checkBoxOffset
            // 
            this.checkBoxOffset.AutoSize = true;
            this.checkBoxOffset.Location = new System.Drawing.Point(8, 30);
            this.checkBoxOffset.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxOffset.Name = "checkBoxOffset";
            this.checkBoxOffset.Size = new System.Drawing.Size(119, 21);
            this.checkBoxOffset.TabIndex = 5;
            this.checkBoxOffset.Text = "Смещение на";
            this.checkBoxOffset.UseVisualStyleBackColor = true;
            // 
            // affineOffsetDx
            // 
            this.affineOffsetDx.Location = new System.Drawing.Point(175, 28);
            this.affineOffsetDx.Margin = new System.Windows.Forms.Padding(4);
            this.affineOffsetDx.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.affineOffsetDx.Name = "affineOffsetDx";
            this.affineOffsetDx.Size = new System.Drawing.Size(69, 22);
            this.affineOffsetDx.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "dy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "dx";
            // 
            // affineOffsetDy
            // 
            this.affineOffsetDy.Location = new System.Drawing.Point(287, 28);
            this.affineOffsetDy.Margin = new System.Windows.Forms.Padding(4);
            this.affineOffsetDy.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.affineOffsetDy.Name = "affineOffsetDy";
            this.affineOffsetDy.Size = new System.Drawing.Size(69, 22);
            this.affineOffsetDy.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
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
            this.groupBox2.Location = new System.Drawing.Point(16, 383);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(368, 370);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Инструменты";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(8, 334);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(172, 21);
            this.radioButton4.TabIndex = 17;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Рисование полигонов";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(185, 255);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 17);
            this.label13.TabIndex = 16;
            this.label13.Text = "label13";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(43, 255);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 17);
            this.label12.TabIndex = 15;
            this.label12.Text = "Выбранное ребро:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(251, 49);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(104, 22);
            this.textBox4.TabIndex = 14;
            this.textBox4.Text = "(362 : 365)";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(37, 53);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(136, 17);
            this.label11.TabIndex = 13;
            this.label11.Text = "Координаты точки:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(251, 139);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(104, 22);
            this.textBox3.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(37, 283);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 17);
            this.label10.TabIndex = 11;
            this.label10.Text = "Ответ:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(99, 283);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(256, 22);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "Пишем сюда ашибки или ответ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 175);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 17);
            this.label9.TabIndex = 9;
            this.label9.Text = "Ответ:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 143);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Координаты точки:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(99, 171);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(256, 22);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "Пишем сюда ашибки или ответ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 78);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 17);
            this.label7.TabIndex = 5;
            this.label7.Text = "лэйбл для вывода ошибок\r\n";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(8, 225);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(286, 21);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "Положение точки относительно ребра";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(8, 114);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(312, 21);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Принадлежит ли заданная точка полигону";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(8, 23);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(278, 21);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "Поиск точки пересечения двух ребер";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.Location = new System.Drawing.Point(392, 16);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(229, 734);
            this.treeView1.TabIndex = 16;
            this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(465, 767);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 24);
            this.comboBox1.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1507, 766);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

