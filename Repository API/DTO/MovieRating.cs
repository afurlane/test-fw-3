using System;

namespace Repository_API.DTO
{
    public class MovieRating
    {
        public Guid MovieRatingId { get; set; }
        public String UserName { get; set; }
        public Movie MovieId { get; set; }
    }
}
