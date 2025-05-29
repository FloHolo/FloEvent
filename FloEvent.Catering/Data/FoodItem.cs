using System.ComponentModel.DataAnnotations;

namespace FloEvent.Catering.Data
{
    public class FoodItem
    {
        [Required]
        public int FoodItemId { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        public string Ingredients { get; set; } = string.Empty; 

        public Diet Diet { get; set; } = Diet.Meat;

        [Required]
        public float UnitPrice { get; set; }

        public ICollection<MenuFoodItems> MenuFoodItems { get; set; } = new List<MenuFoodItems>();

    }

    public enum Diet
    {
        Vegetarian,
        Vegan,
        Meat,
        Fish 
    }


}
