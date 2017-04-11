using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository.Contract
{
    public interface IRepository<TEntity>
    {
        void Create(TEntity Obj);
        Task CreateAsync(TEntity Obj);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity Obj);
        Task UpdateAsync(TEntity Obj);
        void Delete(TEntity Obj);
        Task DeleteAsync(TEntity Obj);
    }
}
