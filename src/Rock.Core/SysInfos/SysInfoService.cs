using LazyDev.Core.Common;
using Rock.Core.SysInfos.Dto;
using System.Threading.Tasks;

namespace Rock.Core.SysInfos
{
    public class SysInfoService:ISysInfoService
    {
        public Task<PageResult<SysInfoOutput>> GetSysInfos(SysInfoPageInput sysInfoPageInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
