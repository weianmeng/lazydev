namespace lazyDev.Dapper
{
    public class ConnectionOption : IConnectionOption
    {
        public string MasterConn { get; set; }
        public string[] ReplicasConn{ get; set; }

    }

}
