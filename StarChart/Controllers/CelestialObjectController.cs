namespace StarChart.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using StarChart.Data;

    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext db)
        {
            _context = db;
        }
    }
}
