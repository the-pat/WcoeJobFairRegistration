using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.DataAccess
{
    internal class LocalStudentRepository : IStudentRepository
    {
        private bool _isDisposed = false;
        private readonly LocalDbContext _db;

        public LocalStudentRepository() : this(new LocalDbContext())
        { }

        public LocalStudentRepository(LocalDbContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Gets the local student information.
        /// If no student is found, returns null.
        /// </summary>
        public Student GetByStudentID(int studentID)
        {
            return _db.Students.FirstOrDefault(s => s.RNumber == studentID);
        }

        /// <summary>
        /// Save the student information locally
        /// </summary>
        public Student SaveStudent(Student student)
        {
            var foundStudent = _db.Students.FirstOrDefault(s => s.RNumber == student.RNumber);

            if (foundStudent == null)
            {
                foundStudent = _db.Students.Add(student);
            }
            else
            {
                foundStudent.IsSynced = false;

                foundStudent.IsPreRegistered = student.IsPreRegistered;
                foundStudent.CheckedInTime = student.CheckedInTime;
            }

            _db.SaveChanges();

            return foundStudent;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                _db.Dispose();
            }

            _isDisposed = true;
        }
    }
}
