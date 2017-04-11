using GenericRepository.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository.Imp
{
    public class InMemoryRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private List<TEntity> _Data;
        public InMemoryRepository()
        {
            _Data = new List<TEntity>();
        }
        public InMemoryRepository(params TEntity[] elements)
        {
            _Data = new List<TEntity>();
            _Data.AddRange(elements);
        }

        public void Create(TEntity Obj)
        {
            _Data.Add(Obj);
        }

        public async Task CreateAsync(TEntity Obj)
        {
            await Task.Run(() => _Data.Add(Obj));
        }

        public void Delete(TEntity Obj)
        {
            _Data.Remove(Obj);
        }

        public async Task DeleteAsync(TEntity Obj)
        {
            await Task.Run(() => _Data.Remove(Obj));
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _Data.AsQueryable();
        }

        public void Update(TEntity Obj)
        {
            var index = _Data.FindIndex(o => IsEquals(o, Obj));
            _Data[index] = Obj;
        }

        public async Task UpdateAsync(TEntity Obj)
        {
            await Task.Run(() =>
            {
                var index = _Data.FindIndex(o => IsEquals(o, Obj));
                _Data[index] = Obj;
            });
        }

        private bool IsEquals(TEntity Obj1, TEntity Obj2)
        {
            var isequals = true;
            var PropertyKey = GetPropertyKey();
            if (GetPropertyKey().Count == 0)
                return false;
            foreach (var property in GetPropertyKey())
            {
                if (!Obj1.GetType().GetProperty(property).GetValue(Obj1).Equals(Obj2.GetType().GetProperty(property).GetValue(Obj2)))
                    isequals = false;
            }
            return isequals;
        }

        private static List<string> GetPropertyKey()
        {
            List<string> lstPropertyKey = new List<string>();

            PropertyInfo[] props = typeof(TEntity).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    KeyAttribute keyAttr = attr as KeyAttribute;
                    if (keyAttr != null)
                    {
                        string propName = prop.Name;


                        lstPropertyKey.Add(propName);
                    }
                }
            }

            return lstPropertyKey;
        }
    }
}
