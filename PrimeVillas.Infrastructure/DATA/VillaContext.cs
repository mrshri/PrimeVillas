﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrimeVillas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeVillas.Infrastructure.DATA
{
    public class VillaContext:IdentityDbContext<IdentityUser>
    {
        public VillaContext(DbContextOptions<VillaContext>options):base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(

                  new Villa
                  {
                      Id = 1,
                      Name = "Royal Villa",
                      Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                      ImageUrl = "https://placehold.co/600x400",
                      Occupancy = 4,
                      Price = 200,
                      Sqft = 550,
                  },
                  new Villa
                  {
                      Id = 2,
                      Name = "Premium Pool Villa",
                      Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                      ImageUrl = "https://placehold.co/600x401",
                      Occupancy = 4,
                      Price = 300,
                      Sqft = 550,
                  },
                  new Villa
                  {
                      Id = 3,
                      Name = "Luxury Pool Villa",
                      Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                      ImageUrl = "https://placehold.co/600x402",
                      Occupancy = 4,
                      Price = 400,
                      Sqft = 750,
                  }


            );
            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber
                {
                    Villa_Number = 101,
                    VillaId = 6,
                    SpecialDetails = "Pool View"
                },
                new VillaNumber
                {
                    Villa_Number = 102,
                    VillaId = 6,
                    SpecialDetails = "Garden View"
                },
                new VillaNumber
                {
                    Villa_Number = 201,
                    VillaId = 2,
                    SpecialDetails = "Pool View"
                },
                new VillaNumber
                {
                    Villa_Number = 202,
                    VillaId = 2,
                    SpecialDetails = "Garden View"
                },
                new VillaNumber
                {
                    Villa_Number = 301,
                    VillaId = 3,
                    SpecialDetails = "Pool View"
                },
                new VillaNumber
                {
                    Villa_Number = 302,
                    VillaId = 3,
                    SpecialDetails = "Garden View"
                }
            );

            modelBuilder.Entity<Amenity>().HasData(
                
                new Amenity
                {
                    Id = 1,
                    Name = "Free Wifi",
                    Description = "Free Wifi is available in all rooms",
                    VillaId = 2
                },
                new Amenity
                {
                    Id = 2,
                    Name = "Free Parking",
                    Description = "Free Parking is available for all guests",
                    VillaId = 2
                },
                new Amenity
                {
                    Id = 3,
                    Name = "Free Breakfast",
                    Description = "Free Breakfast is available for all guests",
                    VillaId = 2
                },
                new Amenity
                {
                    Id = 4,
                    Name = "Free Wifi",
                    Description = "Free Wifi is available in all rooms",
                    VillaId = 2
                },
                new Amenity
                {
                    Id = 5,
                    Name = "Free Parking",
                    Description = "Free Parking is available for all guests",
                    VillaId = 2
                },
                new Amenity
                {
                    Id = 6,
                    Name = "Free Breakfast",
                    Description = "Free Breakfast is available for all guests",
                    VillaId = 2
                },
                new Amenity
                {
                    Id = 7,
                    Name = "Free Wifi",
                    Description = "Free Wifi is available in all rooms",
                    VillaId = 3
                }
            );
        }
    }
}
