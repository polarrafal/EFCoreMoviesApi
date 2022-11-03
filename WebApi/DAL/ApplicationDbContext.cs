using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;
using WebApi.Models.Entities.Seeding;

namespace WebApi.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
            configurationBuilder.Properties<string>().HaveMaxLength(150);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureModels(modelBuilder);
            TestSeeding.Seed(modelBuilder);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CinemaOffer> CinemaOffers { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }

        private static void ConfigureModels(ModelBuilder modelBuilder)
        {
            Genre.SetupDbModel(modelBuilder);
            Actor.SetupDbModel(modelBuilder);
            Cinema.SetupDbModel(modelBuilder);
            Movie.SetupDbModel(modelBuilder);
            CinemaOffer.SetupDbModel(modelBuilder);
            CinemaHall.SetupDbModel(modelBuilder);
            MovieActor.SetupDbModel(modelBuilder);
        }
    }
}
