namespace LAB7
{
    /// <summary>
    /// Форма
    /// </summary>
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
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            comboBox1 = new ComboBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            buttonLoad = new Button();
            buttonSave = new Button();
            comboBox4 = new ComboBox();
            checkBox9 = new CheckBox();
            button1 = new Button();
            textBox2 = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox3 = new TextBox();
            textBox1 = new TextBox();
            groupBox5 = new GroupBox();
            textBox14 = new TextBox();
            label12 = new Label();
            textBox11 = new TextBox();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            textBox12 = new TextBox();
            textBox13 = new TextBox();
            textBox8 = new TextBox();
            label6 = new Label();
            textBox7 = new TextBox();
            label7 = new Label();
            label5 = new Label();
            label8 = new Label();
            label4 = new Label();
            textBox9 = new TextBox();
            checkBox8 = new CheckBox();
            textBox10 = new TextBox();
            comboBox3 = new ComboBox();
            checkBox7 = new CheckBox();
            textBox6 = new TextBox();
            checkBox6 = new CheckBox();
            textBox5 = new TextBox();
            checkBox5 = new CheckBox();
            textBox4 = new TextBox();
            checkBox4 = new CheckBox();
            groupBox4 = new GroupBox();
            textBox18 = new TextBox();
            checkBox13 = new CheckBox();
            textBox15 = new TextBox();
            checkBox12 = new CheckBox();
            checkBox10 = new CheckBox();
            textBox17 = new TextBox();
            textBox16 = new TextBox();
            checkBox11 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            groupBox3 = new GroupBox();
            comboBox2 = new ComboBox();
            saveStatusTimer = new System.Windows.Forms.Timer();
            saveStatusTimer.Interval = 5000;
            saveStatusTimer.Tick += new System.EventHandler(this.saveStatusTimer_Tick);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = Color.White;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(300, 14);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(850, 894);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.SizeChanged += pictureBox1_SizeChanged;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Тетраэдр", "Гексаэдр", "Октаэдр", "Икосаэдр", "Додекаэдр" });
            comboBox1.Location = new Point(7, 22);
            comboBox1.Margin = new Padding(4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(266, 23);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Gainsboro;
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Location = new Point(14, 14);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(279, 54);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Тип многогранника";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox2.BackColor = Color.Gainsboro;
            groupBox2.Controls.Add(buttonLoad);
            groupBox2.Controls.Add(buttonSave);
            groupBox2.Controls.Add(comboBox4);
            groupBox2.Controls.Add(checkBox9);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(textBox3);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(groupBox5);
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(checkBox3);
            groupBox2.Controls.Add(checkBox2);
            groupBox2.Controls.Add(checkBox1);
            groupBox2.Location = new Point(14, 136);
            groupBox2.Margin = new Padding(4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4);
            groupBox2.Size = new Size(279, 772);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Преобразования";
            // 
            // buttonLoad
            // 
            buttonLoad.BackColor = Color.White;
            buttonLoad.Location = new Point(143, 727);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(130, 38);
            buttonLoad.TabIndex = 15;
            buttonLoad.Text = "Загрузить";
            buttonLoad.UseVisualStyleBackColor = false;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.White;
            buttonSave.Location = new Point(7, 727);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(130, 38);
            buttonSave.TabIndex = 14;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += buttonSave_Click;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Items.AddRange(new object[] { "Относительно плоскости OXY", "Относительно плоскости OYZ", "Относительно плоскости OXZ" });
            comboBox4.Location = new Point(7, 651);
            comboBox4.Margin = new Padding(4);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(266, 23);
            comboBox4.TabIndex = 13;
            // 
            // checkBox9
            // 
            checkBox9.AutoSize = true;
            checkBox9.Location = new Point(7, 624);
            checkBox9.Margin = new Padding(4);
            checkBox9.Name = "checkBox9";
            checkBox9.Size = new Size(91, 19);
            checkBox9.TabIndex = 12;
            checkBox9.Text = "Отражение ";
            checkBox9.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.BackColor = Color.White;
            button1.Location = new Point(7, 682);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(266, 38);
            button1.TabIndex = 11;
            button1.Text = "Применить преобразования";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Location = new Point(129, 43);
            textBox2.Margin = new Padding(4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(47, 23);
            textBox2.TabIndex = 6;
            textBox2.TextChanged += textBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(184, 45);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(14, 15);
            label3.TabIndex = 10;
            label3.Text = "Z";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(107, 45);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(14, 15);
            label2.TabIndex = 9;
            label2.Text = "Y";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 45);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(14, 15);
            label1.TabIndex = 8;
            label1.Text = "X";
            // 
            // textBox3
            // 
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.Location = new Point(206, 43);
            textBox3.Margin = new Padding(4);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(47, 23);
            textBox3.TabIndex = 7;
            textBox3.TextChanged += textBox_TextChanged;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(52, 43);
            textBox1.Margin = new Padding(4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(47, 23);
            textBox1.TabIndex = 5;
            textBox1.TextChanged += textBox_TextChanged;
            // 
            // groupBox5
            // 
            groupBox5.BackColor = Color.WhiteSmoke;
            groupBox5.Controls.Add(textBox14);
            groupBox5.Controls.Add(label12);
            groupBox5.Controls.Add(textBox11);
            groupBox5.Controls.Add(label9);
            groupBox5.Controls.Add(label10);
            groupBox5.Controls.Add(label11);
            groupBox5.Controls.Add(textBox12);
            groupBox5.Controls.Add(textBox13);
            groupBox5.Controls.Add(textBox8);
            groupBox5.Controls.Add(label6);
            groupBox5.Controls.Add(textBox7);
            groupBox5.Controls.Add(label7);
            groupBox5.Controls.Add(label5);
            groupBox5.Controls.Add(label8);
            groupBox5.Controls.Add(label4);
            groupBox5.Controls.Add(textBox9);
            groupBox5.Controls.Add(checkBox8);
            groupBox5.Controls.Add(textBox10);
            groupBox5.Controls.Add(comboBox3);
            groupBox5.Controls.Add(checkBox7);
            groupBox5.Controls.Add(textBox6);
            groupBox5.Controls.Add(checkBox6);
            groupBox5.Controls.Add(textBox5);
            groupBox5.Controls.Add(checkBox5);
            groupBox5.Controls.Add(textBox4);
            groupBox5.Controls.Add(checkBox4);
            groupBox5.Location = new Point(7, 109);
            groupBox5.Margin = new Padding(4);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(4);
            groupBox5.Size = new Size(266, 340);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            // 
            // textBox14
            // 
            textBox14.BorderStyle = BorderStyle.FixedSingle;
            textBox14.Location = new Point(193, 307);
            textBox14.Margin = new Padding(4);
            textBox14.Name = "textBox14";
            textBox14.Size = new Size(47, 23);
            textBox14.TabIndex = 30;
            textBox14.TextChanged += textBox_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(154, 309);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(33, 15);
            label12.TabIndex = 31;
            label12.Text = "Угол";
            // 
            // textBox11
            // 
            textBox11.BorderStyle = BorderStyle.FixedSingle;
            textBox11.Location = new Point(116, 278);
            textBox11.Margin = new Padding(4);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(47, 23);
            textBox11.TabIndex = 25;
            textBox11.TextChanged += textBox_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(171, 280);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(18, 15);
            label9.TabIndex = 29;
            label9.Text = "z2";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(94, 280);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(19, 15);
            label10.TabIndex = 28;
            label10.Text = "y2";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(17, 280);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(19, 15);
            label11.TabIndex = 27;
            label11.Text = "x2";
            // 
            // textBox12
            // 
            textBox12.BorderStyle = BorderStyle.FixedSingle;
            textBox12.Location = new Point(193, 278);
            textBox12.Margin = new Padding(4);
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(47, 23);
            textBox12.TabIndex = 26;
            textBox12.TextChanged += textBox_TextChanged;
            // 
            // textBox13
            // 
            textBox13.BorderStyle = BorderStyle.FixedSingle;
            textBox13.Location = new Point(39, 278);
            textBox13.Margin = new Padding(4);
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(47, 23);
            textBox13.TabIndex = 24;
            textBox13.TextChanged += textBox_TextChanged;
            // 
            // textBox8
            // 
            textBox8.BorderStyle = BorderStyle.FixedSingle;
            textBox8.Location = new Point(116, 249);
            textBox8.Margin = new Padding(4);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(47, 23);
            textBox8.TabIndex = 13;
            textBox8.TextChanged += textBox_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(171, 251);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(18, 15);
            label6.TabIndex = 17;
            label6.Text = "z1";
            // 
            // textBox7
            // 
            textBox7.BorderStyle = BorderStyle.FixedSingle;
            textBox7.Location = new Point(193, 164);
            textBox7.Margin = new Padding(4);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(47, 23);
            textBox7.TabIndex = 12;
            textBox7.TextChanged += textBox_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(94, 251);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(19, 15);
            label7.TabIndex = 16;
            label7.Text = "y1";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(150, 166);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(33, 15);
            label5.TabIndex = 23;
            label5.Text = "Угол";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(17, 251);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(19, 15);
            label8.TabIndex = 15;
            label8.Text = "x1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 166);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(28, 15);
            label4.TabIndex = 22;
            label4.Text = "Ось";
            // 
            // textBox9
            // 
            textBox9.BorderStyle = BorderStyle.FixedSingle;
            textBox9.Location = new Point(193, 249);
            textBox9.Margin = new Padding(4);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(47, 23);
            textBox9.TabIndex = 14;
            textBox9.TextChanged += textBox_TextChanged;
            // 
            // checkBox8
            // 
            checkBox8.AutoSize = true;
            checkBox8.Location = new Point(7, 208);
            checkBox8.Margin = new Padding(4);
            checkBox8.Name = "checkBox8";
            checkBox8.Size = new Size(207, 34);
            checkBox8.TabIndex = 21;
            checkBox8.Text = "Вокруг прямой (заданной двумя \r\nточками) на заданный угол";
            checkBox8.UseVisualStyleBackColor = true;
            // 
            // textBox10
            // 
            textBox10.BorderStyle = BorderStyle.FixedSingle;
            textBox10.Location = new Point(39, 249);
            textBox10.Margin = new Padding(4);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(47, 23);
            textBox10.TabIndex = 12;
            textBox10.TextChanged += textBox_TextChanged;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "OX", "OY", "OZ" });
            comboBox3.Location = new Point(59, 163);
            comboBox3.Margin = new Padding(4);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(74, 23);
            comboBox3.TabIndex = 19;
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.Location = new Point(7, 107);
            checkBox7.Margin = new Padding(4);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new Size(229, 49);
            checkBox7.TabIndex = 20;
            checkBox7.Text = "Вокруг прямой, проходящей через \r\nцентр многогранника, параллельно \r\nоси на заданный угол";
            checkBox7.UseVisualStyleBackColor = true;
            // 
            // textBox6
            // 
            textBox6.BorderStyle = BorderStyle.FixedSingle;
            textBox6.Location = new Point(140, 64);
            textBox6.Margin = new Padding(4);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(47, 23);
            textBox6.TabIndex = 16;
            textBox6.TextChanged += textBox_TextChanged;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(7, 65);
            checkBox6.Margin = new Padding(4);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(115, 19);
            checkBox6.TabIndex = 15;
            checkBox6.Text = "Относительно Z";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            textBox5.BorderStyle = BorderStyle.FixedSingle;
            textBox5.Location = new Point(140, 37);
            textBox5.Margin = new Padding(4);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(47, 23);
            textBox5.TabIndex = 14;
            textBox5.TextChanged += textBox_TextChanged;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(7, 38);
            checkBox5.Margin = new Padding(4);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(115, 19);
            checkBox5.TabIndex = 13;
            checkBox5.Text = "Относительно Y";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            textBox4.BorderStyle = BorderStyle.FixedSingle;
            textBox4.Location = new Point(140, 10);
            textBox4.Margin = new Padding(4);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(47, 23);
            textBox4.TabIndex = 12;
            textBox4.TextChanged += textBox_TextChanged;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(7, 11);
            checkBox4.Margin = new Padding(4);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(115, 19);
            checkBox4.TabIndex = 0;
            checkBox4.Text = "Относительно Х";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.BackColor = Color.WhiteSmoke;
            groupBox4.Controls.Add(textBox18);
            groupBox4.Controls.Add(checkBox13);
            groupBox4.Controls.Add(textBox15);
            groupBox4.Controls.Add(checkBox12);
            groupBox4.Controls.Add(checkBox10);
            groupBox4.Controls.Add(textBox17);
            groupBox4.Controls.Add(textBox16);
            groupBox4.Controls.Add(checkBox11);
            groupBox4.Location = new Point(7, 493);
            groupBox4.Margin = new Padding(4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(4);
            groupBox4.Size = new Size(266, 125);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            // 
            // textBox18
            // 
            textBox18.BorderStyle = BorderStyle.FixedSingle;
            textBox18.Location = new Point(193, 90);
            textBox18.Margin = new Padding(4);
            textBox18.Name = "textBox18";
            textBox18.Size = new Size(47, 23);
            textBox18.TabIndex = 39;
            textBox18.TextChanged += textBox_TextChanged;
            // 
            // checkBox13
            // 
            checkBox13.AutoSize = true;
            checkBox13.Location = new Point(7, 90);
            checkBox13.Margin = new Padding(4);
            checkBox13.Name = "checkBox13";
            checkBox13.Size = new Size(146, 19);
            checkBox13.TabIndex = 38;
            checkBox13.Text = "Относительно центра";
            checkBox13.UseVisualStyleBackColor = true;
            // 
            // textBox15
            // 
            textBox15.BorderStyle = BorderStyle.FixedSingle;
            textBox15.Location = new Point(193, 62);
            textBox15.Margin = new Padding(4);
            textBox15.Name = "textBox15";
            textBox15.Size = new Size(47, 23);
            textBox15.TabIndex = 37;
            textBox15.TextChanged += textBox_TextChanged;
            // 
            // checkBox12
            // 
            checkBox12.AutoSize = true;
            checkBox12.Location = new Point(7, 10);
            checkBox12.Margin = new Padding(4);
            checkBox12.Name = "checkBox12";
            checkBox12.Size = new Size(115, 19);
            checkBox12.TabIndex = 32;
            checkBox12.Text = "Относительно Х";
            checkBox12.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            checkBox10.AutoSize = true;
            checkBox10.Location = new Point(7, 64);
            checkBox10.Margin = new Padding(4);
            checkBox10.Name = "checkBox10";
            checkBox10.Size = new Size(115, 19);
            checkBox10.TabIndex = 36;
            checkBox10.Text = "Относительно Z";
            checkBox10.UseVisualStyleBackColor = true;
            // 
            // textBox17
            // 
            textBox17.BorderStyle = BorderStyle.FixedSingle;
            textBox17.Location = new Point(193, 9);
            textBox17.Margin = new Padding(4);
            textBox17.Name = "textBox17";
            textBox17.Size = new Size(47, 23);
            textBox17.TabIndex = 33;
            textBox17.TextChanged += textBox_TextChanged;
            // 
            // textBox16
            // 
            textBox16.BorderStyle = BorderStyle.FixedSingle;
            textBox16.Location = new Point(193, 36);
            textBox16.Margin = new Padding(4);
            textBox16.Name = "textBox16";
            textBox16.Size = new Size(47, 23);
            textBox16.TabIndex = 35;
            textBox16.TextChanged += textBox_TextChanged;
            // 
            // checkBox11
            // 
            checkBox11.AutoSize = true;
            checkBox11.Location = new Point(7, 37);
            checkBox11.Margin = new Padding(4);
            checkBox11.Name = "checkBox11";
            checkBox11.Size = new Size(115, 19);
            checkBox11.TabIndex = 34;
            checkBox11.Text = "Относительно Y";
            checkBox11.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(7, 466);
            checkBox3.Margin = new Padding(4);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(131, 19);
            checkBox3.TabIndex = 2;
            checkBox3.Text = "Масштабирование";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(7, 82);
            checkBox2.Margin = new Padding(4);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(83, 19);
            checkBox2.TabIndex = 1;
            checkBox2.Text = "Вращение";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(7, 22);
            checkBox1.Margin = new Padding(4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(102, 19);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Смещение на";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.Gainsboro;
            groupBox3.Controls.Add(comboBox2);
            groupBox3.Location = new Point(14, 75);
            groupBox3.Margin = new Padding(4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4);
            groupBox3.Size = new Size(279, 54);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "Тип проекции";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Перспективная", "Аксонометрическая" });
            comboBox2.Location = new Point(7, 24);
            comboBox2.Margin = new Padding(4);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(266, 23);
            comboBox2.TabIndex = 0;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(1164, 922);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "LAB7";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.CheckBox checkBox13;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.CheckBox checkBox12;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.CheckBox checkBox11;
        private Button buttonLoad;
        private Button buttonSave;
        private System.Windows.Forms.Timer saveStatusTimer;
    }
}