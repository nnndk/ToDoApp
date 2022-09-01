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
        //[Authorize]
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

            //bool result = await Task.Run(() => userRepository.Register(user));
            //Thread.Sleep(1000); // problem is solved but it is a crutch
            bool result = userRepository.Register(user);

            if (result)
                return await Authenticate(new AuthRequest { Login = user.Login, Password = truePassword });

            return BadRequest("User with such login and/or email already exists!");
        }

        // POST api/<UserController>/authenticate
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequest authData)
        {
            User user = await Task.Run(() => userRepository.GetSingle(u => u.Login == authData.Login));

            if (user != null && Auth.VerifyHashedPassword(user.Password, authData.Password))
                return Ok(new AuthResponse(user, Configuration.GenerateJwtToken(user)));

            return BadRequest(new { message = "Login or password is incorrect!" });
        }
    }
}
