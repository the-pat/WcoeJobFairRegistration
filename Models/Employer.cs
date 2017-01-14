using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcoeJobFairRegistration.Models
{
    internal class Employer
    {
        /// <summary>
        /// The employer's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The employer's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The organization the employer is representing
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// True represents that the employer is an alumni
        /// </summary>
        public bool IsAlumni { get; set; }
    }
}
