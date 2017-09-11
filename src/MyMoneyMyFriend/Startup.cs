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
            // Igreeting has to be registered so that ASP.NEt knows about it. 
            // Note that There are some built-in services that ASP.NEt will provide by default. In particular, the services variables will have 16 Interfaces already configured. 
            // So once a service that implements IGreeter is registered. ASP.NEt will be able to pass that along to any component that is controlled by ASP.NET that needs it.  
            /*
             services.AddSingleton() : Add a single instans of the service. Everyone sees the same instance. 
             services.AddTransient() : Add a service with a transient life time. Anytime that service is needed by a method or component, that service is reinstantiated.   
             services.AddScoped() : Adds a service that will be scoped to HTTP request. All the components inside of a single request will see a same instance, but accross two different HTTP requests will be two different instances.  
            */
            // Since an instance of that object is available we can just pass that object. ASP.NET is smart enough to see the type of this and so matches that type to this instance.  
            services.AddSingleton(Configuration);
            //Bellow tells ASP.NET that when ever you see IGreeter, you need to instanciate and pass in the Greeter class.  
            services.AddSingleton<IGreeter, Greeter>();
            // So Greeter needs and configuration. ASP.NET realizes that Greeter needs a configuration and therefore passes Configuration instance to Greeter. 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. HTTP processing pipeline is configured here. 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IGreeter greeter)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var message = Configuration["Greeting"];
                await context.Response.WriteAsync(greeter.GetGreeting());
            });
        }
    }
}
