using System.Windows.Forms;
using System;

namespace LAB2 {
    partial class Form2 {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.button1 = CreateButton("Назад", new System.Drawing.Point(16, 15), new System.Drawing.Size(100, 28), Button1_Click);
            this.buttonLoad = CreateButton("Загрузить", new System.Drawing.Point(124, 15), new System.Drawing.Size(120, 28), ButtonLoad_Click);

            this.pictureBoxSource = CreatePictureBox(new System.Drawing.Point(16, 50), new System.Drawing.Size(200, 200));
            this.pictureBoxR = CreatePictureBox(new System.Drawing.Point(230, 50), new System.Drawing.Size(200, 200));
            this.pictureBoxG = CreatePictureBox(new System.Drawing.Point(444, 50), new System.Drawing.Size(200, 200));
            this.pictureBoxB = CreatePictureBox(new System.Drawing.Point(658, 50), new System.Drawing.Size(200, 200));

            this.pictureBoxHistR = CreatePictureBox(new System.Drawing.Point(230, 270), new System.Drawing.Size(200, 100));
            this.pictureBoxHistG = CreatePictureBox(new System.Drawing.Point(444, 270), new System.Drawing.Size(200, 100));
            this.pictureBoxHistB = CreatePictureBox(new System.Drawing.Point(658, 270), new System.Drawing.Size(200, 100));

            this.labelProcessing = CreateLabel(new System.Drawing.Point(16, 380), new System.Drawing.Size(200, 23), "");

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.button1, this.buttonLoad,
                this.pictureBoxSource, this.pictureBoxR, this.pictureBoxG, this.pictureBoxB,
                this.pictureBoxHistR, this.pictureBoxHistG, this.pictureBoxHistB,
                this.labelProcessing
            });

            this.ClientSize = new System.Drawing.Size(878, 400);
            this.Name = "Form2";
            this.Text = "Выделение R, G, B";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
        }
        private Button CreateButton(string text, System.Drawing.Point location, System.Drawing.Size size, EventHandler clickHandler) {
            Button button = new Button {
                Text = text,
                Location = location,
                Size = size
            };
            button.Click += clickHandler;
            return button;
        }

        private PictureBox CreatePictureBox(System.Drawing.Point location, System.Drawing.Size size) {
            PictureBox pictureBox = new PictureBox {
                Location = location,
                Size = size,
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            };
            return pictureBox;
        }

        private Label CreateLabel(System.Drawing.Point location, System.Drawing.Size size, string text) {
            Label label = new Label();
            label.Location = location;
            label.Size = size;
            label.Text = text;
            return label;
        }

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.PictureBox pictureBoxSource;
        private System.Windows.Forms.PictureBox pictureBoxR;
        private System.Windows.Forms.PictureBox pictureBoxG;
        private System.Windows.Forms.PictureBox pictureBoxB;
        private System.Windows.Forms.PictureBox pictureBoxHistR;
        private System.Windows.Forms.PictureBox pictureBoxHistG;
        private System.Windows.Forms.PictureBox pictureBoxHistB;
    }
}
