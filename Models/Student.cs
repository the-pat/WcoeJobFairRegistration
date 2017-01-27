using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcoeJobFairRegistration.Models
{
    public class Student
    {
        public int ID { get; set; }

        public int RNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? CheckedInTime { get; set; }

        public bool? IsPreRegistered { get; set; }

        public bool IsSynced { get; set; }
    }
}
