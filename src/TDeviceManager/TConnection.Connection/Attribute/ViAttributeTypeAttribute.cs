namespace TConnection.Connection.Attribute;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class ViAttributeTypeAttribute(Type type) : System.Attribute
{
    public Type AttributeType { get; set; } = type;
}