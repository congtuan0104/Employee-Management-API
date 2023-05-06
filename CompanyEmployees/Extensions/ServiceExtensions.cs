using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contacts;

namespace CompanyEmployees.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        // This is a CORS policy that allows any origin, method, and header.
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination") // allow the client read the X-Pagination header
            );
        });
    }

    public static void ConfigureIISIntegration(this IServiceCollection services)
    {
        // This is a middleware that allows IIS to serve static files.
        services.Configure<IISOptions>(options =>
        {
            // This is the default path for static files in IIS.
            // You don't need to specify this if you're using the default path.

            // Set the display name shown to users on login page
            options.AuthenticationDisplayName = null;

            // If this is set to true, the middleware will handle authentication
            options.AutomaticAuthentication = true;

            options.ForwardClientCertificate = true;
        });
    }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
    }

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
    }

    public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder)
    {
        return builder.AddMvcOptions(
            config => config.OutputFormatters.Add(new CsvOutputFormatter()));
    }
}