using System.ServiceModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SoapCore;
using SoapService.Models;
using SoapService.Service;

namespace SoapService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<ISampleService, SampleService>();
            services.AddMvc(x => x.EnableEndpointRouting = false);
            services.AddSoapCore();
            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSoapEndpoint<ISampleService>("/Service.svc", new BasicHttpBinding(), SoapSerializer.DataContractSerializer);
            app.UseSoapEndpoint<ISampleService>("/Service.asmx", new BasicHttpBinding(), SoapSerializer.XmlSerializer);
            app.UseMvc();
        }
    }
}
