using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlaceHolder.API.Managers;
using PlaceHolder.API.Middlewares;
using PlaceHolder.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

/*
Configure Services
*/

//Enable api versionning
builder.Services.AddApiVersioning(opts =>
{
    opts.AssumeDefaultVersionWhenUnspecified = true;
    opts.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opts.ReportApiVersions = true;
});

//Force the current version of the api in the url of each controller routes
builder.Services.AddVersionedApiExplorer(opts =>
{
    opts.GroupNameFormat = "'v'VVV";
    opts.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers();

builder.Services.AddOpenApiDocument(config =>
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

builder.Services.AddApplicationCore();
builder.Services.AddAdapters(builder.Configuration);

/*
Build and configure app
*/
var app = builder.Build();

if (app.Environment.IsDevelopment())
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

/*
Apply DB Migrations
*/
app.MigrateDatabase();

/*
Ready to run
*/
app.Run();