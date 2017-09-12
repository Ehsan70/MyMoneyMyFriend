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
            // IGreeting has to be registered so that ASP.NEt knows about it. 
            // Note that There are some built-in services that ASP.NEt will provide by default. In particular, the services variables will have 16 Interfaces already configured. 
            // So once a service that implements IGreeter is registered. ASP.NEt will be able to pass that along to any component that is controlled by ASP.NET that needs it.  
            /*
             services.AddSingleton() : Add a single instance of the service. Everyone sees the same instance. 
             services.AddTransient() : Add a service with a transient life time. Anytime that service is needed by a method or component, that service is re-instantiated.   
             services.AddScoped() : Adds a service that will be scoped to HTTP request. All the components inside of a single request will see a same instance, but across two different HTTP requests will be two different instances.  
            */
            // Since an instance of that object is available we can just pass that object. ASP.NET is smart enough to see the type of this and so matches that type to this instance.  
            services.AddSingleton(Configuration);
            //Bellow tells ASP.NET that when ever you see IGreeter, you need to instantiate and pass in the Greeter class.  
            services.AddSingleton<IGreeter, Greeter>();
            // So Greeter needs and configuration. ASP.NET realizes that Greeter needs a configuration and therefore passes Configuration instance to Greeter. 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. HTTP processing pipeline is configured here. 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IGreeter greeter)
        {
            /////////////////////// Logger Middleware /////////////////////// 
            loggerFactory.AddConsole();

            /////////////////////// Exception Middleware for Production and Development /////////////////////// 
            //Developer exception page: This is only installed when we are in development mode and not in production mode.
            // This will catch any unhandled exception that happens during the process of request. It shows stack trace. 
            if (env.IsDevelopment())
            {
                // Because this middleware is really for developers. So it should be shown in the development environment only.  
                // This middleware only care about the response. It's looking for unhanded exceptions that occurred somewhere else down the line. This should be installed first.
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/error"); // This will re-execute my request pipeline using this new path, so that my application can respond in a different way. 
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    // Request Delegate is a method that takes http context as a parameter and returns a task.
                    ExceptionHandler = context => context.Response.WriteAsync("Ops!") // This means given an http context, please evaluate the following expression.  
                }); // This 
            }

            /////////////////////// Static Files Middleware /////////////////////// 
            // app.UseDefaultFiles(); // Look at the incoming requests and sees if there is a default file that matches that request. Default File names : Index.html, etc 
            // app.UseStaticFiles(); // By default it will look for files on the file system in the web root folder
            app.UseFileServer(); // his combines both of UseStaticFiles and UseDefaultFiles. You have directory browsing

            /////////////////////// WelcomePage Middleware /////////////////////// 
            // app.UseWelcomePage(); // When no argument is passed to the USeWelcomePage, it becomes a terminal piece of middle-ware i.e. when a request reaches this middleware, it will not continue to any other middleware. So the order of middlewares matter
            // app.UseWelcomePage("/welcome"); //"/welcome" would the path that this middleware will respond. 
            app.UseWelcomePage(new WelcomePageOptions
            {
                Path = "/welcome"
            }); // Most of the middleware have an options object. WelcomePageOptions has only path property 

            // Run is not used a lot in production applications. It is mostly used for demos and for debugging. Runs allows us to install a really low level middleware  
            /* Run Features: 
             * 1. It receive access to http context. So the method bellow that is setup with lambda expression is invoked for every http request. 
             *    ASP.NET will set up an http context object for every request and passes it in to this middleware.
             */ 
            app.Run(async (context) =>
            {
                // Note that the context object will give you access to information regarding the request and response. 
                var message = Configuration["Greeting"];
                await context.Response.WriteAsync(greeter.GetGreeting());
            });
        }
    }
}
