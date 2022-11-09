using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterUrl { get; set; }
        public HashSet<Genre> Genres { get; set; }
        public HashSet<CinemaHall> CinemaHalls { get; set; }
        public HashSet<MovieActor> MovieActors { get; set; }

        public static void SetupDbModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
               entity.HasKey(p => p.Id);
               entity.Property(p => p.Title).IsRequired().HasMaxLength(250);
               entity.Property(p => p.PosterUrl).HasMaxLength(500).IsUnicode(false);
            });
        }
    }
}
