using System.Collections.Generic;
using System.Threading.Tasks;
using LazyDev.Core.Common;
using LazyDev.Core.Extensions;
using LazyDev.EFCore;
using Microsoft.EntityFrameworkCore;
using Rock.Core.Entities;
using Rock.Core.SysInfoApp.Dto;

namespace Rock.Core.SysInfoApp
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
            query = query.WhereIf(!string.IsNullOrEmpty(sysInfoPageInput.Version),
                x => EF.Functions.Like(x.Version, sysInfoPageInput.Version));
           return await query.PageBy(sysInfoPageInput).ToListAsync();
        }

        public async Task<PageResult<SysInfo>> GetSysInfosNew(SysInfoPageInput sysInfoPageInput)
        {
            var repository = _unitOfWork.Repository<SysInfo>();
            var query = repository.GetQueryable();
            query = query.WhereIf(!string.IsNullOrEmpty(sysInfoPageInput.Version),
                x => EF.Functions.Like(x.Version, sysInfoPageInput.Version));
            var count = await query.CountAsync();
            var data = await query.PageBy(sysInfoPageInput).ToListAsync();
            return PageResult<SysInfo>.Page(sysInfoPageInput.PageIndex, sysInfoPageInput.PageSize, count, data);
        }
    }
}
