using Engine.Services;
using Un4seen.Bass;

namespace Engine.Managers
{
    public enum Error
    {
        /// <summary>
        /// Ошибка отсутствует (значение по умолчанию)
        /// </summary>
        NONE,

        /// <summary>
        /// Неизвестная ошибка, которая не подходит ни под одну категорию
        /// </summary>
        UNKNOWN,

        /// <summary>
        /// Файл не найден по указанному пути
        /// </summary>
        FILE_NOT_FOUND,

        /// <summary>
        /// Отсутствуют права доступа к файлу (заблокирован, защищён или нет прав)
        /// </summary>
        FILE_ACCESS_DENIED,

        /// <summary>
        /// Файл уже существует в целевой папке, а опция перезаписи отключена
        /// </summary>
        FILE_ALREADY_EXISTS,

        /// <summary>
        /// Ошибка получения расширения файла
        /// </summary>
        FILE_GET_EXTENSION_FAILED,

        /// <summary>
        /// Указанная директория не существует
        /// </summary>
        DIRECTORY_NOT_FOUND,

        /// <summary>
        /// Ошибка удаления временного файла
        /// </summary>
        FILE_TEMP_DELETE,

        /// <summary>
        /// Не удалось создать директорию (недостаточно прав, недостаточно места и т.д.)
        /// </summary>
        DIRECTORY_CREATE_FAILED,

        /// <summary>
        /// Не удалось инициализировать BASS аудио движок
        /// </summary>
        BASS_INIT_FAILED,

        /// <summary>
        /// Ошибка загрузки плагина декодирования (BASS plugin)
        /// </summary>
        PLUGIN_LOAD_FAILED,

        /// <summary>
        /// Папка с плагинами декодирования отсутствует, доступна поддержка только MP3
        /// </summary>
        PLUGINS_FOLDER_MISSING,

        /// <summary>
        /// Не удалось создать аудио поток (файл повреждён или имеет неподдерживаемый формат)
        /// </summary>
        STREAM_CREATE_FAILED,

        /// <summary>
        /// Не удалось очистить аудио поток (возможно, он уже был очищен или не существует)
        /// </summary>
        STREAM_FREE_FAILED,

        /// <summary>
        /// Не удалось создать микшер для обработки аудио
        /// </summary>
        MIXER_CREATE_FAILED,

        /// <summary>
        /// Не удалось добавить аудио поток в микшер
        /// </summary>
        MIXER_ADD_CHANNEL_FAILED,

        /// <summary>
        /// Не удалось запустить энкодер
        /// </summary>
        ENCODER_START_FAILED,

        /// <summary>
        /// Не удалось остановить энкодер (возможно, он уже был остановлен или не отвечает)
        /// </summary>
        ENCODER_STOP_FAILED,

        /// <summary>
        /// Отсутствует файл энкодера
        /// </summary>
        ENCODER_FILE_IS_MISSING,

        /// <summary>
        /// Ошибка в процессе кодирования аудио файла
        /// </summary>
        ENCODE_FAILED,

        /// <summary>
        /// Ошибка в процессе декодирования аудио файла
        /// </summary>
        DECODE_FAILED,

        /// <summary>
        /// Неподдерживаемый формат аудио файла
        /// </summary>
        INVALID_FORMAT,

        /// <summary>
        /// Передан неверный параметр (например, null или значение вне допустимого диапазона)
        /// </summary>
        INVALID_PARAMETER,

        /// <summary>
        /// Неподдерживаемый битрейт для выбранного формата кодирования
        /// </summary>
        UNSUPPORTED_BITRATE,

        /// <summary>
        /// Неподдерживаемая частота дискретизации (sample rate)
        /// </summary>
        UNSUPPORTED_SAMPLERATE,

        /// <summary>
        /// Неподдерживаемая разрядность (bits per sample)
        /// </summary>
        UNSUPPORTED_BITSPERSAMPLE,

        /// <summary>
        /// Неподдерживаемое количество каналов (моно/стерео)
        /// </summary>
        UNSUPPORTED_CHANNELS,

        /// <summary>
        /// Не удалось прочитать метаданные (теги) из аудио файла
        /// </summary>
        METADATA_READ_FAILED,

        /// <summary>
        /// Не удалось записать метаданные (теги) в аудио файл
        /// </summary>
        METADATA_WRITE_FAILED,

        /// <summary>
        /// Не удалось скопировать метаданные (теги)
        /// </summary>
        METADATA_COPY_FAILED,

        /// <summary>
        /// Ошибка загрузки конфигурационного файла
        /// </summary>
        CONFIG_LOAD_FAILED,

        /// <summary>
        /// Ошибка сохранения конфигурационного файла
        /// </summary>
        CONFIG_SAVE_FAILED,

        /// <summary>
        /// Операция была прервана пользователем
        /// </summary>
        OPERATION_ABORTED,

        /// <summary>
        /// Время выполнения операции истекло (таймаут)
        /// </summary>
        OPERATION_TIMEOUT,

        /// <summary>
        /// Недостаточно памяти для выполнения операции
        /// </summary>
        OUT_OF_MEMORY,

        /// <summary>
        /// Переполнение буфера при обработке аудио данных
        /// </summary>
        BUFFER_OVERFLOW
    }

    public static class ExceptionManager
    {
        public static event Action<string> OnError;
        public static event Action<Error, string> OnDetailedError;

        static ExceptionManager()
        {
            // Подписываемся на свои же события для логирования
            //OnDetailedError += (errorType, message) =>
            //{
            //    // Используем новый метод LogError с типом ошибки
            //    LogService.LogError(message, errorType.ToString());
            //};
        }

        public static void RaiseError(Error errorType, string details = "")
        {
            string message = GetErrorMessage(errorType);
            if (!string.IsNullOrEmpty(details))
                message += $": {details}";

            OnError?.Invoke(message);
            OnDetailedError?.Invoke(errorType, message);
            LogService.LogError(message, errorType.ToString());
        }

        public static void RaiseError(Error errorType, Exception ex, string details = "")
        {
            string message = GetErrorMessage(errorType);
            if (!string.IsNullOrEmpty(details))
                message += $": {details}";

            OnError?.Invoke(message);
            OnDetailedError?.Invoke(errorType, message);

            // Логируем с исключением
            LogService.LogError(message, ex);
        }
        public static void RaiseBassError(Error errorType)
        {
            RaiseError(errorType, $"Bass: {Bass.BASS_ErrorGetCode().ToString()}");
        }
        public static void RaiseBassError(Error errorType, string details = "")
        {
            RaiseError(errorType, $"Bass: {Bass.BASS_ErrorGetCode().ToString()}; Details: {details}");
        }

        private static string GetErrorMessage(Error errorType)
        {
            switch (errorType)
            {
                case Error.NONE: return "Ошибка отсутствует";
                case Error.UNKNOWN: return "Неизвестная ошибка";
                case Error.FILE_NOT_FOUND: return "Файл не найден по указанному пути";
                case Error.FILE_ACCESS_DENIED: return "Отсутствуют права доступа к файлу";
                case Error.FILE_ALREADY_EXISTS: return "Файл уже существует в целевой папке";
                case Error.FILE_TEMP_DELETE: return "Ошибка удаления временного файла";
                case Error.FILE_GET_EXTENSION_FAILED: return "Ошибка получения расширения файла";
                case Error.DIRECTORY_NOT_FOUND: return "Указанная директория не существует";
                case Error.DIRECTORY_CREATE_FAILED: return "Не удалось создать директорию";
                case Error.BASS_INIT_FAILED: return "Не удалось инициализировать BASS аудио движок";
                case Error.PLUGIN_LOAD_FAILED: return "Ошибка загрузки плагина декодирования";
                case Error.PLUGINS_FOLDER_MISSING: return "Папка с плагинами декодирования отсутствует";
                case Error.STREAM_CREATE_FAILED: return "Не удалось создать аудио поток";
                case Error.STREAM_FREE_FAILED: return "Не удалось очистить аудио поток (возможно, он уже был очищен или не существует)";
                case Error.MIXER_CREATE_FAILED: return "Не удалось создать микшер";
                case Error.MIXER_ADD_CHANNEL_FAILED: return "Не удалось добавить аудио поток в микшер";
                case Error.ENCODER_START_FAILED: return "Не удалось запустить энкодер";
                case Error.ENCODER_STOP_FAILED: return "Не удалось остановить энкодер";
                case Error.ENCODE_FAILED: return "Ошибка в процессе кодирования";
                case Error.DECODE_FAILED: return "Ошибка в процессе декодирования";
                case Error.INVALID_FORMAT: return "Неподдерживаемый формат аудио файла";
                case Error.INVALID_PARAMETER: return "Передан неверный параметр";
                case Error.UNSUPPORTED_BITRATE: return "Неподдерживаемый битрейт";
                case Error.UNSUPPORTED_SAMPLERATE: return "Неподдерживаемая частота дискретизации";
                case Error.UNSUPPORTED_BITSPERSAMPLE: return "Неподдерживаемая разрядность";
                case Error.UNSUPPORTED_CHANNELS: return "Неподдерживаемое количество каналов";
                case Error.METADATA_READ_FAILED: return "Не удалось прочитать метаданные";
                case Error.METADATA_WRITE_FAILED: return "Не удалось записать метаданные";
                case Error.METADATA_COPY_FAILED: return "Не удалось скопировать метаданные";
                case Error.CONFIG_LOAD_FAILED: return "Ошибка загрузки конфигурации";
                case Error.CONFIG_SAVE_FAILED: return "Ошибка сохранения конфигурации";
                case Error.OPERATION_ABORTED: return "Операция прервана пользователем";
                case Error.OPERATION_TIMEOUT: return "Время выполнения операции истекло";
                case Error.OUT_OF_MEMORY: return "Недостаточно памяти";
                case Error.BUFFER_OVERFLOW: return "Переполнение буфера";
                default: return "Неизвестная ошибка";
            }
        }
    }
}