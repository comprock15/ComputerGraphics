namespace Lab5 {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnBack = new System.Windows.Forms.Button();
            this.cmbFractals = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Назад";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // cmbFractals
            // 
            this.cmbFractals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFractals.FormattingEnabled = true;
            this.cmbFractals.Items.AddRange(new object[] {
                "Кривая Коха",
                "Квадратный остров Коха",
                "Ковёр Серпинского",
                "Наконечник Серпинского",
                "Кривая Гильберта",
                "Кривая дракона Хартера-Хейтуэя",
                "Шестиугольная кривая Госпера",
                "Куст 1",
                "Куст 2",
                "Куст 3",
                "Шестиугольная Мозаика",
                "Случайное дерево"});
            this.cmbFractals.Location = new System.Drawing.Point(93, 12);
            this.cmbFractals.Name = "cmbFractals";
            this.cmbFractals.Size = new System.Drawing.Size(250, 21);
            this.cmbFractals.TabIndex = 1;
            this.cmbFractals.SelectedIndexChanged += new System.EventHandler(this.cmbFractals_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbFractals);
            this.Controls.Add(this.btnBack);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "L-системы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ComboBox cmbFractals;
    }
}