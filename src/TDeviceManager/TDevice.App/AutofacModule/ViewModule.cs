using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace TDevice.App.AutofacModule;

/// <summary>
/// 注册View
/// </summary>
public class ViewModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(executingAssembly).Where(t => t.Name.EndsWith("View"));
    }
}