using TConnection.Abstract;
using TConnection.Abstract.Attribute;
using TConnection.Abstract.Models;
using TConnection.Connection;

namespace TConnection.Factory
{
    [ConnectionType(typeof(SerialConnection))]
    public class SerialConnectionFactory:IConnectionFactory
    {
        public IConnection CreateConnection(IReadOnlyDictionary<string, string> paras, bool isAutoConnection = false)
        {
            var serialConnection = new SerialConnection();
            if (paras.TryGetValue("PortName", out var portName))
            {
                serialConnection.PortName = portName;
            }
            if (paras.TryGetValue("BaudRate", out var baudRateStr))
            {
                var baudRate = int.Parse(baudRateStr);
                serialConnection.BaudRate = baudRate;
            }
            if (paras.TryGetValue("DataBits",out var dataBitsStr))
            {
                var dataBits = short.Parse(dataBitsStr);
                serialConnection.DataBits = dataBits;
            }
            if (paras.TryGetValue("StopBits", out var stopBitsStr))
            {
                if (Enum.TryParse<SerialStopBits>(stopBitsStr, out SerialStopBits stopBits))
                {
                    serialConnection.StopBits = stopBits;
                }
            }
            if (paras.TryGetValue("Parity", out var parityStr))
            {
                if (Enum.TryParse<SerialParity>(parityStr,out var parity))
                {
                    serialConnection.Parity = parity;
                }
            }
            if (isAutoConnection)
            {
                serialConnection.Connect();
            }
            return serialConnection;
        }
    }
}
