using System.IO.Ports;

namespace TDevice.Managers;

public class Rs485ClientManager
{
    #region 单例实现

    private static Lazy<Rs485ClientManager> _instanceLazy = new Lazy<Rs485ClientManager>(() => new Rs485ClientManager());
    /// <summary>
    /// 单例
    /// </summary>
    public static Rs485ClientManager Instance => _instanceLazy.Value;

    #endregion

    #region 私有构造

    private Rs485ClientManager()
    {
        
    }

    #endregion

    /// <summary>
    /// 连接字典
    /// </summary>
    public Dictionary<string, SerialPort> Clients { get; set; } = new();
}