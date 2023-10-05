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
    public class MovieDetailsController : ControllerBase
    {
        private readonly ReelTalkReviewsContext _context;

        public MovieDetailsController(ReelTalkReviewsContext context)
        {
            _context = context;
        }

        // GET: api/MovieDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDetail>>> GetMovieDetails()
        {
            if (_context.MovieDetails == null)
            {
                return NotFound();
            }
            return await _context.MovieDetails.ToListAsync();
        }

        // GET: api/MovieDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDetail>> GetMovieDetail(int id)
        {
            if (_context.MovieDetails == null)
            {
                return NotFound();
            }
            var movieDetail = await _context.MovieDetails.FindAsync(id);

            if (movieDetail == null)
            {
                return NotFound();
            }

            return movieDetail;
        }

        // PUT: api/MovieDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieDetail(int id, MovieDetail movieDetail)
        {
            if (id != movieDetail.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(movieDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieDetailExists(id))
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

        // POST: api/MovieDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieDetail>> PostMovieDetail([FromBody] MovieDetail movieDetail)
        {
            if (await CheckMovieTitle(movieDetail.MovieTitle))
                return BadRequest(new { Message = "UserName Already Exist!" });
            int? FilmCertificationId = (int?)movieDetail.FilmCertificationId;
            movieDetail.MoviePoster = movieDetail.MoviePoster;
            movieDetail.CreateDate= DateTime.Now;
            movieDetail.ModifiedDate= DateTime.Now;
            movieDetail.IsDeleted=false;
            movieDetail.ReleaseDate=movieDetail.ReleaseDate;
            movieDetail.MovieRatingOverall = 0 ;
            movieDetail.FilmCertificationId = FilmCertificationId; 
            _context.MovieDetails.Add(movieDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovieDetail", new { id = movieDetail.MovieId }, movieDetail);
        }
        private async Task<bool> CheckMovieTitle(string MovieTitle)
               => await _context.MovieDetails.AnyAsync(movie => movie.MovieTitle== MovieTitle);

        // DELETE: api/MovieDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieDetail(int id)
        {
            if (_context.MovieDetails == null)
            {
                return NotFound();
            }
            var movieDetail = await _context.MovieDetails.FindAsync(id);
            if (movieDetail == null)
            {
                return NotFound();
            }

            _context.MovieDetails.Remove(movieDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieDetailExists(int id)
        {
            return (_context.MovieDetails?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }
    }
}
