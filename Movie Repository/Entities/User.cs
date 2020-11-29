using System;
using System.Collections.Generic;
using System.Text;

namespace Movie_Repository.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
