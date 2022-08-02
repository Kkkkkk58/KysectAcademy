using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LABA_3
{

    public class Genre
    {
        public string name { get; set; }
        public List<Genre> subGenre { get; set; }
        private Genre parentGenre { get; set; }

        public Genre(string Name, List<Genre> SubGenre = null)
        {
            name = Name;
            subGenre = SubGenre;
            
        }

        /// <summary>
        /// Проверкак на принадлежность жанра к определенной группе жанров
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="check_sub_genres"></param>
        /// <returns></returns>
        public bool InGenre(string Name, bool check_sub_genres = true)
        {
            if (name == Name || String.IsNullOrEmpty(Name))
                return true;
            else if (subGenre != null && subGenre.Count > 0 && check_sub_genres)
                return subGenre.FirstOrDefault(x => x.InGenre(Name)) != null;
            else if (parentGenre != null)
                return parentGenre.InGenre(Name, false);
            else
                return false;
        }       
    }
}
