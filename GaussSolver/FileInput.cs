using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GaussSolver
{
    public class FileInput
    {
        // Метод для загрузки данных из файла в DataGridView
        public static void LoadFileToDataGridView(string filePath, DataGridView dgvMatrix, NumericUpDown nudEquationCount, NumericUpDown nudVariableCount)
        {
            string[] lines = File.ReadAllLines(filePath);
            dgvMatrix.Rows.Clear();
            dgvMatrix.Columns.Clear();
            var nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
            if (nonEmptyLines.Length > 0)
            {
                var firstLine = nonEmptyLines[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int variableCount = Math.Max(firstLine.Length - 1, 2); // Количество переменных

                // Создание колонок для переменных
                for (int i = 0; i < variableCount; i++)
                    dgvMatrix.Columns.Add($"x{i + 1}", $"x{i + 1}");
                dgvMatrix.Columns.Add("Свободный член", "Свободный член");

                // Заполнение строк данными из файла
                foreach (string line in nonEmptyLines)
                {
                    string[] row = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    object[] parsedRow = new object[variableCount + 1];
                    for (int i = 0; i < variableCount; i++)
                        parsedRow[i] = (i < row.Length && double.TryParse(row[i], out double variableValue)) ? variableValue : 0;
                    parsedRow[variableCount] = (row.Length > variableCount && double.TryParse(row[variableCount], out double freeTerm)) ? freeTerm : 0;
                    dgvMatrix.Rows.Add(parsedRow);
                }
                nudEquationCount.Value = Math.Max(2, dgvMatrix.Rows.Count);
                nudVariableCount.Value = Math.Max(2, variableCount);

                // Добавление строк, если их меньше 2
                while (dgvMatrix.Rows.Count < 2)
                {
                    var newRow = new DataGridViewRow();
                    for (int i = 0; i < dgvMatrix.Columns.Count; i++)
                        newRow.Cells.Add(new DataGridViewTextBoxCell { Value = 0 });
                    dgvMatrix.Rows.Add(newRow);
                }
            }
            else
            {
                // Если файл пустой, создаем 2 строки с начальными значениями
                nudEquationCount.Value = 2;
                nudVariableCount.Value = 2;
                dgvMatrix.Columns.Add("x1", "x1");
                dgvMatrix.Columns.Add("x2", "x2");
                dgvMatrix.Columns.Add("Свободный член", "Свободный член");
                for (int i = 0; i < 2; i++)
                {
                    var newRow = new DataGridViewRow();
                    for (int j = 0; j < 3; j++) // 2 переменные + 1 свободный член
                        newRow.Cells.Add(new DataGridViewTextBoxCell { Value = 0 });
                    dgvMatrix.Rows.Add(newRow);
                }
                MessageBox.Show("Файл пуст. Выберите другой файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
