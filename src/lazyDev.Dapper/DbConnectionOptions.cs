using System.Collections.Generic;

namespace lazyDev.Dapper
{
    public class DbConnectionOptions
    {
         public List<DbConnectionConfig> DbConnectionConfigs { get; set; }
           
    }

    public class DbConnectionConfig
    {
        public string DbName { get; set; }
        public string MasterConn { get; set; }
        public List<string> ReplicaConns { get; set; }
    }
}
