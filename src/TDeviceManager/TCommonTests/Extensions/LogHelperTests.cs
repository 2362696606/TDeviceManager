using Xunit;
using TCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Repository.Hierarchy;

namespace TCommon.Extensions.Tests
{
    public class LogHelperTests
    {
        [Fact()]
        public void GetFileLoggerByNameTest()
        {
            var log1 = LogHelper.GetFileLoggerByName("LogA");
            log1.Debug("测试日志1");
            var log2 = LogHelper.GetFileLoggerByName("LogA");
            log2.Info("测试日志2");
        }
    }
}