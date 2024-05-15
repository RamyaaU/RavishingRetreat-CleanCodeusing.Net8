using RavishingVilla.Application.Common.Interfaces;
using RavishingVilla.Domain.Entities;
using System.Linq.Expressions;

namespace RavishingVilla.Infrastructure.Repository
{
    public class VillaRepository : IVillaRepository
    {
        public void Add(Villa entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Villa entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Villa> GetAllVillas(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Villa> GetVilla(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Villa entity)
        {
            throw new NotImplementedException();
        }
    }
}
