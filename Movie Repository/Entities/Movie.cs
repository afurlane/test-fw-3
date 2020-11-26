using System;
using System.Collections.Generic;

namespace Movie_Repository.Entities
{
    public class Movie
    {
        public Guid MovieId { get; set; }
        public String Title { get; set; }
        public UInt16 YearOfRelease { get; set; }
        public UInt16 RunningTimeInMinutes { get; set; }

        public ICollection<Rating> movieRatings { get; set; }
        public ICollection<Genre> Genres {get; set;}
    }
}
