using System;
using System.IO;

public static class Logger
{
    private static readonly string LogDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
    private static readonly string LogFilePath = Path.Combine(LogDirectory, "GaussSolverLogs.log");
    public enum LogLevel
    {
        DEBUG,
        INFO,
        WARNING,
        ERROR,
        CRITICAL
    }

    // Настройка логирования: создание директорий и файлов
    public static void SetupLogger()
    {
        try
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);  // Создаём директорию логов, если она не существует
                Console.WriteLine($"Создана директория логов: {LogDirectory}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании директории логов: {ex.Message}");
            throw;
        }
        try
        {
            if (!File.Exists(LogFilePath))
            {
                using (var fs = File.Create(LogFilePath)) { }  // Создаём файл логов, если его нет
                Console.WriteLine($"Создан файл логов: {LogFilePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании файла логов: {ex.Message}");
            throw;
        }

        LogMessage("Logger initialized.", LogLevel.INFO);  // Логируем успешную инициализацию
    }

    // Запись информационного сообщения в лог
    public static void LogMessage(string message, LogLevel level = LogLevel.INFO)
    {
        string logMessage = FormatLogMessage(message, level);
        Console.WriteLine(logMessage);  // Выводим в консоль
        AppendLogToFile(logMessage);    // Записываем в файл
    }

    // Запись исключения в лог
    public static void LogException(Exception ex)
    {
        string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR - Exception occurred: {ex.Message}{Environment.NewLine}" +
                            $"Stack Trace: {ex.StackTrace ?? "No stack trace available"}{Environment.NewLine}";
        Console.WriteLine(logMessage);  // Выводим в консоль
        AppendLogToFile(logMessage);    // Записываем в файл
    }

    // Форматирование сообщения перед записью в лог
    private static string FormatLogMessage(string message, LogLevel level)
    {
        return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {level} - {message}";
    }

    // Добавление сообщения в файл логов
    private static void AppendLogToFile(string logMessage)
    {
        try
        {
            File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);  // Добавляем сообщение в файл
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в лог-файл: {ex.Message}");
        }
    }
}
