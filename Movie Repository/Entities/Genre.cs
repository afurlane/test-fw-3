using System;
using System.Collections.Generic;
using System.Text;

namespace Movie_Repository.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
