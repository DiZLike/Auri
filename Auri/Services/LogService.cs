using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auri.Services
{
    public class LogService
    {
        private static readonly string _logFilePath;
        private static readonly object _lock = new object();

        // Статический конструктор - выполняется при первом обращении к классу
        static LogService()
        {
            try
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                _logFilePath = Path.Combine(appDirectory, "debug.log");

                // Всегда перезаписываем файл при запуске
                File.WriteAllText(_logFilePath, $"=== Лог начат {DateTime.Now} ===\n");
            }
            catch (Exception ex)
            {
                // Если не можем создать файл лога, выводим в консоль
                Console.WriteLine($"Критическая ошибка инициализации логгера: {ex.Message}");
            }
        }

        /// <summary>
        /// Запись обычного сообщения в лог
        /// </summary>
        public static void Log(string message)
        {
            lock (_lock)
            {
                try
                {
                    string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
                    File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);

                    // Для отладки дублируем в консоль
#if DEBUG
                    Console.WriteLine(logEntry);
#endif
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Запись информационного сообщения
        /// </summary>
        public static void LogInfo(string message)
        {
            Log($"[INFO] {message}");
        }

        /// <summary>
        /// Запись предупреждения
        /// </summary>
        public static void LogWarning(string message)
        {
            Log($"[WARNING] {message}");
        }

        /// <summary>
        /// Запись ошибки
        /// </summary>
        public static void LogError(string message)
        {
            Log($"[ERROR] {message}");
        }

        /// <summary>
        /// Запись ошибки с исключением
        /// </summary>
        public static void LogError(string message, Exception ex)
        {
            Log($"[ERROR] {message}");
            Log($"[ERROR] Исключение: {ex.Message}");
            Log($"[ERROR] Stack Trace: {ex.StackTrace}");

            if (ex.InnerException != null)
            {
                Log($"[ERROR] Внутреннее исключение: {ex.InnerException.Message}");
            }
        }

        /// <summary>
        /// Запись отладочного сообщения (только в DEBUG режиме)
        /// </summary>
        public static void LogDebug(string message)
        {
#if DEBUG
            Log($"[DEBUG] {message}");
#endif
        }

        /// <summary>
        /// Запись сообщения с указанием метода/класса
        /// </summary>
        public static void LogMethodCall(string methodName, params object[] parameters)
        {
            string paramsStr = parameters != null && parameters.Length > 0
                ? string.Join(", ", parameters)
                : "нет параметров";

            Log($"[CALL] {methodName}({paramsStr})");
        }

        /// <summary>
        /// Запись значения переменной
        /// </summary>
        public static void LogVariable(string name, object value)
        {
            Log($"[VAR] {name} = {value ?? "null"}");
        }

        /// <summary>
        /// Запись времени выполнения операции
        /// </summary>
        public static IDisposable LogOperationTime(string operationName)
        {
            return new OperationTimer(operationName);
        }

        /// <summary>
        /// Очистка лог-файла
        /// </summary>
        public static void ClearLog()
        {
            lock (_lock)
            {
                try
                {
                    File.WriteAllText(_logFilePath, $"=== Лог очищен и начат заново {DateTime.Now} ===\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка очистки лога: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Вспомогательный класс для замера времени выполнения
        /// </summary>
        private class OperationTimer : IDisposable
        {
            private readonly string _operationName;
            private readonly DateTime _startTime;

            public OperationTimer(string operationName)
            {
                _operationName = operationName;
                _startTime = DateTime.Now;
                Log($"[TIMER] Начало операции: {operationName}");
            }

            public void Dispose()
            {
                var elapsed = DateTime.Now - _startTime;
                Log($"[TIMER] Операция '{_operationName}' завершена за {elapsed.TotalMilliseconds:F2} мс");
            }
        }
    }
}
