using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GaussSolver
{
    public partial class MainForm : Form
    {
        private FileOutput fileOutput;
        private Printer printer;
        private double[,] savedMatrix;
        private double[] currentSolution;
        public MainForm()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            dgvMatrix.RowPostPaint += DgvMatrix_RowPostPaint;
            printer = new Printer();
            fileOutput = new FileOutput();

            // Устанавливаем начальные значения для числовых элементов управления
            nudVariableCount.Minimum = 2;
            nudVariableCount.Value = 2;
            nudEquationCount.Minimum = 2;
            nudEquationCount.Value = 2;
            nudDecimalPlaces.Minimum = 0;
            nudDecimalPlaces.Maximum = 15;
            nudDecimalPlaces.Value = 2;

            // Привязываем обработчики событий
            nudVariableCount.ValueChanged += NudVariableCount_ValueChanged;
            nudEquationCount.ValueChanged += NudEquationCount_ValueChanged;
            nudDecimalPlaces.ValueChanged += NudDecimalPlaces_ValueChanged;
            btnLoadFile.Click += btnLoadFile_Click;
            btnPrint.Click += btnPrint_Click;
            btnSaveResult.Click += btnSaveResult_Click;
            dgvMatrix.CellValidating += dgvMatrix_CellValidating;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvMatrix.RowHeadersWidth = 60;
            int initialVariableCount = (int)nudVariableCount.Value;
            int initialEquationCount = (int)nudEquationCount.Value;
            InitializeDataGridView(initialVariableCount, initialEquationCount);
            Numerics.NudVariableCount_ValueChanged(nudVariableCount, dgvMatrix);
            Numerics.NudEquationCount_ValueChanged(nudEquationCount, dgvMatrix, ref savedMatrix);
        }

        // Инициализация DataGridView на основе количества переменных и уравнений
        private void InitializeDataGridView(int variableCount, int equationCount)
        {
            dgvMatrix.Columns.Clear();
            dgvMatrix.Rows.Clear();

            // Добавляем столбцы для переменных
            for (int i = 0; i < variableCount; i++)
                dgvMatrix.Columns.Add($"x{i + 1}", $"x{i + 1}");
            dgvMatrix.Columns.Add("Свободный член", "Свободный член");

            // Добавляем строки для уравнений
            for (int i = 0; i < equationCount; i++)
            {
                var row = new DataGridViewRow();
                for (int j = 0; j < dgvMatrix.Columns.Count; j++)
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = 0 });
                dgvMatrix.Rows.Add(row);
            }
        }

        // Событие для отображения номеров строк в DataGridView
        private void DgvMatrix_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvMatrix.RowHeadersWidth, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvMatrix.Font, rectangle, dgvMatrix.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void NudVariableCount_ValueChanged(object sender, EventArgs e)
        {
            int variableCount = (int)nudVariableCount.Value;
            int currentVariableCount = dgvMatrix.Columns.Count - 1;  
            if (dgvMatrix.Rows.Count == 0) return;

            if (variableCount > currentVariableCount)
            {
                // Добавляем новые столбцы с значением 0
                for (int i = currentVariableCount; i < variableCount; i++)
                {
                    var newColumn = new DataGridViewTextBoxColumn
                    {
                        Name = $"x{i + 1}",
                        HeaderText = $"x{i + 1}"
                    };
                    dgvMatrix.Columns.Insert(dgvMatrix.Columns.Count - 1, newColumn); 
                    foreach (DataGridViewRow row in dgvMatrix.Rows)
                    {
                        row.Cells[newColumn.Index].Value = 0;  
                    }
                }
            }
            else if (variableCount < currentVariableCount)
            {
                for (int i = currentVariableCount - 1; i >= variableCount; i--)
                {
                    dgvMatrix.Columns.RemoveAt(dgvMatrix.Columns.Count - 2);
                }
            }
        }

        // Событие изменения количества уравнений, настройка строк
        private void NudEquationCount_ValueChanged(object sender, EventArgs e)
        {
            int equationCount = (int)nudEquationCount.Value;
            int currentEquationCount = dgvMatrix.RowCount;

            // Если DataGridView пуст, не выполняем никаких действий
            if (dgvMatrix.Rows.Count == 0) return;

            if (equationCount > currentEquationCount)
            {
                for (int i = currentEquationCount; i < equationCount; i++)
                {
                    var newRow = new DataGridViewRow();
                    for (int j = 0; j < dgvMatrix.Columns.Count; j++)
                        newRow.Cells.Add(new DataGridViewTextBoxCell { Value = 0 });
                    dgvMatrix.Rows.Add(newRow);
                }
            }
            else if (equationCount < currentEquationCount)
            {
                for (int i = currentEquationCount - 1; i >= equationCount; i--)
                    dgvMatrix.Rows.RemoveAt(i);
            }
        }

        // Событие изменения количества знаков после запятой
        private void NudDecimalPlaces_ValueChanged(object sender, EventArgs e) => UpdateResultDisplay();
        // Обновление отображения результатов
        private void UpdateResultDisplay()
        {
            if (currentSolution == null || currentSolution.Length == 0)
            {
                tbResult.Text = string.Empty;
                return;
            }
            int decimalPlaces = (int)nudDecimalPlaces.Value;
            string format = $"F{decimalPlaces}";
            StringBuilder resultBuilder = new StringBuilder();
            for (int i = 0; i < currentSolution.Length; i++)
                resultBuilder.AppendLine($"x{i + 1} = {currentSolution[i].ToString(format)}");
            tbResult.Text = resultBuilder.ToString();
        }

        // Обработка некорректных данных в DataGridView
        private void dgvMatrix_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.IsDisposed || this.Disposing)
            {
                e.Cancel = false;
                return;
            }
            var newValue = e.FormattedValue.ToString();
            if (!double.TryParse(newValue, out double result))
            {
                MessageBox.Show("Введено неверное число. Разрешены отрицательные числа, дробь пишется через «,».", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Получение матрицы из DataGridView
        private double[,] GetMatrixFromGrid()
        {
            int rows = dgvMatrix.RowCount;
            int cols = dgvMatrix.ColumnCount;
            double[,] matrix = new double[rows, cols];
            bool hasInvalidData = false;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var cellValue = dgvMatrix.Rows[i].Cells[j].Value;
                    if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()) || !double.TryParse(cellValue.ToString(), out double value))
                    {
                        matrix[i, j] = 0;
                        dgvMatrix.Rows[i].Cells[j].Value = 0;
                        hasInvalidData = true;
                    }
                    else
                    {
                        matrix[i, j] = value;
                    }
                }
            }
            if (hasInvalidData)
            {
                MessageBox.Show("Обнаружены пустые/некорректные значения. Они заменены на 0.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return matrix;
        }

        // Решение системы уравнений с использованием метода Гаусса или метода наименьших квадратов
        private void btnSolve_Click(object sender, EventArgs e)
        {
            double[,] matrix = GetMatrixFromGrid();
            object solution = null;
            solution = Gauss.SolveGaussElimination(matrix);
            if (solution is string resultMessage)  
            {
                MessageBox.Show(resultMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (solution is double[] resultArray)
            {
                currentSolution = resultArray;
                UpdateResultDisplay();
            }
            else
            {
                MessageBox.Show("Не удалось решить систему.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Загрузка данных из файла в DataGridView
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt",
                Title = "Выберите текстовый файл"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInput.LoadFileToDataGridView(openFileDialog.FileName, dgvMatrix, nudEquationCount, nudVariableCount);
            }
        }

        // Сохранение результата в файл
        private void btnSaveResult_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbResult.Text))
                {
                    fileOutput.SaveToFile(tbResult.Text);
                }
                else
                {
                    MessageBox.Show("Нет результата для сохранения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Печать результата
        private void btnPrint_Click(object sender, EventArgs e) => printer.Print(tbResult.Text);
    }
}
