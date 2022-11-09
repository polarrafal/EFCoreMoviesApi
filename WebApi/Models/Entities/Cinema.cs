using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace WebApi.Models.Entities
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
        public virtual CinemaOffer CinemaOffer { get; set; }
        public virtual HashSet<CinemaHall> CinemaHalls { get; set; }

        public static void SetupDbModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
            });
        }
    }
}
