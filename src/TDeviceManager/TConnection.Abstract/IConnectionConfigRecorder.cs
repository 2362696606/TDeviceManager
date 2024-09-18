using TCommon;
using TConnection.Abstract.Models;

namespace TConnection.Abstract;

/// <summary>
/// 连接配置记录器
/// </summary>
public interface IConnectionConfigRecorder : IEnumerable<ConnectionConfig>
{
    /// <summary>
    /// 重新加载
    /// </summary>
    void Reload();
    /// <summary>
    /// 添加配置
    /// </summary>
    /// <param name="config"></param>
    void AddConfig(ConnectionConfig config);
}