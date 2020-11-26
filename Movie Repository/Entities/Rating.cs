using System;

namespace Movie_Repository.Entities
{
    public class Rating
    {
        public Guid RatingId { get; set; }
        public UInt16 RatingValue { get; set; }

        public Movie MovieId { get; set; }
        public User UserId { get; set; }
    }
}
