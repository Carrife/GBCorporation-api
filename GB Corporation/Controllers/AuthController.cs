using GB_Corporation.DTOs;
using GB_Corporation.Enums;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GB_Corporation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {   
        private readonly IAuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService, IAuthService authService)
        {
            _jwtService = jwtService;
            _authService = authService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterDTO model)
        {
            if (model == null || _authService.IsExists(model))
                return BadRequest();

            _authService.Register(model);

            return Ok("success");
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDTO login)
        {
            var user = _authService.GetUserByEmail(login.Email);

            if(user == null)
            {
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.InvalidEmail));
            }

            if (!BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.InvalidPassword));
            }
            
            var jwt = _jwtService.Generate(user, user.RoleId.Value);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok();
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                return Ok(_authService.GetUserById(userId, jwt));
            }
            catch (Exception _)
            {
                return Unauthorized();
            }   
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            
            return Ok(new
            {
                message = "success"
            });
        }
        
        
        [Authorize(Roles = "Admin, Developer, LineManager, TeamLeader, HR, Accountant, ChiefAccountant, CEO, BA")]
        [HttpPost("UpdatePassword")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordDTO model)
        {
            if (model == null || !_authService.IsExists(model))
                return BadRequest();

            if (model.NewPassword != model.NewPasswordConfirm)
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.InvalidPassword));

            if (!BCrypt.Net.BCrypt.Verify(model.Password, _authService.GetPasswordById(model.UserId)))
                return Conflict(new ErrorResponseDTO((int)ErrorResponses.InvalidPassword));

            _authService.UpdatePassword(model);

            return Ok("success");
        }
    }
}
