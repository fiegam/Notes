using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Notes.CoreWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            RegisterNodeModulesProvider(app, env);
            //RegisterLibsProvider(app, env);
            //RegisterNestedNodeModulesProvider(app, env);
            //RegisterAppFilesProvider(app, env);
            app.UseDefaultFiles();

            app.Use(async (context, next) =>
            {
                await next();

                //if (context.Response.StatusCode == 404
                //    && !Path.HasExtension(context.Request.Path.Value))
                //{
                //    context.Request.Path = "/index.html";
                //    await next();
                //}
            });

            app.UseStaticFiles();
        }

        private static void RegisterNodeModulesProvider(IApplicationBuilder app, IHostingEnvironment env)
        {
            var provider = new PhysicalFileProvider(
    Path.Combine(env.ContentRootPath, "node_modules")
);
            var options = new FileServerOptions();
            options.RequestPath = "/node_modules";
            options.StaticFileOptions.FileProvider = provider;
            options.EnableDirectoryBrowsing = true;
            app.UseFileServer(options);
        }

        private static void RegisterLibsProvider(IApplicationBuilder app, IHostingEnvironment env)
        {
            var provider = new PhysicalFileProvider(
    Path.Combine(env.ContentRootPath, "node_modules")
);
            var options = new FileServerOptions();
            options.RequestPath = "/libs";
            options.StaticFileOptions.FileProvider = provider;
            options.EnableDirectoryBrowsing = true;
            app.UseFileServer(options);
        }

        private static void RegisterNestedNodeModulesProvider(IApplicationBuilder app, IHostingEnvironment env)
        {
            var provider = new PhysicalFileProvider(
    Path.Combine(env.ContentRootPath, "node_modules")
);
            var options = new FileServerOptions();
            options.RequestPath = "/node_modules/angular-oauth2-oidc/node_modules";
            options.StaticFileOptions.FileProvider = provider;
            options.EnableDirectoryBrowsing = true;
            app.UseFileServer(options);
        }

        private void RegisterAppFilesProvider(IApplicationBuilder app, IHostingEnvironment env)
        {
            var provider = new PhysicalFileProvider(
Path.Combine(env.ContentRootPath, "wwwroot/app")
);
            var options = new FileServerOptions();
            options.RequestPath = "";
            options.StaticFileOptions.FileProvider = provider;
            options.EnableDirectoryBrowsing = true;
            app.UseFileServer(options);
        }
    }
}