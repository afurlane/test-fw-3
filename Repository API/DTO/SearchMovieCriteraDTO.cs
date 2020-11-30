using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_API.DTO
{
    public class SearchMovieCriteraDTO
    {
        public string Title { get; set; }
        public ushort? YearOfRelease { get; set; }
        public string[] Genres { get; set; }
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize {
            get { return _pageSize; } 
            set {_pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
        public bool IsSearchValid { 
            get {
                return !(String.IsNullOrEmpty(Title) &&
                    (YearOfRelease == null || YearOfRelease == 0) &&
                    (Genres == null || Genres.Length == 0));
                }
        }
    }
}
