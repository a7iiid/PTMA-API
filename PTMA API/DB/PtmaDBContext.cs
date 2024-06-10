using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PTMA_API.Model.user;

namespace PTMA.DB

{
    public class PtmaDBContext : IdentityDbContext<UserModel>
    {
        public PtmaDBContext(DbContextOptions<PtmaDBContext> options) : base(options)
        {
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure the base method is called to set up Identity tables

          
        }
    }
}
