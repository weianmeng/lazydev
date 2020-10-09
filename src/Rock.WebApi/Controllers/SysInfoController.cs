using LazyDev.Core.Common;
using Microsoft.AspNetCore.Mvc;
using Rock.Core.Dto.SysInfoApp;
using Rock.Core.SysInfos;
using System.Threading.Tasks;

namespace Rock.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysInfoController : ControllerBase
    {
        private readonly ISysInfoService _sysInfoService;

        public SysInfoController(ISysInfoService sysInfoService)
        {
            _sysInfoService = sysInfoService;
        }

        public async Task<PageResult<SysInfoOutput>> SysInfo(SysInfoPageInput sysInfoPageInput)
        {
            return await _sysInfoService.GetSysInfos(sysInfoPageInput);
        }
    }
}
