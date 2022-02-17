using LilFamStudioNet5.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5
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
            services.AddControllersWithViews();
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue;
            });
            services.AddDbContext<Data.ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "AdminCookie";
            })
                .AddCookie("AdminCookie", "AdminCookie", config =>
                {
                    config.LoginPath = new PathString("/studio/login");
                    config.ExpireTimeSpan = TimeSpan.FromMinutes(14400);
                    config.AccessDeniedPath = "/Error/UnAuthorized";
                    config.LogoutPath = "/studio/login";
                })
                
                .AddJwtBearer("UserJWT", "UserJWT", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthUserJWTOptions.ISSUER,

                        ValidateAudience = true,
                        ValidAudience = AuthUserJWTOptions.AUDIENCE,

                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = AuthUserJWTOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));

                options.AddPolicy("RequireUserJWTRole", policy =>
                    policy.RequireRole("User").RequireClaim("UserJWT")
                );

            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();//для перехвата domain
            services.AddControllersWithViews().AddRazorRuntimeCompilation();//чтобы без перезагрузки сервера изменять frontend
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
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                    context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                    context.Context.Response.Headers["Pragma"] = "no-cache";
                    context.Context.Response.Headers["Expires"] = "-1";
                }
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("studio", "studio", new { controller = "Studio", action = "Index" });
                endpoints.MapControllerRoute("studio/login", "studio/login", new { controller = "Studio", action = "Login" });
                endpoints.MapControllerRoute("studio/users", "studio/users", new { controller = "Studio", action = "Users" });
                endpoints.MapControllerRoute("studio/teachers", "studio/teachers", new { controller = "Studio", action = "Teachers" });
                endpoints.MapControllerRoute("studio/branches", "studio/branches", new { controller = "Studio", action = "Branches" });
                endpoints.MapControllerRoute("studio/abonements", "studio/abonements", new { controller = "Studio", action = "Abonements" });
                endpoints.MapControllerRoute("studio/dance_groups", "studio/dance_groups", new { controller = "Studio", action = "DanceGroups" });
                endpoints.MapControllerRoute("studio/discounts", "studio/discounts", new { controller = "Studio", action = "Discounts" });
                endpoints.MapControllerRoute("studio/admins", "studio/admins", new { controller = "Studio", action = "Admins" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
