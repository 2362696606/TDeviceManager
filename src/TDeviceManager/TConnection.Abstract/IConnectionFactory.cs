namespace TConnection.Abstract;
/// <summary>
/// 连接工厂,务必使用<see cref="TConnection.Abstract.Attribute.ConnectionTypeAttribute"/>注明创建可创建的连接类型
/// </summary>
public interface IConnectionFactory
{
    /// <summary>
    /// 创建连接
    /// </summary>
    /// <param name="paras"></param>
    /// <param name="isAutoConnection">是否自动连接</param>
    /// <returns>连接对象</returns>
    IConnection CreateConnection(IReadOnlyDictionary<string, string> paras,bool isAutoConnection = false);
}