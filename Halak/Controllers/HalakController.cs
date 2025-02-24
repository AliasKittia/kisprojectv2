using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Halak.Data;
using Halak.Models;
using Halak.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Halak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HalakController : ControllerBase
    {
        private readonly HalakDbContext _context;

        public HalakController(HalakDbContext context)
        {
            _context = context;
        }

        [HttpGet("byTavakNev/{tavakNev}")]
        public async Task<ActionResult<IEnumerable<HalakWithTavakDto>>> GetHalakByTavakNev(string tavakNev)
        {
            var halak = await _context.Halak
                .Where(h => _context.Tavak.Any(t => t.id == h.to_id && t.nev == tavakNev))
                .Select(h => new HalakWithTavakDto
                {
                    Id = h.id,
                    Nev = h.nev,
                    Faj = h.faj,
                    TavakNev = _context.Tavak.First(t => t.id == h.to_id).nev
                })
                .ToListAsync();

            if (halak == null || !halak.Any())
            {
                return NotFound();
            }

            return halak;
        }

        [HttpGet("legnagyobb")]
        public async Task<ActionResult<LegnagyobbHalDto>> GetLegnagyobbHal()
        {
            var legnagyobbHal = await _context.Halak
                .OrderByDescending(h => h.kep.Length) // Assuming `kep` byte array length represents size
                .Select(h => new LegnagyobbHalDto
                {
                    Nev = h.nev,
                    MeretCm = h.kep.Length // Replace with actual size property if available
                })
                .FirstOrDefaultAsync();

            if (legnagyobbHal == null)
            {
                return NotFound();
            }

            return legnagyobbHal;
        }

        [HttpPost]
        public async Task<ActionResult<HalakModel>> PostHalak(HalakModel halak)
        {
            _context.Halak.Add(halak);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHalak), new { id = halak.id }, halak);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHalak(int id, HalakModel halak)
        {
            if (id != halak.id)
            {
                return BadRequest();
            }

            _context.Entry(halak).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHalak(int id)
        {
            var halak = await _context.Halak.FindAsync(id);
            if (halak == null)
            {
                return NotFound();
            }

            _context.Halak.Remove(halak);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Placeholder method to resolve the error
        [HttpGet("{id}")]
        public async Task<ActionResult<HalakModel>> GetHalak(int id)
        {
            var halak = await _context.Halak.FindAsync(id);

            if (halak == null)
            {
                return NotFound();
            }

            return halak;
        }
    }
}