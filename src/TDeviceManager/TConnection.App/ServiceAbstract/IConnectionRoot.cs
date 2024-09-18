using TConnection.Abstract;

namespace TConnection.App.ServiceAbstract;

public interface IConnectionRoot
{
    /// <summary>
    /// 获取连接对象
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <returns>连接对象</returns>
    IConnection GetConnection(string connectionName);
    /// <summary>
    /// 移除连接
    /// </summary>
    /// <param name="connectionName">连接名</param>
    void RemoveConnection(string connectionName);
    /// <summary>
    /// 添加连接
    /// </summary>
    /// <param name="connectionName">连接名</param>
    /// <param name="connection">连接对象</param>
    void AddConnection(string connectionName, IConnection connection);
}