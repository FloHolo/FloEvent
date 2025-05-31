using FloEvent.Catering.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FloEvent.Web.ViewModels
{
    public class FoodItemViewModel
    {
      public int FoodItemId { get; set; }

        [Required][StringLength(50, MinimumLength = 1)] 
        public string Name { get; set; }

        public string Ingredients { get; set; } = string.Empty;

        [Required]
        public Diet Diet { get; set; } = Diet.Meat;

        [Required]
        public float UnitPrice { get; set; }

        public IEnumerable<SelectListItem> DietOptions { get; set; } = new List<SelectListItem>();
    }

}
