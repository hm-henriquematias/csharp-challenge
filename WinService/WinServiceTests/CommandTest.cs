using System;
using System.Collections.Generic;
using System.IO;
using WinServiceClassLib.Src;
using Xunit;

namespace WinServiceTests
{
    public class CommandTest
    {
        [Fact]
        public void CheckExecutionCLI()
        {
            List<string> resultList = (new Command()).Execute("pwd");
            Assert.True(String.Join("\n", resultList).Equals(Directory.GetCurrentDirectory()));
        }
    }
}
