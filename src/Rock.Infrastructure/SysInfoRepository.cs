using Dapper;
using lazyDev.Dapper;
using LazyDev.Core.Common;
using Rock.Core.Dto.SysInfoApp;
using Rock.Core.Entities;
using Rock.Core.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Rock.Infrastructure
{
    public class SysInfoRepository:ISysInfoRepository
    {
        private readonly IDbContext _dbContext;

        public SysInfoRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PageResult<SysInfoOutput>> GetSysInfos(SysInfoPageInput sysInfoPageInput)
        {
            var infos = await _dbContext.QueryAsync(x =>
                x.QueryAsync<SysInfo>("select * from sys_info limit @PageSize take @Skip",
                    new {sysInfoPageInput.PageSize, sysInfoPageInput.Skip}));


            var count = await _dbContext.QueryAsync(x =>
                x.ExecuteScalarAsync<int>("select count(id) from sys_info"));

            var sysInfoOutputs = infos
                .Select(info => new SysInfoOutput() {Version = info.Version, Remarks = info.Remarks});

            return PageResult<SysInfoOutput>.Page(sysInfoPageInput.PageIndex, sysInfoPageInput.PageSize, count, sysInfoOutputs);
        }
    }
}
