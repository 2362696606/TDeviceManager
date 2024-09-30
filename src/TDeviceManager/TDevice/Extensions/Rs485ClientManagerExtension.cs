using System.IO.Ports;
using TDevice.Managers;
using TDevice.Model;

namespace TDevice.Extensions;

public static class Rs485ClientManagerExtension
{
    /// <summary>
    /// 添加连接
    /// </summary>
    /// <param name="manager">管理器对象</param>
    /// <param name="connectionName">连接名</param>
    /// <param name="serialPort">串口对象</param>
    public static void AddClient(this Rs485ClientManager manager, string connectionName, SerialPort serialPort)
    {
        manager.Clients.Add(connectionName, serialPort);
    }
    /// <summary>
    /// 添加连接
    /// </summary>
    /// <param name="manager">管理器对象</param>
    /// <param name="config">连接配置</param>
    public static void AddClient(this Rs485ClientManager manager, Rs485Config config)
    {
        var serialPort = new SerialPort()
        {
            PortName = config.PortName,
            BaudRate = config.BaudRate,
            DataBits = config.DataBits,
            StopBits = config.StopBits,
            Parity = config.Parity,
        };
        manager.Clients.Add(config.ConnectionName, serialPort);
    }
    /// <summary>
    /// 移除连接
    /// </summary>
    /// <param name="manager">管理器对象</param>
    /// <param name="connectionName">连接名</param>
    /// <returns>如果成功找到并移除该元素，则为 true；否则为 false。 如果没有找到 <paramref name="connectionName"/>，则此方法返回 false。</returns>
    public static bool RemoveClient(this Rs485ClientManager manager, string connectionName)
    {
        if (manager.Clients.ContainsKey(connectionName))
        {
            manager.Clients[connectionName].Dispose();
        }

        return manager.Clients.Remove(connectionName);
    }
    public static SerialPort GetConnection(this Rs485ClientManager manager, string connectionName)
    {
        manager.Clients.TryGetValue(connectionName, out var connection);
        ArgumentNullException.ThrowIfNull(connection);
        return connection;
    }
}