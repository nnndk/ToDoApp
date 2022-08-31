using ToDoApp.Data.Entities;

namespace ToDoApp.Data.Abstract
{
    public interface IUserRepository: IBaseEntityRepository<User>
    {
        bool IsLoginUnique(string login);
        bool IsEmailUnique(string email);
        bool Registration(User user);
    }
}
