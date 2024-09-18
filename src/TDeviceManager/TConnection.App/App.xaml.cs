using Autofac.Extensions.DependencyInjection;
using Autofac;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TConnection.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SetupWebApplication(e.Args);

            base.OnStartup(e);
        }
        private async void SetupWebApplication(string[] args)
        {
            await Task.Run(() =>
            {

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                //替换Ioc为Autofac
                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
                builder.Host.ConfigureContainer<ContainerBuilder>(ConfigureContainer);

                var app = builder.Build();

                //与CommunityToolkit.Mvvm中的Ioc容器合并
                Ioc.Default.ConfigureServices(app.Services);

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                //设置接口
                app.Urls.Add("http://localhost:5000");
                app.Urls.Add("https://localhost:5001");

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            });
        }
        /// <summary>
        /// Autofac容器注册
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyModules();
        }
    }

}
