using TConnection.Abstract;

namespace TConnection.App.ServiceAbstract;

public interface IConnectionFactoryRoot
{
    /// <summary>
    /// 获取连接对象
    /// </summary>
    /// <typeparam name="T">连接类型</typeparam>
    /// <param name="para">连接参数</param>
    /// <param name="autoConnection">是否自动连接</param>
    /// <returns>连接对象</returns>
    T CreateConnection<T>(Dictionary<string, string> para, bool autoConnection) where T : IConnection;
}