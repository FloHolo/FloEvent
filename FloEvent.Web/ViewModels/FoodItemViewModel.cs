using FloEvent.Catering.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace FloEvent.Web.ViewModels
{
    public class FoodItemViewModel
    {
        public FoodItem FoodItem { get; set; } = new FoodItem();

        public IEnumerable<SelectListItem> DietOptions { get; set; } = new List<SelectListItem>();
    }
}
