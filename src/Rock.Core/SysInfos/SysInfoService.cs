using LazyDev.EFCore;
using Microsoft.EntityFrameworkCore;
using Rock.Core.Entities;
using Rock.Core.SysInfos.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rock.Core.SysInfos
{
    public class SysInfoService:ISysInfoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SysInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SysInfo>> GetSysInfos(SysInfoPageInput sysInfoPageInput)
        {
            var repository = _unitOfWork.Repository<SysInfo>();
            var query = repository.GetQueryable();
            if (!string.IsNullOrEmpty(sysInfoPageInput.Version))
            {
                query = query.Where(x => EF.Functions.Like(x.Version, sysInfoPageInput.Version));
            }

            return await query.Skip(sysInfoPageInput.Skip).Take(sysInfoPageInput.PageSize).ToListAsync();
        }
    }
}
