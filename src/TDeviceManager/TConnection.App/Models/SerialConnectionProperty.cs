namespace TConnection.App.Models;

public class SerialConnectionProperty : BaseConnectionProperty
{
    /// <summary>
    /// 串口名
    /// </summary>
    public string PortName { get; set; } = string.Empty;
    /// <summary>
    /// 波特率
    /// </summary>
    public int BaudRate { get; set; } = 0;
}