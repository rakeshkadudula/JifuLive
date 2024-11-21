using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DirectScale.Disco.Extension.Middleware;
using DirectScale.Disco.Extension.Middleware.Models;


namespace JifuLive
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var environmentUrl = Environment.GetEnvironmentVariable("DirectScaleServiceUrl");
            var serverUrl = environmentUrl.Replace("https://jifu.corpadmin.", "");
            var appendUrl = @" http://" + serverUrl + " " + "https://" + serverUrl + " " + "http://*." + serverUrl + " " + "https://*." + serverUrl;

            // var csPolicy = "frame-ancestors https://jifu.corpadmin.directscale.com https://jifu.corpadmin.directscalestage.com https://jifu.office2.directscalestage.com https://jifu.office2.directscale.com" + appendUrl + ";";
            var csPolicy = "frame-src https://jifu.corpadmin.directscale.com https://jifu.corpadmin.directscalestage.com https://jifu.office2.directscalestage.com https://jifu.office2.directscale.com" + appendUrl + ";";
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(csPolicy)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin());

            });

            services.AddControllersWithViews();

            services.AddDirectScale(options =>
            {
            });

            services.AddControllersWithViews();

            //Swagger
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                // context.Response.Headers.Add("Content-Security-Policy", csPolicy);
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                await next();
            });

            //Configure Cors
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //DS
            app.UseDirectScale();

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
