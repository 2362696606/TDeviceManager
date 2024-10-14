using TDevice.Managers;

namespace TDevice.Connection.Zlg;

public abstract class ZlgCanChannelBase:IDisposable
{
    ///// <summary>
    ///// <see cref="ZlgCanBoxBase"/>对象
    ///// </summary>
    //private ZlgCanBoxBase? _canBoxBase;
    /// <summary>
    /// 通道句柄
    /// </summary>
    protected IntPtr ChannelHandel { get; set; } = IntPtr.Zero;

    /// <summary>
    /// <see cref="ZlgCanBoxBase"/>对象
    /// </summary>
    public ZlgCanBoxBase? CanBox => ZlgCanBoxManager.Instance.Clients.GetValueOrDefault(CanBoxName);

    public void Dispose()
    {
        CloseChannel();
    }

    public abstract string CanBoxName { get; }
    /// <summary>
    /// 是否连接
    /// </summary>
    public bool IsConnected => ChannelHandel != IntPtr.Zero;
    /// <summary>
    /// 通道序号
    /// </summary>
    public int ChannelIndex { get; set; }
    ///// <summary>
    ///// 通道句柄
    ///// </summary>
    //public IntPtr ChannelHandel => _channelHandel;
    /// <summary>
    /// 连接
    /// </summary>
    public virtual void Connect()
    {
        InitChannel();
        StartChannel();
    }
    public virtual void CloseChannel()
    {
        if (IsConnected)
        {
            ZlgMethod.ZCAN_ResetCAN(ChannelHandel);
        }
        ChannelHandel = IntPtr.Zero;
    }
    /// <summary>
    /// 初始化Can
    /// </summary>
    protected abstract void InitChannel();
    /// <summary>
    /// 启动Can
    /// </summary>
    protected abstract void StartChannel();
    /// <summary>
    /// 复位Can
    /// </summary>
    protected abstract void ResetChannel();
}