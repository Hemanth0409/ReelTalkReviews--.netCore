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
    public class MovieRatingsController : ControllerBase
    {
        private readonly ReelTalkReviewsContext _context;

        public MovieRatingsController(ReelTalkReviewsContext context)
        {
            _context = context;
        }

        // GET: api/MovieRatings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieRating>>> GetMovieRatings()
        {
          if (_context.MovieRatings == null)
          {
              return NotFound();
          }
            return await _context.MovieRatings.ToListAsync();
        }

        // GET: api/MovieRatings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieRating>> GetMovieRating(int id)
        {
          if (_context.MovieRatings == null)
          {
              return NotFound();
          }
            var movieRating = await _context.MovieRatings.FindAsync(id);

            if (movieRating == null)
            {
                return NotFound();
            }

            return movieRating;
        }

        // PUT: api/MovieRatings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieRating(int id, MovieRating movieRating)
        {
            if (id != movieRating.MovieRatingId)
            {
                return BadRequest();
            }

            _context.Entry(movieRating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieRatingExists(id))
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

        // POST: api/MovieRatings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieRating>> PostMovieRating(MovieRating movieRating)
        {
            _context.MovieRatings.Add(movieRating);
            await _context.SaveChangesAsync();  
            return CreatedAtAction("GetMovieRating", new { id = movieRating.MovieRatingId }, movieRating);
        }

        // DELETE: api/MovieRatings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieRating(int id)
        {
            if (_context.MovieRatings == null)
            {
                return NotFound();
            }
            var movieRating = await _context.MovieRatings.FindAsync(id);
            if (movieRating == null)
            {
                return NotFound();
            }

            _context.MovieRatings.Remove(movieRating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieRatingExists(int id)
        {
            return (_context.MovieRatings?.Any(e => e.MovieRatingId == id)).GetValueOrDefault();
        }
    }
}
