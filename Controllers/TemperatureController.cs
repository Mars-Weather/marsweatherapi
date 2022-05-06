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
    public class TemperatureController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;       
        
        public TemperatureController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Temperature
        [HttpGet]
        public async Task<IEnumerable<object>> GetAllTemperatures()
        {
            return await _context
                .Temperatures
                .Select(c => new
                {
                    c.Id,
                    c.Average,
                    c.Minimum,
                    c.Maximum,
                    c.SolId
                }).ToListAsync();
        }

        // GET: api/Temperature/5
        [HttpGet("{id}")]        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Task<IEnumerable<object>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTemperatureById(int id)
        {
            var temperaturetry = _context.Temperatures.Find(id);
            if (temperaturetry == null)
            {
                return NotFound();
            }

            var temperaturefound = _context
                .Temperatures.Where(t => t.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.Average,
                    c.Minimum,
                    c.Maximum,
                    c.SolId
                }).ToList();

            return Ok(temperaturefound);
        }

        // PUT: api/Temperature/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [DisableCors]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemperature(int id, Temperature temperature, string marsApikey)
        {
            if (marsApikey != _config["marsApikey"])
            {
                return Unauthorized();
            }
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
        [DisableCors]
        [HttpPost]
        public async Task<ActionResult<Temperature>> PostTemperature(Temperature temperature, string marsApikey)
        {
            if (marsApikey != _config["marsApikey"])
            {
                return Unauthorized();
            }
            _context.Temperatures.Add(temperature);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTemperatureById), new { id = temperature.Id }, temperature);
        }

        // DELETE: api/Temperature/5
        [DisableCors]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemperature(int id, string marsApikey)
        {
            if (marsApikey != _config["marsApikey"])
            {
                return Unauthorized();
            }
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
