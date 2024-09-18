namespace TConnection.Abstract;

public interface INetConnection
{
    /// <summary>
    /// Ip地址
    /// </summary>
    public string IpAddress { get; set; }
    /// <summary>
    /// 端口号
    /// </summary>
    public int Port { get; set; }
}