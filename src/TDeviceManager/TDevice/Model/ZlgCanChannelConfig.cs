using TDevice.Connection;
using TDevice.Connection.Zlg;

namespace TDevice.Model;
/// <summary>
/// <see cref="ZlgCanChannelBase"/>配置
/// </summary>
public class ZlgCanChannelConfig
{
    /// <summary>
    /// 通道名称
    /// </summary>
    public string ChannelName { get; set; } = string.Empty;
    /// <summary>
    /// <see cref="ZlgCanBoxBase"/>名称
    /// </summary>
    public string CanBoxName { get; set; } = string.Empty;
    /// <summary>
    /// 通道序号
    /// </summary>
    public int ChannelIndex { get; set; }
    /// <summary>
    /// Ip地址
    /// </summary>
    public string IpAddress { get; set; } = string.Empty;
    /// <summary>
    /// 端口号
    /// </summary>
    public int Port { get; set; }

}