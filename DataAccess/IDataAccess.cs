using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.DataAccess
{
    internal interface IDataAccess
    {
        Employer SaveEmployer(Employer employer);

        Student GetStudentByRNum(int rNum);

        Error LogError(string message);
    }
}
