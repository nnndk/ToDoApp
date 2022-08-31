using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoApp.Data;
using ToDoApp.Data.Models;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Helper;
using ToDoApp.Data.HelpModels;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IConfiguration Configuration { get; }

        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/<UserController>
        //[Authorize]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            using (ToDoAppDbContext db = new ToDoAppDbContext())
            {
                //return db.User.OrderBy(user => user.Id).ToList();
                return db.Set<User>().OrderBy(user => user.Id).ToList();
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            using (ToDoAppDbContext db = new ToDoAppDbContext())
            {
                return db.User.ToList().Where(user => user.Id == id).FirstOrDefault();
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            using (ToDoAppDbContext db = new ToDoAppDbContext())
            {
                User sameLogin = db.User.ToList().Where(u => u.Login == user.Login).FirstOrDefault();
                User sameEmail = db.User.ToList().Where(u => u.Email == user.Email).FirstOrDefault();

                if (sameLogin == null && sameEmail == null)
                {
                    string truePassword = user.Password;
                    user.Password = Auth.HashPassword(user.Password);

                    db.User.Add(user);
                    db.SaveChanges();

                    return Authenticate(new AuthRequest { Login = user.Login, Password = truePassword });
                }

                return BadRequest("User with such login and/or email already exists!");
            }
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthRequest authData)
        {
            using (ToDoAppDbContext db = new ToDoAppDbContext())
            {
                User user = db.User.ToList().Where(u => u.Login == authData.Login).FirstOrDefault();

                if (user != null && Auth.VerifyHashedPassword(user.Password, authData.Password))
                {
                    return Ok(new AuthResponse(user, Configuration.GenerateJwtToken(user)));
                }

                return BadRequest(new { message = "Login or password is incorrect!" });
            }
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
