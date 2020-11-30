using Repository_API;
using Repository_API.DTO;
using Repository_API.Helpers;
using System.Linq;
using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Movie_Repository.Infrastructure.Mapping_Extensions;
using System.Threading.Tasks;

namespace Movie_Repository
{
    public class MovieRepository : IMovieRepository, IDisposable
    {
        MovieDbContext movieDbContext;
        IMapper mapper;

        public MovieRepository(MovieDbContext movieDbContext, IMapper mapper)
        {
            this.movieDbContext = movieDbContext;
            this.mapper = mapper;
            movieDbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            movieDbContext.Database.CloseConnection();
        }

        public async Task<PagedList<MovieDTO>> GetMoviesAsync(SearchMovieCriteraDTO searchCriteria)
        {
            // From the test description is unclear, to me, of how the search sould be done if I specify, for example
            // title, more than one genre, and the title does not have one of the genres.
            Expression<Func<MovieDTO, bool>> titleQuery = r => r.Title.Contains(searchCriteria.Title);
            Expression<Func<MovieDTO, bool>> genresQuery = r => r.Genres.Where(p => searchCriteria.Genres.Contains(p.Name)).Any();
            Expression<Func<MovieDTO, bool>> yearQuery = r => r.YearOfRelease == searchCriteria.YearOfRelease;
            Expression<Func<MovieDTO, bool>> finalQuery = null;
            if (!string.IsNullOrEmpty(searchCriteria.Title)) {
                finalQuery = titleQuery;
            }
            if (searchCriteria.YearOfRelease > 0)
            {
                finalQuery = finalQuery != null ? finalQuery.And(yearQuery) : yearQuery;
            }
            if (searchCriteria.Genres != null && searchCriteria.Genres.Length> 0)
            {
                finalQuery = finalQuery != null ? finalQuery.And(genresQuery) : genresQuery;
            }
            ICollection<MovieDTO> movies = await movieDbContext.Movies.GetItemsAsync(
                mapper,
                finalQuery,
                null,
                new List<Expression<Func<IQueryable<MovieDTO>, IIncludableQueryable<MovieDTO, object>>>>() {
                    item => item.Include(genre => genre.Genres).Include(rating => rating.Ratings)
                }); ; 

            return PagedList<MovieDTO>.ToPagedList(movies, searchCriteria.PageNumber, searchCriteria.PageSize);
        }

        public async Task<PagedList<MovieDTO>> GetUserRatingsAsync(SearchUserCriteraDTO searchCriteria)
        {
            Expression<Func<MovieDTO, bool>> finalQuery = r => r.Ratings.Where(p => p.User.Name == searchCriteria.UserName)
            .OrderByDescending(p => p.Value).Take(5).Any();

            ICollection<MovieDTO> movies = await movieDbContext.Movies.GetItemsAsync(
                mapper,
                finalQuery,
                null,
                new List<Expression<Func<IQueryable<MovieDTO>, IIncludableQueryable<MovieDTO, object>>>>() {
                    item => item.Include(genre => genre.Genres).Include(rating => rating.Ratings)
                }); ;
            return PagedList<MovieDTO>.ToPagedList(movies, searchCriteria.PageNumber, searchCriteria.PageSize);
        }

        public async Task<PagedList<MovieDTO>> GetFiveTopRatedMoviesAsync()
        {
            var result = (from p in movieDbContext.Movies select p.Ratings
            Expression < Func<MovieDTO, bool> > finalQuery = r => r.Ratings.OrderByDescending(r => r.Value).Any();

            ICollection<MovieDTO> movies = await movieDbContext.Movies.GetItemsAsync(
                mapper,
                finalQuery,
                null,
                new List<Expression<Func<IQueryable<MovieDTO>, IIncludableQueryable<MovieDTO, object>>>>() {
                    item => item.Include(genre => genre.Genres).Include(rating => rating.Ratings)
                }); ;
            return PagedList<MovieDTO>.ToPagedList(movies, 1, 5);
        }
    }
}
