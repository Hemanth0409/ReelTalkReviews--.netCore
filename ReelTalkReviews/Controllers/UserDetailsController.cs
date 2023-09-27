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
    public class UserDetailsController : ControllerBase
    {
        private readonly ReelTalkReviewsContext _context;

        public UserDetailsController(ReelTalkReviewsContext context)
        {
            _context = context;
        }

        // GET: api/UserDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetUserDetails()
        {
            if (_context.UserDetails == null)
            {
                return NotFound();
            }
            return await _context.UserDetails.ToListAsync();
        }

        // GET: api/UserDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetail>> GetUserDetail(int id)
        {

            var userDetail = await _context.UserDetails.FindAsync(id);

            if (userDetail == null)
            {
                return NotFound();
            }

            return userDetail;
        }

        // PUT: api/UserDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDetail(int id, UserDetail userDetail)
        {
            if (id != userDetail.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailExists(id))
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

        // POST: api/UserDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDetail>> PostUserDetail([FromBody] UserDetail userDetail)
        {
            userDetail.UserName = userDetail.UserName;
            userDetail.Email = userDetail.Email;
            userDetail.Password = userDetail.Password;
            userDetail.IsDeleted = false;
            userDetail.IsLogged = false;
            userDetail.RoleId = 2;
            userDetail.CreatedDate = DateTime.Now;
            userDetail.ModifiedDate = null;
            userDetail.Bio = userDetail.Bio;
            userDetail.DisplayPic = userDetail.DisplayPic;
            userDetail.LastLoginDate = null;
            //if (imageFile != null && imageFile.Length > 0)
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        imageFile.CopyTo(ms)
            //;
            //        var imageBytes = ms.ToArray();
            //        // Save imageBytes to SQL Server as varbinary
            //    }
            //}
            _context.UserDetails.Add(userDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDetail", new { id = userDetail.UserId }, userDetail);
        }

        // DELETE: api/UserDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDetail(int id)
        {
            if (_context.UserDetails == null)
            {
                return NotFound();
            }
            var userDetail = await _context.UserDetails.FindAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            _context.UserDetails.Remove(userDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDetailExists(int id)
        {
            return (_context.UserDetails?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
