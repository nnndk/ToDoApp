using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ToDoApp.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ToDoApp.Data.Repositories
{
    public class BaseEntityRepository<T> : IBaseEntityRepository<T>
        where T : class, IBaseEntity, new()
    {
        private ToDoAppDbContext _context;

        public BaseEntityRepository(ToDoAppDbContext context)
        {
            _context = context;
        }

        public virtual int Count()
        {
            return _context.Set<T>().Count();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().OrderBy(x => x.Id).AsEnumerable();
        }

        public T GetSingle(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public virtual async void Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            Commit();
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            Commit();
        }

        public virtual void Delete(int id)
        {
            T entity = _context.Set<T>().FirstOrDefault(x => x.Id == id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                Commit();
            }
        }

        private async void Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
