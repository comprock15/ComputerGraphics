
namespace CornishRoom_mpustovoi {
    partial class Form1 {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">Истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cornishRoomPictureBox = new System.Windows.Forms.PictureBox();
            this.redrawButton = new System.Windows.Forms.Button();
            this.firstCubeSpecularityCheckBox = new System.Windows.Forms.CheckBox();
            this.firstSphereSpecularityCheckBox = new System.Windows.Forms.CheckBox();
            this.secondCubeTransparencyCheckBox = new System.Windows.Forms.CheckBox();
            this.secondSphereTransparencyCheckBox = new System.Windows.Forms.CheckBox();
            this.secondLightSourceXPositionTextBox = new System.Windows.Forms.TextBox();
            this.secondLightSourceYPositionTextBox = new System.Windows.Forms.TextBox();
            this.secondLightSourceZPositionTextBox = new System.Windows.Forms.TextBox();
            this.secondLightSourceXPositionLabel = new System.Windows.Forms.Label();
            this.secondLightSourceYPosition = new System.Windows.Forms.Label();
            this.secondLightSourceZPosition = new System.Windows.Forms.Label();
            this.secondLightSourcePositionLabel = new System.Windows.Forms.Label();
            this.firstLightSourceCheckBox = new System.Windows.Forms.CheckBox();
            this.secondLightSourceCheckBox = new System.Windows.Forms.CheckBox();
            this.frontWallSpecularityCheckBox = new System.Windows.Forms.CheckBox();
            this.leftWallSpecularityCheckBox = new System.Windows.Forms.CheckBox();
            this.rightWallSpecularityCheckBox = new System.Windows.Forms.CheckBox();
            this.backWallSpecularityCheckBox = new System.Windows.Forms.CheckBox();
            this.specularityLabel = new System.Windows.Forms.Label();
            this.transparencyLabel = new System.Windows.Forms.Label();
            this.lightSourcesLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cornishRoomPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cornishRoomPictureBox
            // 
            this.cornishRoomPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cornishRoomPictureBox.Location = new System.Drawing.Point(123, 11);
            this.cornishRoomPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.cornishRoomPictureBox.Name = "cornishRoomPictureBox";
            this.cornishRoomPictureBox.Size = new System.Drawing.Size(670, 670);
            this.cornishRoomPictureBox.TabIndex = 0;
            this.cornishRoomPictureBox.TabStop = false;
            // 
            // redrawButton
            // 
            this.redrawButton.Location = new System.Drawing.Point(11, 345);
            this.redrawButton.Margin = new System.Windows.Forms.Padding(2);
            this.redrawButton.Name = "redrawButton";
            this.redrawButton.Size = new System.Drawing.Size(108, 40);
            this.redrawButton.TabIndex = 1;
            this.redrawButton.Text = "Перерисовать\r\n";
            this.redrawButton.UseVisualStyleBackColor = true;
            this.redrawButton.Click += new System.EventHandler(this.redrawButton_Click);
            // 
            // firstCubeSpecularityCheckBox
            // 
            this.firstCubeSpecularityCheckBox.AutoSize = true;
            this.firstCubeSpecularityCheckBox.Location = new System.Drawing.Point(11, 24);
            this.firstCubeSpecularityCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.firstCubeSpecularityCheckBox.Name = "firstCubeSpecularityCheckBox";
            this.firstCubeSpecularityCheckBox.Size = new System.Drawing.Size(86, 17);
            this.firstCubeSpecularityCheckBox.TabIndex = 2;
            this.firstCubeSpecularityCheckBox.Text = "Первый куб";
            this.firstCubeSpecularityCheckBox.UseVisualStyleBackColor = true;
            // 
            // firstSphereSpecularityCheckBox
            // 
            this.firstSphereSpecularityCheckBox.AutoSize = true;
            this.firstSphereSpecularityCheckBox.Location = new System.Drawing.Point(11, 45);
            this.firstSphereSpecularityCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.firstSphereSpecularityCheckBox.Name = "firstSphereSpecularityCheckBox";
            this.firstSphereSpecularityCheckBox.Size = new System.Drawing.Size(89, 17);
            this.firstSphereSpecularityCheckBox.TabIndex = 3;
            this.firstSphereSpecularityCheckBox.Text = "Первый шар";
            this.firstSphereSpecularityCheckBox.UseVisualStyleBackColor = true;
            // 
            // secondCubeTransparencyCheckBox
            // 
            this.secondCubeTransparencyCheckBox.AutoSize = true;
            this.secondCubeTransparencyCheckBox.Location = new System.Drawing.Point(11, 163);
            this.secondCubeTransparencyCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.secondCubeTransparencyCheckBox.Name = "secondCubeTransparencyCheckBox";
            this.secondCubeTransparencyCheckBox.Size = new System.Drawing.Size(82, 17);
            this.secondCubeTransparencyCheckBox.TabIndex = 4;
            this.secondCubeTransparencyCheckBox.Text = "Второй куб";
            this.secondCubeTransparencyCheckBox.UseVisualStyleBackColor = true;
            // 
            // secondSphereTransparencyCheckBox
            // 
            this.secondSphereTransparencyCheckBox.AutoSize = true;
            this.secondSphereTransparencyCheckBox.Location = new System.Drawing.Point(11, 184);
            this.secondSphereTransparencyCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.secondSphereTransparencyCheckBox.Name = "secondSphereTransparencyCheckBox";
            this.secondSphereTransparencyCheckBox.Size = new System.Drawing.Size(85, 17);
            this.secondSphereTransparencyCheckBox.TabIndex = 5;
            this.secondSphereTransparencyCheckBox.Text = "Второй шар";
            this.secondSphereTransparencyCheckBox.UseVisualStyleBackColor = true;
            // 
            // secondLightSourceXPositionTextBox
            // 
            this.secondLightSourceXPositionTextBox.Location = new System.Drawing.Point(32, 273);
            this.secondLightSourceXPositionTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.secondLightSourceXPositionTextBox.Name = "secondLightSourceXPositionTextBox";
            this.secondLightSourceXPositionTextBox.Size = new System.Drawing.Size(52, 20);
            this.secondLightSourceXPositionTextBox.TabIndex = 6;
            this.secondLightSourceXPositionTextBox.Text = "4";
            // 
            // secondLightSourceYPositionTextBox
            // 
            this.secondLightSourceYPositionTextBox.Location = new System.Drawing.Point(32, 297);
            this.secondLightSourceYPositionTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.secondLightSourceYPositionTextBox.Name = "secondLightSourceYPositionTextBox";
            this.secondLightSourceYPositionTextBox.Size = new System.Drawing.Size(52, 20);
            this.secondLightSourceYPositionTextBox.TabIndex = 7;
            this.secondLightSourceYPositionTextBox.Text = "-4";
            // 
            // secondLightSourceZPositionTextBox
            // 
            this.secondLightSourceZPositionTextBox.Location = new System.Drawing.Point(32, 321);
            this.secondLightSourceZPositionTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.secondLightSourceZPositionTextBox.Name = "secondLightSourceZPositionTextBox";
            this.secondLightSourceZPositionTextBox.Size = new System.Drawing.Size(52, 20);
            this.secondLightSourceZPositionTextBox.TabIndex = 8;
            this.secondLightSourceZPositionTextBox.Text = "-5";
            // 
            // secondLightSourceXPositionLabel
            // 
            this.secondLightSourceXPositionLabel.AutoSize = true;
            this.secondLightSourceXPositionLabel.Location = new System.Drawing.Point(11, 276);
            this.secondLightSourceXPositionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.secondLightSourceXPositionLabel.Name = "secondLightSourceXPositionLabel";
            this.secondLightSourceXPositionLabel.Size = new System.Drawing.Size(17, 13);
            this.secondLightSourceXPositionLabel.TabIndex = 9;
            this.secondLightSourceXPositionLabel.Text = "X:";
            // 
            // secondLightSourceYPosition
            // 
            this.secondLightSourceYPosition.AutoSize = true;
            this.secondLightSourceYPosition.Location = new System.Drawing.Point(11, 300);
            this.secondLightSourceYPosition.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.secondLightSourceYPosition.Name = "secondLightSourceYPosition";
            this.secondLightSourceYPosition.Size = new System.Drawing.Size(17, 13);
            this.secondLightSourceYPosition.TabIndex = 10;
            this.secondLightSourceYPosition.Text = "Y:";
            // 
            // secondLightSourceZPosition
            // 
            this.secondLightSourceZPosition.AutoSize = true;
            this.secondLightSourceZPosition.Location = new System.Drawing.Point(11, 324);
            this.secondLightSourceZPosition.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.secondLightSourceZPosition.Name = "secondLightSourceZPosition";
            this.secondLightSourceZPosition.Size = new System.Drawing.Size(17, 13);
            this.secondLightSourceZPosition.TabIndex = 11;
            this.secondLightSourceZPosition.Text = "Z:";
            // 
            // secondLightSourcePositionLabel
            // 
            this.secondLightSourcePositionLabel.AutoSize = true;
            this.secondLightSourcePositionLabel.Location = new System.Drawing.Point(11, 258);
            this.secondLightSourcePositionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.secondLightSourcePositionLabel.Name = "secondLightSourcePositionLabel";
            this.secondLightSourcePositionLabel.Size = new System.Drawing.Size(108, 13);
            this.secondLightSourcePositionLabel.TabIndex = 12;
            this.secondLightSourcePositionLabel.Text = "Положение второго";
            // 
            // firstLightSourceCheckBox
            // 
            this.firstLightSourceCheckBox.AutoSize = true;
            this.firstLightSourceCheckBox.Checked = true;
            this.firstLightSourceCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.firstLightSourceCheckBox.Location = new System.Drawing.Point(11, 218);
            this.firstLightSourceCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.firstLightSourceCheckBox.Name = "firstLightSourceCheckBox";
            this.firstLightSourceCheckBox.Size = new System.Drawing.Size(64, 17);
            this.firstLightSourceCheckBox.TabIndex = 13;
            this.firstLightSourceCheckBox.Text = "Первое";
            this.firstLightSourceCheckBox.UseVisualStyleBackColor = true;
            // 
            // secondLightSourceCheckBox
            // 
            this.secondLightSourceCheckBox.AutoSize = true;
            this.secondLightSourceCheckBox.Location = new System.Drawing.Point(11, 239);
            this.secondLightSourceCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.secondLightSourceCheckBox.Name = "secondLightSourceCheckBox";
            this.secondLightSourceCheckBox.Size = new System.Drawing.Size(62, 17);
            this.secondLightSourceCheckBox.TabIndex = 14;
            this.secondLightSourceCheckBox.Text = "Второе";
            this.secondLightSourceCheckBox.UseVisualStyleBackColor = true;
            // 
            // frontWallSpecularityCheckBox
            // 
            this.frontWallSpecularityCheckBox.AutoSize = true;
            this.frontWallSpecularityCheckBox.Location = new System.Drawing.Point(11, 66);
            this.frontWallSpecularityCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.frontWallSpecularityCheckBox.Name = "frontWallSpecularityCheckBox";
            this.frontWallSpecularityCheckBox.Size = new System.Drawing.Size(108, 17);
            this.frontWallSpecularityCheckBox.TabIndex = 15;
            this.frontWallSpecularityCheckBox.Text = "Передняя стена";
            this.frontWallSpecularityCheckBox.UseVisualStyleBackColor = true;
            // 
            // leftWallSpecularityCheckBox
            // 
            this.leftWallSpecularityCheckBox.AutoSize = true;
            this.leftWallSpecularityCheckBox.Location = new System.Drawing.Point(11, 87);
            this.leftWallSpecularityCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.leftWallSpecularityCheckBox.Name = "leftWallSpecularityCheckBox";
            this.leftWallSpecularityCheckBox.Size = new System.Drawing.Size(90, 17);
            this.leftWallSpecularityCheckBox.TabIndex = 16;
            this.leftWallSpecularityCheckBox.Text = "Левая стена";
            this.leftWallSpecularityCheckBox.UseVisualStyleBackColor = true;
            // 
            // rightWallSpecularityCheckBox
            // 
            this.rightWallSpecularityCheckBox.AutoSize = true;
            this.rightWallSpecularityCheckBox.Location = new System.Drawing.Point(11, 108);
            this.rightWallSpecularityCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.rightWallSpecularityCheckBox.Name = "rightWallSpecularityCheckBox";
            this.rightWallSpecularityCheckBox.Size = new System.Drawing.Size(96, 17);
            this.rightWallSpecularityCheckBox.TabIndex = 17;
            this.rightWallSpecularityCheckBox.Text = "Правая стена";
            this.rightWallSpecularityCheckBox.UseVisualStyleBackColor = true;
            // 
            // backWallSpecularityCheckBox
            // 
            this.backWallSpecularityCheckBox.AutoSize = true;
            this.backWallSpecularityCheckBox.Location = new System.Drawing.Point(11, 129);
            this.backWallSpecularityCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.backWallSpecularityCheckBox.Name = "backWallSpecularityCheckBox";
            this.backWallSpecularityCheckBox.Size = new System.Drawing.Size(95, 17);
            this.backWallSpecularityCheckBox.TabIndex = 18;
            this.backWallSpecularityCheckBox.Text = "Задняя стена";
            this.backWallSpecularityCheckBox.UseVisualStyleBackColor = true;
            // 
            // specularityLabel
            // 
            this.specularityLabel.AutoSize = true;
            this.specularityLabel.Location = new System.Drawing.Point(11, 9);
            this.specularityLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.specularityLabel.Name = "specularityLabel";
            this.specularityLabel.Size = new System.Drawing.Size(79, 13);
            this.specularityLabel.TabIndex = 19;
            this.specularityLabel.Text = "Зеркальность";
            // 
            // transparencyLabel
            // 
            this.transparencyLabel.AutoSize = true;
            this.transparencyLabel.Location = new System.Drawing.Point(11, 148);
            this.transparencyLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.transparencyLabel.Name = "transparencyLabel";
            this.transparencyLabel.Size = new System.Drawing.Size(79, 13);
            this.transparencyLabel.TabIndex = 20;
            this.transparencyLabel.Text = "Прозрачность";
            // 
            // lightSourcesLabel
            // 
            this.lightSourcesLabel.AutoSize = true;
            this.lightSourcesLabel.Location = new System.Drawing.Point(11, 203);
            this.lightSourcesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lightSourcesLabel.Name = "lightSourcesLabel";
            this.lightSourcesLabel.Size = new System.Drawing.Size(66, 13);
            this.lightSourcesLabel.TabIndex = 21;
            this.lightSourcesLabel.Text = "Освещение";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(804, 692);
            this.Controls.Add(this.lightSourcesLabel);
            this.Controls.Add(this.transparencyLabel);
            this.Controls.Add(this.specularityLabel);
            this.Controls.Add(this.backWallSpecularityCheckBox);
            this.Controls.Add(this.rightWallSpecularityCheckBox);
            this.Controls.Add(this.leftWallSpecularityCheckBox);
            this.Controls.Add(this.frontWallSpecularityCheckBox);
            this.Controls.Add(this.secondLightSourceCheckBox);
            this.Controls.Add(this.firstLightSourceCheckBox);
            this.Controls.Add(this.secondLightSourcePositionLabel);
            this.Controls.Add(this.secondLightSourceZPosition);
            this.Controls.Add(this.secondLightSourceYPosition);
            this.Controls.Add(this.secondLightSourceXPositionLabel);
            this.Controls.Add(this.secondLightSourceZPositionTextBox);
            this.Controls.Add(this.secondLightSourceYPositionTextBox);
            this.Controls.Add(this.secondLightSourceXPositionTextBox);
            this.Controls.Add(this.secondSphereTransparencyCheckBox);
            this.Controls.Add(this.secondCubeTransparencyCheckBox);
            this.Controls.Add(this.firstSphereSpecularityCheckBox);
            this.Controls.Add(this.firstCubeSpecularityCheckBox);
            this.Controls.Add(this.redrawButton);
            this.Controls.Add(this.cornishRoomPictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Корнуэльская комната";
            ((System.ComponentModel.ISupportInitialize)(this.cornishRoomPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox cornishRoomPictureBox;
        private System.Windows.Forms.Button redrawButton;
        private System.Windows.Forms.CheckBox firstCubeSpecularityCheckBox;
        private System.Windows.Forms.CheckBox firstSphereSpecularityCheckBox;
        private System.Windows.Forms.CheckBox secondCubeTransparencyCheckBox;
        private System.Windows.Forms.CheckBox secondSphereTransparencyCheckBox;
        private System.Windows.Forms.TextBox secondLightSourceXPositionTextBox;
        private System.Windows.Forms.TextBox secondLightSourceYPositionTextBox;
        private System.Windows.Forms.TextBox secondLightSourceZPositionTextBox;
        private System.Windows.Forms.Label secondLightSourceXPositionLabel;
        private System.Windows.Forms.Label secondLightSourceYPosition;
        private System.Windows.Forms.Label secondLightSourceZPosition;
        private System.Windows.Forms.Label secondLightSourcePositionLabel;
        private System.Windows.Forms.CheckBox firstLightSourceCheckBox;
        private System.Windows.Forms.CheckBox secondLightSourceCheckBox;
        private System.Windows.Forms.CheckBox frontWallSpecularityCheckBox;
        private System.Windows.Forms.CheckBox leftWallSpecularityCheckBox;
        private System.Windows.Forms.CheckBox rightWallSpecularityCheckBox;
        private System.Windows.Forms.CheckBox backWallSpecularityCheckBox;
        private System.Windows.Forms.Label specularityLabel;
        private System.Windows.Forms.Label transparencyLabel;
        private System.Windows.Forms.Label lightSourcesLabel;
    }
}