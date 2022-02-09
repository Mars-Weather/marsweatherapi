#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarsWeatherApi.Contexts;
using MarsWeatherApi.Models;

namespace MarsWeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TemperatureController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Temperature
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Temperature>>> GetTemperatures()
        {
            return await _context.Temperatures.ToListAsync();
        }

        // GET: api/Temperature/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Temperature>> GetTemperature(int id)
        {
            var temperature = await _context.Temperatures.FindAsync(id);

            if (temperature == null)
            {
                return NotFound();
            }

            return temperature;
        }

        // PUT: api/Temperature/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemperature(int id, Temperature temperature)
        {
            if (id != temperature.Id)
            {
                return BadRequest();
            }

            _context.Entry(temperature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperatureExists(id))
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

        // POST: api/Temperature
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Temperature>> PostTemperature(Temperature temperature)
        {
            _context.Temperatures.Add(temperature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemperature", new { id = temperature.Id }, temperature);
        }

        // DELETE: api/Temperature/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemperature(int id)
        {
            var temperature = await _context.Temperatures.FindAsync(id);
            if (temperature == null)
            {
                return NotFound();
            }

            _context.Temperatures.Remove(temperature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TemperatureExists(int id)
        {
            return _context.Temperatures.Any(e => e.Id == id);
        }
    }
}
