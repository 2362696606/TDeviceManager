using Autofac;
using TConnection.App.Service;
using TConnection.App.ServiceAbstract;

namespace TConnection.App.AutofacModule;
/// <summary>
/// <see cref="IConnectionFactoryRoot"/>注册
/// </summary>
public class ConnectionFactoryRootModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ConnectionFactoryRoot>().As<IConnectionFactoryRoot>().SingleInstance();
    }
}