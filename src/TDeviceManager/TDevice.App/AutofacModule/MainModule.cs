using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace TDevice.App.AutofacModule;

/// <summary>
/// 模块注入
/// </summary>
public class MainModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterModule(builder);
    }
    /// <summary>
    /// 注册模块
    /// </summary>
    /// <param name="builder"></param>
    private void RegisterModule(ContainerBuilder builder)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyModules(executingAssembly);
    }
}