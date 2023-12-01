using JobOpeningsAPI.Data;
using JobOpeningsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobOpeningsAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationsController : ControllerBase
    {
        private readonly JobContext _context;

        public LocationsController(JobContext context)
        {
            _context = context;
        }

        /// <summary>
        /// this api returns list of location
        /// </summary>
        /// <returns></returns>
        // GET: api/v1/Locations
       // [Authorize]
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Locations>>> GetLocation()
        {
            return await _context.Location.ToListAsync();
        }

        /// <summary>
        /// this api creates location
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        // POST : api/v1/Locations
       // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Locations>> PostLocations(Locations req)
        {
            _context.Location.Add(req);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LocationExists(req.LocationId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        /// <summary>
        /// this api modifies location data based on location id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        //PUT: api/v1/Locations/5
       // [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationgs(int id, Locations req)
        {
            if (id != req.LocationId)
            {
                return BadRequest();
            }

            _context.Entry(req).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.LocationId == id);
        }

    }
}
