using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace GaussSolver
{
    public class Printer
    {
        private readonly PrintDocument _printDocument;  // Объект для работы с печатью
        private bool _isPrintDialogOpen;  // Флаг для предотвращения повторного открытия диалога печати
        private string _textToPrint;  // Текст, который будет отправлен на печать
        public Printer()
        {
            _printDocument = new PrintDocument();
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        // Обработчик печати страницы
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            var printFont = new Font("Times New Roman", 14);
            e.Graphics.DrawString(_textToPrint, printFont, Brushes.Black, 10, 10);
        }

        // Метод для инициирования печати
        public void Print(string text)
        {
            if (_isPrintDialogOpen) return;  // Проверяем, не открыт ли уже диалог печати
            _isPrintDialogOpen = true;
            _textToPrint = text;

            // Проверка, есть ли текст для печати
            if (string.IsNullOrEmpty(_textToPrint))
            {
                MessageBox.Show("Нет результата для печати.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _isPrintDialogOpen = false;  // Сбрасываем флаг после выполнения
                return;
            }
            try
            {
                Logger.LogMessage("Запуск диалога печати", Logger.LogLevel.INFO);

                var printDialog = new PrintDialog
                {
                    Document = _printDocument
                };

                var result = printDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Logger.LogMessage("Принтер выбран, начинается печать", Logger.LogLevel.INFO);
                    _printDocument.Print();  // Запуск печати
                }
                else
                {
                    Logger.LogMessage("Печать отменена пользователем", Logger.LogLevel.INFO);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                MessageBox.Show($"Ошибка при открытии диалога печати: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isPrintDialogOpen = false;  // Сбрасываем флаг после выполнения
            }
        }
    }
}
