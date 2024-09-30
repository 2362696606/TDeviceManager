using Autofac;
using TConnection.App.Service;
using TConnection.App.ServiceAbstract;

namespace TConnection.App.AutofacModule;

/// <summary>
/// <see cref="IConnectionRoot"/>注册
/// </summary>
public class ConnectionRootModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ConnectionRoot>().As<IConnectionRoot>().SingleInstance();
    }
}