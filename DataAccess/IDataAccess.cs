﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcoeJobFairRegistration.DataAccess
{
    internal interface IDataAccess
    {
        IStudentRepository StudentRepo { get; }

        IEmployerRepository EmployerRepository { get; }
    }
}
