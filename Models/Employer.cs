using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcoeJobFairRegistration.Models
{
    public class Employer
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Organization { get; set; }

        public string Title { get; set; }

        public string Hotel { get; set; }

        public int NumberOfNights { get; set; }

        public bool IsAlumni { get; set; }

        public string Other { get; set; }

        public DateTime CheckedInTime { get; set; }
    }
}
