namespace Music.Models
{
    /// <summary>
    /// Результат поиска
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Найденные треки
        /// </summary>
        public List<Track> Tracks { get; set; } = new List<Track>();

        /// <summary>
        /// Успешность выполнения
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Сообщение об ошибке (если есть)
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Количество найденных треков
        /// </summary>
        public int Count => Tracks.Count;

        /// <summary>
        /// Поисковый запрос
        /// </summary>
        public string Query { get; set; }
    }
}