using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcoeJobFairRegistration.Models
{
    internal class Student
    {
        /// <summary>
        /// The student's RNumber
        /// </summary>
        public int RNumber { get; set; }

        /// <summary>
        /// The student's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The students's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The student's major
        /// </summary>
        public string Major { get; set; }

        /// <summary>
        /// The student's graduation date
        /// </summary>
        public DateTime GraduationDate { get; set; }
    }
}
