using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_API.DTO
{
    public class MovieDTO
    {
        public Guid MovieId { get; set; }
        public String Title { get; set; }
        public UInt16 YearOfRelease { get; set; }
        public String Genre { get; set; }
        public UInt16 RunningTimeInMinutes { get; set; }
        public Double AverageRating { get; set; }
    }
}
