namespace TConnection.App.Models;

public class NetConnectionProperty : BaseConnectionProperty
{
    /// <summary>
    /// Ip
    /// </summary>
    public string Ip { get; set; } = string.Empty;
    /// <summary>
    /// 端口
    /// </summary>
    public int Port { get; set; }
}