using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ToDoApp.Data.Abstract
{
    public interface IBaseEntityRepository<T> where T: class, IBaseEntity, new()
    {
        int Count();
        IEnumerable<T> GetAll();
        T GetSingle(int id);
        T GetSingle(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
