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
        // VANHA
        /*public async Task<ActionResult<IEnumerable<Pressure>>> GetAllPressures()
        {
            Console.WriteLine("GetAllPressures-metodia kutsuttu");
            System.Diagnostics.Debug.WriteLine("GetAllPressures-metodia kutsuttu");
            return await _context.Pressures.ToListAsync();
        }*/
        // UUSI
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
        // VANHA
        /*public async Task<ActionResult<Pressure>> GetPressureById(int id)
        {
            var pressure = await _context.Pressures.FindAsync(id);

            if (pressure == null)
            {
                return NotFound();
            }

            return pressure;
        }*/
        // UUSI
        public async Task<IEnumerable<object>> GetPressureById(int id)
        {
            return await _context
                .Pressures.Where(s => s.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.Average,
                    c.Minimum,
                    c.Maximum,
                    c.SolId
                }).ToListAsync();
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

            return CreatedAtAction(nameof(GetPressureById), new { id = pressure.Id }, pressure);
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
