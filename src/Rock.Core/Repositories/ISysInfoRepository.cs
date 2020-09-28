using System.Threading.Tasks;
using Rock.Core.Entities;

namespace Rock.Core.Repositories
{
    public interface ISysInfoRepository
    {
        Task<SysInfo> GetAsync(int id);

    }
}
