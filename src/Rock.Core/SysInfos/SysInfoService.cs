using LazyDev.Core.Common;
using Rock.Core.Repositories;
using System.Threading.Tasks;
using Rock.Core.SysInfos.Dto;

namespace Rock.Core.SysInfos
{
    public class SysInfoService:ISysInfoService
    {
        private readonly ISysInfoRepository _sysInfoRepository;

        public SysInfoService(ISysInfoRepository sysInfoRepository)
        {
            _sysInfoRepository = sysInfoRepository;
        }
        public async Task<PageResult<SysInfoOutput>> GetSysInfos(SysInfoPageInput sysInfoPageInput)
        {
            return await _sysInfoRepository.GetSysInfos(sysInfoPageInput);
        }
    }
}
