using RavishingVilla.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RavishingVilla.Application.Common.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAllVillas(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        T GetVilla(Expression<Func<T, bool>> filter, string? includeProperties = null);

        void Add(T entity);

       // void Update(T entity);

        void Delete(T entity);

        //void Save();
    }
}
