using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_3
{
    public class GenresCollection
    {
        public List<Genre> genres { get; set; }

        public GenresCollection(List<Genre> Genres)
        {
            genres = (Genres == null) ? new List<Genre>() : Genres;
        }

        /// <summary>
        /// Поиск объекта жанра по названию
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Genre FindGenre(string Name)
        {
            if (String.IsNullOrEmpty(Name))
                return null;

            for (var i = 0; i < genres.Count; i++)
            {
                var f = bfs(genres[i], Name);
                if (f != null)
                    return f;
            }
            return null;
        }

        private Genre bfs(Genre genre, string name)
        {
            if (genre.name == name)
                return genre;
            else if (genre.subGenre != null && genre.subGenre.Count > 0)
                for (var i = 0; i < genre.subGenre.Count; i++)
                {
                    var f = bfs(genre.subGenre[i], name);
                    if (f != null)
                        return f;
                }

            return null;
        }
    }
}
