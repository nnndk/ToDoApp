using System.Threading.Tasks;
using ToDoApp.Data.Entities;

namespace ToDoApp.Data.Abstract
{
    public interface IUserRepository: IBaseEntityRepository<User>
    {
        Task<bool> IsLoginUnique(string login);
        Task<bool> IsEmailUnique(string email);
        bool Register(User user);
    }
}
