using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository_API.DTO
{
    public class SearchUserCriteraDTO: IValidatableObject
    {
        public string UserName { get; set; }
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize {
            get { return _pageSize; } 
            set {_pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            var results = new List<ValidationResult>();
            // If we have annotations
            Validator.TryValidateProperty(UserName,
                new ValidationContext(this, null, null) { MemberName = "UserName" }, results);

            // some other random test
            if (String.IsNullOrEmpty(UserName))
            {
                results.Add(new ValidationResult("UserName should not be null"));
            }
            return results;
        }
    }
}
