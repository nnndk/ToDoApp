using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ToDoApp.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Data.Repositories
{
    public class BaseEntityRepository<T> : IBaseEntityRepository<T>
        where T : class, IBaseEntity, new()
    {
        protected ToDoAppDbContext _context;

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

        public async Task<T> GetSingle(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
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

        protected async void Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
