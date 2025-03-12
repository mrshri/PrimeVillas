﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeVillas.Domain.Entities
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Villa")]
        [Display(Name ="Villa Name")]
        public int VillaId { get; set; }
        [ValidateNever]
        public Villa Villa { get; set; }
    }
}
