using System;

namespace Movie_Repository.Entities
{
    public class MovieRating
    {
        public Guid MovieRatingId { get; set; }
        public UInt16 Rating { get; set; }
        public Movie MovieId { get; set; }
        public User UserName { get; set; }
    }
}
