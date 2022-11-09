using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.Entities
{
    public class CinemaOffer
    {
        public int Id { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public decimal DiscountPercentage { get; set; }

        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        public static void SetupDbModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CinemaOffer>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.DiscountPercentage).HasPrecision(5, 2);
                entity.HasOne(p => p.Cinema)
                    .WithOne(p => p.CinemaOffer)
                    .HasForeignKey<CinemaOffer>(p => p.CinemaId);
            });
        }
    }
}
