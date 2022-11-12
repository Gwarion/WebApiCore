using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlaceHolder.API.Middlewares;
using PlaceHolder.API.Version;
using PlaceHolder.DependencyInjection;

namespace PlaceHolder.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        
        /// <summary>
        /// All method named 'Configure____Services' get called through reflection by the runtime
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(option => option.Conventions.Add(new VersionByNamespaceConvention()));

            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = ".NET Core 6.0 WebApi";
                    document.Info.Description = "Uses : CQRS (Mediatr pipeline), Hexagonal Architecture and DDD";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Geoffrey Warion",
                        Email = "geoffrey.warion@live.fr",
                    };
                };
            });

            services.AddApplicationCore().AddAdapters(Configuration);
        }

        /// <summary>
        /// Gets called by the runtime
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.Use((context, next) => next.Invoke());
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi().UseSwaggerUi3(config =>
            {
                config.EnableTryItOut = true;
            });
        }
    }
}
