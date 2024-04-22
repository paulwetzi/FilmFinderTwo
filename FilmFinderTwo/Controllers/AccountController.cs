using FilmFinder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IO;
using static FilmFinderTwo.Controllers.MovieController;

/*
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 * 
 * Here is the API interface for Account
 * 
 * \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
 */

namespace FilmFinderTwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        Manager manager = new Manager();

        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost("register")]
        public ActionResult Post(RegisterDTO user)
        {
            manager.Register(user.email, user.name, user.password);
            _logger.LogInformation(user.ToString());
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult<User> Get([FromQuery] LoginDTO toRead)
        {
            User user = manager.Login(toRead.email, toRead.password);
            _logger.LogInformation($"Read user with email: {toRead.email}");
            return Ok(user);
        }


        public record RegisterDTO(string email, string name, string password);
        public record LoginDTO(string email, string password);
    }
}