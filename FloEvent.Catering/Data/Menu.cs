using System.ComponentModel.DataAnnotations;

namespace FloEvent.Catering.Data
{
    public class Menu
    {
        [Required]
        public int MenuId { get; set; }

        [Required]
        public string MenuName { get; set; }   

        public ICollection<MenuFoodItems> MenuFoodItems { get; set; } = new List<MenuFoodItems>();
    }
}
