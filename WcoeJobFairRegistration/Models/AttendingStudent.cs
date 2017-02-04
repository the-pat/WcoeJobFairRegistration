using System;

namespace WcoeJobFairRegistration.Models
{
    public class AttendingStudent
    {
        public string RNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CheckInTime { get; set; }
    }
}