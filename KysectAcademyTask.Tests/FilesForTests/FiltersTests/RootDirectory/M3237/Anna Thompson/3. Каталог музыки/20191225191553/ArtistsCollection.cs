using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LABA_3
{
    public class ArtistsCollection
    {
        private List<Artist> artists { get; set; }
        public List<Artist> Artists => artists;

        public ArtistsCollection(List<Artist> Artists = null)
        {
            artists = (Artists == null) ? new List<Artist>() : Artists;
        }

        public Artist GetArtist(string Name)
        {
            if (String.IsNullOrEmpty(Name)) return null;
            return artists.FirstOrDefault(x => x.name == Name);
        }

        public void AddArtist(Artist Artist)
        {
            artists.Add(Artist);
        }
    }
}
