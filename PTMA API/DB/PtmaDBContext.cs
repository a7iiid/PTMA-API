using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PTMA_API.Model;
using PTMA_API.Model.user;

namespace PTMA.DB

{
    public class PtmaDBContext : IdentityDbContext<UserModel>
    {
        public PtmaDBContext(DbContextOptions<PtmaDBContext> options) : base(options)
        {
        }
        public DbSet<BusModel> BusModels { get; set; }
        public DbSet<Station>Stations { get; set; }
        public DbSet<History> History { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Station>()
               .ComplexProperty(s => s.Location, location =>
               {
                   location.Property(l => l.Latitude).HasColumnName("latitude");
                   location.Property(l => l.Longitude).HasColumnName("longitude");
               });
            modelBuilder.Entity<BusModel>()
                .HasKey(b => b.Id);

            // Configure owned type LatLong
            modelBuilder.Entity<BusModel>()
                .ComplexProperty(b => b.busLocation, location =>
                {
                    location.Property(l => l.Latitude).HasColumnName("latitude");
                    location.Property(l => l.Longitude).HasColumnName("longitude");
                });

            modelBuilder.Entity<BusModel>()
                .HasOne(b => b.startStation)
                .WithMany(); // Configure relationship as needed

            modelBuilder.Entity<BusModel>()
                .HasOne(b => b.endStation)
                .WithMany();
           


            base.OnModelCreating(modelBuilder); // Ensure the base method is called to set up Identity tables
        }
    }
}
