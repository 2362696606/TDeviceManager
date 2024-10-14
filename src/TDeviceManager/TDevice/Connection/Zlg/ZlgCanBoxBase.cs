using TCommon.Extensions;

namespace TDevice.Connection.Zlg;

public abstract class ZlgCanBoxBase : IDisposable
{
    private IntPtr _deviceHandle;
    //private bool _isConnected;
    //private int _maxChannelCount;
    //private readonly List<ZlgCanChannelBase> _channels = new();

    /// <summary>
    /// 设备类型
    /// </summary>
    public abstract ZlgDeviceType DeviceType { get; }
    /// <summary>
    /// 设备序号
    /// </summary>
    public abstract int DeviceIndex { get; }
    /// <summary>
    /// 设备句柄
    /// </summary>
    public IntPtr DeviceHandle => _deviceHandle;

    /// <summary>
    /// 是否已经开启
    /// </summary>
    public bool IsConnected => _deviceHandle != IntPtr.Zero;

    /// <summary>
    /// 最大通道数
    /// </summary>
    public int MaxChannelCount
    {
        get
        {
            var channelCountAttribute = DeviceType.GetAttribute<ChannelCountAttribute>();
            return channelCountAttribute.ChannelCount;
        }
    }
    ///// <summary>
    ///// Can通道
    ///// </summary>
    //public List<ZlgCanChannelBase> Channels => _channels;

    /// <summary>
    /// 打开设备
    /// </summary>
    public virtual void OpenDevice()
    {
        _deviceHandle = ZlgMethod.ZCAN_OpenDevice((uint)DeviceType, (uint)DeviceIndex, 0);
        if (_deviceHandle == IntPtr.Zero)
        {
            throw new ArgumentException("打开设备失败,请检查设备类型和设备索引号是否正确");
        }
    }
    /// <summary>
    /// 关闭设备
    /// </summary>
    public virtual void CloseDevice()
    {
        if (_deviceHandle != IntPtr.Zero)
        {
            ZlgMethod.ZCAN_CloseDevice(_deviceHandle);
        }
    }

    public void Dispose()
    {
        if (_deviceHandle != IntPtr.Zero)
        {
            ZlgMethod.ZCAN_CloseDevice(_deviceHandle);
        }

        GC.ReRegisterForFinalize(this);
    }
}