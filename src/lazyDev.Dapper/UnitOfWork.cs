using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace lazyDev.Dapper
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDapperProxy _dapper;

        public UnitOfWork(IServiceProvider serviceProvider,IDapperProxy dapper)
        {
            _serviceProvider = serviceProvider;
            _dapper = dapper;
        }
        public T Repository<T>() where T : class, IRepository
        {
           return _serviceProvider.GetService<T>();
        }

        public async Task<int> CommitAsync()
        {
            return await _dapper.CommitAsync();
        }
    }
}
