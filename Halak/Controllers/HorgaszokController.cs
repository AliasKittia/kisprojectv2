using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Halak.Data;
using Halak.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Halak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorgaszokController : ControllerBase
    {
        private readonly HalakDbContext _context;

        public HorgaszokController(HalakDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorgaszokModel>>> GetHorgaszok()
        {
            return await _context.Horgaszok.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HorgaszokModel>> GetHorgaszok(int id)
        {
            var horgaszok = await _context.Horgaszok.FindAsync(id);

            if (horgaszok == null)
            {
                return NotFound();
            }

            return horgaszok;
        }

        [HttpPost]
        public async Task<ActionResult<HorgaszokModel>> PostHorgaszok(HorgaszokModel horgaszok)
        {
            _context.Horgaszok.Add(horgaszok);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHorgaszok), new { id = horgaszok.id }, horgaszok);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorgaszok(int id, HorgaszokModel horgaszok)
        {
            if (id != horgaszok.id)
            {
                return BadRequest();
            }

            _context.Entry(horgaszok).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorgaszok(int id)
        {
            var horgaszok = await _context.Horgaszok.FindAsync(id);
            if (horgaszok == null)
            {
                return NotFound();
            }

            _context.Horgaszok.Remove(horgaszok);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}