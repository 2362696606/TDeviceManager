using TConnection.Abstract;
using TConnection.Abstract.Attribute;
using TConnection.Abstract.Models;
using TConnection.Connection;

namespace TConnection.Factory;

/// <summary>
/// Visa串口连接工厂
/// </summary>
[ConnectionType(typeof(VisaSerialConnection))]
public class VisaSerialConnectionFactory: IConnectionFactory
{
    public IConnection CreateConnection(IReadOnlyDictionary<string, string> paras, bool isAutoConnection = false)
    {
        if ((!paras.ContainsKey("ResourceName") && (!paras.ContainsKey("PortName"))))
        {
            throw new InvalidOperationException("Visa串口连接必须指定ResourceName或者PortName");
        }
        
        var visaSerialConnection = new VisaSerialConnection();
        if (paras.TryGetValue("ResourceName", out var resourceName))
        {
            visaSerialConnection.ResourceName = resourceName;
        }
        if (paras.TryGetValue("BaudRate", out var baudRate))
        {
            visaSerialConnection.BaudRate = int.Parse(baudRate);
        }
        if (paras.TryGetValue("DataBits", out var dataBits))
        {
            visaSerialConnection.DataBits = short.Parse(dataBits);
        }
        if (paras.TryGetValue("StopBits", out var stopBitsStr))
        {
            if (Enum.TryParse<SerialStopBits>(stopBitsStr, out var stopBits))
            {
                visaSerialConnection.StopBits = stopBits;
            }
        }
        if (paras.TryGetValue("Parity", out var parityStr))
        {
            if (Enum.TryParse<SerialParity>(parityStr, out var parity))
            {
                visaSerialConnection.Parity = parity;
            }
        }
        if (isAutoConnection)
        {
            visaSerialConnection.Connect();
        }
        return visaSerialConnection;
    }
}