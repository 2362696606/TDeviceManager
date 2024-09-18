using System.IO.Ports;

namespace TConnection.Abstract;

public interface IConnection:IDisposable
{
    /// <summary>
    /// 获取当前连接是否已经连接
    /// </summary>
    bool IsConnected {get; }
    /// <summary>
    /// 连接
    /// </summary>
    void Connect();
    /// <summary>
    /// 断开连接
    /// </summary>
    public void DisConnect();
}