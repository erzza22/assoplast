using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MVC.Data;
using MVC.Entities;
using MVC.Interfaces;
using MVC.Services;
using MVC.Repositories;
using MVC.Helpers;
using MVC.Email.EmailSettings;

namespace MVC
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
            services.AddDbContext<AssoplastPlannerContext>(x => x.UseSqlServer(Configuration.GetSection("Connection").Value));

            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(
                 Configuration.GetSection("Connection").Value));

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("AppSettings:Secret"));
            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.RequireHttpsMetadata = false;
                    jwtOptions.SaveToken = true;
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IApplianceCategoryService, ApplianceCategoryService>();
            services.AddScoped<ITransportatoreCategoryService, TransportatoreCategoryService>();
            services.AddScoped<IProduttoreDetentoreCategoryService, ProduttoreDetentoreCategoryService>();
            services.AddScoped<IProduttoreDetentoreService, ProduttoreDetentoreService>();
            services.AddScoped<IDestinatarioCategoryService, DestinatarioCategoryService>();
            services.AddScoped<IReceiverService, ReceiverService>();
            services.AddScoped<IPickupRequestService, PickupRequestService>();
            services.AddScoped<ITransportatoreService, TransportatoreService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUserUploadService, UserUploadService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IRequestCategoryService, RequestCategoryService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.Configure<AppSettings>(options =>
            {
                options.Secret =
                    Configuration.GetSection("AppSettings:Secret").Value;
                options.ConnectionString = Configuration["Connection"];
                options.Database = Configuration.GetSection("Database:DB").Value;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                  policyBuilder => policyBuilder.WithOrigins(Configuration.GetValue<string>("CorsHost"))
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
