using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using NuGet.Protocol.Core.Types;
using ReelTalkReviews.Conrollers;
using ReelTalkReviews.ErrorInfo;
using ReelTalkReviews.Models;
using ReelTalkReviews.Models.Dto;
using ReelTalkReviews.RepoPattern;

namespace ReelTalkReviews.Class
{
#nullable disable 
    public abstract class Create
    {
        public abstract Task<ActionResult> PostDetails(UserDetail userDetail);
    }
    public class Oops:Create
    {
        private readonly ReelTalkReviewsContext _context;
        private readonly IRepository<UserDetail> _userDetailsRepository;
        private readonly Token _token;

        public Oops(ReelTalkReviewsContext context, IRepository<UserDetail> userDetailsRepository, Token token)
        {
            _context = context;
            _userDetailsRepository = userDetailsRepository;
            _token = token;
        }
        private async Task<bool> CheckUserName(string UserName)
               => await _context.UserDetails.AnyAsync(user => user.UserName == UserName);

        private async Task<bool> CheckEmail(string Email)
        => await _context.UserDetails.AnyAsync(user => user.Email == Email);
        protected internal bool UserDetailExists(int id)
        {
            return (_context.UserDetails?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
        public  override async Task<ActionResult> PostDetails(UserDetail userDetail)
        {
            if (await CheckUserName(userDetail.UserName))
                throw new UnAuthorizedException("UserName Already Exist!");
            if (await CheckEmail(userDetail.Email))
                throw new UnAuthorizedException("Email Already Exist!");
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
            _userDetailsRepository.Add(userDetail);
            throw new SuccessException("Created Successfully");
        }
        public async Task<ActionResult> PostMethod(UserDetail userDetail)
        {
            if (await CheckUserName(userDetail.UserName))
                throw new UnAuthorizedException("UserName Already Exist!");
            if (await CheckEmail(userDetail.Email))
                throw new UnAuthorizedException("Email Already Exist!");
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
            _userDetailsRepository.Add(userDetail);
            throw new SuccessException("Created Successfully");
        }

        public async Task<ActionResult> AuthenticateUser(UserLogin userObj)
        {
            if (userObj == null)
                throw new BadRequestException("User error");
            UserDetail user = await _context.UserDetails.FirstOrDefaultAsync(user => user.Email.ToLower() == userObj.Email.ToLower());
            if (user == null)
                throw new UnAuthorizedException("User not Found");
            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
                throw new UnAuthorizedException("Password Incorrect");
            user.LastLoginDate = DateTime.Now;
            user.Token = _token.CreateJwt(user);
            var newAccessToken = user.Token;
            var newRefreshToken = _token.CreateRefreshtoken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.Now.AddMinutes(3);
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception Ex)
            {
                throw new BadRequestException(Ex.Message);
            }
            throw new SuccessException("logged successfully");
        }

      
    }
}
