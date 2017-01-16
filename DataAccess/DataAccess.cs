using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.DataAccess
{
    internal class DataAccess : IDataAccess
    {
        private readonly LocalDbContext _db;

        public DataAccess() : this(new LocalDbContext())
        { }

        public DataAccess(LocalDbContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Save the employer info in the database.
        /// </summary>
        /// <param name="employer">The employer information.</param>
        /// <returns>Null if there was an error saving the employer info.</returns>
        public Employer SaveEmployer(Employer employer)
        {
            Employer loggedEmployer = null;

            try
            {
                loggedEmployer = _db.Employers.Add(employer);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                LogError("An error occurred while adding an employer.\n" +
                         $"Employer Name: {employer.FirstName} {employer.LastName}\n" +
                         $"Employer Organization: {employer.Organization}\n" +
                         $"Error: {e.Message}");
            }
            
            return loggedEmployer;
        }

        /// <summary>
        /// Given a student id (RNumber), return the student.
        /// </summary>
        /// <param name="rNum">The student id</param>
        /// <returns>Null if no student was found.</returns>
        public Student GetStudentById(int rNum)
        {
            var student = _db.Students.FirstOrDefault(s => s.RNumber == rNum);

            if (student != null)
            {
                //student.IsPreRegistered =;
                student.SignInCount++;
                student.LastSignInTime = DateTime.UtcNow;

                _db.SaveChanges();
            }
            else
            {
                LogError($"No student with the RNumber {rNum} found in the database.");
            }

            return student;
        }

        /// <summary>
        /// Save the error in the database.
        /// </summary>
        /// <param name="message">The error message string</param>
        /// <returns>Null if there was an issue saving to the database.</returns>
        public Error LogError(string message)
        {
            var error = _db.Errors.Add(new Error
            {
                Message = message,
                ErrorTime = DateTime.UtcNow
            });

            _db.SaveChanges();
            return error;
        }
    }
}
