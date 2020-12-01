using Repository_API.DTO;
using Repository_API.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository_API
{
    public interface IMovieRepository
    {
        Task<PagedList<MovieDTO>> GetUserRatingsAsync(SearchUserCriteraDTO searchCriteria);
        Task<PagedList<MovieDTO>> GetMoviesAsync(SearchMovieCriteraDTO searchCriteria);
        Task<List<MovieDTO>> GetFiveTopRatedMoviesAsync();
        Task UpdateUserRatingAsync(UserRatingDTO userRatingDTO);
    }
}
