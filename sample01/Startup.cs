using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NCore;
using NCore.AspNetCore.Extensions;
using NCore.Extensions.Autofac;
using NCore.Extensions.DapperEX;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace sample01
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }


        public IConfigurationRoot Configuration { get; private set; }

        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var alists = new List<Assembly>();
            alists.Add(this.GetType().GetTypeInfo().Assembly);

            builder.UseAutofac<AopInterceptor>(alists);

            var log = AppServiceProvider.Instance().Services.GetRequiredService<NCore.Logging.ILogger>();
            log.Error("测试日志功能:" + Guid.NewGuid().ToString());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            MapperHelper.CreateMap();//Map映射配置
            services.UseNCoreAspNet<NCoreAspNetOptions>(options =>
            {
                //日志配置
                options.Log4netConfig = "log4net.config";
                options.UseUpload = true;
                options.UseAnyCors = true;
                options.ApiSecurityFilter = false;
                //数据库配置
                options.DefaultDBOptions = new DefaultDBOptions
                {
                    DBSectionName = "DBConnectionSetting",
                    DefaultConnectionName = "defaultConnection"
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            this.AutofacContainer.UseAppContainerServices();

            app.UseNCoreCorsMiddleware();

            app.UseNCoreAspNetConfigure();

            app.UseRouting();
            app.UseCors("any");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }



}
