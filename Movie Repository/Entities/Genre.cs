using System;
using System.Collections.Generic;
using System.Text;

namespace Movie_Repository.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
