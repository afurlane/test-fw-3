using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_API.DTO
{
    public class MovieDTO: BaseMovieDTO
    {
        public ICollection<GenreDTO> Genres { get; set; }
        public ICollection<RatingDTO> Ratings { get; set; }

        // This is good candidate for https://docs.automapper.org/en/stable/Queryable-Extensions.html#aggregations
        public double AverageRating() {
            double AverageRating = 0.0;
            if (Ratings != null && Ratings.Count > 0) {
                foreach (RatingDTO rating in Ratings)
                {
                    AverageRating += rating.Value;
                }
                AverageRating = Math.Round(AverageRating * 2, MidpointRounding.AwayFromZero) / 2;
            }
            return AverageRating;
        }
    }
}
