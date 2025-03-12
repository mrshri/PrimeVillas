using PrimeVillas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeVillas.Application.Common.Interfaces
{
    public interface IAmenityRepository :IRepository<Amenity>
    {
       void Update(Amenity entity);
    }
}
