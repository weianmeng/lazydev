using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Rock.Core.Entities;
using System.Threading.Tasks;
using LazyDev.EFCore;
using Rock.Core.SysInfoApp;
using Rock.Core.SysInfoApp.Dto;

namespace Rock.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysInfoController : ControllerBase
    {
        private readonly IRepository<SysInfo> _sysInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysInfoService _sysInfoService;

        public SysInfoController(IRepository<SysInfo> sysInfoRepository, IUnitOfWork unitOfWork,ISysInfoService sysInfoService)
        {
            _sysInfoRepository = sysInfoRepository;
            _unitOfWork = unitOfWork;
            _sysInfoService = sysInfoService;
        }



        [HttpPost("add")]
        public async Task<int> AddSysInfo(SysInfoAddInput sysInfo)
        {
            await _sysInfoRepository.AddAsync(new SysInfo {Version = sysInfo.Version});
           return  await _unitOfWork.CommitAsync();

        }


        [HttpPost("list")]
        public async Task<List<SysInfo>> SysInfoPage(SysInfoPageInput sysInfoPage)
        {
            return await _sysInfoService.GetSysInfos(sysInfoPage);
        }
    }
}
