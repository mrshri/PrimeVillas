using PrimeVillas.Application.Common.Interfaces;
using PrimeVillas.Infrastructure.DATA;
using PrimeVillas.Infrastructure.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeVillas.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VillaContext _dbContext;
        public IVillaRepository Villa { get; private set; }
        public IVillaNumberRepository VillaNumber { get; private set; }
        public IAmenityRepository Amenity { get; private set; }
        public UnitOfWork(VillaContext villaContext)
        {
            _dbContext = villaContext;
            Villa = new VillaRepository(_dbContext);
            VillaNumber = new VillaNumberRepository(_dbContext);
            Amenity = new AmenityRepository(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
