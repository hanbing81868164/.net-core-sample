using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NCore;
using System.IO;

namespace sample01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // 添加端口配置文件
                .AddJsonFile("hosting.json", true)
                .Build();

            var host = Host.CreateDefaultBuilder(args)
                // 添加Autofac作为IOC容器
                .UseServiceProviderFactory(AppServiceProvider.Instance().AutofacServiceProviderFactory)
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder
                      .UseContentRoot(Directory.GetCurrentDirectory())
                      .UseConfiguration(config)
                      .UseStartup<Startup>();
                })
                .Build();

            host.Run();
        }
    }
}
