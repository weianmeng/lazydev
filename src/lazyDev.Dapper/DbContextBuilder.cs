using System;

namespace lazyDev.Dapper
{
    public class DbContextBuilder
    {
        public Type DbConnectionFactory;
        public string MasterConn { get; set; }
        public string[] ReplicasConn { get; set; }
        public void SetDbFactory<T>() where T:class,IDbConnectionFactory
        {
            DbConnectionFactory = typeof(T);
        }
    }
}
