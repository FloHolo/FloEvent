using System.ComponentModel.DataAnnotations;

namespace FloEvent.Management.Data
{
    public class Staff
    {
        public Staff()
        {
        }

        public Staff(int staffId, string staffName, string staffEmail, string staffPhone, bool isFirstAider)
        {
            StaffId = staffId;
            StaffName = staffName;
            StaffEmail = staffEmail;
            StaffPhone = staffPhone;
            IsFirstAider = isFirstAider;
        }

        public int StaffId { get; set; }

        public string StaffName { get; set; }

        public string StaffEmail { get; set; }

        public string StaffPhone { get; set; }

        public bool IsFirstAider { get; set; }

        public ICollection<EventStaffs> EventStaffs { get; set; } = new List<EventStaffs>();

    }
}
