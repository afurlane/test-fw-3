using Repository_API;
using Repository_API.DTO;
using Repository_API.Helpers;
using System.Linq;
using System;

namespace Movie_Repository
{
    public class MovieRepository : IMovieRepository
    {
        MovieDbContext movieDbContext;
        public MovieRepository(MovieDbContext movieDbContext)
        {
            this.movieDbContext = movieDbContext;
        }

        public async Movie GetMovie(Guid MovieId)
        {
            throw new NotImplementedException();
        }

        public async PagedList<Movie> GetMovies(SearchCritera searchCriteria)
        {
            // Maybe we could use this
            // https://github.com/AutoMapper/AutoMapper.Extensions.ExpressionMapping
            var movies = from p in movieDbContext.Movies where p.Title == searchCriteria.Title select p;

            return PagedList<Movie>.ToPagedList(movies,
                searchCriteria.PageNumber,
                searchCriteria.PageSize);
        }
    }
}
