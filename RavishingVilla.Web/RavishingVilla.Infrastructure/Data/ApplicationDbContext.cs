using Microsoft.EntityFrameworkCore;
using RavishingVilla.Domain.Entities;

namespace RavishingVilla.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Villa> Villas { get; set; }
    }
}
