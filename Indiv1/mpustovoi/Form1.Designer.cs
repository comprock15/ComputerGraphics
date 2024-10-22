namespace mpustovoi {
    /// <summary>
    /// Форма.
    /// </summary>
    partial class Form1 {
        /// <summary>
        /// Компоненты.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Кнопка очистки формы.
        /// </summary>
        private Button clearButton;
        /// <summary>
        /// Кнопка удаления последней точки.
        /// </summary>
        private Button undoButton;

        /// <summary>
        /// Освобождает ресурсы формы.
        /// </summary>
        /// <param name="disposing">Флаг освобождения.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Инициализация компонентов формы.
        /// </summary>
        private void InitializeComponent() {
            clearButton = new Button();
            undoButton = new Button();
            SuspendLayout();
            // 
            // clearButton
            // 
            clearButton.Location = new Point(12, 12);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(75, 23);
            clearButton.TabIndex = 0;
            clearButton.Text = "Очистить";
            clearButton.Click += ClearButtonClick;
            // 
            // undoButton
            // 
            undoButton.Location = new Point(93, 12);
            undoButton.Name = "undoButton";
            undoButton.Size = new Size(75, 23);
            undoButton.TabIndex = 1;
            undoButton.Text = "Отмена";
            undoButton.Click += UndoButtonClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            Controls.Add(clearButton);
            Controls.Add(undoButton);
            Name = "Form1";
            Text = "Построение выпуклой оболочки";
            Paint += OnPaint;
            MouseClick += OnMouseClick;
            ResumeLayout(false);
        }
    }
}