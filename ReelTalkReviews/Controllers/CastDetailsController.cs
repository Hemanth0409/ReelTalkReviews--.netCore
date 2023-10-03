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
    [Route("api/[controller]")]
    [ApiController]
    public class CastDetailsController : ControllerBase
    {
        private readonly ReelTalkReviewsContext _context;

        public CastDetailsController(ReelTalkReviewsContext context)
        {
            _context = context;
        }

        // GET: api/CastDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CastDetail>>> GetCastDetails()
        {
          if (_context.CastDetails == null)
          {
              return NotFound();
          }
            return await _context.CastDetails.ToListAsync();
        }

        // GET: api/CastDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CastDetail>> GetCastDetail(int id)
        {
          if (_context.CastDetails == null)
          {
              return NotFound();
          }
            var castDetail = await _context.CastDetails.FindAsync(id);

            if (castDetail == null)
            {
                return NotFound();
            }

            return castDetail;
        }

        // PUT: api/CastDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCastDetail(int id, CastDetail castDetail)
        {
            if (id != castDetail.CastId)
            {
                return BadRequest();
            }

            _context.Entry(castDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CastDetailExists(id))
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

        // POST: api/CastDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CastDetail>> PostCastDetail(CastDetail castDetail)
        {
          if (_context.CastDetails == null)
          {
              return Problem("Entity set 'ReelTalkReviewsContext.CastDetails'  is null.");
          }
            _context.CastDetails.Add(castDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCastDetail", new { id = castDetail.CastId }, castDetail);
        }

        // DELETE: api/CastDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCastDetail(int id)
        {
            if (_context.CastDetails == null)
            {
                return NotFound();
            }
            var castDetail = await _context.CastDetails.FindAsync(id);
            if (castDetail == null)
            {
                return NotFound();
            }

            _context.CastDetails.Remove(castDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CastDetailExists(int id)
        {
            return (_context.CastDetails?.Any(e => e.CastId == id)).GetValueOrDefault();
        }
    }
}
