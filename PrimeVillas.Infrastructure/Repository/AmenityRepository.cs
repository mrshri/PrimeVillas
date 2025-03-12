using PrimeVillas.Application.Common.Interfaces;
using PrimeVillas.Domain.Entities;
using PrimeVillas.Infrastructure.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeVillas.Infrastructure.Repository
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private readonly VillaContext _context;
        public AmenityRepository(VillaContext villaContext):base(villaContext)
        {
         _context = villaContext;   
        }
        public void Update(Amenity entity)
        {
          _context.Amenities.Update(entity);
        }
    }
}
