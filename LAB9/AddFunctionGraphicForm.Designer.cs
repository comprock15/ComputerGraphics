namespace LAB9
{
    partial class AddFunctionGraphicForm
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
            this.plottingCheckBox = new System.Windows.Forms.CheckBox();
            this.showPlotButton = new System.Windows.Forms.Button();
            this.numericUpDownStep = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.numericUpDownY1 = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownY0 = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.numericUpDownX1 = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDownX0 = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxFunc = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStep)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX0)).BeginInit();
            this.SuspendLayout();
            // 
            // plottingCheckBox
            // 
            this.plottingCheckBox.AutoSize = true;
            this.plottingCheckBox.Location = new System.Drawing.Point(249, 118);
            this.plottingCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.plottingCheckBox.Name = "plottingCheckBox";
            this.plottingCheckBox.Size = new System.Drawing.Size(99, 17);
            this.plottingCheckBox.TabIndex = 9;
            this.plottingCheckBox.Text = "Вид под углом";
            this.plottingCheckBox.UseVisualStyleBackColor = true;
            // 
            // showPlotButton
            // 
            this.showPlotButton.BackColor = System.Drawing.Color.White;
            this.showPlotButton.Location = new System.Drawing.Point(14, 139);
            this.showPlotButton.Margin = new System.Windows.Forms.Padding(2);
            this.showPlotButton.Name = "showPlotButton";
            this.showPlotButton.Size = new System.Drawing.Size(334, 33);
            this.showPlotButton.TabIndex = 8;
            this.showPlotButton.Text = "Построить график";
            this.showPlotButton.UseVisualStyleBackColor = false;
            this.showPlotButton.Click += new System.EventHandler(this.showPlotButton_Click);
            // 
            // numericUpDownStep
            // 
            this.numericUpDownStep.Location = new System.Drawing.Point(147, 115);
            this.numericUpDownStep.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownStep.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStep.Name = "numericUpDownStep";
            this.numericUpDownStep.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownStep.TabIndex = 8;
            this.numericUpDownStep.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(15, 117);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(123, 13);
            this.label18.TabIndex = 3;
            this.label18.Text = "Количество разбиений";
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox7.Controls.Add(this.numericUpDownY1);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.numericUpDownY0);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.numericUpDownX1);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.numericUpDownX0);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Location = new System.Drawing.Point(11, 33);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(337, 73);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Диапазоны отсечения";
            // 
            // numericUpDownY1
            // 
            this.numericUpDownY1.DecimalPlaces = 2;
            this.numericUpDownY1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownY1.Location = new System.Drawing.Point(221, 44);
            this.numericUpDownY1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownY1.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownY1.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownY1.Name = "numericUpDownY1";
            this.numericUpDownY1.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownY1.TabIndex = 7;
            this.numericUpDownY1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownY1.ValueChanged += new System.EventHandler(this.numericUpDownY_ValueChanged);
            this.numericUpDownY1.Click += new System.EventHandler(this.numericUpDownY_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(199, 45);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(18, 13);
            this.label17.TabIndex = 6;
            this.label17.Text = "y1";
            // 
            // numericUpDownY0
            // 
            this.numericUpDownY0.DecimalPlaces = 2;
            this.numericUpDownY0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownY0.Location = new System.Drawing.Point(55, 44);
            this.numericUpDownY0.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownY0.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownY0.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownY0.Name = "numericUpDownY0";
            this.numericUpDownY0.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownY0.TabIndex = 5;
            this.numericUpDownY0.Value = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.numericUpDownY0.ValueChanged += new System.EventHandler(this.numericUpDownY_ValueChanged);
            this.numericUpDownY0.Click += new System.EventHandler(this.numericUpDownY_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(33, 45);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(18, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "y0";
            // 
            // numericUpDownX1
            // 
            this.numericUpDownX1.DecimalPlaces = 2;
            this.numericUpDownX1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownX1.Location = new System.Drawing.Point(221, 21);
            this.numericUpDownX1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownX1.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownX1.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownX1.Name = "numericUpDownX1";
            this.numericUpDownX1.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownX1.TabIndex = 3;
            this.numericUpDownX1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownX1.ValueChanged += new System.EventHandler(this.numericUpDownX_ValueChanged);
            this.numericUpDownX1.Click += new System.EventHandler(this.numericUpDownX_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(200, 22);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(18, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "x1";
            // 
            // numericUpDownX0
            // 
            this.numericUpDownX0.DecimalPlaces = 2;
            this.numericUpDownX0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownX0.Location = new System.Drawing.Point(55, 21);
            this.numericUpDownX0.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownX0.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownX0.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownX0.Name = "numericUpDownX0";
            this.numericUpDownX0.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownX0.TabIndex = 1;
            this.numericUpDownX0.Value = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.numericUpDownX0.ValueChanged += new System.EventHandler(this.numericUpDownX_ValueChanged);
            this.numericUpDownX0.Click += new System.EventHandler(this.numericUpDownX_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(33, 22);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(18, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "x0";
            // 
            // textBoxFunc
            // 
            this.textBoxFunc.Location = new System.Drawing.Point(57, 9);
            this.textBoxFunc.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxFunc.Name = "textBoxFunc";
            this.textBoxFunc.Size = new System.Drawing.Size(291, 20);
            this.textBoxFunc.TabIndex = 1;
            this.textBoxFunc.Text = "2 * cos(x^2 + y^2) / (x^2 + y^2+ 1)";
            this.textBoxFunc.TextChanged += new System.EventHandler(this.textBoxFunc_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 11);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "f(x, y) =";
            // 
            // AddFunctionGraphicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(359, 187);
            this.Controls.Add(this.showPlotButton);
            this.Controls.Add(this.plottingCheckBox);
            this.Controls.Add(this.textBoxFunc);
            this.Controls.Add(this.numericUpDownStep);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.groupBox7);
            this.Name = "AddFunctionGraphicForm";
            this.Text = "Построение графика двух переменных";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStep)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox plottingCheckBox;
        private System.Windows.Forms.Button showPlotButton;
        private System.Windows.Forms.NumericUpDown numericUpDownStep;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown numericUpDownY1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numericUpDownY0;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown numericUpDownX1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericUpDownX0;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxFunc;
        private System.Windows.Forms.Label label13;
    }
}