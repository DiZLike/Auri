using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Auri.Services
{
    // Класс для представления трека
    public class CueTrack
    {
        public int Number { get; set; }
        public string Type { get; set; } // AUDIO, CDG, MODE1/2048 и т.д.
        public TimeSpan Index01 { get; set; } // Начало трека
        public TimeSpan Index00 { get; set; } // Предварительный промежуток (опционально)
        public TimeSpan StartTime { get; set; } // Время начала трека (аналог Index01)
        public TimeSpan EndTime { get; set; } // Время окончания трека
        public string Title { get; set; }
        public string Performer { get; set; }
        public string Songwriter { get; set; }
        public string ISRC { get; set; }
        public Dictionary<string, string> Flags { get; set; } // DCP, 4CH, PRE, SCMS

        public CueTrack()
        {
            Flags = new Dictionary<string, string>();
        }

        public override string ToString()
        {
            return $"Track {Number}: {Title} - {Performer} ({StartTime} - {EndTime})";
        }
    }

    // Класс для представления всего CUE файла
    public class CueFile
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; } // WAVE, MP3, BINARY, FLAC и т.д.
        public string Catalog { get; set; } // Каталожный номер
        public string CdTextFile { get; set; } // CD-TEXT файл
        public string Title { get; set; } // Название альбома
        public string Date { get; set; } // Год или дата
        public string Performer { get; set; } // Исполнитель альбома
        public string Songwriter { get; set; } // Автор песен альбома
        public string Genre { get; set; } // Жанр (из REM GENRE)
        public string Comment { get; set; } // Комментарий (из REM COMMENT)
        public string DiscId { get; set; } // DiscID (из REM DISCID)
        public List<CueTrack> Tracks { get; set; }
        public TimeSpan TotalDuration { get; set; } // Общая длительность (опционально)

        public CueFile()
        {
            Tracks = new List<CueTrack>();
        }
    }

    // Парсер CUE файлов
    public class CueService
    {
        private static string _filePath;
        public static CueFile Parse(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(filePath))
                throw new FileNotFoundException("CUE файл не найден", filePath);

            string[] lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
            return ParseLines(lines);
        }

        public static CueFile ParseText(string cueContent)
        {
            string[] lines = cueContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            return ParseLines(lines);
        }

        private static CueFile ParseLines(string[] lines)
        {
            var cueFile = new CueFile();
            CueTrack currentTrack = null;

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                    continue;

                string[] parts = trimmedLine.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2)
                    continue;

                string command = parts[0].ToUpper();
                string argument = parts[1].Trim();

                switch (command)
                {
                    case "FILE":
                        ParseFileCommand(argument, cueFile);
                        break;

                    case "CATALOG":
                        cueFile.Catalog = argument.Trim('"');
                        break;

                    case "CDTEXTFILE":
                        cueFile.CdTextFile = argument.Trim('"');
                        break;

                    case "TITLE":
                        if (currentTrack != null)
                            currentTrack.Title = argument.Trim('"');
                        else
                            cueFile.Title = argument.Trim('"');
                        break;

                    case "PERFORMER":
                        if (currentTrack != null)
                            currentTrack.Performer = argument.Trim('"');
                        else
                            cueFile.Performer = argument.Trim('"');
                        break;

                    case "SONGWRITER":
                        if (currentTrack != null)
                            currentTrack.Songwriter = argument.Trim('"');
                        else
                            cueFile.Songwriter = argument.Trim('"');
                        break;

                    case "TRACK":
                        currentTrack = new CueTrack();
                        string[] trackParts = argument.Split(' ');
                        if (trackParts.Length >= 2)
                        {
                            currentTrack.Number = int.Parse(trackParts[0]);
                            currentTrack.Type = trackParts[1];
                        }
                        cueFile.Tracks.Add(currentTrack);
                        break;

                    case "INDEX":
                        if (currentTrack != null)
                            ParseIndexCommand(argument, currentTrack);
                        break;

                    case "FLAGS":
                        if (currentTrack != null)
                            ParseFlagsCommand(argument, currentTrack);
                        break;

                    case "ISRC":
                        if (currentTrack != null)
                            currentTrack.ISRC = argument.Trim('"');
                        break;

                    case "REM":
                        ParseRemCommand(argument, cueFile, currentTrack);
                        break;
                }
            }

            // Вычисляем время окончания для каждого трека
            CalculateTrackEndTimes(cueFile);

            string cueFolder = Path.GetDirectoryName(_filePath);
            cueFile.FilePath = Path.Combine(cueFolder, cueFile.FileName);
            return cueFile;
        }

        private static void ParseFileCommand(string argument, CueFile cueFile)
        {
            // Формат: "filename.wav" WAVE
            var match = Regex.Match(argument, @"""([^""]+)""\s+(.+)");
            if (match.Success)
            {
                cueFile.FileName = match.Groups[1].Value;
                cueFile.FileType = match.Groups[2].Value;
            }
            else
            {
                // Если имя файла без кавычек
                string[] parts = argument.Split(' ');
                if (parts.Length >= 2)
                {
                    cueFile.FileName = parts[0];
                    cueFile.FileType = parts[1];
                }
            }
        }

        private static void ParseIndexCommand(string argument, CueTrack track)
        {
            // Формат: 01 00:00:00
            string[] parts = argument.Split(' ');
            if (parts.Length >= 2)
            {
                int indexNumber = int.Parse(parts[0]);
                TimeSpan time = ParseTime(parts[1]);

                if (indexNumber == 0)
                    track.Index00 = time;
                else if (indexNumber == 1)
                {
                    track.Index01 = time;
                    track.StartTime = time; // Устанавливаем время начала
                }
            }
        }

        private static void ParseFlagsCommand(string argument, CueTrack track)
        {
            string[] flags = argument.Split(' ');
            foreach (string flag in flags)
            {
                string[] flagParts = flag.Split('=');
                if (flagParts.Length == 1)
                    track.Flags[flagParts[0]] = "true";
                else
                    track.Flags[flagParts[0]] = flagParts[1];
            }
        }

        private static void ParseRemCommand(string argument, CueFile cueFile, CueTrack currentTrack)
        {
            string trimmedArg = argument.Trim();

            // Регулярное выражение для поиска команд в формате REM KEY VALUE
            var remMatch = Regex.Match(trimmedArg, @"^([A-Z]+)\s+(.+)$", RegexOptions.IgnoreCase);

            if (remMatch.Success)
            {
                string key = remMatch.Groups[1].Value.ToUpper();
                string value = remMatch.Groups[2].Value.Trim('"');

                switch (key)
                {
                    case "DATE":
                        cueFile.Date = value;
                        break;

                    case "YEAR":
                        if (string.IsNullOrEmpty(cueFile.Date))
                            cueFile.Date = value;
                        break;

                    case "GENRE":
                        cueFile.Genre = value;
                        break;

                    case "COMMENT":
                        cueFile.Comment = value;
                        break;

                    case "DISCID":
                        cueFile.DiscId = value;
                        break;
                }
            }
            else
            {
                // Обработка REM без явного ключа (простой комментарий)
                // Можно игнорировать или добавить в общий комментарий
                if (string.IsNullOrEmpty(cueFile.Comment))
                    cueFile.Comment = trimmedArg;
                else
                    cueFile.Comment += "; " + trimmedArg;
            }
        }

        private static TimeSpan ParseTime(string timeString)
        {
            // Формат: MM:SS:FF (минуты:секунды:фреймы)
            // 1 фрейм = 1/75 секунды
            string[] parts = timeString.Split(':');
            if (parts.Length == 3)
            {
                int minutes = int.Parse(parts[0]);
                int seconds = int.Parse(parts[1]);
                int frames = int.Parse(parts[2]);

                double totalSeconds = minutes * 60 + seconds + (frames / 75.0);
                return TimeSpan.FromSeconds(totalSeconds);
            }

            return TimeSpan.Zero;
        }

        /// <summary>
        /// Вычисляет время окончания каждого трека на основе времени начала следующего трека
        /// </summary>
        private static void CalculateTrackEndTimes(CueFile cueFile)
        {
            if (cueFile.Tracks == null || cueFile.Tracks.Count == 0)
                return;

            for (int i = 0; i < cueFile.Tracks.Count; i++)
            {
                var currentTrack = cueFile.Tracks[i];

                // Если есть следующий трек, его начало - это конец текущего
                if (i + 1 < cueFile.Tracks.Count)
                {
                    var nextTrack = cueFile.Tracks[i + 1];
                    currentTrack.EndTime = nextTrack.StartTime;
                }
                else
                {
                    // Для последнего трека время окончания не определено
                    // Можно установить в MaxValue или оставить как Zero
                    currentTrack.EndTime = TimeSpan.MaxValue;
                }
            }
        }
    }
}