using System.Collections.Generic;
using System.Threading.Tasks;
using LazyDev.Core.Dependency;
using Rock.Core.Entities;
using Rock.Core.SysInfoApp.Dto;

namespace Rock.Core.SysInfoApp
{
    public interface ISysInfoService:IScopedDependency
    {

        Task<List<SysInfo>> GetSysInfos(SysInfoPageInput sysInfoPageInput);
    }
}
