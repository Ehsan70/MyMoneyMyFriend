using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace MyMoneyMyFriend
{
    /* Asp.net will look for `StartUp` class by convention. In side of this class, application is configured or the configuration sources are configured.   
     */
    public class Startup
    {
        // This will be executed before ConfigureServices and Configure. 
        public Startup(IHostingEnvironment env)
        {
            // Tell ASP.NET about the configuration file so that it can read it in. 
            // Telling the configuration builder about my base path; i.e. where to look for files by default.  
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json").AddEnvironmentVariables();
            // Build a configuration for me that will return an object that implement IConfiguration.  
            Configuration = builder.Build();    
            
        }

        public  IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. HTTP processing pipeline is configured here. 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var message = Configuration["Greeting"];
                await context.Response.WriteAsync(message);
            });
        }
    }
}
