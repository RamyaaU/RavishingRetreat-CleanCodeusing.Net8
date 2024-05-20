using RavishingVilla.Domain.Entities;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace RavishingVilla.Application.Common.Interfaces
{
    public interface IVillaRepository : IGenericRepository<Villa>
    {
        void Update(Villa entity);
        //void Save();
    }
}
