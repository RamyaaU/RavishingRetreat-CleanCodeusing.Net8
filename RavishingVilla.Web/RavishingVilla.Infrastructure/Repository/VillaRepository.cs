using Microsoft.EntityFrameworkCore;
using RavishingVilla.Application.Common.Interfaces;
using RavishingVilla.Domain.Entities;
using RavishingVilla.Infrastructure.Data;
using System.Linq.Expressions;

namespace RavishingVilla.Infrastructure.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public VillaRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Add(Villa entity)
        {
            _applicationDbContext.Add(entity);
        }

        public void Delete(Villa entity)
        {
            _applicationDbContext.Remove(entity);
        }

        public IEnumerable<Villa> GetAllVillas(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Villa> query = _applicationDbContext.Set<Villa>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        public Villa GetVilla(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
        {
            IQueryable<Villa> query = _applicationDbContext.Set<Villa>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
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
