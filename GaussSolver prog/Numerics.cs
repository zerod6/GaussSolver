using System;
using System.Text;
using System.Windows.Forms;

namespace GaussSolver
{
    public static class Numerics
    {
        // Обрабатываем изменение количества переменных
        public static void NudVariableCount_ValueChanged(NumericUpDown nudVariableCount, DataGridView dgvMatrix)
        {
            int variableCount = (int)nudVariableCount.Value;
            Logger.LogMessage($"Изменение количества переменных: {variableCount}", Logger.LogLevel.DEBUG);

            // Минимум 2 переменные
            if (variableCount < 2)
            {
                variableCount = 2;
                nudVariableCount.Value = 2;
            }
            var currentData = ExtractDataFromGrid(dgvMatrix);
            dgvMatrix.ColumnCount = variableCount + 1;  // Добавляем столбец для свободного члена
            RestoreDataToGrid(dgvMatrix, currentData);  // Восстанавливаем данные в таблицу
            UpdateColumnHeaders(dgvMatrix, variableCount);  // Обновляем заголовки
            Logger.LogMessage($"Количество переменных установлено: {variableCount}. Заголовки обновлены.", Logger.LogLevel.INFO);
        }

        // Обрабатываем изменение количества уравнений
        public static void NudEquationCount_ValueChanged(NumericUpDown nudEquationCount, DataGridView dgvMatrix, ref double[,] savedMatrix)
        {
            int equationCount = (int)nudEquationCount.Value;
            int oldRowCount = dgvMatrix.RowCount;
            Logger.LogMessage($"Изменение количества уравнений: {equationCount}. Старое количество строк: {oldRowCount}.", Logger.LogLevel.DEBUG);
            savedMatrix = ExtractDataFromGrid(dgvMatrix, oldRowCount);
            dgvMatrix.RowCount = equationCount;  // Устанавливаем новое количество строк
            RestoreDataToGrid(dgvMatrix, savedMatrix, equationCount);  // Восстанавливаем данные
            Logger.LogMessage("Количество строк в таблице обновлено. Данные восстановлены.", Logger.LogLevel.INFO);
        }

        // Обрабатываем изменение количества знаков после запятой
        public static void NudDecimalPlaces_ValueChanged(TextBox tbResult, NumericUpDown nudDecimalPlaces, DataGridView dgvMatrix)
        {
            int decimalPlaces = (int)nudDecimalPlaces.Value;
            Logger.LogMessage($"Изменение количества знаков после запятой: {decimalPlaces}", Logger.LogLevel.DEBUG);
            if (!string.IsNullOrEmpty(tbResult.Text))  // Обновляем решение, если оно уже есть
            {
                UpdateSolutionDisplay(tbResult, dgvMatrix, decimalPlaces);
            }
        }

        // Обновление отображения решения системы
        private static void UpdateSolutionDisplay(TextBox tbResult, DataGridView dgvMatrix, int decimalPlaces)
        {
            try
            {
                Logger.LogMessage("Обновление отображения решения системы.", Logger.LogLevel.DEBUG);
                double[,] matrix = GetMatrixFromGrid(dgvMatrix);
                double[] solution = SolveGauss(matrix);
                if (decimalPlaces < 0)  // Проверка на допустимое количество знаков после запятой
                {
                    throw new ArgumentOutOfRangeException(nameof(decimalPlaces), "Количество знаков должно быть неотрицательным.");
                }
                string format = $"F{decimalPlaces}";
                StringBuilder resultBuilder = new StringBuilder();

                // Формируем строку с результатами
                for (int i = 0; i < solution.Length; i++)
                {
                    resultBuilder.AppendLine($"x{i + 1} = {solution[i].ToString(format)}");
                }
                tbResult.Text = resultBuilder.ToString();  // Отображаем результат
                Logger.LogMessage("Решение обновлено и отображено в текстовом поле.", Logger.LogLevel.INFO);
            }
            catch (Exception ex)
            {
                Logger.LogMessage($"Ошибка при выводе результатов: {ex.Message}", Logger.LogLevel.ERROR);
                MessageBox.Show($"Ошибка при выводе результатов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Получение матрицы из DataGrid
        private static double[,] GetMatrixFromGrid(DataGridView dgvMatrix)
        {
            int rows = dgvMatrix.RowCount;
            int cols = dgvMatrix.ColumnCount;
            double[,] matrix = new double[rows, cols];

            // Заполняем матрицу значениями из таблицы
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var cellValue = dgvMatrix.Rows[i].Cells[j].Value;
                    matrix[i, j] = (cellValue == null || !double.TryParse(cellValue.ToString(), out double value)) ? 0 : value;
                }
            }
            Logger.LogMessage("Матрица успешно извлечена из таблицы.", Logger.LogLevel.INFO);
            return matrix;
        }

        // Заглушка для метода решения системы Гауссом
        private static double[] SolveGauss(double[,] matrix)
        {
            Logger.LogMessage("Вызов метода решения системы уравнений методом Гаусса.", Logger.LogLevel.INFO);
            return new double[matrix.GetLength(0)];  // Пока просто возвращаем пустой массив
        }

        // Извлечение данных из DataGrid
        private static double[,] ExtractDataFromGrid(DataGridView dgvMatrix, int rowCount = -1)
        {
            int rows = rowCount == -1 ? dgvMatrix.RowCount : rowCount;
            int cols = dgvMatrix.ColumnCount;
            double[,] data = new double[rows, cols];

            // Извлекаем данные из таблицы
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var cellValue = dgvMatrix.Rows[i].Cells[j].Value;
                    if (cellValue != null && double.TryParse(cellValue.ToString(), out double value))
                    {
                        data[i, j] = value;
                    }
                }
            }
            return data;
        }

        // Восстановление данных в DataGrid
        private static void RestoreDataToGrid(DataGridView dgvMatrix, double[,] data, int rowCount = -1)
        {
            int rows = rowCount == -1 ? dgvMatrix.RowCount : rowCount;
            int cols = dgvMatrix.ColumnCount;

            // Восстанавливаем данные в таблицу
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i < data.GetLength(0) && j < data.GetLength(1))
                    {
                        dgvMatrix.Rows[i].Cells[j].Value = data[i, j];
                    }
                }
            }
        }

        // Обновление заголовков столбцов DataGrid
        private static void UpdateColumnHeaders(DataGridView dgvMatrix, int variableCount)
        {
            // Обновляем заголовки для переменных и свободного члена
            for (int i = 0; i < variableCount; i++)
            {
                dgvMatrix.Columns[i].HeaderText = $"x{i + 1}";
            }
            dgvMatrix.Columns[variableCount].HeaderText = "Свободный член";
        }
    }
}
