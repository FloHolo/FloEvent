using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FloEvent.Management.Data
{
    public class EventStaffs
    {
        public EventStaffs()
        {
        }
        public EventStaffs(int staffId, int eventId)
        {
            StaffId = staffId;
            EventId = eventId;
        }

        public int StaffId { get; set; }

        [ValidateNever]
        public Staff Staff { get; set; }

        public int EventId { get; set; }
        [ValidateNever]
        public Event Event { get; set; }
    }
}
