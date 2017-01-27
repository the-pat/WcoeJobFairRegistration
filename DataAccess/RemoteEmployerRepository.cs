using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.DataAccess
{
    internal class RemoteEmployerRepository : IEmployerRepository
    {
        private bool _isDisposed = false;
        private readonly RemoteDbContext _db;

        public RemoteEmployerRepository() : this(new RemoteDbContext())
        { }

        public RemoteEmployerRepository(RemoteDbContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Saves the employer information to the remote database
        /// </summary>
        public Employer SaveEmployer(Employer employer)
        {
            var savedEmployer = _db.Employers.Add(employer);
            _db.SaveChanges();

            return savedEmployer;
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
