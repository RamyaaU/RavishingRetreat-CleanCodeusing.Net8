﻿using RavishingVilla.Application.Common.Interfaces;
using RavishingVilla.Infrastructure.Data;

namespace RavishingVilla.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IVillaRepository Villa { get; private set; }

        public IVillaNumberRepository VillaNumber { get; private set; }

        //public IVillaNumberRepository VillaNumberRepository => throw new NotImplementedException();

        //public IVillaRepository VillaRepository => throw new  NotImplementedException();

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
            Villa = new VillaRepository(_dbContext);
            VillaNumber = new VillaNumberRepository(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
