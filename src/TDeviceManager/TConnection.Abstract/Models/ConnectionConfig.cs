using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TConnection.Abstract.Models;
/// <summary>
/// 连接配置信息
/// </summary>
public class ConnectionConfig
{
    /// <summary>
    /// 连接名
    /// </summary>
    [NotNull]
    public string? ConnectionName { get; set; }

    /// <summary>
    /// 连接类型
    /// </summary>
    [NotNull]
    public Type? ConnectionType { get; set; }

    /// <summary>
    /// 是否自动连接
    /// </summary>
    public bool IsAutoConnect { get; set; }

    /// <summary>
    /// 配置字典
    /// </summary>
    public Dictionary<string, string> Paras { get; set; } = new();
}