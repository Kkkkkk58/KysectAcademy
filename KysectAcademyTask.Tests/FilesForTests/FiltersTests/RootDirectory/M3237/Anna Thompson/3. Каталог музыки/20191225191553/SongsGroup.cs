using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_3
{
    public class SongsGroup
    {
        public string name { get; set; }
        public Artist artist { get; set; }
        public List<Song> songs { get; set; }

        public SongsGroup(string Name, Artist Artist = null, List<Song> Songs = null)
        {
            name = Name;
            artist = Artist;
            songs = (Songs == null) ? new List<Song>() : Songs;
        }

        /// <summary>
        /// Добавление песни в группу
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Genre"></param>
        public void AddSong(string Name, Artist Artist, Genre Genre = null)
        {
            songs.Add(new Song(Name, Artist, Genre));
        }

        /// <summary>
        /// Язвляется ли группа альбомом
        /// </summary>
        public bool IsAlbum
        {
            get { return artist != null; }
        }
    }
}
