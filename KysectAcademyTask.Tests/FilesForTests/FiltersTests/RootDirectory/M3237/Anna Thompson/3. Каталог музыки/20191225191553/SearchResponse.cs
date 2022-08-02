using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_3
{
    public class SearchResponse
    {
        public List<Song> sounds { get; set; }
        public List<SongsGroup> albums { get; set; }
        public List<SongsGroup> compilation { get; set; }

        public SearchResponse()
        {
            sounds = new List<Song>();
            albums = new List<SongsGroup>();
            compilation = new List<SongsGroup>();
        }
    }
}
