namespace GaussSolver
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
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnSaveResult = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.nudDecimalPlaces = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudVariableCount = new System.Windows.Forms.NumericUpDown();
            this.nudEquationCount = new System.Windows.Forms.NumericUpDown();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.btnSolve = new System.Windows.Forms.Button();
            this.dgvMatrix = new System.Windows.Forms.DataGridView();
            this.ColumnX1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnX2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFree = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nudDecimalPlaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVariableCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEquationCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatrix)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(2, 16);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(121, 64);
            this.btnLoadFile.TabIndex = 26;
            this.btnLoadFile.Text = "Загрузить файл";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            // 
            // btnSaveResult
            // 
            this.btnSaveResult.Location = new System.Drawing.Point(742, 348);
            this.btnSaveResult.Name = "btnSaveResult";
            this.btnSaveResult.Size = new System.Drawing.Size(128, 55);
            this.btnSaveResult.TabIndex = 28;
            this.btnSaveResult.Text = "Сохранить результат";
            this.btnSaveResult.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(742, 420);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(128, 52);
            this.btnPrint.TabIndex = 27;
            this.btnPrint.Text = "Распечатать результат";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(474, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(249, 42);
            this.label4.TabIndex = 25;
            this.label4.Text = "Выберите количество знаков после запятой:";
            // 
            // nudDecimalPlaces
            // 
            this.nudDecimalPlaces.Location = new System.Drawing.Point(561, 58);
            this.nudDecimalPlaces.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudDecimalPlaces.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudDecimalPlaces.Name = "nudDecimalPlaces";
            this.nudDecimalPlaces.Size = new System.Drawing.Size(62, 22);
            this.nudDecimalPlaces.TabIndex = 24;
            this.nudDecimalPlaces.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(769, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Результат:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Выберите количество переменных:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Выберите количество уравнений:";
            // 
            // nudVariableCount
            // 
            this.nudVariableCount.Location = new System.Drawing.Point(390, 63);
            this.nudVariableCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudVariableCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudVariableCount.Name = "nudVariableCount";
            this.nudVariableCount.Size = new System.Drawing.Size(62, 22);
            this.nudVariableCount.TabIndex = 20;
            this.nudVariableCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // nudEquationCount
            // 
            this.nudEquationCount.Location = new System.Drawing.Point(380, 16);
            this.nudEquationCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudEquationCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudEquationCount.Name = "nudEquationCount";
            this.nudEquationCount.Size = new System.Drawing.Size(62, 22);
            this.nudEquationCount.TabIndex = 19;
            this.nudEquationCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(728, 115);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ReadOnly = true;
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(158, 213);
            this.tbResult.TabIndex = 18;
            // 
            // btnSolve
            // 
            this.btnSolve.Location = new System.Drawing.Point(729, 6);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(141, 64);
            this.btnSolve.TabIndex = 17;
            this.btnSolve.Text = "Решить";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // dgvMatrix
            // 
            this.dgvMatrix.AllowUserToAddRows = false;
            this.dgvMatrix.AllowUserToDeleteRows = false;
            this.dgvMatrix.AllowUserToResizeColumns = false;
            this.dgvMatrix.AllowUserToResizeRows = false;
            this.dgvMatrix.ColumnHeadersHeight = 29;
            this.dgvMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMatrix.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnX1,
            this.ColumnX2,
            this.ColumnFree});
            this.dgvMatrix.Location = new System.Drawing.Point(2, 115);
            this.dgvMatrix.Name = "dgvMatrix";
            this.dgvMatrix.RowHeadersWidth = 25;
            this.dgvMatrix.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMatrix.RowTemplate.Height = 24;
            this.dgvMatrix.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMatrix.Size = new System.Drawing.Size(687, 381);
            this.dgvMatrix.TabIndex = 16;
            // 
            // ColumnX1
            // 
            this.ColumnX1.HeaderText = "x1";
            this.ColumnX1.MinimumWidth = 45;
            this.ColumnX1.Name = "ColumnX1";
            this.ColumnX1.Width = 190;
            // 
            // ColumnX2
            // 
            this.ColumnX2.HeaderText = "x2";
            this.ColumnX2.MinimumWidth = 45;
            this.ColumnX2.Name = "ColumnX2";
            this.ColumnX2.Width = 190;
            // 
            // ColumnFree
            // 
            this.ColumnFree.HeaderText = "Свободный член";
            this.ColumnFree.MinimumWidth = 45;
            this.ColumnFree.Name = "ColumnFree";
            this.ColumnFree.Width = 190;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 495);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.btnSaveResult);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudDecimalPlaces);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudVariableCount);
            this.Controls.Add(this.nudEquationCount);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.dgvMatrix);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudDecimalPlaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVariableCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEquationCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatrix)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnSaveResult;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudDecimalPlaces;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudVariableCount;
        private System.Windows.Forms.NumericUpDown nudEquationCount;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.DataGridView dgvMatrix;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnX2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFree;
    }
}

