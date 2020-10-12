using LazyDev.Core.Dependency;
using Rock.Core.Entities;
using Rock.Core.SysInfos.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rock.Core.SysInfos
{
    public interface ISysInfoService:IScopedDependency
    {

        Task<List<SysInfo>> GetSysInfos(SysInfoPageInput sysInfoPageInput);
    }
}
