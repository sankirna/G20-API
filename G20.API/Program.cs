using Nop.Web.Framework.Infrastructure.Extensions;
using Nop.Core.Configuration;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Configuration.AddJsonFile(NopConfigurationDefaults.AppSettingsFilePath, true, true);
if (!string.IsNullOrEmpty(builder.Environment?.EnvironmentName))
{
    var path = string.Format(NopConfigurationDefaults.AppSettingsEnvironmentFilePath, builder.Environment.EnvironmentName);
    builder.Configuration.AddJsonFile(path, true, true);
}
builder.Configuration.AddEnvironmentVariables();
builder.Services.ConfigureApplicationSettings(builder);


//load application settings
builder.Host.UseDefaultServiceProvider(options =>
{
    //we don't validate the scopes, since at the app start and the initial configuration we need 
    //to resolve some services (registered as "scoped") through the root container
    options.ValidateScopes = false;
    options.ValidateOnBuild = true;
});

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//add services to the application and configure service provider
builder.Services.ConfigureApplicationServices(builder);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapSwagger().RequireAuthorization();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
// Specify the custom static files path
/*
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.GetRootPath(), FileHelper.SaticFilesPath)),
    RequestPath = "/" + FileHelper.SaticFilesPath
});*/
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")),
    RequestPath = "/StaticFiles"
});
app.MapControllers();
//configure the application HTTP request pipeline
app.ConfigureRequestPipeline();
await app.StartEngineAsync();
app.Run();
