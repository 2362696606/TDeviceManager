using TCommon.Extensions;
using TDevice.Dto;

namespace TDevice.DevicesAbstract;

// ReSharper disable once InconsistentNaming
public abstract class DD064:ModbusDevice
{
    /// <summary>
    /// 从站Id
    /// </summary>
    public int SlaveId { get; set; }
    /// <summary>
    /// 控制负载输入开关
    /// </summary>
    /// <param name="channel">负载通道(1-4)</param>
    /// <param name="open"><value>true</value>:开启;<value>false</value>:关闭</param>
    public virtual void SetLoadOn(int channel,bool open)
    {
        ArgumentNullException.ThrowIfNull(ModbusMaster);
        var readHoldingRegisters = ModbusMaster.ReadHoldingRegisters((byte)SlaveId, 0, 1);
        var readHoldingRegister = readHoldingRegisters[0];
        var boolByIndex = readHoldingRegister.SetBoolByIndex(channel - 1, open);
        ModbusMaster.WriteSingleRegister((byte)SlaveId, 0, boolByIndex);
    }
    /// <summary>
    /// 获取负载输入开关
    /// </summary>
    /// <param name="channel">负载通道(1-4)</param>
    /// <returns><value>true</value>:开启;<value>false</value>:关闭</returns>
    public virtual bool GetLoadOn(int channel)
    {
        ArgumentNullException.ThrowIfNull(ModbusMaster);
        var readHoldingRegisters = ModbusMaster.ReadHoldingRegisters((byte)SlaveId, 0, 1);
        var readHoldingRegister = readHoldingRegisters[0];
        var boolByIndex = readHoldingRegister.GetBoolByIndex(channel - 1);
        return boolByIndex;
    }
    /// <summary>
    /// 设置负载模式
    /// </summary>
    /// <param name="channel">负载通道(1-4)</param>
    /// <param name="mode">负载模式</param>
    public virtual void SetLoadMode(int channel, LoadMode mode)
    {
        ArgumentNullException.ThrowIfNull(ModbusMaster);
        int startAddress = (channel - 1) * 8 + 1;
        int modeValue = mode == LoadMode.CV ? 1 : 0;
        ModbusMaster.WriteSingleRegister((byte)SlaveId, (ushort)startAddress, (ushort)modeValue);
    }
    /// <summary>
    /// 获取负载模式
    /// </summary>
    /// <param name="channel">负载通道(1-4)</param>
    public virtual LoadMode GetLoadMode(int channel)
    {
        ArgumentNullException.ThrowIfNull(ModbusMaster);
        int startAddress = (channel - 1) * 8 + 1;
        var readHoldingRegisters = ModbusMaster.ReadHoldingRegisters((byte)SlaveId, (ushort)startAddress, 1);
        var readHoldingRegister = readHoldingRegisters[0];
        return readHoldingRegister == 0 ? LoadMode.CC : LoadMode.CV;
    }
    /// <summary>
    /// 设置负载值
    /// CV模式下为电压，单位(V)
    /// CC模式下为电流，单位(A)
    /// </summary>
    /// <param name="channel">负载通道(1-4)</param>
    /// <param name="value">负载值</param>
    public virtual void SetLoadValue(int channel, float value)
    {
        ArgumentNullException.ThrowIfNull(ModbusMaster);
        int startAddress = (channel - 1) * 8 + 2;
        int setValue = EncodeFloat(value);
        ModbusMaster.WriteSingleRegister((byte)SlaveId, (ushort)startAddress, (ushort)setValue);
    }
    /// <summary>
    /// 获取当前设置的负载值
    /// CV模式下为电压，单位(V)
    /// CC模式下为电流，单位(A)
    /// </summary>
    /// <param name="channel">负载通道(1-4)</param>
    /// <returns>负载值</returns>
    public virtual float GetLoadValue(int channel)
    {
        ArgumentNullException.ThrowIfNull(ModbusMaster);
        int startAddress = (channel - 1) * 8 + 2;
        var readHoldingRegisters = ModbusMaster.ReadHoldingRegisters((byte)SlaveId, (ushort)startAddress, 1);
        var readHoldingRegister = readHoldingRegisters[0];
        return DecodeInt(readHoldingRegister);
    }
    /// <summary>
    /// 获取通道状态码
    /// </summary>
    /// <param name="channel">负载通道(1-4)</param>
    /// <returns>状态码</returns>
    public virtual int GetStatusCode(int channel)
    {
        ArgumentNullException.ThrowIfNull(ModbusMaster);
        int startAddress = (channel - 1) * 3;
        var readHoldingRegisters = ModbusMaster.ReadInputRegisters((byte)SlaveId, (ushort)startAddress, 1);
        var readHoldingRegister = readHoldingRegisters[0];
        return readHoldingRegister;
    }
    /// <summary>
    /// 获取负载输入电流
    /// </summary>
    /// <param name="channel">通道(1-4)</param>
    /// <returns>输入电流</returns>
    public virtual float GetInputCurrent(int channel)
    {
        ArgumentNullException.ThrowIfNull(ModbusMaster);
        int startAddress = (channel - 1) * 3 + 1;
        var readInputRegisters = ModbusMaster.ReadInputRegisters((byte)SlaveId, (ushort)startAddress, 1);
        var readInputRegister = readInputRegisters[0];
        return DecodeInt(readInputRegister);
    }
    /// <summary>
    /// 获取负载输入电压
    /// </summary>
    /// <param name="channel">通道(1-4)</param>
    /// <returns>输入电压</returns>
    public virtual float GetInputVoltage(int channel)
    {
        ArgumentNullException.ThrowIfNull(ModbusMaster);
        int startAddress = (channel - 1) * 3 + 2;
        var readInputRegisters = ModbusMaster.ReadInputRegisters((byte)SlaveId, (ushort)startAddress, 1);
        var readInputRegister = readInputRegisters[0];
        return DecodeInt(readInputRegister);
    }
    /// <summary>
    /// 获取风扇状态
    /// </summary>
    /// <returns>风扇状态,<value>true</value>:风扇启动;<value>false</value>:风扇停止</returns>
    public virtual bool GetFanOn()
    {
        ArgumentNullException.ThrowIfNull(ModbusMaster);
        int startAddress = 12;
        var readInputRegisters = ModbusMaster.ReadInputRegisters((byte)SlaveId, (ushort)startAddress, 1);
        var readInputRegister = readInputRegisters[0];
        return readInputRegister != 0;
    }
    /// <summary>
    /// 获取散热器温度
    /// </summary>
    /// <param name="radiatorIndex">散热器序号(1-2)</param>
    /// <returns>温度值</returns>
    public virtual float GetRadiatorTemp(int radiatorIndex)
    {
        ArgumentNullException.ThrowIfNull(ModbusMaster);
        int startAddress = radiatorIndex + 12;
        var readInputRegisters = ModbusMaster.ReadInputRegisters((byte)SlaveId, (ushort)startAddress, 1);
        var readInputRegister = readInputRegisters[0];
        return (float)(readInputRegister * 0.1);
    }
    /// <summary>
    /// 按照协议的float表示方法编码为int值
    /// </summary>
    /// <param name="value">实际值</param>
    /// <returns>编码值</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    protected int EncodeFloat(float value)
    {
        int k = 0;

        if (value <= 0.01)
        {
            return 0;
        }

        while (value < 1000)
        {
            value *= 10;
            k++;
        }
        int v = (int)(value + k * 10000);

        return v;
    }
    /// <summary>
    /// 按照协议的float表示方法解码为float值
    /// </summary>
    /// <param name="value">编码值</param>
    /// <returns>实际值</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    protected float DecodeInt(int value)
    {
        if (value < 10000)
        {
            return 0;
        }

        int k = value / 10000;
        int c = value % 10000;
        float kRate = (float)Math.Pow(10, 0 - k);

        return kRate * c;
    }
}