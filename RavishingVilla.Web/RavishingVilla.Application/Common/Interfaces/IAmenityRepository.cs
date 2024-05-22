using RavishingVilla.Domain.Entities;

namespace RavishingVilla.Application.Common.Interfaces
{
    public interface IAmenityRepository : IGenericRepository<Amenity>
    {
        void Update(Amenity entity);
    }
}
