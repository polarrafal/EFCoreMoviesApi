using Microsoft.EntityFrameworkCore;
using WebApi.Enums;

namespace WebApi.Models.Entities
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public CinemaHallType CinemaHallType { get; set; }
        public decimal Cost { get; set; }

        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        public HashSet<Movie> Movies { get; set; }

        public static void SetupDbModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CinemaHall>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Cost).HasPrecision(9, 2);

                entity.HasOne(p => p.Cinema)
                     .WithMany(p => p.CinemaHalls)
                     .HasForeignKey(p => p.CinemaId);

                entity.Property(p => p.CinemaHallType).HasDefaultValue(CinemaHallType.TwoDimensions);
            });
        }
    }
}
