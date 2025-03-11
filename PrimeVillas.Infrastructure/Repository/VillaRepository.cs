using Microsoft.EntityFrameworkCore;
using PrimeVillas.Application.Common.Interfaces;
using PrimeVillas.Domain.Entities;
using PrimeVillas.Infrastructure.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrimeVillas.Infrastructure.Repository.Repository
{
    public class VillaRepository :Repository<Villa>, IVillaRepository
    {
        private readonly VillaContext _dbContext;
        public VillaRepository(VillaContext villaContext) : base(villaContext)
        {
            _dbContext = villaContext;
        }


        public void Update(Villa entity)
        {
           _dbContext.Villas.Update(entity);
        }
    }
}
