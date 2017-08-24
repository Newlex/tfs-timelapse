using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Timelase.Core;
using Timelase.TFS;

namespace Timelase.Web
{
    public class Startup
    {
	    public Startup(IHostingEnvironment env)
	    {
		    var builder = new ConfigurationBuilder()
			    .SetBasePath(env.ContentRootPath)
			    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			    .AddEnvironmentVariables();
		    Configuration = builder.Build();
	    }

	    public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
	        var tfsSettings = Configuration.GetSection("TFS").Get<TfsSettings>();
            services.AddSingleton(tfsSettings);
            services.AddTransient<ITfsRepository, TfsRepository>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
             !Path.HasExtension(context.Request.Path.Value) &&
             !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });
            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
