#nullable disable
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarsWeatherApi.Contexts;
using MarsWeatherApi.Models;

namespace MarsWeatherApi.Controllers
{
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
        /*public async Task<ActionResult<IEnumerable<Pressure>>> GetPressures()
        {
            Console.WriteLine("GetPressure-metodia kutsuttu");
            System.Diagnostics.Debug.WriteLine("GetPressure-metodia kutsuttu");
            return await _context.Pressures.ToListAsync();
        }*/
        public async Task<IEnumerable<object>> GetPressures()
        {
            return await _context
                .Pressures
                .Select(c => new
                {
                    c.Id,
                    c.Average,
                    c.Minimum,
                    c.Maximum,
                    c.Sol,
                    c.SolId
                }).ToListAsync();
        }

        // GET: api/Pressure/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pressure>> GetPressure(int id)
        {
            var pressure = await _context.Pressures.FindAsync(id);

            if (pressure == null)
            {
                return NotFound();
            }

            return pressure;
        }

        // PUT: api/Pressure/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        [HttpPost]
        public async Task<ActionResult<Pressure>> PostPressure(Pressure pressure)
        {
            _context.Pressures.Add(pressure);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPressure), new { id = pressure.Id }, pressure);
        }

        // DELETE: api/Pressure/5
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
