#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarsWeatherApi.Models;

namespace MarsWeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolController : ControllerBase
    {
        private readonly SolContext _context;

        public SolController(SolContext context)
        {
            _context = context;
        }

        // GET: api/Sol
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sol>>> GetSols()
        {
            return await _context.Sols.ToListAsync();
        }

        // GET: api/Sol/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sol>> GetSol(int id)
        {
            var sol = await _context.Sols.FindAsync(id);

            if (sol == null)
            {
                return NotFound();
            }

            return sol;
        }

        // PUT: api/Sol/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSol(int id, Sol sol)
        {
            if (id != sol.Id)
            {
                return BadRequest();
            }

            _context.Entry(sol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolExists(id))
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

        // POST: api/Sol
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sol>> PostSol(Sol sol)
        {
            _context.Sols.Add(sol);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSol", new { id = sol.Id }, sol);
        }

        // DELETE: api/Sol/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSol(int id)
        {
            var sol = await _context.Sols.FindAsync(id);
            if (sol == null)
            {
                return NotFound();
            }

            _context.Sols.Remove(sol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SolExists(int id)
        {
            return _context.Sols.Any(e => e.Id == id);
        }
    }
}
