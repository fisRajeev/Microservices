using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OzNet;

namespace Example
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

       
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            HttpResponseMessage content;
            Router router = new Router("routes.json");
            app.Run(async (context) =>
            {
                content = await router.RouteRequest(context.Request);
                string result = await content.Content.ReadAsStringAsync();
                               
               await context.Response.WriteAsync(result);                
            });

           

            //app.Run(context =>
            //{
            //    context.Response.OnStarting(async () =>
            //    {
            //        //content = await router.RouteRequest(context.Request);
            //        content = await router.RouteRequest(context.Request);
            //        await context.Response.WriteAsync(await content.Content.ReadAsStringAsync());
            //    });
            //    return Task.CompletedTask;
            //});
        }
    }
}
