using LazyDev.Core.Common;
using System.Threading.Tasks;
using Rock.Core.SysInfos.Dto;

namespace Rock.Core.SysInfos
{
    public interface ISysInfoService
    {

        Task<PageResult<SysInfoOutput>> GetSysInfos(SysInfoPageInput sysInfoPageInput);
    }
}
