namespace TDevice.Managers;

public class ManagerBase<TCurrentType,TBeManagerType>
    where TCurrentType : new() 
    where TBeManagerType:class
{
    #region 单例实现

    // ReSharper disable once InconsistentNaming
    protected static Lazy<TCurrentType> _instanceLazy = new Lazy<TCurrentType>(() => new TCurrentType());
    public static TCurrentType Instance => _instanceLazy.Value;

    #endregion

    #region 保护构造

    protected ManagerBase()
    {
        
    }

    #endregion

    #region 管理对象字典

    public Dictionary<string, TBeManagerType> Clients { get; set; } = new Dictionary<string, TBeManagerType>();

    #endregion
}