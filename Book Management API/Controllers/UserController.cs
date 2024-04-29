using Book_Management_API.Data;
using Book_Management_API.Dto;
using Book_Management_API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("SignUp")]
        public ActionResult<UserDto> SingUp(CreateUserDto user)
        {
            if (user is null) { return BadRequest(); }

            var createdUser = _userService.Register(user);
            return Ok(createdUser);
        }

        [HttpPost("LogIn")]
        public ActionResult<string> Login(LoginDto login)
        {
            try
            {
                if (login is null) { return BadRequest(); }

                string token = _userService.Login(login);
                return Ok(token);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("ChangeRole")]
        public ActionResult ChangeRole(string userName, string newRoleName)
        {
            if (string.IsNullOrEmpty(userName)) { return BadRequest(); }
            if (string.IsNullOrEmpty(newRoleName)) { return BadRequest(); }
            if (!Enum.TryParse(newRoleName, out RoleType roleType)){ return BadRequest(); }

            _userService.UpdateUserRole(userName, roleType);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public ActionResult<List<DisplayUserDto>> GetAllUsers()
        {
            List<DisplayUserDto> usersDto = _userService.GetAllUsers();

            if(usersDto.Count == 0) { return BadRequest(); }
            return usersDto;
        }
    }
}
