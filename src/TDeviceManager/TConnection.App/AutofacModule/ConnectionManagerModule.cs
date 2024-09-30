using Autofac;
using TConnection.App.Service;
using TConnection.App.ServiceAbstract;

namespace TConnection.App.AutofacModule;
/// <summary>
/// <see cref="IConnectionManager"/>注册
/// </summary>
public class ConnectionManagerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ConnectionManager>().As<IConnectionManager>().SingleInstance();
    }
}