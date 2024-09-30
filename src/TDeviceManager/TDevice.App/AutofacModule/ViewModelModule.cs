using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace TDevice.App.AutofacModule;

/// <summary>
/// 注册ViewModel
/// </summary>
public class ViewModelModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(executingAssembly).Where(t => t.Name.EndsWith("ViewModel"));
    }
}