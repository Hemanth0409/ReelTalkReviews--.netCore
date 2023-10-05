using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using ReelTalkReviews.Models;
using System;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using ReelTalkReviews.Models.Dto;
using ReelTalkReviews.Helper;
using ReelTalkReviews.UtilitService;
using NETCore.MailKit;
namespace ReelTalkReviews.Conrollers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly ReelTalkReviewsContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public UserDetailsController(ReelTalkReviewsContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        // GET: api/UserDetails
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetUserDetails()
        {
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
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserLogin userObj)
        {
            if (userObj == null)

                return BadRequest();


            UserDetail? user = await _context.UserDetails.FirstOrDefaultAsync(user => user.Email.ToLower() == userObj.Email.ToLower());
            if (user == null)
                return NotFound(new { Message = "User not found!" });

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect" });
            }

            if (user == null)

                return NotFound(new { Message = "User Not Found" });
            user.LastLoginDate = DateTime.Now;
            user.Token = CreateJwt(user);
            var newAccessToken = user.Token;
            var newRefreshToken = CreateRefreshtoken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.Now.AddDays(7);
            await _context.SaveChangesAsync();
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }

            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        // POST: api/UserDetails
        [HttpPost]
        public async Task<ActionResult<UserDetail>> PostUserDetail([FromBody] UserDetail userDetail)
        {
            if (await CheckUserName(userDetail.UserName))
                return BadRequest(new { Message = "UserName Already Exist!" });
            if (await CheckEmail(userDetail.Email))
                return BadRequest(new { Message = "Email Already Exist!" });
            userDetail.UserName = userDetail.UserName;

            userDetail.Email = userDetail.Email?.ToLower();
            userDetail.Password = PasswordHasher.HashPassword(userDetail.Password);
            userDetail.IsDeleted = false;
            userDetail.RoleId = 2;
            userDetail.CreatedDate = DateTime.Now;
            userDetail.ModifiedDate = null;
            userDetail.Token = "";
            userDetail.Bio = userDetail.Bio;
            userDetail.DisplayPic = userDetail.DisplayPic;
            userDetail.LastLoginDate = null;

            _context.UserDetails.Add(userDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDetail", new { id = userDetail.UserId }, userDetail);
        }
        private async Task<bool> CheckUserName(string UserName)
                => await _context.UserDetails.AnyAsync(user => user.UserName == UserName);

        private async Task<bool> CheckEmail(string Email)
        => await _context.UserDetails.AnyAsync(user => user.Email == Email);

        private string CreateJwt(UserDetail user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("GnXXMmNjWUkjXQyJmoBesXgSRXEica7n");
            var RoleName = _context.Roles.FirstOrDefault(x => x.RoleId == user.RoleId);
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,RoleName.RoleName),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub,user.UserId.ToString()),

            });
            //Use convert the Key into Bytes
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
        private string CreateRefreshtoken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            var tokenUser = _context.UserDetails.Any(a => a.RefreshToken == refreshToken);
            if (tokenUser)
            {
                return CreateRefreshtoken();
            }
            return refreshToken;
        }

        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("GnXXMmNjWUkjXQyJmoBesXgSRXEica7n");
            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var pricipal = tokenHandler.ValidateToken(token, tokenValidationParameter, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("This is Invalid");
            }
            return pricipal;
        }
        [HttpPost]
        public async Task<IActionResult> Refresh(TokenApiDto tokenApiDto)
        {
            if (tokenApiDto is null)
            {
                return BadRequest("Invalid Client Request");
            }
            string accessToken = tokenApiDto.AccessToken;
            string refreshToken = tokenApiDto.RefreshToken;
            var pricipal = GetPrincipleFromExpiredToken(refreshToken);
            var userName = pricipal.Identity.Name;
            var user = await _context.UserDetails.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.Now)
                return BadRequest("Invalid Request");
            var newAccessToken = CreateJwt(user);
            var newRefreshToken = CreateRefreshtoken();
            user.RefreshToken = newRefreshToken;
            await _context.SaveChangesAsync();
            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            });

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
        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var user = await _context.UserDetails.FirstOrDefaultAsync(a => a.Email == email);
            if (user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Email Doesn't Exist"
                });
            }
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            user.ResetPasswordToken = emailToken;
            user.ResetPasswordTokenExpiry = DateTime.Now.AddDays(7);
            string from = _configuration["EmailSettings:From"];
            var emailModel = new EmailModel(email, "Reset passwort", EmailBody.EmailStringBody(email, emailToken));
            _emailService.SendEmail(emailModel);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Email Sent"
            });

        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var newtoken = resetPasswordDto.EmailToken.Replace("", "+");
            var user = await _context.UserDetails.AsNoTracking().FirstOrDefaultAsync(a => a.Email == resetPasswordDto.Email);
            if (user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Email Doesn't Exist"
                });
            }
            var tokenCode = user.ResetPasswordToken;
            DateTime emailTokenExpiry = Convert.ToDateTime(user.ResetPasswordTokenExpiry);
            if (tokenCode != resetPasswordDto.EmailToken || emailTokenExpiry < DateTime.Now)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "InValid Reset Link"
                });
            }
            user.Password = PasswordHasher.HashPassword(resetPasswordDto.NewPassword);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Password reset successfully!!"
            });

        }

    }
}
