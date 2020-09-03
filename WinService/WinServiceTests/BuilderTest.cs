using Domain.Builder;
using Domain.Entities;
using Xunit;

namespace WinServiceTests
{
    public class BuilderTest
    {
        [Fact]
        public void CheckServerInfoBuilder()
        {
            var director = new ServerInfoDirector();
            var builder = new ServerInfoBuilder();
            director.Builder = builder;
            director.BuildServerInfo();
            Assert.IsType<ServerInfo>(builder.GetServerInfo());
        }
    }
}
