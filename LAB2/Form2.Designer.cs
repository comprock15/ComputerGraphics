using System.Windows.Forms;

namespace LAB2 {
    partial class Form2 {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.PictureBox pictureBoxSource;
        private System.Windows.Forms.PictureBox pictureBoxR;
        private System.Windows.Forms.PictureBox pictureBoxG;
        private System.Windows.Forms.PictureBox pictureBoxB;
        private System.Windows.Forms.PictureBox pictureBoxHistR;
        private System.Windows.Forms.PictureBox pictureBoxHistG;
        private System.Windows.Forms.PictureBox pictureBoxHistB;
        private System.Windows.Forms.Label labelProcessing;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.button1 = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.pictureBoxSource = new System.Windows.Forms.PictureBox();
            this.pictureBoxR = new System.Windows.Forms.PictureBox();
            this.pictureBoxG = new System.Windows.Forms.PictureBox();
            this.pictureBoxB = new System.Windows.Forms.PictureBox();
            this.pictureBoxHistR = new System.Windows.Forms.PictureBox();
            this.pictureBoxHistG = new System.Windows.Forms.PictureBox();
            this.pictureBoxHistB = new System.Windows.Forms.PictureBox();
            this.labelProcessing = new System.Windows.Forms.Label();

            this.button1.Location = new System.Drawing.Point(16, 15);
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.Text = "Назад";
            this.button1.Click += new System.EventHandler(this.Button1_Click);

            this.buttonLoad.Location = new System.Drawing.Point(124, 15);
            this.buttonLoad.Size = new System.Drawing.Size(120, 28);
            this.buttonLoad.Text = "Загрузить";
            this.buttonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);

            this.pictureBoxSource.Location = new System.Drawing.Point(16, 50);
            this.pictureBoxSource.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxSource.SizeMode = PictureBoxSizeMode.Zoom;

            this.pictureBoxR.Location = new System.Drawing.Point(230, 50);
            this.pictureBoxR.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxR.SizeMode = PictureBoxSizeMode.Zoom;

            this.pictureBoxG.Location = new System.Drawing.Point(444, 50);
            this.pictureBoxG.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxG.SizeMode = PictureBoxSizeMode.Zoom;

            this.pictureBoxB.Location = new System.Drawing.Point(658, 50);
            this.pictureBoxB.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxB.SizeMode = PictureBoxSizeMode.Zoom;

            this.pictureBoxHistR.Location = new System.Drawing.Point(230, 270);
            this.pictureBoxHistR.Size = new System.Drawing.Size(200, 100);
            this.pictureBoxHistR.SizeMode = PictureBoxSizeMode.Zoom;

            this.pictureBoxHistG.Location = new System.Drawing.Point(444, 270);
            this.pictureBoxHistG.Size = new System.Drawing.Size(200, 100);
            this.pictureBoxHistG.SizeMode = PictureBoxSizeMode.Zoom;

            this.pictureBoxHistB.Location = new System.Drawing.Point(658, 270);
            this.pictureBoxHistB.Size = new System.Drawing.Size(200, 100);
            this.pictureBoxHistB.SizeMode = PictureBoxSizeMode.Zoom;

            this.labelProcessing.Location = new System.Drawing.Point(16, 380);
            this.labelProcessing.Size = new System.Drawing.Size(200, 23);
            this.labelProcessing.Text = "";

            this.ClientSize = new System.Drawing.Size(878, 400);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.pictureBoxSource);
            this.Controls.Add(this.pictureBoxR);
            this.Controls.Add(this.pictureBoxG);
            this.Controls.Add(this.pictureBoxB);
            this.Controls.Add(this.pictureBoxHistR);
            this.Controls.Add(this.pictureBoxHistG);
            this.Controls.Add(this.pictureBoxHistB);
            this.Controls.Add(this.labelProcessing);
            this.Name = "Form2";
            this.Text = "Выделение R, G, B";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHistR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHistG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHistB)).EndInit();
        }
    }
}
