using Auri.Audio;
using Auri.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Auri.Services
{
    public class MetaService
    {
        public bool CopyMetadata(string sourceAudio, string targetAudio)
        {
            try
            {
                using (var sourceFile = TagLib.File.Create(sourceAudio))
                using (var targetFile = TagLib.File.Create(targetAudio))
                {
                    // Копируем основные теги
                    targetFile.Tag.Title = sourceFile.Tag.Title;
                    targetFile.Tag.Performers = sourceFile.Tag.Performers;
                    targetFile.Tag.AlbumArtists = sourceFile.Tag.AlbumArtists;
                    targetFile.Tag.Album = sourceFile.Tag.Album;
                    targetFile.Tag.Year = sourceFile.Tag.Year;
                    targetFile.Tag.Track = sourceFile.Tag.Track;
                    targetFile.Tag.TrackCount = sourceFile.Tag.TrackCount;
                    targetFile.Tag.Disc = sourceFile.Tag.Disc;
                    targetFile.Tag.DiscCount = sourceFile.Tag.DiscCount;
                    targetFile.Tag.Genres = sourceFile.Tag.Genres;
                    targetFile.Tag.Comment = sourceFile.Tag.Comment;
                    targetFile.Tag.Composers = sourceFile.Tag.Composers;
                    targetFile.Tag.Copyright = sourceFile.Tag.Copyright;
                    targetFile.Tag.Lyrics = sourceFile.Tag.Lyrics;

                    // Копируем обложки
                    if (sourceFile.Tag.Pictures != null && sourceFile.Tag.Pictures.Length > 0)
                    {
                        targetFile.Tag.Pictures = sourceFile.Tag.Pictures;
                    }

                    // Сохраняем
                    targetFile.Save();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.RaiseError(Error.METADATA_COPY_FAILED, ex.Message);
                return false;
            }
        }
        public AudioInfo GetTrackInfo(string sourceAudio)
        {
            using (var sourceFile = TagLib.File.Create(sourceAudio))
            {
                string name = Path.GetFileNameWithoutExtension(sourceAudio);
                string codec = sourceFile.Properties.Codecs.First().Description;
                string duration = $"{(int)sourceFile.Properties.Duration.TotalMinutes:D2}:{sourceFile.Properties.Duration.Seconds:D2}";
                float frequency = sourceFile.Properties.AudioSampleRate / 1000f;
                int bitrate = sourceFile.Properties.AudioBitrate;
                int channels = sourceFile.Properties.AudioChannels;

                return new AudioInfo(name, codec, duration, frequency, bitrate, channels);
            }
        }
    }
}