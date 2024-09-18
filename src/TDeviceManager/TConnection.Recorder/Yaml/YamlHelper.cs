using System.Text;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace TConnection.Recorder.Yaml;

public class YamlHelper
{
    /// <summary>
    /// 从yml文件反序列化
    /// </summary>
    /// <typeparam name="T">反序列化类型</typeparam>
    /// <param name="file">文件路径</param>
    /// <param name="namingConvention"></param>
    /// <returns>反序列化对象</returns>
    public static T ReadYaml<T>(string file, INamingConvention? namingConvention = null)
    {
        var target = DeserializeFromFile<T>(file, namingConvention);
        return target;
    }

    /// <summary>
    /// 从Yaml格式字符串反序列化为对象
    /// </summary>
    /// <typeparam name="T">反序列化类型</typeparam>
    /// <param name="yaml">Yaml格式化字符串</param>
    /// <param name="namingConvention">属性命名格式</param>
    /// <returns>反序列化对象</returns>
    public static T Deserialize<T>(string yaml, INamingConvention? namingConvention = null)
    {
        namingConvention ??= CamelCaseNamingConvention.Instance;
        IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(namingConvention).Build();
        return deserializer.Deserialize<T>(yaml);
    }

    /// <summary>
    /// 从Yaml文件反序列化到实体对象
    /// </summary>
    /// <typeparam name="T">反序列化类型</typeparam>
    /// <param name="filePath">文件路径</param>
    /// <param name="namingConvention"></param>
    /// <returns>反序列化对象</returns>
    public static T DeserializeFromFile<T>(string filePath, INamingConvention? namingConvention = null)
    {
        var yaml = File.ReadAllText(filePath, Encoding.UTF8);
        return Deserialize<T>(yaml, namingConvention);
    }

    /// <summary>
    /// 序列化对象到文件
    /// </summary>
    /// <typeparam name="T">序列化对象类型</typeparam>
    /// <param name="file">文件路径</param>
    /// <param name="obj">序列化对象</param>
    /// <param name="namingConvention">命名格式</param>
    /// <returns>序列化操作结果</returns>
    public static bool WriteToYaml<T>(string file, T obj, INamingConvention? namingConvention = null)
    {
        return SerializeToFile(file, obj, namingConvention);
    }

    /// <summary>
    /// 序列化对象为Yaml格式化字符串
    /// </summary>
    /// <typeparam name="T">序列化对象类型</typeparam>
    /// <param name="target">序列化对象</param>
    /// <param name="namingConvention">命名格式</param>
    /// <returns>Yaml格式化字符串</returns>
    public static string Serialize<T>(T target, INamingConvention? namingConvention = null)
    {
        namingConvention ??= CamelCaseNamingConvention.Instance;
        var serializer = new SerializerBuilder().WithNamingConvention(namingConvention).Build();
        return serializer.Serialize(target);
    }

    /// <summary>
    /// 序列化对象到文件
    /// </summary>
    /// <typeparam name="T">序列化对象类型</typeparam>
    /// <param name="filePath">序列化文件路径</param>
    /// <param name="target">序列化对象</param>
    /// <param name="namingConvention">命名格式</param>
    /// <returns>序列化操作结果</returns>
    public static bool SerializeToFile<T>(string filePath, T target, INamingConvention? namingConvention = null)
    {
        var content = Serialize(target, namingConvention);
        File.WriteAllText(filePath, content, Encoding.UTF8);
        return true;
    }
}