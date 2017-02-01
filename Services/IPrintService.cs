using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.Services
{
    internal interface IPrintService
    {
        /// <summary>
        /// Given a student, print a label with the student's information
        /// </summary>
        /// <param name="student">The given student</param>
        /// <returns>True represents that the label was printed successfully</returns>
        bool PrintLabel(Student student);

        /// <summary>
        /// Given an employer, print a label with the employer's information
        /// </summary>
        /// <param name="employer">The given employer</param>
        /// <returns>True represents that the label was printed successfully</returns>
        bool PrintLabel(Employer employer);
    }
}
