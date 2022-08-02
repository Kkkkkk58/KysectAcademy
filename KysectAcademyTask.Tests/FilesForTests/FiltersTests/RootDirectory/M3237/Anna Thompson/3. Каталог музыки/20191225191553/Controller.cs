using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_3
{
    public class Controller
    {
        public Catalog Catalog { get; set; }

        public Controller()
        {
            Catalog = new Catalog();
        }

        public void SetSongs()
        {
            Catalog.AddAlbum("ConanFirst", "Conan", new List<Song>()
            {
                new Song("1", null, DbContext.GenresCollection.FindGenre("Rap")),
                new Song("2", null, DbContext.GenresCollection.FindGenre("Rap"))
            });
            
            Catalog.AddAlbum("OliverFirst", "Oliver", new List<Song>()
            {
                new Song("1", null, DbContext.GenresCollection.FindGenre("Classic-Rock")),
                new Song("2", null, DbContext.GenresCollection.FindGenre("Rock")),
                new Song("3", null, DbContext.GenresCollection.FindGenre("Soul-Rock")),
                new Song("4", null, DbContext.GenresCollection.FindGenre("Rap"))
            });
            
            Catalog.AddAlbum("NFFirst", "NF", new List<Song>()
            {
                new Song("1", null, DbContext.GenresCollection.FindGenre("New-Pop")),
                new Song("2", null, DbContext.GenresCollection.FindGenre("Pop")),
                new Song("3", null, DbContext.GenresCollection.FindGenre("Old-Pop"))
            });
            
            Catalog.AddCompilation("FirstCompilation", new List<Song>()
            {
                Catalog.Search("Conan", "ConanFirst", "1").sounds[0],
                Catalog.Search("Oliver", "OliverFirst", "2").sounds[0],
                Catalog.Search("NF", "NFFirst", "3").sounds[0],
            });
        }
    }
}
