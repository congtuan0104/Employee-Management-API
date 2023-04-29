using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ========= Configure the HTTP request pipeline. (middleware) =======
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    // HTTP Strict Transport Security Protocol
    app.UseHsts();  
}

// Enables using static files for the request, use wwwroot as default folder
// app.UseStaticFiles();

// This middleware use to forward proxy hearders to the current request
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();  // redirect http to https

app.UseAuthorization();


// Task UseMiddleware(HttpContext context, Func<Task> next)
app.Use(async (context, next) =>
{
    Console.WriteLine($"Logic before executing the next delegate in the Use method");
    await next.Invoke();
    Console.WriteLine($"Logic after executing the next delegate in the Use method");
});

// ReSharper disable once VariableHidesOuterVariable
app.Map("/usingmapbranch", builder =>
{
    builder.Use(async (context, next) =>
    {
        Console.WriteLine("Map branch logic in the Use method before the next delegate");
        await next.Invoke();
        Console.WriteLine("Map branch logic in the Use method after the next delegate");
    });
    builder.Run(async context =>
    {
        Console.WriteLine($"Map branch response to the client in the Run method");
        await context.Response.WriteAsync("Hello from the map branch.");
    });
});

app.MapWhen(context => context.Request.Query.ContainsKey("testquerystring"), 
    builder =>
{
    builder.Run(async context =>
    {
        await context.Response.WriteAsync("Hello from the MapWhen branch.");
    });
});

// app.Run(async context =>
// {
//     Console.WriteLine($"Writing the response to the client in the Run method");
//     await context.Response.WriteAsync("This message from custom middleware.");
// });

app.MapControllers();   // add endpoints from controllers to the route

app.Run();

