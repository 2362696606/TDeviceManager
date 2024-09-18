using System.Reflection;
using System.Windows;
using Autofac;
using TConnection.Abstract;
using TConnection.Recorder.Yaml;
using Module = Autofac.Module;

namespace TConnection.App.AutofacModule;

/// <summary>
/// <see cref="IConnectionConfigRecorder"/>注册
/// </summary>
public class ConnectionConfigRecorderModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        ////扫描并注册记录器
        //var assembly = Assembly.GetAssembly(typeof(IConnectionConfigRecorder));
        //if (assembly != null)
        //{
        //    builder.RegisterAssemblyTypes(assembly).Where(x => x.IsAssignableFrom(typeof(IConnectionConfigRecorder)))
        //        .As<IConnectionConfigRecorder>();
        //}
        
        //注册Yaml记录器
        string fileType = "./Config/ConnectionsConfig.yml";
        var yamlConnectionConfigRecorder = new YamlConnectionConfigRecorder(fileType);
        builder.RegisterInstance(yamlConnectionConfigRecorder).As<IConnectionConfigRecorder>();
    }
}