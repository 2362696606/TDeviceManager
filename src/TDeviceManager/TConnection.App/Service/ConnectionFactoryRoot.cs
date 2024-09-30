using System.Reflection;
using TConnection.Abstract;
using TConnection.Abstract.Attribute;
using TConnection.App.ServiceAbstract;

namespace TConnection.App.Service;

public class ConnectionFactoryRoot(IEnumerable<IConnectionFactory> factories) : IConnectionFactoryRoot
{
    #region 方法

    public T CreateConnection<T>(IReadOnlyDictionary<string, string> para, bool isAutoConnect) where T : IConnection
    {
        var connectionType = typeof(T);

        var connection = CreateConnection(connectionType, para, isAutoConnect);
        if (connection is T tConnection)
        {
            return tConnection;
        }
        else
        {
            connection.Dispose();
            throw new InvalidOperationException($"无法创建类型{typeof(T)}的连接");
        }
    }

    public IConnection CreateConnection(Type connectionType, IReadOnlyDictionary<string, string> para, bool isAutoConnect)
    {
        var connectionFactory = factories.FirstOrDefault((x) =>
        {
            var connectionTypeAttribute = x.GetType().GetCustomAttribute<ConnectionTypeAttribute>();
            return connectionTypeAttribute != null && connectionTypeAttribute.Type == connectionType;
        });
        if (connectionFactory != null)
        {
            var connection = connectionFactory.CreateConnection(para, isAutoConnect);
            if (connection.GetType() == connectionType)
            {
                return connection;
            }
            else
            {
                connection.Dispose();
                throw new InvalidOperationException($"{connectionFactory.GetType()}无法创建类型{connectionType}的连接");
            }
        }
        throw new InvalidOperationException($"未找到类型{connectionType}对应的工厂类");
    }

    #endregion
}