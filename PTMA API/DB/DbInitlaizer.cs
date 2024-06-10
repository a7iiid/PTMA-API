using PTMA.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace PTMA.Models
{
    public static class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app)
        {
            PtmaDBContext context = app.ApplicationServices.CreateScope()
               .ServiceProvider.GetRequiredService<PtmaDBContext>();
           
                
            
        }
    }
}
