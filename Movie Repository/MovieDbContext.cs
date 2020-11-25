using Microsoft.EntityFrameworkCore;
using Movie_Repository.Entities;

namespace Movie_Repository
{
    public class MovieDbContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }

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
            modelBuilder.Entity<Movie>().Property(p => p.MovieId).ValueGeneratedOnAdd().;
            modelBuilder.Entity<Movie>().Property(p => p.Title).HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Movie>().Property(p => p.Genre).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Movie>().HasIndex(p => p.Title);
            modelBuilder.Entity<Movie>().HasIndex(p => p.Genre);
            modelBuilder.Entity<Movie>().HasMany<MovieRating>();

            modelBuilder.Entity<MovieRating>().Property(p => p.MovieRatingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<MovieRating>().Property(p => p.Rating).IsRequired();
            modelBuilder.Entity<MovieRating>().HasOne<Movie>();

        }
    }
}
