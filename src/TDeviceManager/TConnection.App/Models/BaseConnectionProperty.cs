namespace TConnection.App.Models;

public class BaseConnectionProperty
{
    /// <summary>
    /// 是否连接
    /// </summary>
    public bool IsConnected { get; set; }

    /// <summary>
    /// 连接类型
    /// </summary>
    public Type ConnectionType { get; set; } = typeof(object);
}