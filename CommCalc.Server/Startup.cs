using CommCalc.Contracts;
using CommCalc.Server.Services;
using CoreWCF;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace CommCalc.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
                options.AllowSynchronousIO = true);
            services.AddServiceModelServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment _)
        {
            app.UseServiceModel(builder =>
            {
                builder.AddService<CalcService>();
                builder.AddServiceEndpoint<CalcService, ICalcService>(new BasicHttpBinding(), "/basichttp");
                builder.AddServiceEndpoint<CalcService, ICalcService>(new NetTcpBinding(SecurityMode.None), "/nettcp");
            });
        }
    }
}
