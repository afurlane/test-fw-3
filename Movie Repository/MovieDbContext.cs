using Microsoft.EntityFrameworkCore;
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

        public MovieDbContext(DbContextOptions<MovieDbContext> options): base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseInMemoryDatabase("MoviesDataBase")
                .EnableSensitiveDataLogging()
                .UseLazyLoadingProxies();
        }

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

            Genre[] genres = new Genre[10] {
                new Genre { GenreId = 1, GenreName = "Thriller" },
                new Genre { GenreId = 2, GenreName = "Crime" },
                new Genre { GenreId = 3, GenreName = "Drama" },
                new Genre { GenreId = 4, GenreName = "Horror" },
                new Genre { GenreId = 5, GenreName = "Mistery" },
                new Genre { GenreId = 6, GenreName = "Action" },
                new Genre { GenreId = 7, GenreName = "Adventure" },
                new Genre { GenreId = 8, GenreName = "Comedy" },
                new Genre { GenreId = 9, GenreName = "Sport" },
                new Genre { GenreId = 10, GenreName = "Thriller" }
            };

            modelBuilder.Entity<User>().Property(p => p.UserId).IsRequired();
            modelBuilder.Entity<User>().HasKey(p => p.UserId);
            modelBuilder.Entity<User>().Property(p => p.UserName).HasMaxLength(10).IsRequired();
            modelBuilder.Entity<User>().HasData(
                new { UserId = usersGuid[0], UserName = "user0" }, new { UserId = usersGuid[1], UserName = "user1" },
                new { UserId = usersGuid[2], UserName = "user2" }, new { UserId = usersGuid[3], UserName = "user3" },
                new { UserId = usersGuid[4], UserName = "user4" }, new { UserId = usersGuid[5], UserName = "user5" },
                new { UserId = usersGuid[6], UserName = "user6" }, new { UserId = usersGuid[7], UserName = "user7" },
                new { UserId = usersGuid[8], UserName = "user0" }, new { UserId = usersGuid[9], UserName = "user1" },
                new { UserId = usersGuid[10], UserName = "user2" }, new { UserId = usersGuid[11], UserName = "user3" },
                new { UserId = usersGuid[12], UserName = "user4" }, new { UserId = usersGuid[13], UserName = "user5" },
                new { UserId = usersGuid[14], UserName = "user6" }, new { UserId = usersGuid[15], UserName = "user7" },
                new { UserId = usersGuid[16], UserName = "user6" }, new { UserId = usersGuid[17], UserName = "user7" },
                new { UserId = usersGuid[18], UserName = "user6" }, new { UserId = usersGuid[19], UserName = "user7" }
            );
            modelBuilder.Entity<Genre>().Property(p => p.GenreId).IsRequired();
            modelBuilder.Entity<Genre>().HasKey(p => p.GenreId);
            modelBuilder.Entity<Genre>().Property(p => p.GenreName).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Genre>().HasData(genres);

            modelBuilder.Entity<Movie>().Property(p => p.MovieId).IsRequired();
            modelBuilder.Entity<Movie>().HasKey(p => p.MovieId);
            modelBuilder.Entity<Movie>().Property(p => p.Title).HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Movie>().HasIndex(p => p.Title);
            modelBuilder.Entity<Movie>().HasMany<Genre>();
            modelBuilder.Entity<Movie>().HasMany<Rating>();
            modelBuilder.Entity<Movie>().HasData(
            new { MovieId = moviesGuid[0], Genre = new List<Genre> { genres[1], genres[6], genres[7]}, RunningTimeInMinutes = 135,
                  Title = "The hunt for a red october", YearOfRelease = 1990 },
            new { MovieId = moviesGuid[1], Genres = new List<Genre> { genres[2], genres[4], genres[5] }, RunningTimeInMinutes = 120,
                  Title = "Manhunter", YearOfRelease = 1986 },
            new { MovieId = moviesGuid[2], Genre = new List<Genre> { genres[2], genres[8], genres[9] }, RunningTimeInMinutes = 117,
                  Title = "The Big Lebowski", YearOfRelease = 1998 },
            new { MovieId = moviesGuid[3], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[4], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[5], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[6], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[7], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[8], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[9], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "", YearOfRelease = 1980 }
            );

            modelBuilder.Entity<Rating>().Property(p => p.RatingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Rating>().Property(p => p.RatingValue).IsRequired();
            modelBuilder.Entity<Rating>().HasOne<Movie>();
            modelBuilder.Entity<Rating>().HasOne<User>();

        }
    }
}
