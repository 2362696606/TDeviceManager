using Xunit;
using TDevice.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TDevice.Extensions.Tests
{
    public class TimeStampExtensionTests
    {
        private readonly ITestOutputHelper _output;

        public TimeStampExtensionTests(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact()]
        public void GetCurrentTimeStampTest()
        {
            //Xunit.Assert.Fail("This test needs an implementation");
            var currentTimeStamp = TimeStampExtension.NowTimeStamp;
            _output.WriteLine($"CurrentTimeStamp:{currentTimeStamp}");
            var dateTime = currentTimeStamp.ToDateTime();
            _output.WriteLine($"ToDateTime:{dateTime:O}");
            var timeStamp = dateTime.ToTimeStamp();
            _output.WriteLine($"ToTimeStamp:{timeStamp}");

            var nowDateTime = DateTime.Now;
            _output.WriteLine($"NowDateTime:{nowDateTime:O}");
            var nowTimeStamp = nowDateTime.ToTimeStamp();
            _output.WriteLine($"NowTimeStamp:{nowTimeStamp}");
            var time = nowTimeStamp.ToDateTime();
            _output.WriteLine($"NowToDateTime:{time:O}");
            var stamp = time.ToTimeStamp();
            _output.WriteLine($"AgainCoverToTimeStamp:{stamp}");
            var dateTime1 = stamp.ToDateTime();
            _output.WriteLine($"AgainCoverToDateTime:{dateTime1:O}");
        }
    }
}