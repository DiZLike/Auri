using System;

namespace Music.Models
{
    /// <summary>
    /// Модель музыкального трека
    /// </summary>
    public class Track
    {
        /// <summary>
        /// ID трека
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Название трека
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Исполнитель
        /// </summary>
        public string Artist { get; set; } = string.Empty;

        /// <summary>
        /// Длительность трека
        /// </summary>
        public string Duration { get; set; } = string.Empty;

        /// <summary>
        /// Ссылка для скачивания
        /// </summary>
        public string DownloadUrl { get; set; } = string.Empty;

        /// <summary>
        /// Ссылка на обложку
        /// </summary>
        public string CoverUrl { get; set; } = string.Empty;

        /// <summary>
        /// Полное название (исполнитель - трек)
        /// </summary>
        public string FullTitle => $"{Artist} - {Title}";

        public override string ToString()
        {
            return $"{FullTitle} ({Duration})";
        }
    }
}