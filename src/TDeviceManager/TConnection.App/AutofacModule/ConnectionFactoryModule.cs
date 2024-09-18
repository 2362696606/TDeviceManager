using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Connections;
using Module = Autofac.Module;

namespace TConnection.App.AutofacModule;

/// <summary>
/// <see cref="IConnectionFactory"/>注册
/// </summary>
public class ConnectionFactoryModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = Assembly.GetAssembly(typeof(IConnectionFactory));
        if (assembly != null)
        {
            builder.RegisterAssemblyTypes(assembly).Where(t => t.IsAssignableFrom(typeof(IConnectionFactory)))
                .As<IConnectionFactory>();
        }
    }
}