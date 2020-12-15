using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Repositories;

namespace Theater_ticket_booking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // �������� ������ ����������� �� ����� ������������
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<TheaterContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.LoginPath = "/";
                })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // ��������, ����� �� �������������� �������� ��� ��������� ������
                            ValidateIssuer = true,
                            // ������, �������������� ��������
                            ValidIssuer = AuthOptions.ISSUER,

                            // ����� �� �������������� ����������� ������
                            ValidateAudience = true,
                            // ��������� ����������� ������
                            ValidAudience = AuthOptions.AUDIENCE,
                            // ����� �� �������������� ����� �������������
                            ValidateLifetime = true,

                            // ��������� ����� ������������
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // ��������� ����� ������������
                            ValidateIssuerSigningKey = true,

                        };


                        options.Events = new JwtBearerEvents()
                        {
                            OnMessageReceived = context =>
                            {
                                var token = context.Request.Headers["Authorization"].ToString();
                                if (string.IsNullOrEmpty(token))
                                    token = context.Request.Cookies["Authorization"];


                                if (!string.IsNullOrEmpty(token))
                                {
                                    var split = token.Split(' ');
                                    if (split.Length == 2)
                                        token = split[1];
                                }

                                context.Token = token;

                                return Task.CompletedTask;
                            },
                        };
                    });

            services.AddControllersWithViews().AddNewtonsoftJson();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Theater API",
                    Description = "API ��������-����� ������",
                });
            });

            AddRepositories(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<TheaterContext>().Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // ����������� swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHttpsRedirection();
        }

        private void AddRepositories(IServiceCollection services)
        {
            services
                .AddTransient(typeof(UsersRepository));
        }
    }
}
