using FloEvent.Catering.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FloEvent.Web.ViewModels
{
    public class MenuViewModel
    {

        [Required]
        public int MenuId { get; set; }

        [Required]
        public string MenuName { get; set; }

        public List<int> SelectedFoodItemIds { get; set; } = new List<int>();

        public IEnumerable<SelectListItem> FoodItemOptions { get; set; } = new List<SelectListItem>();
    }
}
