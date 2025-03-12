using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrimeVillas.Domain.Entities;

namespace PrimeVillasWeb.ViewModels
{
    public class AmenityVM
    {
        public Amenity  Amenity { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? VillaList { get; set; }
    }
}
