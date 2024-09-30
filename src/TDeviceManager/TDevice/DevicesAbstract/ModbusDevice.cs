using NModbus;
using TDevice.Interface;

namespace TDevice.DevicesAbstract;

public abstract class ModbusDevice:IDevice
{
    protected IModbusMaster? ModbusMaster;

    public virtual void Dispose()
    {
        ModbusMaster?.Dispose();
        GC.SuppressFinalize(this);
    }

    public virtual bool IsConnected => ModbusMaster != null;
    public abstract void Connect();
    public abstract void DisConnect();
}