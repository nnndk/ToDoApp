using System.Threading.Tasks;
using ToDoApp.Data.Abstract;
using ToDoApp.Data.Entities;

namespace ToDoApp.Data.Repositories
{
    public class UserRepository : BaseEntityRepository<User>, IUserRepository
    {
        public UserRepository(ToDoAppDbContext context) : base(context) { }

        public async Task<bool> IsLoginUnique(string login)
        {
            return await GetSingle(u => u.Login == login) == null;
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            return await GetSingle(u => u.Email == email) == null;
        }

        public bool Register(User user)
        {
            bool isLoginUnique = IsLoginUnique(user.Login).Result;
            bool isEmailUnique = IsEmailUnique(user.Email).Result;

            if (isLoginUnique && isEmailUnique)
            {
                //Add(user);
                _context.Set<User>().Add(user);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
