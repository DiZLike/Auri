using Engine.Managers;
using System.Text;
using System.Text.RegularExpressions;

namespace Engine.Services
{
    public class PathPatternService
    {
        private readonly string _outputDirectory;
        private readonly string _outputFileName;
        private readonly string _pattern;

        // Регулярное выражение для поиска паттернов в квадратных скобках
        private static readonly Regex PatternRegex = new Regex(@"\[(.*?)\]", RegexOptions.Compiled);

        /// <summary>
        /// Создает экземпляр генератора путей для аудиофайлов
        /// </summary>
        /// <param name="outputDirectory">Базовая выходная директория</param>
        /// <param name="outputFileName">Имя выходного файла (может быть использовано в паттерне как [filename])</param>
        /// <param name="pattern">Паттерн для генерации пути, например: [artist]\[year] - [album]\[number] - [title].[format]</param>
        public PathPatternService(string outputDirectory, string outputFileName, string pattern)
        {
            _outputDirectory = outputDirectory ?? throw new ArgumentNullException(nameof(outputDirectory));
            _outputFileName = outputFileName ?? throw new ArgumentNullException(nameof(outputFileName));
            _pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
        }

        /// <summary>
        /// Генерирует полный путь к файлу на основе тегов аудиофайла
        /// </summary>
        /// <param name="audioFile">Путь к исходному аудиофайлу</param>
        /// <returns>Сгенерированный путь к файлу</returns>
        public string GeneratePath(string audioFile, string extension)
        {
            if (!System.IO.File.Exists(audioFile))
                throw new FileNotFoundException("Аудиофайл не найден", audioFile);

            using (var file = TagLib.File.Create(audioFile))
            {
                return GeneratePath(file, extension);
            }
        }

        /// <summary>
        /// Генерирует полный путь к файлу на основе тегов (из уже открытого файла TagLib)
        /// </summary>
        /// <param name="file">Открытый файл TagLib</param>
        /// <returns>Сгенерированный путь к файлу</returns>
        public string GeneratePath(TagLib.File file, string extension)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            var tags = file.Tag;
            var properties = file.Properties;

            // Создаем словарь со всеми доступными тегами
            var tagValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["artist"] = GetFirstNonEmpty(tags.Performers, "Unknown Artist"),
                ["album"] = tags.Album ?? "Unknown Album",
                ["title"] = tags.Title ?? "Unknown Title",
                ["year"] = tags.Year > 0 ? tags.Year.ToString() : "0000",
                ["number"] = tags.Track > 0 ? tags.Track.ToString("D2") : "00",
                ["track"] = tags.Track > 0 ? tags.Track.ToString() : "0",
                ["trackcount"] = tags.TrackCount > 0 ? tags.TrackCount.ToString() : "0",
                ["disc"] = tags.Disc > 0 ? tags.Disc.ToString() : "1",
                ["disccount"] = tags.DiscCount > 0 ? tags.DiscCount.ToString() : "1",
                ["genre"] = GetFirstNonEmpty(tags.Genres, "Unknown Genre"),
                ["composer"] = GetFirstNonEmpty(tags.Composers, "Unknown Composer"),
                ["albumartist"] = GetFirstNonEmpty(tags.AlbumArtists, tags.JoinedPerformers),
                ["format"] = extension,
                ["filename"] = Path.GetFileNameWithoutExtension(_outputFileName),
                ["bitrate"] = properties.AudioBitrate > 0 ? properties.AudioBitrate.ToString() : "0",
                ["duration"] = properties.Duration.ToString(@"hh\:mm\:ss"),
                ["samplerate"] = properties.AudioSampleRate > 0 ? properties.AudioSampleRate.ToString() : "0"
            };

            // Заменяем все паттерны в формате [tag] на соответствующие значения
            string relativePath = PatternRegex.Replace(_pattern, match =>
            {
                string key = match.Groups[1].Value.ToLowerInvariant();
                return tagValues.TryGetValue(key, out string value) ? SanitizeForPath(value) : match.Value;
            });

            // Комбинируем с выходной директорией
            string fullPath = Path.Combine(_outputDirectory, relativePath);

            // Создаем директорию, если она не существует
            string directory = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                try
                {
                    Directory.CreateDirectory(directory);
                }
                catch (Exception ex)
                {
                    ExceptionManager.RaiseError(Error.DIRECTORY_CREATE_FAILED, ex.Message);
                    return String.Empty;
                }
            }

            return $"{fullPath}.{extension}";
        }

        /// <summary>
        /// Получает первое непустое значение из массива
        /// </summary>
        private string GetFirstNonEmpty(string[] values, string defaultValue)
        {
            if (values != null && values.Length > 0 && !string.IsNullOrWhiteSpace(values[0]))
                return values[0].Trim();

            return defaultValue;
        }

        /// <summary>
        /// Очищает строку от недопустимых символов для использования в пути файловой системы
        /// </summary>
        private string SanitizeForPath(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            // Заменяем недопустимые символы на подчеркивание
            char[] invalidChars = Path.GetInvalidFileNameChars();
            var sanitized = new StringBuilder();

            foreach (char c in input)
            {
                if (Array.IndexOf(invalidChars, c) >= 0)
                    sanitized.Append('_');
                else
                    sanitized.Append(c);
            }

            // Удаляем лишние пробелы в начале и конце
            return sanitized.ToString().Trim();
        }
    }
}