using TConnection.Abstract;

namespace TConnection.App.ServiceAbstract;

public interface IConnectionFactoryRoot
{
    /// <summary>
    /// 创建连接对象
    /// </summary>
    /// <typeparam name="T">连接类型</typeparam>
    /// <param name="para">连接参数</param>
    /// <param name="isAutoConnect">是否自动连接</param>
    /// <returns>连接对象</returns>
    T CreateConnection<T>(IReadOnlyDictionary<string, string> para, bool isAutoConnect) where T : IConnection;
    /// <summary>
    /// 创建连接对象
    /// </summary>
    /// <param name="connectionType">连接类型</param>
    /// <param name="para">连接参数</param>
    /// <param name="isAutoConnect">是否自动连接</param>
    /// <returns>连接对象</returns>
    public IConnection CreateConnection(Type connectionType, IReadOnlyDictionary<string, string> para,
        bool isAutoConnect);
}