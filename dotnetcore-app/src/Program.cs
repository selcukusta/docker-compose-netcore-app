using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Linq;

namespace DotNetCoreApp
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Error);
            app.Run(context =>
            {
                var cookies = context.Request.Cookies;
                var cookieDefinitionBuilder = new StringBuilder();
                cookies.ToList().ForEach(cookie =>
                {
                    cookieDefinitionBuilder.AppendLine($"{cookie.Key}={cookie.Value}");
                });
                return context.Response.WriteAsync(cookieDefinitionBuilder.ToString());
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .ConfigureLogging((factory) =>
                {
                    factory.AddConsole(LogLevel.Error);
                })
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}