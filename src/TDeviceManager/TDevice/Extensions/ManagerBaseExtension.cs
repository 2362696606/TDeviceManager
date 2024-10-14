using System.IO.Ports;
using TDevice.Managers;
using TDevice.Model;

namespace TDevice.Extensions;

public static class ManagerBaseExtension
{
    /// <summary>
    /// 将被管理对象添加到管理器中
    /// </summary>
    /// <param name="manager">管理器对象</param>
    /// <param name="connectionName">连接名</param>
    /// <param name="client">被管理对象</param>
    public static void AddClient<TCurrentType, TBeManagerType>(this ManagerBase<TCurrentType, TBeManagerType> manager,
        string connectionName, TBeManagerType client)
        where TCurrentType : new()
        where TBeManagerType : class
    {
        manager.Clients.Add(connectionName, client);
    }
    /// <summary>
    /// 移除连接
    /// </summary>
    /// <param name="manager">管理器对象</param>
    /// <param name="clientName">管理对象名</param>
    /// <returns>如果成功找到并移除该元素，则为 true；否则为 false。 如果没有找到 <paramref name="clientName"/>，则此方法返回 false。</returns>
    public static bool RemoveClient<TCurrentType, TBeManagerType>(this ManagerBase<TCurrentType, TBeManagerType> manager, string clientName)
        where TCurrentType : new()
        where TBeManagerType : class
    {
        if (manager.Clients.ContainsKey(clientName) && manager.Clients[clientName] is IDisposable disposable)
        {
            disposable.Dispose();
        }
        return manager.Clients.Remove(clientName);
    }
    /// <summary>
    /// 获取管理对象
    /// </summary>
    /// <typeparam name="TCurrentType">当前管理器类型</typeparam>
    /// <typeparam name="TBeManagerType">被管理类型</typeparam>
    /// <param name="manager">管理器对象</param>
    /// <param name="clientName">被管理对象名</param>
    /// <returns>被管理对象</returns>
    public static TBeManagerType GetConnection<TCurrentType, TBeManagerType>(this ManagerBase<TCurrentType, TBeManagerType> manager, string clientName)
        where TCurrentType : new()
        where TBeManagerType : class
    {
        manager.Clients.TryGetValue(clientName, out var client);
        ArgumentNullException.ThrowIfNull(client);
        return client;
    }
}