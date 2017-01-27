using System.Data.Entity;

namespace WcoeJobFairRegistration.Models
{
    public class RemoteDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Employer> Employers { get; set; }
    }
}
