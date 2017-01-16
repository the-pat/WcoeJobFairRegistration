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

        public string Major { get; set; }

        public DateTime GraduationDate { get; set; }

        public DateTime? LastSignInTime { get; set; }

        public int SignInCount { get; set; }

        public bool? IsPreRegistered { get; set; }
    }
}
