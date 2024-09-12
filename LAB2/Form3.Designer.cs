namespace LAB2
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_loadFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_saveFile = new System.Windows.Forms.Button();
            this.trackBar_Hue = new System.Windows.Forms.TrackBar();
            this.trackBar_Saturation = new System.Windows.Forms.TrackBar();
            this.trackBar_Value = new System.Windows.Forms.TrackBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Hue = new System.Windows.Forms.TextBox();
            this.textBox_Saturation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Value = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Hue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Saturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_loadFile
            // 
            this.button_loadFile.Location = new System.Drawing.Point(13, 11);
            this.button_loadFile.Name = "button_loadFile";
            this.button_loadFile.Size = new System.Drawing.Size(142, 23);
            this.button_loadFile.TabIndex = 2;
            this.button_loadFile.Text = "Загрузить файл...";
            this.button_loadFile.UseVisualStyleBackColor = true;
            this.button_loadFile.Click += new System.EventHandler(this.button_loadFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "*.png|*.jpg";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button_saveFile
            // 
            this.button_saveFile.Enabled = false;
            this.button_saveFile.Location = new System.Drawing.Point(13, 40);
            this.button_saveFile.Name = "button_saveFile";
            this.button_saveFile.Size = new System.Drawing.Size(142, 23);
            this.button_saveFile.TabIndex = 3;
            this.button_saveFile.Text = "Сохранить файл...";
            this.button_saveFile.UseVisualStyleBackColor = true;
            this.button_saveFile.Click += new System.EventHandler(this.button_saveFile_Click);
            // 
            // trackBar_Hue
            // 
            this.trackBar_Hue.BackColor = System.Drawing.SystemColors.Control;
            this.trackBar_Hue.Enabled = false;
            this.trackBar_Hue.Location = new System.Drawing.Point(243, 12);
            this.trackBar_Hue.Maximum = 360;
            this.trackBar_Hue.Name = "trackBar_Hue";
            this.trackBar_Hue.Size = new System.Drawing.Size(248, 45);
            this.trackBar_Hue.TabIndex = 4;
            this.trackBar_Hue.TickFrequency = 0;
            this.trackBar_Hue.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Hue.Value = 360;
            this.trackBar_Hue.Scroll += new System.EventHandler(this.trackBar_Hue_Scroll);
            // 
            // trackBar_Saturation
            // 
            this.trackBar_Saturation.Enabled = false;
            this.trackBar_Saturation.Location = new System.Drawing.Point(564, 12);
            this.trackBar_Saturation.Maximum = 100;
            this.trackBar_Saturation.Name = "trackBar_Saturation";
            this.trackBar_Saturation.Size = new System.Drawing.Size(248, 45);
            this.trackBar_Saturation.TabIndex = 5;
            this.trackBar_Saturation.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Saturation.Value = 50;
            this.trackBar_Saturation.Scroll += new System.EventHandler(this.trackBar_Saturation_Scroll);
            // 
            // trackBar_Value
            // 
            this.trackBar_Value.Enabled = false;
            this.trackBar_Value.Location = new System.Drawing.Point(564, 40);
            this.trackBar_Value.Maximum = 100;
            this.trackBar_Value.Name = "trackBar_Value";
            this.trackBar_Value.Size = new System.Drawing.Size(248, 45);
            this.trackBar_Value.TabIndex = 6;
            this.trackBar_Value.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Value.Value = 50;
            this.trackBar_Value.Scroll += new System.EventHandler(this.trackBar_Value_Scroll);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = global::LAB2.Properties.Resources.HueScale1;
            this.pictureBox2.Location = new System.Drawing.Point(251, 40);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(231, 23);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(13, 69);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(796, 369);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "H";
            // 
            // textBox_Hue
            // 
            this.textBox_Hue.Location = new System.Drawing.Point(192, 16);
            this.textBox_Hue.Name = "textBox_Hue";
            this.textBox_Hue.ReadOnly = true;
            this.textBox_Hue.Size = new System.Drawing.Size(34, 20);
            this.textBox_Hue.TabIndex = 9;
            this.textBox_Hue.Text = "0";
            this.textBox_Hue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Saturation
            // 
            this.textBox_Saturation.Location = new System.Drawing.Point(524, 16);
            this.textBox_Saturation.Name = "textBox_Saturation";
            this.textBox_Saturation.ReadOnly = true;
            this.textBox_Saturation.Size = new System.Drawing.Size(34, 20);
            this.textBox_Saturation.TabIndex = 11;
            this.textBox_Saturation.Text = "0";
            this.textBox_Saturation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(503, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "S";
            // 
            // textBox_Value
            // 
            this.textBox_Value.Location = new System.Drawing.Point(524, 43);
            this.textBox_Value.Name = "textBox_Value";
            this.textBox_Value.ReadOnly = true;
            this.textBox_Value.Size = new System.Drawing.Size(34, 20);
            this.textBox_Value.TabIndex = 13;
            this.textBox_Value.Text = "0";
            this.textBox_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(503, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "V";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "img.png";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(174, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Сброс";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_Value);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Saturation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_Hue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.trackBar_Value);
            this.Controls.Add(this.trackBar_Saturation);
            this.Controls.Add(this.trackBar_Hue);
            this.Controls.Add(this.button_saveFile);
            this.Controls.Add(this.button_loadFile);
            this.Name = "Form3";
            this.Text = "Задание 3";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form3_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Hue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Saturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_loadFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_saveFile;
        private System.Windows.Forms.TrackBar trackBar_Hue;
        private System.Windows.Forms.TrackBar trackBar_Saturation;
        private System.Windows.Forms.TrackBar trackBar_Value;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Hue;
        private System.Windows.Forms.TextBox textBox_Saturation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Value;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button1;
    }
}