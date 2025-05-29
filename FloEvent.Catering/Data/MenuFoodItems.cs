using System.ComponentModel.DataAnnotations;

namespace FloEvent.Catering.Data
{
    public class MenuFoodItems
    {

        [Required]
        public int MenuId { get; set; } // Foreign key to Menu

        [Required]
        public int FoodItemId { get; set; } // Foreign key to FoodItem

        public Menu? Menu { get; set; } // Navigation property to Menu

        public FoodItem? FoodItem { get; set; } // Navigation property to FoodItem

    }
}
