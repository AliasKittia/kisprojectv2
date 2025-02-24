using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Halak.Data;
using Halak.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Halak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TavakController : ControllerBase
    {
        private readonly HalakDbContext _context;

        public TavakController(HalakDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TavakModel>>> GetTavak()
        {
            return await _context.Tavak.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TavakModel>> GetTavak(int id)
        {
            var tavak = await _context.Tavak.FindAsync(id);

            if (tavak == null)
            {
                return NotFound();
            }

            return tavak;
        }

        [HttpPost]
        public async Task<ActionResult<TavakModel>> PostTavak(TavakModel tavak)
        {
            _context.Tavak.Add(tavak);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTavak), new { id = tavak.id }, tavak);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTavak(int id, TavakModel tavak)
        {
            if (id != tavak.id)
            {
                return BadRequest();
            }

            _context.Entry(tavak).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTavak(int id)
        {
            var tavak = await _context.Tavak.FindAsync(id);
            if (tavak == null)
            {
                return NotFound();
            }

            _context.Tavak.Remove(tavak);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}