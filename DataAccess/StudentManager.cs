using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.DataAccess
{
    internal class StudentManager : IStudentManager
    {
        public StudentManager() 
        {
            // TODO: Connect to the database
        }

        /// <summary>
        /// Given a student id, return the student
        /// </summary>
        /// <param name="id">The student id</param>
        /// <returns>A student object</returns>
        public Student GetStudentById(int id)
        {
            return null;
            throw new NotImplementedException();
        }
    }
}
