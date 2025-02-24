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
    public class FogasokController : ControllerBase
    {
        private readonly HalakDbContext _context;

        public FogasokController(HalakDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FogasokModel>>> GetFogasok()
        {
            return await _context.Fogasok.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FogasokModel>> GetFogasok(int id)
        {
            var fogasok = await _context.Fogasok.FindAsync(id);

            if (fogasok == null)
            {
                return NotFound();
            }

            return fogasok;
        }

        [HttpPost]
        public async Task<ActionResult<FogasokModel>> PostFogasok(FogasokModel fogasok)
        {
            _context.Fogasok.Add(fogasok);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFogasok), new { id = fogasok.id }, fogasok);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFogasok(int id, FogasokModel fogasok)
        {
            if (id != fogasok.id)
            {
                return BadRequest();
            }

            _context.Entry(fogasok).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFogasok(int id)
        {
            var fogasok = await _context.Fogasok.FindAsync(id);
            if (fogasok == null)
            {
                return NotFound();
            }

            _context.Fogasok.Remove(fogasok);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("horgaszok")]
        public async Task<ActionResult<IEnumerable<HorgaszokFogasokDto>>> GetHorgaszokFogasok()
        {
            var fogasok = await _context.Fogasok
                .Select(f => new HorgaszokFogasokDto
                {
                    HorgaszNev = _context.Horgaszok.First(h => h.id == f.horgasz_id).nev,
                    HalNev = _context.Halak.First(h => h.id == f.hal_id).nev,
                    Datum = f.datum
                })
                .ToListAsync();

            if (fogasok == null || !fogasok.Any())
            {
                return NotFound();
            }

            return fogasok;
        }
    }
}