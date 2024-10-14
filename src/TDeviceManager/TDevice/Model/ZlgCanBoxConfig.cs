using TDevice.Connection;
using TDevice.Connection.Zlg;

namespace TDevice.Model;
/// <summary>
/// <see cref="ZlgCanBoxBase"/>配置
/// </summary>
public class ZlgCanBoxConfig
{
    /// <summary>
    /// Can盒名称
    /// </summary>
    public string CanBoxName { get; set; } = string.Empty;
    /// <summary>
    /// Can盒类型
    /// </summary>
    public ZlgDeviceType DeviceType { get; set; }
    /// <summary>
    /// 设备序号
    /// </summary>
    public int DeviceIndex { get; set; }
}