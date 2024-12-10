using System;
using System.IO;
using System.Windows.Forms;

namespace GaussSolver
{
    public class FileOutput
    {
        private bool isSaveDialogOpen = false;  // Флаг для предотвращения повторного открытия диалога

        // Метод для сохранения данных в файл
        public void SaveToFile(string content)
        {
            if (isSaveDialogOpen) return;  // Предотвращаем повторное открытие диалога
            isSaveDialogOpen = true;

            // Инициализация диалога сохранения файла
            using (var saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Сохранить результаты"
            })
            {
                try
                {
                    Logger.LogMessage("Запуск диалога сохранения файла", Logger.LogLevel.INFO);
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Сохраняем данные в выбранный файл
                        File.WriteAllText(saveFileDialog.FileName, content);
                        MessageBox.Show("Результаты успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Logger.LogMessage($"Файл успешно сохранён: {saveFileDialog.FileName}", Logger.LogLevel.INFO);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);  // Логируем ошибку
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    isSaveDialogOpen = false;  // Сбрасываем флаг после завершения
                }
            }
        }
    }
}
