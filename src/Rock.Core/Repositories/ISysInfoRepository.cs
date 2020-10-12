using LazyDev.Core.Common;
using System.Threading.Tasks;
using LazyDev.Core.Dependency;
using Rock.Core.SysInfos.Dto;

namespace Rock.Core.Repositories
{
    public interface ISysInfoRepository:IScopedDependency
    {
        Task<PageResult<SysInfoOutput>> GetSysInfos(SysInfoPageInput sysInfoPageInput);

    }
}
