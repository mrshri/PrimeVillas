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

namespace PrimeVillas.Infrastructure.Repository
{
    public class VillaNumberRepository :Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly VillaContext _context;
        public VillaNumberRepository(VillaContext villaContext) : base(villaContext)
        {
            _context = villaContext;
        } 

        public void Update(VillaNumber entity)
        {
           _context.VillaNumbers.Update(entity);
        }
    }
}
