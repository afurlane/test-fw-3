using Repository_API.DTO;
using Repository_API.Helpers;
using System;

namespace Repository_API
{
    public interface IMovieRepository
    {
        Movie GetMovie(Guid MovieId);
        // MovieRating GetUserRating(Guid MovieRatingId);
        PagedList<Movie> GetMovies(SearchCritera searchCriteria);
    }
}
