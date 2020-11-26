using Microsoft.EntityFrameworkCore;
using Movie_Repository.Entities;
using System;
using System.Collections.Generic;

namespace Movie_Repository
{
    public class MovieDbContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
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
            modelBuilder.Entity<Movie>().Property(p => p.MovieId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Movie>().Property(p => p.Title).HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Movie>().Property(p => p.Genre).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Movie>().HasIndex(p => p.Title);
            modelBuilder.Entity<Movie>().HasIndex(p => p.Genre);
            modelBuilder.Entity<Movie>().HasMany<MovieRating>();
            modelBuilder.Entity<Movie>().HasData(
            new { MovieId = moviesGuid[0], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "The hunt for a red october", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[1], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "The hunt for a red october", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[2], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "The hunt for a red october", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[3], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "The hunt for a red october", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[4], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "The hunt for a red october", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[5], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "The hunt for a red october", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[6], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "The hunt for a red october", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[7], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "The hunt for a red october", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[8], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "The hunt for a red october", YearOfRelease = 1980 },
            new { MovieId = moviesGuid[9], Genre = "Drama", RunningTimeInMinutes = 120,
                  Title = "The hunt for a red october", YearOfRelease = 1980 }
            );

            modelBuilder.Entity<MovieRating>().Property(p => p.MovieRatingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<MovieRating>().Property(p => p.Rating).IsRequired();
            modelBuilder.Entity<MovieRating>().HasOne<Movie>();
            modelBuilder.Entity<MovieRating>().HasOne<User>();

            modelBuilder.Entity<User>().Property(p => p.UserId).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(p => p.UserName).HasMaxLength(10).IsRequired();
        }
    }
}
