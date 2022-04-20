#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web.Http.Cors;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarsWeatherApi.Contexts;
using MarsWeatherApi.Models;

namespace MarsWeatherApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET")]
    [Route("api/[controller]")]
    [ApiController]
    public class SolController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ILogger<SolController> _logger;

        public SolController(ApplicationDbContext context, ILogger<SolController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Sol
        [HttpGet]
        public async Task<IEnumerable<object>> GetAllSols()
        {
            return await _context
                .Sols
                .Select(c => new
                {
                    c.Id,
                    c.Start,
                    c.End,
                    c.Season,
                    c.SolNumber,
                    c.Wind,
                    c.Pressure,
                    c.Temperature
                }).ToListAsync();
        }

        // GET: api/Sol/5
        [HttpGet("{id}")] 
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Task<IEnumerable<object>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSolById(int id)
        {
            var soltry = _context.Sols.Find(id);
            if (soltry == null)
            {
                return NotFound();
            }

            var solfound = _context
                .Sols.Where(s => s.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.Start,
                    c.End,
                    c.Season,
                    c.SolNumber,
                    c.Wind,
                    c.Pressure,
                    c.Temperature
                }).ToList();

            return Ok(solfound);
        }
        
        // GET: api/sol/solNumber/200
        [HttpGet("solNumber/{solNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Task<IEnumerable<object>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSolBySolNumber(int solNumber)
        {

            var solfound = _context
                .Sols.Where(s => s.SolNumber == solNumber)
                .Select(c => new
                {
                    c.Id,
                    c.Start,
                    c.End,
                    c.Season,
                    c.SolNumber,
                    c.Wind,
                    c.Pressure,
                    c.Temperature
                }).ToList();

            if (solfound.Count != 0) {
                return Ok(solfound);
            }
            else {
                return NotFound();
            }            
        }

        // GET: api/sol/date?start=xxx&end=xxx where "xxx" are DateTimes
        [HttpGet("date")]
        public IActionResult GetSolsByDate([Required] DateTime start, [Required] DateTime end)
        {     
            try
            {
                var solsfound = _context
                .Sols.Where(s => (DateTime.Compare(s.Start, start) >=0
                                && DateTime.Compare(s.Start, end) <0)
                            || (DateTime.Compare(s.End, end) <=0 
                                && DateTime.Compare(s.End, start) >0))
                .Select(c => new
                {
                    c.Id,
                    c.Start,
                    c.End,
                    c.Season,
                    c.SolNumber,
                    c.Wind,
                    c.Pressure,
                    c.Temperature
                }).ToList();

                return Ok(solsfound);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Sol/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [DisableCors]
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

            return Ok(sol);
        }

        // POST: api/Sol
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [DisableCors]
        [HttpPost]
        public async Task<ActionResult<Sol>> PostSol(Sol sol)
        {
            _context.Sols.Add(sol);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetSol", new { id = sol.Id }, sol);
            return CreatedAtAction(nameof(GetSolById), new { id = sol.Id }, sol);
            //return Ok(await _context.Sols.ToListAsync()); // <-- tämä vaikuttaa palauttavan, mutta SSL-sertifikaattiongelma oli
        }

        // DELETE: api/Sol/5
        [DisableCors]
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
