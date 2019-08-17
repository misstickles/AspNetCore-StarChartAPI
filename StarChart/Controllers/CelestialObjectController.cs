namespace StarChart.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using StarChart.Data;
    using StarChart.Models;

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

        [HttpPost]
        public IActionResult Create([FromBody] CelestialObject obj)
        {
            _context.CelestialObjects.Add(obj);
            _context.SaveChanges();

            return CreatedAtRoute("GetById", new { id = obj.Id }, obj);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject obj)
        {
            var itm = _context.CelestialObjects.Find(id);

            if (itm == null) return NotFound();

            itm.Name = obj.Name;
            itm.OrbitalPeriod = obj.OrbitalPeriod;
            itm.OrbitedObjectId = obj.OrbitedObjectId;

            _context.CelestialObjects.Update(itm);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var itm = _context.CelestialObjects.Find(id);

            if (itm == null) return NotFound();

            itm.Name = name;

            _context.CelestialObjects.Update(itm);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var list = _context.CelestialObjects.Where(c => c.Id == id || c.OrbitedObjectId == id).ToList();

            if (!list.Any()) return NotFound();

            _context.CelestialObjects.RemoveRange(list);
            _context.SaveChanges();

            return NoContent();


        }
    }
}
