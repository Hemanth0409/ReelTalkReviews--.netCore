using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReelTalkReviews.Models;

namespace ReelTalkReviews.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilmCertificationsController : ControllerBase
    {
        private readonly ReelTalkReviewsContext _context;

        public FilmCertificationsController(ReelTalkReviewsContext context)
        {
            _context = context;
        }

        // GET: api/FilmCertifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmCertification>>> GetFilmCertifications()
        {
          if (_context.FilmCertifications == null)
          {
              return NotFound();
          }
            return await _context.FilmCertifications.ToListAsync();
        }

        // GET: api/FilmCertifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmCertification>> GetFilmCertification(int id)
        {
          if (_context.FilmCertifications == null)
          {
              return NotFound();
          }
            var filmCertification = await _context.FilmCertifications.FindAsync(id);

            if (filmCertification == null)
            {
                return NotFound();
            }

            return filmCertification;
        }

        // PUT: api/FilmCertifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmCertification(int id, FilmCertification filmCertification)
        {
            if (id != filmCertification.FilmCertificationId)
            {
                return BadRequest();
            }

            _context.Entry(filmCertification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmCertificationExists(id))
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

        // POST: api/FilmCertifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FilmCertification>> PostFilmCertification(FilmCertification filmCertification)
        {
          if (_context.FilmCertifications == null)
          {
              return Problem("Entity set 'ReelTalkReviewsContext.FilmCertifications'  is null.");
          }
            _context.FilmCertifications.Add(filmCertification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilmCertification", new { id = filmCertification.FilmCertificationId }, filmCertification);
        }

        // DELETE: api/FilmCertifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilmCertification(int id)
        {
            if (_context.FilmCertifications == null)
            {
                return NotFound();
            }
            var filmCertification = await _context.FilmCertifications.FindAsync(id);
            if (filmCertification == null)
            {
                return NotFound();
            }

            _context.FilmCertifications.Remove(filmCertification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmCertificationExists(int id)
        {
            return (_context.FilmCertifications?.Any(e => e.FilmCertificationId == id)).GetValueOrDefault();
        }
    }
}
