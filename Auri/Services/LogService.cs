using System;
using System.IO;

namespace Auri.Services
{
    public class LogService
    {
        private static readonly string _logFilePath;
        private static readonly string _errorLogFilePath;
        private static readonly object _lock = new object();

        // Статический конструктор - выполняется при первом обращении к классу
        static LogService()
        {
            try
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                _logFilePath = Path.Combine(appDirectory, "debug.log");
                _errorLogFilePath = Path.Combine(appDirectory, "error.log");

                // Основной лог всегда перезаписываем при запуске
                File.WriteAllText(_logFilePath, $"=== Лог начат {DateTime.Now} ===\n");

                // Для error.log НЕ перезаписываем, только добавляем разделитель при запуске
                File.AppendAllText(_errorLogFilePath,
                    $"\n=== Сессия начата {DateTime.Now} ===\n");
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
        /// Запись ошибки в отдельный error.log (без перезаписи)
        /// </summary>
        public static void LogError(string message)
        {
            // Записываем в основной лог
            Log($"[ERROR] {message}");

            // Дублируем в error.log
            WriteToErrorLog(message);
        }

        /// <summary>
        /// Запись ошибки с исключением
        /// </summary>
        public static void LogError(string message, Exception ex)
        {
            var errorMessage = $"[ERROR] {message}";
            var exceptionMessage = $"[ERROR] Исключение: {ex.Message}";
            var stackTraceMessage = $"[ERROR] Stack Trace: {ex.StackTrace}";

            // Записываем в основной лог
            Log(errorMessage);
            Log(exceptionMessage);
            Log(stackTraceMessage);

            if (ex.InnerException != null)
            {
                var innerExMessage = $"[ERROR] Внутреннее исключение: {ex.InnerException.Message}";
                Log(innerExMessage);

                // Записываем все в error.log одной операцией
                WriteToErrorLog(
                    errorMessage + Environment.NewLine +
                    exceptionMessage + Environment.NewLine +
                    stackTraceMessage + Environment.NewLine +
                    innerExMessage
                );
            }
            else
            {
                // Записываем все в error.log одной операцией
                WriteToErrorLog(
                    errorMessage + Environment.NewLine +
                    exceptionMessage + Environment.NewLine +
                    stackTraceMessage
                );
            }
        }

        /// <summary>
        /// Запись ошибки с указанием типа ошибки
        /// </summary>
        public static void LogError(string message, string errorType)
        {
            var formattedMessage = $"[ERROR][{errorType}] {message}";
            Log(formattedMessage);
            WriteToErrorLog(formattedMessage);
        }

        /// <summary>
        /// Внутренний метод для записи в error.log (без перезаписи)
        /// </summary>
        private static void WriteToErrorLog(string message)
        {
            lock (_lock)
            {
                try
                {
                    string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
                    File.AppendAllText(_errorLogFilePath, logEntry + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка записи в error.log: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Запись ошибки с указанием метода
        /// </summary>
        public static void LogErrorWithMethod(string methodName, string message, Exception ex = null)
        {
            if (ex != null)
            {
                LogError($"В методе {methodName}: {message}", ex);
            }
            else
            {
                LogError($"В методе {methodName}: {message}");
            }
        }

        /// <summary>
        /// Очистка основного лог-файла
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
        /// Очистка файла ошибок (если нужно)
        /// </summary>
        public static void ClearErrorLog()
        {
            lock (_lock)
            {
                try
                {
                    File.WriteAllText(_errorLogFilePath, $"=== Лог ошибок очищен {DateTime.Now} ===\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка очистки error.log: {ex.Message}");
                }
            }
        }

        // Остальные методы остаются без изменений
        public static void LogInfo(string message) => Log($"[INFO] {message}");
        public static void LogWarning(string message) => Log($"[WARNING] {message}");

#if DEBUG
        public static void LogDebug(string message) => Log($"[DEBUG] {message}");
#else
        public static void LogDebug(string message) { }
#endif

        public static void LogMethodCall(string methodName, params object[] parameters)
        {
            string paramsStr = parameters != null && parameters.Length > 0
                ? string.Join(", ", parameters)
                : "нет параметров";

            Log($"[CALL] {methodName}({paramsStr})");
        }

        public static void LogVariable(string name, object value)
        {
            Log($"[VAR] {name} = {value ?? "null"}");
        }

        public static IDisposable LogOperationTime(string operationName)
        {
            return new OperationTimer(operationName);
        }

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