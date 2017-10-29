using Microsoft.Extensions.FileProviders;
using System.IO;

// Typically you dont add classes to a name space that is controller by some other thirdparty like Microsoft.
// It is common that when you write extension method for a class you put that extension into the same namespace. 
namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicaitonBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, string root)
        {
            var path = Path.Combine(root, "node_modules");
            // The PhysicalFileProvider is a class designed to work with the file system 
            var provider = new PhysicalFileProvider(path);
            var options = new StaticFileOptions();
            // Try to respond if the request path starts with node_module. The is the path requested in the URL
            options.RequestPath = "/node_modules";
            // This is the physical path that files are provided.
            options.FileProvider = provider;
            // Based on the options object, the file server is created.
            app.UseStaticFiles(options);
            return app;
        }
    }
}
