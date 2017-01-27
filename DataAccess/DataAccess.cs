using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.DataAccess
{
    public class DataAccess : IDataAccess
    {
        public IStudentRepository StudentRepo { get; }

        public IEmployerRepository EmployerRepository { get; }

        public DataAccess(IStudentRepository studentRepo, IEmployerRepository employerRepo)
        {
            StudentRepo = studentRepo;
            EmployerRepository = employerRepo;
        }
    }
}
