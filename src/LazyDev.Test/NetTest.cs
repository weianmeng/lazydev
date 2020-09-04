using LazyDev.Utilities;
using Xunit;

namespace LazyDev.Test
{
    public class NetTest
    {
        [Fact]
        public void GetHostIp()
        {
          var ip=  NetUtility.GetHostIp();
          Assert.NotEmpty(ip);
        }
    }
}
