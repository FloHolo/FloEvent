using System.ComponentModel.DataAnnotations;

namespace FloEvent.Catering.Data
{
    public class FoodBooking
    {

        [Required]
        public int FoodBookingId { get; set; }

        [Required]
        public int ClientReferenceId { get; set; } // Foreign key to ClientReference

        public int NumberOfGuests { get; set; }

        [Required]
        public int MenuId { get; set; } // Key to menu  
    }
}
