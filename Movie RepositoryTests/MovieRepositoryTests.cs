using NUnit.Framework;
using Movie_Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using Repository_API;
using Repository_API.DTO;
using Repository_API.Helpers;

namespace Movie_Repository.Tests
{
    [TestFixture()]
    public class MovieRepositoryTests
    {
        DbContextOptions<MovieDbContext> dbOptions;
        MovieDbContext dbContext;
        IMapper mapper;
        IMovieRepository movieRepository;

        [SetUp]
        public async Task CreateContext()
        {
            dbOptions = new DbContextOptions<MovieDbContext>();
            dbContext = new MovieDbContext(dbOptions);
            var config = new MapperConfiguration(cfg => {
                // cfg.AddProfile<AppProfile>();
                // cfg.CreateMap<Source, Dest>();
            });
            mapper = config.CreateMapper();
            movieRepository = new MovieRepository(dbContext, mapper);
            await dbContext.Database.EnsureCreatedAsync();
            await dbContext.Database.OpenConnectionAsync();
        }

        [TearDown]
        public async Task CloseContext()
        {
            await dbContext.Database.CloseConnectionAsync();
        }

        [Test(Author ="Alessandro Furlanetto", Description ="")]
        public async Task MovieRepositoryTest()
        {
            SearchCriteraDTO search = new SearchCriteraDTO();
            search.Title = "Snatch";
            PagedList<MovieDTO> movies = await movieRepository.GetMovies(search);
            Assert.IsNotNull(movies);
        }

        [Test()]
        public void GetMovieTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetMoviesTest()
        {
            Assert.Fail();
        }
    }
}