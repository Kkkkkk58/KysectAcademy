using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace LABA_3
{
    public static class DbContext
    {
        public static ArtistsCollection ArtistCollection { get; set; } = 
            new ArtistsCollection(new List<Artist>()
        {
            new Artist("Conan"),
            new Artist("Oliver"),
            new Artist("NF")
        });
        public static GenresCollection GenresCollection { get; set; } =
            new GenresCollection(new List<Genre>()
        {
            new Genre("Rock", new List<Genre>()
            {
                new Genre("Classic-Rock"),
                new Genre("Soul-Rock")
            }),
            new Genre("Rap"),
            new Genre("Pop", new List<Genre>()
            {
                new Genre("New-Pop"),
                new Genre("Old-Pop")
            })
        });
    }
}