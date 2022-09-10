using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoApp.Data;
using ToDoApp.Data.Entities;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Helper;
using ToDoApp.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using ToDoApp.Data.Abstract;
using ToDoApp.Data.Repositories;
using System.Threading;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IConfiguration Configuration { get; }
        private ToDoAppDbContext context;
        private IUserRepository userRepository;

        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
            context = new ToDoAppDbContext();
            userRepository = new UserRepository(context);
        }

        // GET: api/<UserController>
        [Authorize]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return userRepository.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await userRepository.GetSingle(id);
        }

        // POST api/<UserController>/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            string truePassword = user.Password;
            user.Password = Auth.HashPassword(user.Password);
            bool result = userRepository.Register(user);

            if (result)
                return await Login(new AuthRequest { Login = user.Login, Password = truePassword });

            return BadRequest("User with such login and/or email already exists!");
        }

        // POST api/<UserController>/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest authData)
        {
            User user = await Task.Run(() => userRepository.GetSingle(u => u.Login == authData.Login));

            if (user != null && Auth.VerifyHashedPassword(user.Password, authData.Password))
            {
                // создаем один claim
                var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, authData.Login) };

                // создаем объект ClaimsIdentity
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                // установка аутентификационных куки
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Ok("Login successful!");
                //return Ok(new AuthResponse(user, Configuration.GenerateJwtToken(user)));
            }

            return BadRequest(new { message = "Login or password is incorrect!" });
        }

        // GET api/<UserController>/logout
        [Authorize]
        [HttpGet("logout")]
        public async void Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        // GET api/<UserController>/isAuth
        [HttpGet("isAuth")]
        public bool IsAuth()
        {
            bool t = HttpContext.User.Identity.IsAuthenticated;
            return t;
        }
    }
}
