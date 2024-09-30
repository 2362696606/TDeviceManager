using System.IO.Ports;

namespace TDevice.Model;
/// <summary>
/// 串口配置
/// </summary>
public class SerialConfig
{
    /// <summary>
    /// 串口名
    /// </summary>
    public string PortName { get; set; } = "Com1";

    /// <summary>
    /// 波特率
    /// </summary>
    public int BaudRate { get; set; } = 9600;

    /// <summary>
    /// 数据位
    /// </summary>
    public int DataBits { get; set; } = 8;

    /// <summary>
    /// 停止位
    /// </summary>
    public StopBits StopBits { get; set; } = StopBits.One;

    /// <summary>
    /// 奇偶校验位
    /// </summary>
    public Parity Parity { get; set; } = Parity.None;
}