using System.Reflection;
using TConnection.Abstract;
using TConnection.Abstract.Attribute;

namespace TConnection.App.Service;

public class ConnectionFactoryRoot(IEnumerable<IConnectionFactory> factories)
{
    #region 方法

    public T CreateConnection<T>(Dictionary<string,string> para,bool autoConnection) where T:IConnection
    {
        var connectionType = typeof(T);

        var connectionFactory = factories.FirstOrDefault((x) =>
        {
            var connectionTypeAttribute = x.GetType().GetCustomAttribute<ConnectionTypeAttribute>();
            return connectionTypeAttribute != null && connectionTypeAttribute.Type == connectionType;
        });
        if (connectionFactory != null)
        {
            var connection = connectionFactory.CreateConnection(para, autoConnection);
            if (connection is T tConnection)
            {
                return tConnection;
            }
            throw new InvalidOperationException($"{connectionFactory.GetType()}无法创建类型{typeof(T)}的连接");
        }
        throw new InvalidOperationException($"未找到类型{connectionType}对应的工厂类");
    }

    #endregion
}