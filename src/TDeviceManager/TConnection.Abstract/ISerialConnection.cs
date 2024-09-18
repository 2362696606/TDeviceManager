using TConnection.Abstract.Models;

namespace TConnection.Abstract;

public interface ISerialConnection
{
    /// <summary>
    /// Com接口名
    /// </summary>
    public string PortName { get; set; }

    /// <summary>
    /// 波特率
    /// </summary>
    public int BaudRate { get; set; }

    /// <summary>
    /// 数据位
    /// </summary>
    public short DataBits { get; set; }

    /// <summary>
    /// 停止位
    /// </summary>
    public SerialStopBits StopBits { get; set; }

    /// <summary>
    /// 奇偶校验位
    /// </summary>
    public SerialParity Parity { get; set; }
}