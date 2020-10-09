using LazyDev.Core.Common;
using Rock.Core.Dto.SysInfoApp;
using System.Threading.Tasks;

namespace Rock.Core.Repositories
{
    public interface ISysInfoRepository
    {
        Task<PageResult<SysInfoOutput>> GetSysInfos(SysInfoPageInput sysInfoPageInput);

    }
}
