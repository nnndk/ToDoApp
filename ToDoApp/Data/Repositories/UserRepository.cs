using System.Threading.Tasks;
using ToDoApp.Data.Abstract;
using ToDoApp.Data.Entities;

namespace ToDoApp.Data.Repositories
{
    public class UserRepository : BaseEntityRepository<User>, IUserRepository
    {
        public UserRepository(ToDoAppDbContext context) : base(context) { }

        public bool IsLoginUnique(string login)
        {
            return GetSingle(u => u.Login == login) == null;
        }

        public bool IsEmailUnique(string email)
        {
            return GetSingle(u => u.Email == email) == null;
        }

        public bool Registration(User user)
        {
            if (IsLoginUnique(user.Login) && IsEmailUnique(user.Email))
            {
                Add(user);
                return true;
            }

            return false;
        }
    }
}
