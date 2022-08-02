using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LABA_3
{
    public class Catalog
    {
        public List<SongsGroup> songsGroup { get; set; }
        private List<Song> songs { get; set; }

        public Catalog()
        {
            songsGroup = new List<SongsGroup>();
            songs = new List<Song>();
        }

        public SearchResponse Search(string ArtistName = null, string AlbumName = null, string SongName = null, string GenreName = null)
        {
            SearchResponse answer = new SearchResponse();
            songsGroup
            .Where(sg =>
                (String.IsNullOrEmpty(ArtistName) || !sg.IsAlbum || sg.artist == ArtistName) &&
                (String.IsNullOrEmpty(AlbumName) || sg.name == AlbumName)).ToList()
            .ForEach(sg =>
            {
                var sounds = 
                sg.songs
                .Where(s =>
                    (String.IsNullOrEmpty(ArtistName) || s.artist == ArtistName) &&
                    (String.IsNullOrEmpty(SongName) || s.name == SongName) &&
                    (String.IsNullOrEmpty(GenreName) || s.genre.InGenre(GenreName))).ToList();

                if (sounds.Count > 0)
                {
                    if (sg.IsAlbum)
                    {
                        foreach (var x in sounds)
                            answer.sounds.Add(x);
                        answer.albums.Add(sg);
                    }
                    else
                        answer.compilation.Add(sg);
                }
            });

            return answer;
        }

        
        /// <summary>
        /// Добавить группу песен
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ArtistName"></param>
        /// <param name="Songs"></param>
        private void AddSongsGroup(string Name, List<Song> Songs, string ArtistName = null)
        {
            
            Artist artist = (ArtistName == null) ? null : DbContext.ArtistCollection.GetArtist(ArtistName);
            if (artist != null)
            {
                foreach (var song in Songs)
                {
                    song.artist = artist;
                    AddSong(song);
                }
            }
            songsGroup.Add(new SongsGroup(Name, artist, Songs));
        }
        /// <summary>
        /// Добавить альбом
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ArtistName"></param>
        /// <param name="Songs"></param>
        public void AddAlbum(string Name, string ArtistName, List<Song> Songs)
        {
            AddSongsGroup(Name, Songs, ArtistName);
        }
        /// <summary>
        /// Добавить сборник
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Songs"></param>
        public void AddCompilation(string Name, List<Song> Songs)
        {
            AddSongsGroup(Name, Songs, null);
        }
        
        
        /// <summary>
        /// Добавить песню
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Genre"></param>
        private void AddSong(Song Song)
        {
            songs.Add(Song);
        }
        /// <summary>
        /// Добавить песню
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Genre"></param>
        private void AddSong(string Name, Artist Artist, string Genre)
        {
            if (Artist == null) return;
            var genre = (String.IsNullOrEmpty(Genre)) ? null : DbContext.GenresCollection.FindGenre(Genre);
            AddSong(new Song(Name, Artist, genre));
        }
        /// <summary>
        /// Добавить песню
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Genre"></param>
        private void AddSong(string Name, string ArtistName, string Genre)
        {
            var artist = DbContext.ArtistCollection.GetArtist(ArtistName);
            AddSong(Name, artist, Genre);
        }
    }
}
