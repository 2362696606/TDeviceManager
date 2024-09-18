namespace TConnection.Abstract.Attribute;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ConnectionTypeAttribute(Type type) : System.Attribute
{
    /// <summary>
    /// 连接类型
    /// </summary>
    public Type Type { get; } = type;
}