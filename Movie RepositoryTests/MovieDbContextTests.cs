using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Movie_Repository.Entities;
using System.Threading.Tasks;

namespace Movie_Repository.Tests
{
    [TestFixture()]
    public class MovieDbContextTests
    {
        DbContextOptions<MovieDbContext> dbOptions;
        MovieDbContext dbContext;

        [SetUp]
        public async Task CreateContext ()
        {
            dbOptions = new DbContextOptions<MovieDbContext>();
            dbContext = new MovieDbContext(dbOptions);
            await dbContext.Database.EnsureCreatedAsync();
            await dbContext.Database.OpenConnectionAsync();
        }

        [TearDown]
        public async Task CloseContext()
        {
            await dbContext.Database.CloseConnectionAsync();
        }

        /// <summary>
        /// Simpli test that the db is seeded as needed.
        /// </summary>
        /// <returns></returns>
        [Test(Author = "Alessandro Furlanetto", Description = "Test the seed of data")]
        public async Task TestSeed()
        {
            Movie movie = await dbContext.Movies.FirstOrDefaultAsync();
            Assert.IsNotNull(movie.Title);
            Assert.IsNotNull(movie.Ratings);
            Assert.IsTrue(movie.Ratings.Count > 0);
            foreach(Rating rating in movie.Ratings)
            {
                Assert.IsNotNull(rating.Value);
                Assert.IsNotNull(rating.User.Name);
            }
            foreach(Genre genre in movie.Genres)
            {
                Assert.IsNotNull(genre.Name);
            }
        }
    }
}