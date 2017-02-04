using System.Threading.Tasks;
using WcoeJobFairRegistration.Models;

namespace WcoeJobFairRegistration.DataAccess
{
    public interface IEmployerRepository
    {
        Task<bool> Save(Employer employer);
    }
}