using System;

namespace WcoeJobFairRegistration.Models
{
    public class Employer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Organization { get; set; }

        public string Title { get; set; }

        public string Hotel { get; set; }

        public int NumberOfNights { get; set; }

        public bool IsAlumni { get; set; }

        public DateTime CheckedInTime { get; set; }
    }
}