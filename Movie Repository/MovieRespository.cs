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
    public class MovieRepository : IMovieRepository
    {
        MovieDbContext movieDbContext;
        IMapper mapper;

        public MovieRepository(MovieDbContext movieDbContext, IMapper mapper)
        {
            this.movieDbContext = movieDbContext;
            this.mapper = mapper;
        }

        public async Task<MovieDTO> GetMovie(Guid MovieId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<MovieDTO>> GetMovies(SearchCriteraDTO searchCriteria)
        {
            Expression<Func<MovieDTO, bool>> titleQuery = r => r.Title.Contains(searchCriteria.Title);
            Expression<Func<MovieDTO, bool>> genresQuery = r => searchCriteria.Genres.Contains(r.Genre);
            Expression<Func<MovieDTO, bool>> yearQuery = r => r.YearOfRelease == searchCriteria.YearOfRelease;
            Expression<Func<MovieDTO, bool>> finalQuery = null;
            if (!string.IsNullOrEmpty(searchCriteria.Title)) {
                finalQuery = titleQuery;
            }
            if (searchCriteria.YearOfRelease > 0)
            {
                if (finalQuery != null)
                {
                    finalQuery = Expression.Lambda<Func<MovieDTO, bool>>(Expression.And(finalQuery, yearQuery));
                } else
                {
                    finalQuery = yearQuery;
                }
            }
            if (searchCriteria.Genres != null && searchCriteria.Genres.Length> 0)
            {
                if (finalQuery != null)
                {
                    finalQuery = Expression.Lambda<Func<MovieDTO, bool>>(Expression.And(finalQuery, genresQuery));
                }
                else
                {
                    finalQuery = genresQuery;
                }
            }

            ICollection<MovieDTO> movies = await movieDbContext.Movies.GetItemsAsync(
                mapper,
                finalQuery,
                null,
                new List<Expression<Func<IQueryable<MovieDTO>, IIncludableQueryable<MovieDTO, object>>>>() {
                    item => item.Include(s => s.Genre)
                }); 

            return PagedList<MovieDTO>.ToPagedList(movies, searchCriteria.PageNumber, searchCriteria.PageSize);
        }
    }
}
