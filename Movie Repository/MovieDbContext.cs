using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movie_Repository.Entities;
using System;
using System.Collections.Generic;

namespace Movie_Repository
{
    public class MovieDbContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected Random random = new Random();

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public MovieDbContext(): base(new DbContextOptions<MovieDbContext>())
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("DataSource=Test.db")
                .EnableSensitiveDataLogging()
                .UseLazyLoadingProxies();
        }

        /// <summary>
        /// This seeds data in the db before use, it's a single method, but it's for demostrational purpose.
        /// </summary>
        /// <param name="modelBuilder">Refer to .NET EF Core documentation</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Guid[] moviesGuid = new Guid[10] {
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()
            };
            Guid[] usersGuid = new Guid[20] {
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()
            };
            Guid[] movieRatingsGuid = new Guid[20]{
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()
            };

            Genre[] genres = new Genre[14] {
                new Genre { Id = 1, Name = "Thriller" },
                new Genre { Id = 2, Name = "Crime" },
                new Genre { Id = 3, Name = "Drama" },
                new Genre { Id = 4, Name = "Horror" },
                new Genre { Id = 5, Name = "Mistery" },
                new Genre { Id = 6, Name = "Action" },
                new Genre { Id = 7, Name = "Adventure" },
                new Genre { Id = 8, Name = "Comedy" },
                new Genre { Id = 9, Name = "Sport" },
                new Genre { Id = 10, Name = "Animation" },
                new Genre { Id = 11, Name = "Family"},
                new Genre { Id = 12, Name = "War"},
                new Genre { Id = 13, Name = "Music"},
                new Genre { Id = 14, Name = "Sci-Fi"}
            };

            modelBuilder.Entity<User>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<User>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().Property(p => p.Name).HasMaxLength(10).IsRequired();
            modelBuilder.Entity<User>().HasMany<Rating>(p => p.Ratings).WithOne(p => p.User).HasForeignKey(p => p.UserId);
            modelBuilder.Entity<User>().HasData(
                new { Id = usersGuid[0], Name = "user0" }, new { Id = usersGuid[1], Name = "user1" },
                new { Id = usersGuid[2], Name = "user2" }, new { Id = usersGuid[3], Name = "user3" },
                new { Id = usersGuid[4], Name = "user4" }, new { Id = usersGuid[5], Name = "user5" },
                new { Id = usersGuid[6], Name = "user6" }, new { Id = usersGuid[7], Name = "user7" },
                new { Id = usersGuid[8], Name = "user0" }, new { Id = usersGuid[9], Name = "user1" },
                new { Id = usersGuid[10], Name = "user2" }, new { Id = usersGuid[11], Name = "user3" },
                new { Id = usersGuid[12], Name = "user4" }, new { Id = usersGuid[13], Name = "user5" },
                new { Id = usersGuid[14], Name = "user6" }, new { Id = usersGuid[15], Name = "user7" },
                new { Id = usersGuid[16], Name = "user6" }, new { Id = usersGuid[17], Name = "user7" },
                new { Id = usersGuid[18], Name = "user6" }, new { Id = usersGuid[19], Name = "user7" }
            );

            modelBuilder.Entity<Genre>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<Genre>().HasKey(p => p.Id);
            modelBuilder.Entity<Genre>().Property(p => p.Name).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Genre>().HasMany<Movie>(p => p.Movies).WithMany(p => p.Genres);
            modelBuilder.Entity<Genre>().HasData(genres);            

            modelBuilder.Entity<Movie>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<Movie>().HasKey(p => p.Id);
            modelBuilder.Entity<Movie>().Property(p => p.Title).HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Movie>().HasIndex(p => p.Title);
            modelBuilder.Entity<Movie>().HasMany<Genre>(p => p.Genres).WithMany(p => p.Movies)
                .UsingEntity(j => j.HasData(
                new { MoviesId = moviesGuid[0], GenresId = genres[0].Id },
                new { MoviesId = moviesGuid[0], GenresId = genres[5].Id },
                new { MoviesId = moviesGuid[0], GenresId = genres[6].Id },
                new { MoviesId = moviesGuid[1], GenresId = genres[1].Id },
                new { MoviesId = moviesGuid[1], GenresId = genres[3].Id },
                new { MoviesId = moviesGuid[1], GenresId = genres[4].Id },
                new { MoviesId = moviesGuid[2], GenresId = genres[1].Id },
                new { MoviesId = moviesGuid[2], GenresId = genres[7].Id },
                new { MoviesId = moviesGuid[2], GenresId = genres[8].Id },
                new { MoviesId = moviesGuid[3], GenresId = genres[4].Id },
                new { MoviesId = moviesGuid[3], GenresId = genres[9].Id },
                new { MoviesId = moviesGuid[3], GenresId = genres[10].Id },
                new { MoviesId = moviesGuid[4], GenresId = genres[0].Id },
                new { MoviesId = moviesGuid[4], GenresId = genres[2].Id },
                new { MoviesId = moviesGuid[4], GenresId = genres[11].Id },
                new { MoviesId = moviesGuid[5], GenresId = genres[1].Id },
                new { MoviesId = moviesGuid[5], GenresId = genres[7].Id },
                new { MoviesId = moviesGuid[5], GenresId = genres[12].Id },
                new { MoviesId = moviesGuid[6], GenresId = genres[1].Id },
                new { MoviesId = moviesGuid[6], GenresId = genres[7].Id },
                new { MoviesId = moviesGuid[7], GenresId = genres[3].Id },
                new { MoviesId = moviesGuid[7], GenresId = genres[4].Id },
                new { MoviesId = moviesGuid[7], GenresId = genres[13].Id },
                new { MoviesId = moviesGuid[8], GenresId = genres[6].Id },
                new { MoviesId = moviesGuid[8], GenresId = genres[13].Id },
                new { MoviesId = moviesGuid[9], GenresId = genres[7].Id }
                ));
            modelBuilder.Entity<Movie>().HasMany<Rating>(p => p.Ratings).WithOne(p => p.Movie).HasForeignKey(p => p.MovieId);
            modelBuilder.Entity<Movie>().HasData(
            new { Id = moviesGuid[0], RunningTimeInMinutes = (ushort)135, Title = "The hunt for a red october", YearOfRelease = (ushort)1990 },
            new { Id = moviesGuid[1], RunningTimeInMinutes = (ushort)120, Title = "Manhunter", YearOfRelease = (ushort)1986 },
            new { Id = moviesGuid[2], RunningTimeInMinutes = (ushort)117, Title = "The Big Lebowski", YearOfRelease = (ushort)1998 },
            new { Id = moviesGuid[3], RunningTimeInMinutes = (ushort)98, Title = "WALL·E", YearOfRelease = (ushort)2008 },
            new { Id = moviesGuid[4], RunningTimeInMinutes = (ushort)128, Title = "Rules of Engagement", YearOfRelease = (ushort)2000 },
            new { Id = moviesGuid[5], RunningTimeInMinutes = (ushort)118, Title = "Be Cool", YearOfRelease = (ushort)2005 },
            new { Id = moviesGuid[6], RunningTimeInMinutes = (ushort)104, Title = "Snatch", YearOfRelease = (ushort)2000 },
            new { Id = moviesGuid[7], RunningTimeInMinutes = (ushort)109, Title = "The Thing", YearOfRelease = (ushort)1982 },
            new { Id = moviesGuid[8], RunningTimeInMinutes = (ushort)149, Title = "2001: A Space Odyssey", YearOfRelease = (ushort)1968 },
            new { Id = moviesGuid[9], RunningTimeInMinutes = (ushort)94, Title = "Life of Brian", YearOfRelease = (ushort)1979 }
            );

            modelBuilder.Entity<Rating>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Rating>().Property(p => p.Value).IsRequired();
            modelBuilder.Entity<Rating>().HasOne<Movie>(p => p.Movie).WithMany(p => p.Ratings).HasForeignKey(p => p.MovieId);
            modelBuilder.Entity<Rating>().HasOne<User>(p => p.User).WithMany(p => p.Ratings).HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Rating>().HasData(
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[0], MovieId = moviesGuid[1] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[1], MovieId = moviesGuid[2] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[2], MovieId = moviesGuid[3] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[3], MovieId = moviesGuid[4] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[4], MovieId = moviesGuid[5] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[5], MovieId = moviesGuid[6] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[6], MovieId = moviesGuid[7] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[7], MovieId = moviesGuid[8] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[8], MovieId = moviesGuid[9] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[9], MovieId = moviesGuid[0] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[10], MovieId = moviesGuid[1] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[11], MovieId = moviesGuid[2] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[12], MovieId = moviesGuid[3] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[13], MovieId = moviesGuid[4] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[14], MovieId = moviesGuid[5] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[15], MovieId = moviesGuid[6] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[16], MovieId = moviesGuid[7] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[17], MovieId = moviesGuid[8] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[18], MovieId = moviesGuid[9] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[19], MovieId = moviesGuid[0] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[0], MovieId = moviesGuid[1] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[1], MovieId = moviesGuid[2] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[2], MovieId = moviesGuid[3] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[3], MovieId = moviesGuid[4] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[4], MovieId = moviesGuid[5] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[5], MovieId = moviesGuid[6] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[6], MovieId = moviesGuid[7] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[7], MovieId = moviesGuid[8] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[8], MovieId = moviesGuid[9] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[9], MovieId = moviesGuid[0] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[10], MovieId = moviesGuid[1] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[11], MovieId = moviesGuid[2] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[12], MovieId = moviesGuid[3] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[13], MovieId = moviesGuid[4] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[14], MovieId = moviesGuid[5] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[15], MovieId = moviesGuid[6] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[16], MovieId = moviesGuid[7] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[17], MovieId = moviesGuid[8] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[18], MovieId = moviesGuid[9] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[19], MovieId = moviesGuid[0] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[0], MovieId = moviesGuid[1] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[1], MovieId = moviesGuid[3] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[2], MovieId = moviesGuid[4] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[3], MovieId = moviesGuid[5] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[4], MovieId = moviesGuid[6] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[5], MovieId = moviesGuid[7] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[6], MovieId = moviesGuid[8] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[7], MovieId = moviesGuid[9] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[8], MovieId = moviesGuid[0] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[9], MovieId = moviesGuid[1] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[10], MovieId = moviesGuid[2] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[11], MovieId = moviesGuid[3] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[12], MovieId = moviesGuid[4] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[13], MovieId = moviesGuid[5] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[14], MovieId = moviesGuid[6] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[15], MovieId = moviesGuid[7] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[16], MovieId = moviesGuid[8] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[17], MovieId = moviesGuid[9] },
                new { Id = Guid.NewGuid(), Value = GetRatingValue(), UserId = usersGuid[18], MovieId = moviesGuid[0] }
                );
        }

        private ushort GetRatingValue()
        {
            return (ushort)random.Next(1, 5);
        }

        private ushort GetCustomerId()
        {
            return (ushort)random.Next(0, 19);
        }

        private ushort GetMoviesId()
        {
            return (ushort)random.Next(0, 9);
        }
    }
}
