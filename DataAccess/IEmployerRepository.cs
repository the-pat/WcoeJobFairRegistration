﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.DataAccess
{
    public interface IEmployerRepository : IDisposable
    {
        Employer SaveEmployer(Employer employer);
    }
}