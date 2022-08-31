using ToDoApp.Data.Entities;

namespace ToDoApp.Data.Models
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthResponse(User user, string token)
        {
            Id = user.Id;
            Login = user.Login;
            Email = user.Email;
            Token = token;
        }
    }
}
