using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReelTalkReviews.Class;
using ReelTalkReviews.ErrorInfo;
using ReelTalkReviews.Models;
using ReelTalkReviews.Models.Dto;
using ReelTalkReviews.RepoPattern;

namespace ReelTalkReviews.Conrollers
{
#nullable disable
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IRepository<UserDetail> _userDetailsRepository;
        private readonly ReelTalkReviewsContext _context;
        private readonly Oops _oops;
        private readonly Token _token;

        public UserDetailsController(ReelTalkReviewsContext context, IRepository<UserDetail> userDetailsRepository, Oops oops, Token token)
        {
            _userDetailsRepository = userDetailsRepository;
            _context = context;
            _oops = oops;
            _token = token;

        }


       

        // GET: api/UserDetails
        //[Authorize]
        [HttpGet]
        public IEnumerable<UserDetail> GetUserDetails()
        {
            return _userDetailsRepository.GetAll();
        }

        // GET: api/UserDetails/5
        [HttpGet("{id}")]
        public UserDetail GetUserDetail(int id)
        {

            var userDetail = _userDetailsRepository.GetById(id);

            if (userDetail == null)
            {
                throw new NotFoundException("User not Found");
            }

            return userDetail;
        }


        [HttpPut("{id}")]
        public IActionResult PutUserDetail(int id, UserDetail userDetail)
        {

            try
            {
                _userDetailsRepository.Update(userDetail);
            }
            catch (Exception ex)
            {
                if (!_oops.UserDetailExists(id))
                {
                    throw new BadRequestException("User Id Not found");
                }
            }
            throw new SuccessException("Updated successfully");
        }
        [HttpPost]
        public async Task<ActionResult<UserDetail>> PostUserDetail([FromBody] UserDetail userDetail)
        {
            await _oops.PostDetails(userDetail);
            throw new SuccessException("Posted successfully");
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserLogin userObj)
        {
            await _oops.AuthenticateUser(userObj);
            throw new SuccessException("Logged Successfully");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUserDetail(int id, UserDetail userDetail)
        {

            try
            {
                _userDetailsRepository.Delete(userDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_oops.UserDetailExists(id))
                {
                    throw new BadRequestException("User Id Not found");
                }
            }
            throw new SuccessException("Deleted successfully");
        }
       
    }
}
