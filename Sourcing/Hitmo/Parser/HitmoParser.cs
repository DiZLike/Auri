using HtmlAgilityPack;
using Music.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Xml;
using JsonException = Newtonsoft.Json.JsonException;

namespace Music.Parser
{
    /// <summary>
    /// Парсер для hitmo-top.com
    /// </summary>
    public class HitmoParser : IDisposable
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://eu.hitmo-top.com";

        public HitmoParser()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            _httpClient.DefaultRequestHeaders.Add("Referer", BaseUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Поиск треков по запросу
        /// </summary>
        /// <param name="query">Поисковый запрос</param>
        /// <returns>Результат поиска</returns>
        public SearchResult SearchTracks(string query)
        {
            var result = new SearchResult
            {
                Query = query,
                Success = false
            };

            try
            {
                string url = $"{BaseUrl}/search";
                string fullUrl = $"{url}?q={Uri.EscapeDataString(query)}";
                string response = _httpClient.GetStringAsync(fullUrl).Result;

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(response);

                var trackNodes = htmlDoc.DocumentNode.SelectNodes("//li[contains(@class, 'tracks__item')]");

                if (trackNodes != null)
                {
                    foreach (var node in trackNodes)
                    {
                        var metaJson = node.GetAttributeValue("data-musmeta", "");
                        if (!string.IsNullOrEmpty(metaJson))
                        {
                            try
                            {
                                var trackData = JsonConvert.DeserializeObject<dynamic>(metaJson);

                                var durationNode = node.SelectSingleNode(".//div[contains(@class, 'track__fulltime')]");
                                string duration = durationNode?.InnerText.Trim() ?? "Неизвестно";

                                var downloadNode = node.SelectSingleNode(".//a[contains(@class, 'track__download-btn')]");
                                string downloadUrl = downloadNode?.GetAttributeValue("href", "") ??
                                                     (string)trackData?.url;

                                var track = new Track
                                {
                                    Id = node.GetAttributeValue("data-musid", "").Replace("track-id-", ""),
                                    Artist = (string)trackData?.artist ?? "",
                                    Title = (string)trackData?.title ?? "",
                                    Duration = duration,
                                    DownloadUrl = downloadUrl,
                                    CoverUrl = (string)trackData?.img ?? ""
                                };

                                result.Tracks.Add(track);
                            }
                            catch (JsonException ex)
                            {
                                // Пропускаем ошибочный трек
                                Console.WriteLine($"Ошибка парсинга трека: {ex.Message}");
                            }
                        }
                    }
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Поиск треков по запросу с проверкой ссылок на доступность
        /// </summary>
        /// <param name="query">Поисковый запрос</param>
        /// <returns>Результат поиска с проверенными ссылками</returns>
        public SearchResult SearchTracksWithLinkCheck(string query)
        {
            var result = new SearchResult
            {
                Query = query,
                Success = false
            };

            try
            {
                string url = $"{BaseUrl}/search";
                string fullUrl = $"{url}?q={Uri.EscapeDataString(query)}";
                string response = _httpClient.GetStringAsync(fullUrl).Result;

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(response);

                var trackNodes = htmlDoc.DocumentNode.SelectNodes("//li[contains(@class, 'tracks__item')]");

                if (trackNodes != null)
                {
                    foreach (var node in trackNodes)
                    {
                        var metaJson = node.GetAttributeValue("data-musmeta", "");
                        if (!string.IsNullOrEmpty(metaJson))
                        {
                            try
                            {
                                var trackData = JsonConvert.DeserializeObject<dynamic>(metaJson);

                                var durationNode = node.SelectSingleNode(".//div[contains(@class, 'track__fulltime')]");
                                string duration = durationNode?.InnerText.Trim() ?? "Неизвестно";

                                var downloadNode = node.SelectSingleNode(".//a[contains(@class, 'track__download-btn')]");
                                string downloadUrl = downloadNode?.GetAttributeValue("href", "") ??
                                                     (string)trackData?.url;

                                var track = new Track
                                {
                                    Id = node.GetAttributeValue("data-musid", "").Replace("track-id-", ""),
                                    Artist = (string)trackData?.artist ?? "",
                                    Title = (string)trackData?.title ?? "",
                                    Duration = duration,
                                    DownloadUrl = downloadUrl,
                                    CoverUrl = (string)trackData?.img ?? ""
                                };

                                // Проверяем ссылку на доступность
                                if (!string.IsNullOrEmpty(downloadUrl) && IsUrlAccessible(downloadUrl))
                                {
                                    result.Tracks.Add(track);
                                }
                                // Если ссылка недоступна (404) - пропускаем трек
                            }
                            catch (JsonException ex)
                            {
                                // Пропускаем ошибочный трек
                                Console.WriteLine($"Ошибка парсинга трека: {ex.Message}");
                            }
                        }
                    }
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Проверка доступности URL
        /// </summary>
        /// <param name="url">URL для проверки</param>
        /// <returns>Доступен ли URL (не возвращает 404)</returns>
        private bool IsUrlAccessible(string url)
        {
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Head, url))
                {
                    // Копируем заголовки из основного клиента
                    foreach (var header in _httpClient.DefaultRequestHeaders)
                    {
                        request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }

                    var response = _httpClient.Send(request);

                    // Если статус 404 Not Found - ссылка недоступна
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return false;
                    }

                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Проверка и фильтрация существующего списка треков
        /// </summary>
        /// <param name="tracks">Список треков для проверки</param>
        /// <returns>Список доступных треков (без 404 ошибок)</returns>
        public List<Track> FilterValidTracks(List<Track> tracks)
        {
            var validTracks = new List<Track>();

            foreach (var track in tracks)
            {
                if (!string.IsNullOrEmpty(track.DownloadUrl) && IsUrlAccessible(track.DownloadUrl))
                {
                    validTracks.Add(track);
                }
            }

            return validTracks;
        }

        /// <summary>
        /// Скачивание трека по ссылке
        /// </summary>
        /// <param name="downloadUrl">Ссылка на скачивание</param>
        /// <param name="savePath">Путь для сохранения</param>
        /// <returns>Успешность скачивания</returns>
        public bool DownloadTrack(string downloadUrl, string savePath)
        {
            try
            {
                // Предварительная проверка ссылки на 404
                if (!IsUrlAccessible(downloadUrl))
                {
                    return false;
                }

                var response = _httpClient.GetAsync(downloadUrl).Result;
                response.EnsureSuccessStatusCode();

                var bytes = response.Content.ReadAsByteArrayAsync().Result;
                File.WriteAllBytes(savePath, bytes);

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Получение прямой ссылки для BASS (с проходом по редиректам и правильными заголовками)
        /// </summary>
        /// <param name="downloadUrl">Исходная ссылка на скачивание</param>
        /// <returns>Прямая ссылка на аудиофайл или null, если недоступно</returns>
        public string GetDirectStreamUrl(string downloadUrl)
        {
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Head, downloadUrl))
                {
                    // Добавляем все необходимые заголовки
                    request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
                    request.Headers.Add("Accept", "audio/webm,audio/ogg,audio/wav,audio/*;q=0.9,application/ogg;q=0.7,video/*;q=0.6,*/*;q=0.5");
                    request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                    request.Headers.Add("Referer", BaseUrl);
                    request.Headers.Add("Origin", BaseUrl);
                    request.Headers.Add("Range", "bytes=0-"); // Запрашиваем диапазон для стриминга

                    var response = _httpClient.Send(request);

                    // Получаем финальный URL после всех редиректов
                    string finalUrl = response.RequestMessage?.RequestUri?.ToString() ?? downloadUrl;

                    // Проверяем, что это аудиофайл
                    if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.PartialContent)
                    {
                        // Если есть Content-Type, проверяем что это аудио
                        if (response.Content.Headers.ContentType != null)
                        {
                            string contentType = response.Content.Headers.ContentType.MediaType;
                            if (contentType.StartsWith("audio/") ||
                                contentType == "application/octet-stream" ||
                                downloadUrl.EndsWith(".mp3") ||
                                downloadUrl.EndsWith(".m4a") ||
                                downloadUrl.EndsWith(".ogg"))
                            {
                                return finalUrl;
                            }
                        }
                        else
                        {
                            // Если Content-Type не определен, но URL похож на аудио
                            if (finalUrl.EndsWith(".mp3") || finalUrl.EndsWith(".m4a") ||
                                finalUrl.EndsWith(".ogg") || finalUrl.Contains("/audio/"))
                            {
                                return finalUrl;
                            }
                        }
                    }
                }

                // Если HEAD не сработал, пробуем GET с небольшим буфером
                using (var request = new HttpRequestMessage(HttpMethod.Get, downloadUrl))
                {
                    request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
                    request.Headers.Add("Accept", "audio/webm,audio/ogg,audio/wav,audio/*;q=0.9,application/ogg;q=0.7,video/*;q=0.6,*/*;q=0.5");
                    request.Headers.Add("Referer", BaseUrl);
                    request.Headers.Add("Range", "bytes=0-1024"); // Запрашиваем только первые 1KB

                    var response = _httpClient.Send(request);
                    string finalUrl = response.RequestMessage?.RequestUri?.ToString() ?? downloadUrl;

                    if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.PartialContent)
                    {
                        return finalUrl;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка получения прямой ссылки: {ex.Message}");
            }

            return null;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}