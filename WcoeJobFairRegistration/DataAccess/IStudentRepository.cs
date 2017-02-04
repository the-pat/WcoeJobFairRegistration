using System.Threading.Tasks;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.DataAccess
{
    public interface IStudentRepository
    {
        Task<bool> Load(string fileName);

        Task<JobGridStudent> Find(string rNumber);

        Task<bool> Save(AttendingStudent student);
    }
}