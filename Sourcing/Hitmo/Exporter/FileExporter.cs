using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.Models;

namespace Music.Exporter
{
    /// <summary>
    /// Экспортер результатов
    /// </summary>
    public class FileExporter
    {
        /// <summary>
        /// Сохранить треки в текстовый файл
        /// </summary>
        public async Task SaveToTxtAsync(List<Track> tracks, string filePath)
        {
            var sb = new StringBuilder();
            foreach (var track in tracks)
            {
                sb.AppendLine($"{track.Artist} - {track.Title} ({track.Duration})");
                sb.AppendLine($"Ссылка: {track.DownloadUrl}");
                sb.AppendLine();
            }

            await File.WriteAllTextAsync(filePath, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// Сохранить только ссылки в текстовый файл
        /// </summary>
        public async Task SaveLinksOnlyAsync(List<Track> tracks, string filePath)
        {
            var sb = new StringBuilder();
            foreach (var track in tracks)
            {
                sb.AppendLine($"{track.FullTitle}: {track.DownloadUrl}");
            }

            await File.WriteAllTextAsync(filePath, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// Сохранить в CSV
        /// </summary>
        public async Task SaveToCsvAsync(List<Track> tracks, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("ID,Исполнитель,Название,Длительность,Ссылка,Обложка");

            foreach (var track in tracks)
            {
                sb.AppendLine($"\"{track.Id}\",\"{track.Artist}\",\"{track.Title}\",\"{track.Duration}\",\"{track.DownloadUrl}\",\"{track.CoverUrl}\"");
            }

            await File.WriteAllTextAsync(filePath, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// Сохранить в JSON
        /// </summary>
        public async Task SaveToJsonAsync(List<Track> tracks, string filePath)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(tracks, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(filePath, json, Encoding.UTF8);
        }
    }
}