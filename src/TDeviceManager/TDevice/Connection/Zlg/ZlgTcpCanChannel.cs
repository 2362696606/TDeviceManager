using System.Runtime.InteropServices;
using System.Text;
using TDevice.Extensions;

namespace TDevice.Connection.Zlg;

public class ZlgTcpCanChannel:ZlgNetCanChannel
{
    private bool _isServer;
    private bool _isDataMerge;

    public ZlgTcpCanChannel(string canBoxName) : base(canBoxName)
    {
    }

    /// <summary>
    /// 是否开启服务器模式
    /// </summary>
    public bool IsServer
    {
        get => _isServer;
        set
        {
            if (IsConnected)
            {
                throw new InvalidOperationException("当前通道已启动，不允许修改模式");
            }
            else
            {
                _isServer = value;
            }
        }
    }

    /// <summary>
    /// 是否开启数据合并
    /// </summary>
    public bool IsDataMerge
    {
        get => _isDataMerge;
        set
        {
            if (IsConnected)
            {
                throw new InvalidOperationException("当前通道已启动，不允许修改模式");
            }
            else
            {
                _isServer = value;
            }
        }
    }
    

    protected override void InitChannel()
    {
        ArgumentNullException.ThrowIfNull(CanBox);
        try
        {
            if (!CanBox.IsTcpDevice())
            {
                throw new ArgumentException($"{nameof(CanBox)}类型\"{CanBox.DeviceType}\"无法支持当前启动当前类型通道");
            }
            SetNetMode();
            if (IsServer)
            {
                SetLocalPort();
            }
            else
            {
                SetRemoteAddress();
                SetRemotePort();
            }

            var config = new ZCanChannelInitConfig();

            IntPtr pConfig = Marshal.AllocHGlobal(Marshal.SizeOf(config));
            Marshal.StructureToPtr(config, pConfig, true);
            ChannelHandel = ZlgMethod.ZCAN_InitCAN(CanBox.DeviceHandle, (uint)ChannelIndex, pConfig);
            Marshal.FreeHGlobal(pConfig);
            if (CanBox.IsCanFdNetDevice())
            {
                if (!SetDataMerge())
                {
                    throw new ArgumentNullException($"设置数据合并失败");
                }
            }
            if (ChannelHandel == IntPtr.Zero)
            {
                throw new InvalidOperationException("初始化Can通道失败");
            }
        }
        catch (Exception e)
        {
            if (ChannelHandel != IntPtr.Zero)
            {
                ZlgMethod.ZCAN_ResetCAN(ChannelHandel);
                ChannelHandel = IntPtr.Zero;
            }
            throw;
        }
    }

    protected override void StartChannel()
    {
        ArgumentNullException.ThrowIfNull(CanBox);
        try
        {
            if (ZlgMethod.ZCAN_StartCAN(ChannelHandel) != 1)
            {
                throw new InvalidOperationException("启动Can通道失败");
            }
        }
        catch (Exception e)
        {
            if (ChannelHandel != IntPtr.Zero)
            {
                ZlgMethod.ZCAN_ResetCAN(ChannelHandel);
                ChannelHandel = IntPtr.Zero;
            }
            throw;
        }
    }

    protected override void ResetChannel()
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// 设置网络模式
    /// </summary>
    /// <returns>
    /// <para>true:成功</para>
    /// <para>false:失败</para>
    /// </returns>
    private bool SetNetMode()
    {
        ArgumentNullException.ThrowIfNull(CanBox);
        string path = ChannelIndex + "/work_mode";
        string value = IsServer ? "1" : "0";
        return 1 == ZlgMethod.ZCAN_SetValue(CanBox.DeviceHandle, path, Encoding.ASCII.GetBytes(value));
    }
    /// <summary>
    /// 设置数据合并
    /// </summary>
    /// <returns>
    /// <para>true:成功</para>
    /// <para>false:失败</para>
    /// </returns>
    private bool SetDataMerge()
    {
        ArgumentNullException.ThrowIfNull(CanBox);
        byte merge = 0;
        if (IsDataMerge) merge = 1;
        string path = ChannelIndex + "/set_device_recv_merge";
        string value = merge.ToString();
        return 1 == ZlgMethod.ZCAN_SetValue(CanBox.DeviceHandle, path, Encoding.ASCII.GetBytes(value));
    }
}