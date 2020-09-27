using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace LazyDev.Core.Utility
{
    public static  class NetUtility
    {
        private static string _hostIpAddress;

        public static string GetHostIp()
        {
            if (!string.IsNullOrEmpty(_hostIpAddress))
            {
                return _hostIpAddress;
            }
            _hostIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList
                .FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork)?.ToString();
            return _hostIpAddress;
        }
    }
}
