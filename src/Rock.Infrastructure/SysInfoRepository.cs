using LazyDev.Core.Common;
using Rock.Core.Repositories;
using Rock.Core.SysInfos.Dto;
using System.Threading.Tasks;

namespace Rock.Infrastructure
{

    public class SysInfoRepository:ISysInfoRepository
    {

        public  Task<PageResult<SysInfoOutput>> GetSysInfos(SysInfoPageInput sysInfoPageInput)
        {

            return null;
        }
    }
}
