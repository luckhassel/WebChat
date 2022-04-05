using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebChat.Entities;
using WebChat.Services.Auth;
using WebChat.Services.Users;

namespace WebChat.Controllers
{
    [ApiController]
    [Route("/api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;
        public LoginController(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = _userRepository.GetUser(model.Username);

            if (user == null)
                return NotFound(new { message = "Wrong user" });

            var pwdmatch = _userRepository.PasswordMatch(model.Password, user.Password);
            if (!pwdmatch)
                return NotFound(new { message = "Wrong password" });

            var token = Token.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult AddUser(User user)
        {
            if (_userRepository.UserExists(user.Username))
            {
                return BadRequest($"User {user.Username} already exists");
            }
            _userRepository.AddUser(user);
            _userRepository.Save();
            return Created("", new { user.Id, user.Username, user.Role });
        }
    }
}
