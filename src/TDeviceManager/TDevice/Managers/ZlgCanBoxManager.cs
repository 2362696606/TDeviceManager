using TDevice.Connection;
using TDevice.Connection.Zlg;

namespace TDevice.Managers;

public class ZlgCanBoxManager : ManagerBase<ZlgCanBoxManager, ZlgCanBoxBase>
{
    //#region 单例实现

    //private static Lazy<ZlgCanBoxManager> _instanceLazy = new Lazy<ZlgCanBoxManager>(() => new ZlgCanBoxManager());
    //public static ZlgCanBoxManager Instance => _instanceLazy.Value;

    //#endregion

    //#region 私有构造

    //private ZlgCanBoxManager()
    //{
        
    //}

    //#endregion
    ///// <summary>
    ///// Can盒字典,key为CanIndex
    ///// </summary>
    //public Dictionary<string, ZlgCanBoxBase> ZlgCanBoxes { get; set; } = new Dictionary<string, ZlgCanBoxBase>();
}