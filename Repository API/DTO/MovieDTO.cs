using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_API.DTO
{
    public class MovieDTO
    {
        public string Title { get; set; }
        public ushort YearOfRelease { get; set; }
        public ICollection<GenreDTO> Genres { get; set; }
        public ushort RunningTimeInMinutes { get; set; }
        public ICollection<RatingDTO> Ratings { get; set; }
        
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
