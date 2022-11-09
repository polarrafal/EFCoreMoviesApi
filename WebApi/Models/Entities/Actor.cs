using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public virtual HashSet<MovieActor> MovieActors { get; set; }

        public static void SetupDbModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Biography).HasColumnType("nvarchar(max)");
            });
        }
    }
}
