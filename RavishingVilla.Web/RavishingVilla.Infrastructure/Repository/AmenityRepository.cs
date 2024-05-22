using RavishingVilla.Application.Common.Interfaces;
using RavishingVilla.Domain.Entities;
using RavishingVilla.Infrastructure.Data;

namespace RavishingVilla.Infrastructure.Repository
{
    public class AmenityRepository : GenericRepository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AmenityRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Update(Amenity entity)
        {
            _applicationDbContext.Amenities.Update(entity);
        }
    }
}
