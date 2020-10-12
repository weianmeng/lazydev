using LazyDev.Core.Common;
using System.Threading.Tasks;
using LazyDev.Core.Dependency;
using Rock.Core.SysInfos.Dto;

namespace Rock.Core.SysInfos
{
    public interface ISysInfoService:IScopedDependency
    {

        Task<PageResult<SysInfoOutput>> GetSysInfos(SysInfoPageInput sysInfoPageInput);
    }
}
