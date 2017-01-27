using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.DataAccess
{
    internal class RemoteStudentRepository : IStudentRepository
    {
        private bool _isDisposed = false;
        private readonly RemoteDbContext _db;

        public RemoteStudentRepository() : this(new RemoteDbContext())
        { }

        public RemoteStudentRepository(RemoteDbContext context)
        {
            _db = context;
        }

        public Student GetByStudentID(int studentID)
        {
            throw new NotImplementedException();
        }

        public Student SaveStudent(Student student)
        {
            throw new NotImplementedException();
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
