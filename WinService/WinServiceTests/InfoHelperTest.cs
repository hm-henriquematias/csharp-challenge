using Domain.Builder;
using Domain.Entities;
using WinServiceClassLib.Utils;
using Xunit;

namespace WinServiceTests
{
    public class InfoHelperTest
    {
        [Fact]
        public void CheckEnvironmentHelper()
        {
            EnvironmentHelper envHelper = new EnvironmentHelper();
            Assert.Equal("LAPTOP-GNDDVLE5", envHelper.GetName());
            Assert.Equal("192.168.99.1", envHelper.GetIp());
            Assert.Equal("Windows 10 Home Single Language - Release: 1903", envHelper.GetOsVersion());
            Assert.Equal("3.1.0", envHelper.GetVersion());
        }

        [Fact]
        public void CheckSecutiyHelper()
        {
            SecurityHelper secHelper = new SecurityHelper();
            Assert.False(secHelper.IsActiveAntivirus());
            Assert.True(secHelper.IsActiveFirewall());
        }
    }
}
