namespace LAB3 {
    partial class Form2 {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioButtonBresenham;
        private System.Windows.Forms.RadioButton radioButtonWu;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonUndo;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButtonBresenham = new System.Windows.Forms.RadioButton();
            this.radioButtonWu = new System.Windows.Forms.RadioButton();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonUndo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Выход";
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(16, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(960, 580);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseClick);
            // 
            // radioButtonBresenham
            // 
            this.radioButtonBresenham.Checked = true;
            this.radioButtonBresenham.Location = new System.Drawing.Point(122, 17);
            this.radioButtonBresenham.Name = "radioButtonBresenham";
            this.radioButtonBresenham.Size = new System.Drawing.Size(100, 24);
            this.radioButtonBresenham.TabIndex = 1;
            this.radioButtonBresenham.TabStop = true;
            this.radioButtonBresenham.Text = "Брезенхем";
            this.radioButtonBresenham.CheckedChanged += new System.EventHandler(this.RadioButtonBresenham_CheckedChanged);
            // 
            // radioButtonWu
            // 
            this.radioButtonWu.Location = new System.Drawing.Point(228, 17);
            this.radioButtonWu.Name = "radioButtonWu";
            this.radioButtonWu.Size = new System.Drawing.Size(50, 24);
            this.radioButtonWu.TabIndex = 2;
            this.radioButtonWu.Text = "Ву";
            this.radioButtonWu.CheckedChanged += new System.EventHandler(this.RadioButtonBresenham_CheckedChanged);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(390, 15);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(100, 28);
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "Сброс";
            this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // buttonUndo
            // 
            this.buttonUndo.Location = new System.Drawing.Point(284, 15);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(100, 28);
            this.buttonUndo.TabIndex = 3;
            this.buttonUndo.Text = "Отмена";
            this.buttonUndo.UseVisualStyleBackColor = true;
            this.buttonUndo.Click += new System.EventHandler(this.ButtonUndo_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButtonBresenham);
            this.Controls.Add(this.radioButtonWu);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Рисование отрезка";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
    }
}