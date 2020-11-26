using System;

namespace Repository_API.DTO
{
    public class MovieRatingDTO
    {
        public Guid MovieRatingId { get; set; }
        public String UserName { get; set; }
        public MovieDTO MovieId { get; set; }
    }
}
