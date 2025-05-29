using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FloEvent.Management.Data
{
    public class EventGuests
    {
        public EventGuests()
        {
        }

        public EventGuests(int guestId, int eventId, bool hasAttended)
        {
            GuestId = guestId;
            EventId = eventId;
            HasAttended = hasAttended;
        }
        public int GuestId { get; set; }
        [ValidateNever]
        public Guest Guest { get; set; }
        public int EventId { get; set; }

        [ValidateNever]
        public Event Event { get; set; }

        public bool HasAttended { get; set; } = false;
    }
}
