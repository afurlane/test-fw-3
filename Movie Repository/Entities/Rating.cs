using System;

namespace Movie_Repository.Entities
{
    public class Rating
    {
        public Guid Id { get; set; }
        public ushort Value { get; set; }

        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public Guid UserId { get; set;  }
        public virtual User User { get; set; }
    }
}
