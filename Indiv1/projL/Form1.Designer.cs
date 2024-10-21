namespace projL
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
            this.button_createShell = new System.Windows.Forms.Button();
            this.button_addRandomPoint = new System.Windows.Forms.Button();
            this.numericUpDown_countRandomP = new System.Windows.Forms.NumericUpDown();
            this.button_Reset = new System.Windows.Forms.Button();
            this.button_addPointByCoords = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_countRandomP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(17, 86);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(943, 454);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // button_createShell
            // 
            this.button_createShell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_createShell.Location = new System.Drawing.Point(779, 15);
            this.button_createShell.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_createShell.Name = "button_createShell";
            this.button_createShell.Size = new System.Drawing.Size(183, 28);
            this.button_createShell.TabIndex = 1;
            this.button_createShell.Text = "Построить оболочку";
            this.button_createShell.UseVisualStyleBackColor = true;
            this.button_createShell.Click += new System.EventHandler(this.button_createShell_Click);
            // 
            // button_addRandomPoint
            // 
            this.button_addRandomPoint.Location = new System.Drawing.Point(17, 15);
            this.button_addRandomPoint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_addRandomPoint.Name = "button_addRandomPoint";
            this.button_addRandomPoint.Size = new System.Drawing.Size(292, 28);
            this.button_addRandomPoint.TabIndex = 2;
            this.button_addRandomPoint.Text = "Добавить случайные точки";
            this.button_addRandomPoint.UseVisualStyleBackColor = true;
            this.button_addRandomPoint.Click += new System.EventHandler(this.button_addRandomPoint_Click);
            // 
            // numericUpDown_countRandomP
            // 
            this.numericUpDown_countRandomP.Location = new System.Drawing.Point(385, 17);
            this.numericUpDown_countRandomP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDown_countRandomP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_countRandomP.Name = "numericUpDown_countRandomP";
            this.numericUpDown_countRandomP.Size = new System.Drawing.Size(164, 22);
            this.numericUpDown_countRandomP.TabIndex = 3;
            this.numericUpDown_countRandomP.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button_Reset
            // 
            this.button_Reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Reset.Location = new System.Drawing.Point(779, 50);
            this.button_Reset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(183, 28);
            this.button_Reset.TabIndex = 4;
            this.button_Reset.Text = "Сброс";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // button_addPointByCoords
            // 
            this.button_addPointByCoords.Location = new System.Drawing.Point(17, 50);
            this.button_addPointByCoords.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_addPointByCoords.Name = "button_addPointByCoords";
            this.button_addPointByCoords.Size = new System.Drawing.Size(292, 28);
            this.button_addPointByCoords.TabIndex = 5;
            this.button_addPointByCoords.Text = "Добавить точку по координатам";
            this.button_addPointByCoords.UseVisualStyleBackColor = true;
            this.button_addPointByCoords.Click += new System.EventHandler(this.button_addPointByCoords_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(319, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Кол-во:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Х:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(445, 57);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Y:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(349, 54);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(73, 22);
            this.numericUpDown1.TabIndex = 9;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(476, 54);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(73, 22);
            this.numericUpDown2.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(671, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 64);
            this.button1.TabIndex = 11;
            this.button1.Text = "Построить оболочку с анимацией";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 555);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_addPointByCoords);
            this.Controls.Add(this.button_Reset);
            this.Controls.Add(this.numericUpDown_countRandomP);
            this.Controls.Add(this.button_addRandomPoint);
            this.Controls.Add(this.button_createShell);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(781, 592);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuickHull";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_countRandomP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_createShell;
        private System.Windows.Forms.Button button_addRandomPoint;
        private System.Windows.Forms.NumericUpDown numericUpDown_countRandomP;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.Button button_addPointByCoords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button button1;
    }
}

