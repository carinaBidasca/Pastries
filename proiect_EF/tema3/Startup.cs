using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PastriesCommon;
using PastriesCommon.Util;
using PastriesData;
using PastriesDataPersistence.Repositories;
using PastriesInterfaces;
using PastriesServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using tema3.Middleware.Auth;
using tema3.Middleware.Exceptions;
using tema3.Utils;
using Microsoft.EntityFrameworkCore;
namespace tema3
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

            services.AddCors();//pt varianta 2
            // configure strongly typed settings object
           // services.Configure<AuthorizationSettings>(Configuration.GetSection("AuthorizationSettings"));//iau din fisier aceste setari
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddDbContext<PastriesDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            // add controllers

            services.AddControllers();

            //!!!!!!!!!!!!!
           // ConfigureAuthServices(services, Configuration);

            // DI for services and repo
            services.AddScoped<IIngredientRepositoryAsync, IngredientRepositoryAsync>();
            services.AddScoped<IIngredientServiceAsync, IngredientServiceAsync>();

            services.AddScoped<IProductRepositoryAsync, ProductRepositoryAsync>();
            services.AddScoped<IPastriesFactoryRepository, PastriesFactoryRepositoryAsync>();

            services.AddScoped<IProductServiceAsync, ProductServiceAsync>();
            services.AddScoped<IPastriesFactoryService, PastriesFactoryServiceAsync>();
            services.AddScoped<IUserService, UserService>();

            ConfigureSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMiddleware<JwtMiddleware>();

            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            UseAuth(app);*/
            //!!!!!!!!!!!!!!!!!

            UseSwagger(app);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }


        private static void ConfigureAuthServices(IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();//golesc token vechi

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;//aici pun toti param pt token,clock e pt cazul cand am 2 servere cu ceas dereglat
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["AuthorizationSettings:Issuer"],
                        ValidAudience = configuration["AuthorizationSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthorizationSettings:Secret"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

          /*  services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.User, policy =>
                    policy.Requirements.Add(new PrivilegeRequirement(Policies.User))
                );
                config.AddPolicy(Policies.Admin, policy =>
                    policy.Requirements.Add(new PrivilegeRequirement(Policies.Admin))
                );
                config.AddPolicy(Policies.All, policy =>
                    policy.Requirements.Add(new PrivilegeRequirement(Policies.All))
                );
            });
            */
            //services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        }
        private static void UseAuth(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }


        private static void ConfigureSwagger(IServiceCollection services)
        {
            // !!! Do not forget to enable the generation of docs from the code in the .csproj file 

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Ingredients for pastries API",
                    Description = "A simple example ASP.NET Core Web API"
                });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
               
                c.IncludeXmlComments(xmlPath);
            });
        }


        private static void UseSwagger(IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {//nu are fisierul swagger!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ingredients for pastries API v1");

                // To serve the Swagger UI at the app's root (http://localhost:<port>/), set the RoutePrefix property to an empty string.
                c.RoutePrefix = string.Empty;
            });
        }

    }
}
