using LazyDev.Core.Common;
using Rock.Core.Dto.SysInfoApp;
using System.Threading.Tasks;

namespace Rock.Core.SysInfos
{
    public interface ISysInfoService
    {

        Task<PageResult<SysInfoOutput>> GetSysInfos(SysInfoPageInput sysInfoPageInput);
    }
}
