using System.ComponentModel.DataAnnotations;

namespace FloEvent.Management.Data
{
    public class Guest
    {
        public Guest()
        {
        }

        public Guest(int guestId, string guestName, string guestEmail, string guestPhone)
        {
            GuestId = guestId;
            GuestName = guestName;
            GuestEmail = guestEmail;
            GuestPhone = guestPhone;
        }

        [Required]
        public int GuestId { get; set; }

        [Required][StringLength(50, MinimumLength = 1)]
        public string GuestName { get; set; }

        [Required]
        public string GuestEmail { get; set; }

        public string GuestPhone { get; set; }

        public ICollection<EventGuests> EventGuests { get; set; } = new List<EventGuests>();


    }
}
