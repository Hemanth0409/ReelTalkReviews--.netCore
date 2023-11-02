using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReelTalkReviews.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ReelTalkReviews.Class
{
    public  class Token
    {
        private readonly ReelTalkReviewsContext _context;
        public Token(ReelTalkReviewsContext context)
        {
            _context = context;
        }
        protected internal string CreateJwt(UserDetail user)
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
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
        protected internal string CreateRefreshtoken()
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

        protected internal ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
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
    }
}
