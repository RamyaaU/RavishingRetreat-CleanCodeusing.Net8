using RavishingVilla.Domain.Entities;

namespace RavishingVilla.Application.Common.Interfaces
{
    public interface IVillaNumberRepository : IGenericRepository<VillaNumber>
    {
        void Update(VillaNumber entity);
        //void Save();
    }
}
