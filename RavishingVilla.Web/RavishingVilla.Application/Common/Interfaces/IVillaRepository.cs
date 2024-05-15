using RavishingVilla.Domain.Entities;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace RavishingVilla.Application.Common.Interfaces
{
    public interface IVillaRepository
    {
        IEnumerable<Villa> GetAllVillas(Expression<Func<Villa, bool>>?filter = null, string? includeProperties = null);

        Villa GetVilla(Expression<Func<Villa, bool>> filter, string? includeProperties = null);

        void Add(Villa entity);
        void Update(Villa entity);

        void Delete(Villa entity);

        void Save();
    }
}
