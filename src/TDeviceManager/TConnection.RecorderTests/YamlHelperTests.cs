using System.Globalization;
using TConnection.Abstract.Models;
using TConnection.Recorder.Yaml;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

// ReSharper disable once CheckNamespace
namespace TConnection.Recorder.Tests
{
    public class YamlHelperTests(ITestOutputHelper output)
    {
        private readonly ITestOutputHelper _output = output;

        [Fact(DisplayName = "序列化为字符串")]
        public void SerializeTest()
        {
            var connectionConfigs = new List<ConnectionConfig>
            {
                new ConnectionConfig()
                {
                    ConnectionName = "Connection1",
                    ConnectionType = typeof(YamlHelper),
                    Paras = new Dictionary<string, string>()
                        { { "para1", "value1" }, { "para2",2.ToString() }, { "para3", DateTime.Now.ToString(CultureInfo.InvariantCulture) } }
                },
                new ConnectionConfig()
                {
                    ConnectionName = "Connection2",
                    ConnectionType = typeof(YamlHelper),
                    Paras = new Dictionary<string, string>()
                        { { "para1", "value1" }, { "para2",2.ToString() }, { "para3", DateTime.Now.ToString(CultureInfo.InvariantCulture) } }
                }
            };
            var serialize = YamlHelper.Serialize(connectionConfigs);
            _output.WriteLine(serialize);
        }

        [Fact(DisplayName = "序列化到文件")]
        public void WriteToYamlTest()
        {
            string filePath = @"./TestYaml.yml";
            var connectionConfigs = new List<ConnectionConfig>
            {
                new ConnectionConfig()
                {
                    ConnectionName = "Connection1",
                    ConnectionType = typeof(YamlHelper),
                    Paras = new Dictionary<string, string>()
                        { { "para1", "value1" }, { "para2",2.ToString() }, { "para3", DateTime.Now.ToString(CultureInfo.InvariantCulture) } }
                },
                new ConnectionConfig()
                {
                    ConnectionName = "Connection2",
                    ConnectionType = typeof(YamlHelper),
                    Paras = new Dictionary<string, string>()
                        { { "para1", "value1" }, { "para2",2.ToString() }, { "para3", DateTime.Now.ToString(CultureInfo.InvariantCulture) } }
                }
            };
            var writeToYaml = YamlHelper.WriteToYaml(filePath, connectionConfigs);
            Assert.True(writeToYaml);
        }

        [Fact(DisplayName = "从文件反序列化")]
        public void ReadYamlTest()
        {
            string filePath = @"./TestYaml.yml";
            var connectionConfigs = YamlHelper.ReadYaml<List<ConnectionConfig>>(filePath);
            _output.WriteLine(connectionConfigs.ToString());
            Assert.NotNull(connectionConfigs);
        }
    }
}