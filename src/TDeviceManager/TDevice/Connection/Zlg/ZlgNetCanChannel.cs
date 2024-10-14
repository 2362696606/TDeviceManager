using System.Text;

namespace TDevice.Connection.Zlg;

public abstract class ZlgNetCanChannel:ZlgCanChannelBase
{
    protected ZlgNetCanChannel(string canBoxName)
    {
        CanBoxName = canBoxName;
    }

    public override string CanBoxName { get; }

    /// <summary>
    /// Ip地址
    /// </summary>
    public string RemoteAddress { get; set; } = string.Empty;
    /// <summary>
    /// 端口号
    /// </summary>
    public int RemotePort { get; set; }
    /// <summary>
    /// 本地端口
    /// </summary>
    public int LocalPort { get; set; }
    /// <summary>
    /// 设置本地端口
    /// </summary>
    /// <returns>
    /// <para>true:成功</para>
    /// <para>false:失败</para>
    /// </returns>
    protected bool SetLocalPort()
    {
        ArgumentNullException.ThrowIfNull(CanBox);
        string path = ChannelIndex + "/local_port";
        uint ret = ZlgMethod.ZCAN_SetValue(CanBox.DeviceHandle, path, Encoding.ASCII.GetBytes(LocalPort.ToString()));
        return 1 == ret;
    }
    /// <summary>
    /// 设置远程地址
    /// </summary>
    /// <returns>
    /// <para>true:成功</para>
    /// <para>false:失败</para>
    /// </returns>
    protected bool SetRemoteAddress()
    {
        ArgumentNullException.ThrowIfNull(CanBox);
        string path = ChannelIndex + "/ip";
        string value = RemoteAddress;
        return 1 == ZlgMethod.ZCAN_SetValue(CanBox.DeviceHandle, path, Encoding.ASCII.GetBytes(value));
    }
    /// <summary>
    /// 设置远程端口
    /// </summary>
    /// <returns>
    /// <para>true:成功</para>
    /// <para>false:失败</para>
    /// </returns>
    protected bool SetRemotePort()
    {
        ArgumentNullException.ThrowIfNull(CanBox);
        string path = ChannelIndex + "/work_port";
        string value = RemotePort.ToString();
        return 1 == ZlgMethod.ZCAN_SetValue(CanBox.DeviceHandle, path, Encoding.ASCII.GetBytes(value));
    }
}