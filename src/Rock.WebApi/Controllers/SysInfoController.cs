using LazyDev.EFCore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Rock.Core.Entities;
using System.Threading.Tasks;
using LazyDev.EFCore;
using Rock.Core.SysInfos.Dto;

namespace Rock.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysInfoController : ControllerBase
    {
        private readonly IRepository<SysInfo> _sysInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SysInfoController(IRepository<SysInfo> sysInfoRepository, IUnitOfWork unitOfWork)
        {
            _sysInfoRepository = sysInfoRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<SysInfo> SysInfo(int id)
        {
            return await _sysInfoRepository.FindAsync(id);
        }

        [HttpPost]
        public async Task<int> SysInfo(SysInfoAddInput sysInfo)
        {
            await _sysInfoRepository.AddAsync(new SysInfo {Version = sysInfo.Version});
           return  await _unitOfWork.CommitAsync();

        }
    }
}
