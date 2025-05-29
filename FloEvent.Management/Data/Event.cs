namespace FloEvent.Management.Data
{
    public class Event
    {
        public Event()
        {
        }

        public Event(int eventId, string eventName, DateTime eventDate, int foodBookingId, string reference, string venueCode, string venueName)
        {
            EventId = eventId;
            EventName = eventName;
            EventDate = eventDate;
            FoodBookingId = foodBookingId;
            Reference = reference;
            VenueCode = venueCode;
            VenueName = venueName;
        }


        public int EventId { get; set; }

        public string EventName { get; set; }

        public bool Canceled { get; set; } = false;

        public DateTime EventDate { get; set; }

        public int FoodBookingId { get; set; } // Key to food booking

        public int Id { get; set; } // Key to Event type

        public string Reference { get; set; } // Key to Veue Reservation

        public string VenueCode { get; set; } 

        public string VenueName { get; set; }

        public ICollection<EventGuests> EventGuests { get; set; } = new List<EventGuests>();
        public ICollection<EventStaffs> EventStaffs { get; set; } = new List<EventStaffs>();

    }
}
