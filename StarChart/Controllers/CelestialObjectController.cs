namespace StarChart.Controllers
{
    using System.Linq;
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

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var objects = _context.CelestialObjects.Find(id);

            if (objects == null) return NotFound();

            objects.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == id).ToList();

            return Ok(objects);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var objects = _context.CelestialObjects.Where(c => c.Name == name);

            if (!objects.Any()) return NotFound();

            foreach (var obj in objects)
            {
                obj.Satellites = _context.CelestialObjects.Where(c => c.Name == name).ToList();
            }

            return Ok(objects);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var objects = _context.CelestialObjects.ToList();

            if (!objects.Any()) return NotFound();

            foreach (var obj in objects)
            {
                obj.Satellites = _context.CelestialObjects.Where(c => c.Name == obj.Name).ToList();
            }

            return Ok(objects);
        }
    }
}
