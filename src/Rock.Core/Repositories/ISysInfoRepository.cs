using LazyDev.Core.Common;
using System.Threading.Tasks;
using Rock.Core.SysInfos.Dto;

namespace Rock.Core.Repositories
{
    public interface ISysInfoRepository
    {
        Task<PageResult<SysInfoOutput>> GetSysInfos(SysInfoPageInput sysInfoPageInput);

    }
}
