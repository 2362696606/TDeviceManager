
using NModbus;
using NModbus.Serial;
using TDevice.DevicesAbstract;
using TDevice.Extensions;
using TDevice.Managers;

namespace TDevice.DeviceImpl;
/// <summary>
/// DD064使用ModbusRtu通讯
/// </summary>
// ReSharper disable once InconsistentNaming
public class DD064ModbusRtu:DD064
{
    /// <summary>
    /// 连接名
    /// </summary>
    public string ConnectionName { get; set; } = string.Empty;

    public override void Dispose()
    {
        //ModbusMaster?.Dispose();
        GC.SuppressFinalize(this);
    }

    public override void Connect()
    {
        if (string.IsNullOrEmpty(ConnectionName))
        {
            throw new ArgumentNullException(nameof(ConnectionName));
        }
        var port = Rs485ClientManager.Instance.GetConnection(ConnectionName);
        port.Open();
        var modbusFactory = new ModbusFactory();
        ModbusMaster = modbusFactory.CreateRtuMaster(port);
    }

    public override void DisConnect()
    {
        this.ModbusMaster = null;
    }
}