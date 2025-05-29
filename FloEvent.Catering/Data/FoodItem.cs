using System.ComponentModel.DataAnnotations;

namespace FloEvent.Catering.Data
{
    public class FoodItem
    {
        [Required]
        public int FoodItemId { get; set; }

        [Required][StringLength(50, MinimumLength = 1)]
        public string? Description { get; set; }

        [Required]
        public float UnitPrice { get; set; }

        public ICollection<MenuFoodItems> MenuFoodItems { get; set; } = new List<MenuFoodItems>();

    }
}
