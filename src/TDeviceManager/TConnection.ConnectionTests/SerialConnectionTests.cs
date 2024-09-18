using Xunit;
using TConnection.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCommon.Extensions;

namespace TConnection.Connection.Tests
{
    public class SerialConnectionTests
    {
        [Fact()]
        public void DisposeTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void ConnectTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void DisConnectTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void WriteTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void ReadBytesTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void SendAndReceivedTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void WriteTest1()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void ReadStringTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void SendAndReceivedStringTest()
        {
            Xunit.Assert.Fail("This test needs an implementation");
        }

        [Fact()]
        public void ConvertStringTest()
        {
            var bytes = new byte[] { 0x31, 0x32, 0x33, 0x0D, 0x0A };
            var hexString1 = Convert.ToHexString(bytes);
            bytes.ToHexString();

            var s = BitConverter.ToString(bytes).Replace('-', ' ');
            string test = "0A-0D";
            var hexStringToBytes = test.ToHexBytes();
        }
    }
}