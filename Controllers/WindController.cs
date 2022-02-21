#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarsWeatherApi.Models;
using MarsWeatherApi.Contexts;

namespace MarsWeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WindController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WindController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Wind
        [HttpGet]
        // VANHA
        /*public async Task<ActionResult<IEnumerable<Wind>>> GetWinds()
        {
            return await _context.Winds.ToListAsync();
        }*/
        // UUSI
        public async Task<IEnumerable<object>> GetAllWinds()
        {
            return await _context
                .Winds
                .Select(c => new
                {
                    c.Id,
                    c.Average,
                    c.Minimum,
                    c.Maximum,
                    c.MostCommonDirection,
                    c.SolId
                }).ToListAsync();
        }

        // GET: api/Wind/5
        [HttpGet("{id}")]
        // VANHA
        /*public async Task<ActionResult<Wind>> GetWindById(int id)
        {
            var wind = await _context.Winds.FindAsync(id);

            if (wind == null)
            {
                return NotFound();
            }

            return wind;
        }*/
        // VANHA
        /*public async Task<IEnumerable<object>> GetWindById(int id)
        {
            return await _context
                .Winds.Where(s => s.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.Average,
                    c.Minimum,
                    c.Maximum,
                    c.MostCommonDirection,
                    c.SolId
                }).ToListAsync();
        }*/
        // UUSI
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Task<IEnumerable<object>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetWindById(int id)
        {
            var windtry = _context.Winds.Find(id);
            if (windtry == null)
            {
                return NotFound();
            }

            var windfound = _context
                .Winds.Where(w => w.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.Average,
                    c.Minimum,
                    c.Maximum,
                    c.MostCommonDirection,
                    c.SolId
                }).ToList();

            return Ok(windfound);
        }

        // PUT: api/Wind/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWind(int id, Wind wind)
        {
            if (id != wind.Id)
            {
                return BadRequest();
            }

            _context.Entry(wind).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WindExists(id))
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

        // POST: api/Wind
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wind>> PostWind(Wind wind)
        {
            _context.Winds.Add(wind);
            await _context.SaveChangesAsync();
            
            //return CreatedAtAction("GetSol", new { id = sol.Id }, sol);
            return CreatedAtAction(nameof(GetWindById), new { id = wind.Id }, wind);
        }

        // DELETE: api/Wind/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWind(int id)
        {
            var wind = await _context.Winds.FindAsync(id);
            if (wind == null)
            {
                return NotFound();
            }

            _context.Winds.Remove(wind);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WindExists(int id)
        {
            return _context.Winds.Any(e => e.Id == id);
        }
    }
}
