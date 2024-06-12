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
            modelBuilder.Entity<BusModel>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<BusModel>()
                .OwnsOne(b => b.busLocation);

            modelBuilder.Entity<BusModel>()
                .HasOne(b => b.StartStation)
                .WithMany()
                .HasForeignKey(b => b.StartStationId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<BusModel>()
                .HasOne(b => b.EndStation)
                .WithMany()
                .HasForeignKey(b => b.EndStationId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Station>()
                .OwnsOne(s => s.Location);
            modelBuilder.Entity<Station>()
                .HasKey(s => s.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
