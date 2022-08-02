using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_3
{
    public class Song
    {
        public string name { get; set; }
        public Genre genre { get; set; }
        public Artist artist { get; set; }

        public Song(string Name, Artist Artist, Genre Genre = null)
        {
            name = Name;
            artist = Artist;
            genre = Genre;
        }
    }
}
