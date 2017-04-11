using GenericRepository.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository.Imp
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        public EFRepository(DbContext Context)
        {
            _context = Context;
        }
        public void Create(TEntity Obj)
        {
            _context.Set<TEntity>().Add(Obj);
            _context.SaveChanges();
        }

        public async Task CreateAsync(TEntity Obj)
        {
            _context.Set<TEntity>().Add(Obj);
            await _context.SaveChangesAsync();

        }

        public void Delete(TEntity Obj)
        {
            _context.Set<TEntity>().Remove(Obj);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(TEntity Obj)
        {
            _context.Set<TEntity>().Remove(Obj);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public void Update(TEntity Obj)
        {
            _context.Entry (Obj).State = EntityState.Modified ;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(TEntity Obj)
        {
            _context.Entry(Obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
