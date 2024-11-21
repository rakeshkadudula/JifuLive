using DirectScale.Disco.Extension.Middleware;
using JifuLive;
/**
var builder = WebApplication.CreateBuilder(args);

// Register HttpClient in DI container
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<>

var app = builder.Build();
app.UseDirectScale();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    options.RoutePrefix = string.Empty; // Set to empty string to serve Swagger at the root
});

// Add Authorization Middleware (if needed)
app.UseAuthorization();

// Your other middlewares
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
*/

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}