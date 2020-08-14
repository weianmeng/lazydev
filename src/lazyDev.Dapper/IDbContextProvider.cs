using System;
using System.Collections.Generic;
using System.Text;

namespace lazyDev.Dapper
{
    public  interface IDbContextProvider
    {
        IDbContext GetDbContext(string dbName);
    }
}
