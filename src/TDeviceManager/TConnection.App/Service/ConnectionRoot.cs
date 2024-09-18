using TConnection.Abstract;
using TConnection.App.ServiceAbstract;

namespace TConnection.App.Service;

public class ConnectionRoot : IConnectionRoot
{

    #region 属性

    private readonly Dictionary<string, IConnection> _connections = new Dictionary<string, IConnection>();
    /// <summary>
    /// 连接对象集合
    /// </summary>
    public IReadOnlyDictionary<string, IConnection> Connections => _connections;

    #endregion

    #region 方法

    public IConnection GetConnection(string connectionName)
    {
        return _connections[connectionName];
    }
    
    public void RemoveConnection(string connectionName)
    {
        if (_connections.TryGetValue(connectionName, out var connection))
        {
            connection.Dispose();
            _connections.Remove(connectionName);
        }
    }

    public void AddConnection(string connectionName, IConnection connection)
    {
        _connections[connectionName] = connection;
    }

    #endregion

}