namespace lazyDev.Dapper
{
    public interface IConnectionOption
    {
        string MasterConn { get; set; }
        string[] ReplicasConn { get; set; }
    }
}