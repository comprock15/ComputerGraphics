namespace Indiv2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCompute = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.buttonChangeColor = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.checkBoxAdditionalLight = new System.Windows.Forms.CheckBox();
            this.additLightX = new System.Windows.Forms.NumericUpDown();
            this.additLightY = new System.Windows.Forms.NumericUpDown();
            this.additLightZ = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.additLightValue = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lightY = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lightValue = new System.Windows.Forms.NumericUpDown();
            this.lightX = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.lightZ = new System.Windows.Forms.NumericUpDown();
            this.frame = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.additLightX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.additLightY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.additLightZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.additLightValue)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lightY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frame)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCompute
            // 
            this.btnCompute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompute.Location = new System.Drawing.Point(529, 417);
            this.btnCompute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(261, 28);
            this.btnCompute.TabIndex = 1;
            this.btnCompute.Text = "Отрендерить";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.renderButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.radioButton8);
            this.groupBox2.Controls.Add(this.radioButton7);
            this.groupBox2.Controls.Add(this.radioButton6);
            this.groupBox2.Controls.Add(this.radioButton5);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.buttonChangeColor);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(155, 252);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(261, 190);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Цвета объектов";
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new System.Drawing.Point(140, 74);
            this.radioButton8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(52, 20);
            this.radioButton8.TabIndex = 10;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "Куб";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(140, 48);
            this.radioButton7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(113, 20);
            this.radioButton7.TabIndex = 9;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "Большой шар";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(140, 21);
            this.radioButton6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(100, 20);
            this.radioButton6.TabIndex = 8;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "Малый шар";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(5, 126);
            this.radioButton5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(118, 20);
            this.radioButton5.TabIndex = 7;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Правая стена";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(5, 98);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(109, 20);
            this.radioButton4.TabIndex = 6;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Левая стена";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(5, 73);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(116, 20);
            this.radioButton3.TabIndex = 5;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Задняя стена";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(5, 47);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(54, 20);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Пол";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(5, 21);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(84, 20);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Потолок";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // buttonChangeColor
            // 
            this.buttonChangeColor.Location = new System.Drawing.Point(5, 150);
            this.buttonChangeColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonChangeColor.Name = "buttonChangeColor";
            this.buttonChangeColor.Size = new System.Drawing.Size(247, 27);
            this.buttonChangeColor.TabIndex = 2;
            this.buttonChangeColor.Text = "Изменить цвет";
            this.buttonChangeColor.UseVisualStyleBackColor = true;
            this.buttonChangeColor.Click += new System.EventHandler(this.buttonChangeColor_Click);
            // 
            // checkBoxAdditionalLight
            // 
            this.checkBoxAdditionalLight.AutoSize = true;
            this.checkBoxAdditionalLight.Location = new System.Drawing.Point(5, 18);
            this.checkBoxAdditionalLight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxAdditionalLight.Name = "checkBoxAdditionalLight";
            this.checkBoxAdditionalLight.Size = new System.Drawing.Size(191, 20);
            this.checkBoxAdditionalLight.TabIndex = 5;
            this.checkBoxAdditionalLight.Text = "Вкл. доп. источник света";
            this.checkBoxAdditionalLight.UseVisualStyleBackColor = true;
            // 
            // additLightX
            // 
            this.additLightX.Location = new System.Drawing.Point(29, 50);
            this.additLightX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.additLightX.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.additLightX.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            -2147483648});
            this.additLightX.Name = "additLightX";
            this.additLightX.Size = new System.Drawing.Size(55, 22);
            this.additLightX.TabIndex = 6;
            this.additLightX.Value = new decimal(new int[] {
            7,
            0,
            0,
            -2147483648});
            this.additLightX.ValueChanged += new System.EventHandler(this.additLightX_ValueChanged);
            // 
            // additLightY
            // 
            this.additLightY.Location = new System.Drawing.Point(115, 50);
            this.additLightY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.additLightY.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.additLightY.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            -2147483648});
            this.additLightY.Name = "additLightY";
            this.additLightY.Size = new System.Drawing.Size(55, 22);
            this.additLightY.TabIndex = 6;
            this.additLightY.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.additLightY.ValueChanged += new System.EventHandler(this.additLightY_ValueChanged);
            // 
            // additLightZ
            // 
            this.additLightZ.Location = new System.Drawing.Point(197, 50);
            this.additLightZ.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.additLightZ.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.additLightZ.Name = "additLightZ";
            this.additLightZ.Size = new System.Drawing.Size(55, 22);
            this.additLightZ.TabIndex = 6;
            this.additLightZ.ValueChanged += new System.EventHandler(this.additLightZ_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Z:";
            // 
            // additLightValue
            // 
            this.additLightValue.DecimalPlaces = 1;
            this.additLightValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.additLightValue.Location = new System.Drawing.Point(197, 87);
            this.additLightValue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.additLightValue.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.additLightValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.additLightValue.Name = "additLightValue";
            this.additLightValue.Size = new System.Drawing.Size(55, 22);
            this.additLightValue.TabIndex = 6;
            this.additLightValue.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            this.additLightValue.ValueChanged += new System.EventHandler(this.additLightValue_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Интенсивность:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.additLightY);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.checkBoxAdditionalLight);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.additLightX);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.additLightValue);
            this.groupBox3.Controls.Add(this.additLightZ);
            this.groupBox3.Location = new System.Drawing.Point(530, 102);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(261, 121);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Дополнительный свет";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lightY);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.lightValue);
            this.groupBox4.Controls.Add(this.lightX);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.lightZ);
            this.groupBox4.Location = new System.Drawing.Point(530, 11);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(261, 87);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Свет";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Z:";
            // 
            // lightY
            // 
            this.lightY.Location = new System.Drawing.Point(115, 18);
            this.lightY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lightY.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.lightY.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            -2147483648});
            this.lightY.Name = "lightY";
            this.lightY.Size = new System.Drawing.Size(55, 22);
            this.lightY.TabIndex = 6;
            this.lightY.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.lightY.ValueChanged += new System.EventHandler(this.lightY_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(95, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Y:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "Интенсивность:";
            // 
            // lightValue
            // 
            this.lightValue.DecimalPlaces = 1;
            this.lightValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.lightValue.Location = new System.Drawing.Point(197, 50);
            this.lightValue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lightValue.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.lightValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.lightValue.Name = "lightValue";
            this.lightValue.Size = new System.Drawing.Size(55, 22);
            this.lightValue.TabIndex = 6;
            this.lightValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lightValue.ValueChanged += new System.EventHandler(this.lightValue_ValueChanged);
            // 
            // lightX
            // 
            this.lightX.Location = new System.Drawing.Point(29, 18);
            this.lightX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lightX.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.lightX.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            -2147483648});
            this.lightX.Name = "lightX";
            this.lightX.Size = new System.Drawing.Size(55, 22);
            this.lightX.TabIndex = 6;
            this.lightX.ValueChanged += new System.EventHandler(this.lightX_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "X:";
            // 
            // lightZ
            // 
            this.lightZ.Location = new System.Drawing.Point(197, 18);
            this.lightZ.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lightZ.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.lightZ.Name = "lightZ";
            this.lightZ.Size = new System.Drawing.Size(55, 22);
            this.lightZ.TabIndex = 6;
            this.lightZ.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.lightZ.ValueChanged += new System.EventHandler(this.lightZ_ValueChanged);
            // 
            // frame
            // 
            this.frame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frame.BackColor = System.Drawing.Color.White;
            this.frame.Location = new System.Drawing.Point(12, 7);
            this.frame.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.frame.Name = "frame";
            this.frame.Size = new System.Drawing.Size(512, 436);
            this.frame.TabIndex = 10;
            this.frame.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 455);
            this.Controls.Add(this.frame);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCompute);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Корнуэльская комната";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.additLightX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.additLightY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.additLightZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.additLightValue)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lightY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonChangeColor;
        private System.Windows.Forms.CheckBox checkBoxAdditionalLight;
        private System.Windows.Forms.NumericUpDown additLightX;
        private System.Windows.Forms.NumericUpDown additLightY;
        private System.Windows.Forms.NumericUpDown additLightZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown additLightValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown lightY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown lightValue;
        private System.Windows.Forms.NumericUpDown lightX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown lightZ;
        private System.Windows.Forms.PictureBox frame;
    }

}

