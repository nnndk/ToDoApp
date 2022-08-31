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
        public User Get(int id)
        {
            return userRepository.GetSingle(id);
        }

        // POST api/<UserController>/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            string truePassword = user.Password;
            user.Password = Auth.HashPassword(user.Password);

            bool result = userRepository.Registration(user);

            if (result)
                return Authenticate(new AuthRequest { Login = user.Login, Password = truePassword });

            return BadRequest("User with such login and/or email already exists!");
        }

        // POST api/<UserController>/authenticate
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthRequest authData)
        {
            User user = userRepository.GetSingle(u => u.Login == authData.Login);

            if (user != null && Auth.VerifyHashedPassword(user.Password, authData.Password))
                return Ok(new AuthResponse(user, Configuration.GenerateJwtToken(user)));

            return BadRequest(new { message = "Login or password is incorrect!" });
        }

        /*[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            using (ToDoAppDbContext db = new ToDoAppDbContext())
            {
                var users = db.User.ToList();
                return Ok(users);
            }
        }*/

        /*// PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            using (ToDoAppDbContext db = new ToDoAppDbContext())
            {
                db.User.Update(user);
                db.SaveChangesAsync();
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (ToDoAppDbContext db = new ToDoAppDbContext())
            {
                var user = db.User.FindAsync(id);

                if (user != null)
                {
                    db.User.Remove(user.Result);
                    db.SaveChangesAsync();
                }
            }
        }*/
    }
}
