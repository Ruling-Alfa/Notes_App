using CrossCutting.Security.Interfaces;
using CRUD_API.Business.Interfaces;
using CRUD_API.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CRUD_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("user")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public LoginController(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginModel loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid Request Data");
            }
            var userDetails = await _userService.VerifyUserLogin(loginModel);
            if (userDetails is not null && userDetails.Id > 0)
            {
                var token = _tokenHelper.GenerateToken(new CrossCutting.Security.Models.TokenModel()
                {
                    Id = userDetails.Id,
                    Username = userDetails.UserName,
                    Role = "User"
                });
                var userTokenModel = new UserLoginResponseModel(userDetails) with { Token = token };
                return Ok(userTokenModel);
            }
            return NotFound("User Not Found");
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetUserById(int Id)
        {
            return Ok(new UserLoginModel { });
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterModel registerModel)
        {
            if (registerModel is null)
            {
                return BadRequest("Invalid Request Data");
            }
            var userDetails = await _userService.CreateUser(registerModel);
            if (userDetails is not null && userDetails.Id > 0)
            {
                return Ok(userDetails);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);

        }
    }
}
