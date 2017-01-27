﻿using System.Data.Entity;

namespace WcoeJobFairRegistration.Models
{
    public class LocalDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Employer> Employers { get; set; }
    }
}
