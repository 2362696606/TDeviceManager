using System.Collections;
using TConnection.Abstract;
using TConnection.Abstract.Models;

namespace TConnection.Recorder.Yaml
{
    public class YamlConnectionConfigRecorder : IConnectionConfigRecorder
    {
        #region 字段
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private string _yamlFilePath;
        /// <summary>
        /// 配置缓存
        /// </summary>
        private List<ConnectionConfig> _configs = new();

        #endregion

        public YamlConnectionConfigRecorder(string yamlFilePath)
        {
            this._yamlFilePath = yamlFilePath;
            this.Load();
        }

        #region 方法

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <returns>加载结果</returns>
        private void Load()
        {
            _configs = YamlHelper.ReadYaml<List<ConnectionConfig>>(_yamlFilePath);
        }

        #endregion


        #region IConnectionConfigRecorder实现

        public void Reload()
        {
            Load();
        }

        public void AddConfig(ConnectionConfig config)
        {
            this._configs.Add(config);
        }

        public void RemoveConfig(string configName)
        {
            var connectionConfig = this._configs.First(x => x.ConnectionName == configName);
            this._configs.Remove(connectionConfig);
        }

        public void SaveConfigs()
        {
            YamlHelper.SerializeToFile(_yamlFilePath, _configs);
        }

        #region IEnumerable<ConnectionConfig>实现

        public IEnumerator<ConnectionConfig> GetEnumerator()
        {
            return _configs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
        #endregion
    }
}
