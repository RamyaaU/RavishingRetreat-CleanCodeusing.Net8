using System.Linq.Expressions;

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

        bool Any(Expression<Func<T, bool>> predicate);
    }
}
