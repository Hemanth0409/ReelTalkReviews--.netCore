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
    public class FilmIndustryMembersController : ControllerBase
    {
        private readonly ReelTalkReviewsContext _context;

        public FilmIndustryMembersController(ReelTalkReviewsContext context)
        {
            _context = context;
        }

        // GET: api/FilmIndustryMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmIndustryMember>>> GetFilmIndustryMembers()
        {
          if (_context.FilmIndustryMembers == null)
          {
              return NotFound();
          }
            return await _context.FilmIndustryMembers.ToListAsync();
        }

        // GET: api/FilmIndustryMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmIndustryMember>> GetFilmIndustryMember(int id)
        {
          if (_context.FilmIndustryMembers == null)
          {
              return NotFound();
          }
            var filmIndustryMember = await _context.FilmIndustryMembers.FindAsync(id);

            if (filmIndustryMember == null)
            {
                return NotFound();
            }

            return filmIndustryMember;
        }

        // PUT: api/FilmIndustryMembers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmIndustryMember(int id, FilmIndustryMember filmIndustryMember)
        {
            if (id != filmIndustryMember.MemberId)
            {
                return BadRequest();
            }

            _context.Entry(filmIndustryMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmIndustryMemberExists(id))
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

        // POST: api/FilmIndustryMembers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FilmIndustryMember>> PostFilmIndustryMember(FilmIndustryMember filmIndustryMember)
        {
          if (_context.FilmIndustryMembers == null)
          {
              return Problem("Entity set 'ReelTalkReviewsContext.FilmIndustryMembers'  is null.");
          }
            _context.FilmIndustryMembers.Add(filmIndustryMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilmIndustryMember", new { id = filmIndustryMember.MemberId }, filmIndustryMember);
        }

        // DELETE: api/FilmIndustryMembers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilmIndustryMember(int id)
        {
            if (_context.FilmIndustryMembers == null)
            {
                return NotFound();
            }
            var filmIndustryMember = await _context.FilmIndustryMembers.FindAsync(id);
            if (filmIndustryMember == null)
            {
                return NotFound();
            }

            _context.FilmIndustryMembers.Remove(filmIndustryMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmIndustryMemberExists(int id)
        {
            return (_context.FilmIndustryMembers?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
