using RavishingVilla.Application.Common.Interfaces;
using RavishingVilla.Domain.Entities;
using RavishingVilla.Infrastructure.Data;

namespace RavishingVilla.Infrastructure.Repository
{
    public class VillaRepository :GenericRepository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public VillaRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Update(Villa entity)
        {
            _applicationDbContext.Villas.Update(entity);
        }
    }
}
