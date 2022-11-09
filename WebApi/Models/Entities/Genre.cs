using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual HashSet<Movie> Movies { get; set; }

        public static void SetupDbModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
            });
        }
    }
}
