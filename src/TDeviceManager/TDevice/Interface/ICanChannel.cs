using TDevice.Model;

namespace TDevice.Interface;

public interface ICanChannel
{
    public bool IsConnected { get; }

    /// <summary>
    /// 连接
    /// </summary>
    void Connect();
    /// <summary>
    /// 断开连接
    /// </summary>
    void Disconnect();

    void SendCanFrame(CanFrame frame);
}