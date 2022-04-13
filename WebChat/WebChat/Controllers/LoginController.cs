using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebChat.Controllers
{
    [ApiController]
    [Route("/api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepositoryService _userRepository;
        public LoginController(IUserRepositoryService userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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

            var token = Common.Settings.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(User user)
        {
            if (_userRepository.AddUser(user))
            {
                await _userRepository.Save();
                return Created("", new { user.Id, user.Username, user.Role });
            }
            return BadRequest();
        }
    }
}
