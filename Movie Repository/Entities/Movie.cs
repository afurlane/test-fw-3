using System;
using System.Collections.Generic;

namespace Movie_Repository.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTimeInMinutes { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Genre> Genres {get; set;}
    }
}
