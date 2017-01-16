namespace WcoeJobFairRegistration.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class LocalDbContext : DbContext
    {
        public LocalDbContext() : base("name=LocalDbContext")
        {  }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
    }
}