namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            // This is a CORS policy that allows any origin, method, and header.
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
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
}
