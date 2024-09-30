using System.ComponentModel;
// ReSharper disable InconsistentNaming

namespace TDevice.Dto;
/// <summary>
/// 负载模式
/// </summary>
public enum LoadMode
{
    /// <summary>
    /// CC模式
    /// </summary>
    [Description("CC模式")]
    CC,
    /// <summary>
    /// CV模式
    /// </summary>
    [Description("CV模式")]
    CV
}