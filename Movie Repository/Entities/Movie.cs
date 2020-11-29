using System;
using System.Collections.Generic;

namespace Movie_Repository.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ushort YearOfRelease { get; set; }
        public ushort RunningTimeInMinutes { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Genre> Genres {get; set;}
    }
}
