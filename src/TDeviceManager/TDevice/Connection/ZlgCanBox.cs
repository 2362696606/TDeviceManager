namespace TDevice.Connection;

public class ZlgCanBox
{
    private IntPtr _deviceHandle;
    private bool _isConnected;
    private readonly int _maxChannelCount;

    /// <summary>
    /// 设备类型
    /// </summary>
    public int DeviceType { get; set; }
    /// <summary>
    /// 设备序号
    /// </summary>
    public int DeviceIndex { get; set; }
    /// <summary>
    /// 设备句柄
    /// </summary>
    public IntPtr DeviceHandle => _deviceHandle;
    /// <summary>
    /// 是否已经开启
    /// </summary>
    public bool IsConnected => _isConnected;

    /// <summary>
    /// 最大通道数
    /// </summary>
    public int MaxChannelCount => _maxChannelCount;

    /// <summary>
    /// 打开设备
    /// </summary>
    public void OpenDevice()
    {

    }
}