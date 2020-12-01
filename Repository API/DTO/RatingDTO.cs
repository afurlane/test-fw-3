using System;

namespace Repository_API.DTO
{
    public class RatingDTO
    {
        public UserDTO User{ get; set; }
        public int Value { get; set; }
    }
}
