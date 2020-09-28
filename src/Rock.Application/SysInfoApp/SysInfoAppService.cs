using System.Threading.Tasks;
using Rock.Application.SysInfoApp.Models.ViewModels;
using Rock.Core.Repositories;

namespace Rock.Application.SysInfoApp
{
    public class SysInfoAppService
    {
        private readonly ISysInfoRepository _sysInfoRepository;

        public SysInfoAppService(ISysInfoRepository sysInfoRepository)
        {
            _sysInfoRepository = sysInfoRepository;
        }

        public async Task<SysInfoViewModel> GetAsync(int id)
        {
            var sysInfo = await _sysInfoRepository.GetAsync(id);
            return new SysInfoViewModel();
        }
    }
}
