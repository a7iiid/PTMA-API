using Microsoft.AspNetCore.Mvc;
using PTMA_API.Model;

namespace PTMA_API.Controllers
{
    public class StationController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Station>>> Get()
        {
            return Unauthorized();

        }
    }
}
