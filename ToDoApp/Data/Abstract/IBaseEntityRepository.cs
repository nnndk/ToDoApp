using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToDoApp.Data.Abstract
{
    public interface IBaseEntityRepository<T> where T: class, IBaseEntity, new()
    {
        int Count();
        IEnumerable<T> GetAll();
        Task<T> GetSingle(int id);
        Task<T> GetSingle(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
