#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarsWeatherApi.Contexts;
using MarsWeatherApi.Models;

namespace MarsWeatherApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET")]
    [Route("api/[controller]")]
    [ApiController]
    public class PressureController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PressureController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Pressure
        [HttpGet]
        public async Task<IEnumerable<object>> GetAllPressures()
        {
            return await _context
                .Pressures
                .Select(c => new
                {
                    c.Id,
                    c.Average,
                    c.Minimum,
                    c.Maximum,
                    c.SolId
                }).ToListAsync();
        }

        // GET: api/Pressure/5
        [HttpGet("{id}")]        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Task<IEnumerable<object>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPressureById(int id)
        {
            var pressuretry = _context.Pressures.Find(id);
            if (pressuretry == null)
            {
                return NotFound();
            }

            var pressurefound = _context
                .Pressures.Where(p => p.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.Average,
                    c.Minimum,
                    c.Maximum,
                    c.SolId
                }).ToList();

            return Ok(pressurefound);
        }

        // PUT: api/Pressure/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [DisableCors]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPressure(int id, Pressure pressure)
        {
            if (id != pressure.Id)
            {
                return BadRequest();
            }

            _context.Entry(pressure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PressureExists(id)) 
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

        // POST: api/Pressure
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [DisableCors]
        [HttpPost]
        public async Task<ActionResult<Pressure>> PostPressure(Pressure pressure)
        {
            _context.Pressures.Add(pressure);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPressureById), new { id = pressure.Id }, pressure);
        }

        // DELETE: api/Pressure/5
        [DisableCors]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePressure(int id)
        {
            var pressure = await _context.Pressures.FindAsync(id);
            if (pressure == null)
            {
                return NotFound();
            }

            _context.Pressures.Remove(pressure);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PressureExists(int id)
        {
            return _context.Pressures.Any(e => e.Id == id);
        }
    }
}
