using Repository_API.DTO;
using Repository_API.Helpers;
using System;
using System.Threading.Tasks;

namespace Repository_API
{
    public interface IMovieRepository
    {
        Task<MovieDTO> GetMovie(Guid MovieId);
        // MovieRating GetUserRating(Guid MovieRatingId);
        Task<PagedList<MovieDTO>> GetMovies(SearchCriteraDTO searchCriteria);
    }
}
