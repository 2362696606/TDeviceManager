using TCommon;

namespace TDevice.Interface;

public interface IDevice:IDisposable
{
    /// <summary>
    /// 是否已连接
    /// </summary>
    public bool IsConnected { get; }
    /// <summary>
    /// 连接
    /// </summary>
    /// <returns></returns>
    OperateResult Connect();
    /// <summary>
    /// 断开连接
    /// </summary>
    void DisConnect();
}