using Microsoft.EntityFrameworkCore;
using PrimeVillas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeVillas.Infrastructure.DATA
{
    public class VillaContext:DbContext
    {
        public VillaContext(DbContextOptions<VillaContext>options):base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
