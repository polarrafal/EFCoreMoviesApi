using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.Entities
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int ActorId { get; set; }
        public Actor Actor { get; set; }

        public string Character { get; set; }
        public int Order { get; set; }

        public static void SetupDbModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>(entity =>
            {
                entity.HasKey(p => new { p.MovieId, p.ActorId });
            });
        }
    }
}
