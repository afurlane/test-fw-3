using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository_API.DTO
{
    public class UserRatingDTO
    {
        public BaseMovieDTO Movie { get; set; }
        [Required]
        [Range(minimum: 1, maximum: 5)]
        public int Rating { get; set; }
        [Required]
        public UserDTO User { get; set; }
    }
}
