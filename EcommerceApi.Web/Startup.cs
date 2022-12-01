using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Web
{
    public class Startup
    {
        //For Automapper
        private MapperConfiguration mapperConfiguration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //Configuring mapping profile
            //mapperConfiguration = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile(new MappingProfile());
            //    cfg.AddProfile(new WebMappingProfile());
            //});
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //EF Core
            //services.AddDbContext<ReimbursementContext>(
            //    options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            //    .EnableSensitiveDataLogging()
            //);


            //Identity
            //services.AddIdentity<ApiUser, IdentityRole>()
            //   .AddEntityFrameworkStores<ReimbursementContext>()
            //   .AddTokenProvider("ReimbursementPortalApi", typeof(DataProtectorTokenProvider<ApiUser>));

            //Configure Password
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredLength = 8;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequiredUniqueChars = 1;
            //});

            //JWT
            //var jwtSection = Configuration.GetSection("Jwt");
            //var jwtOptions = new JwtOptions();
            //jwtSection.Bind(jwtOptions);
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            //        ValidateIssuer = true,
            //        ValidateLifetime = true,
            //        ValidateAudience = false,
            //        ValidIssuer = jwtOptions.Issuer
            //    };
            //});

            //IOption Jwt
            //services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));

            //Repositories
            //services.AddScoped<IReimbursementRepository, ReimbursementRepository>();

            //Mapper
            //services.AddScoped<IMapper>(sp => mapperConfiguration.CreateMapper());

            //Services
            //services.AddScoped<IReimbursementService, ReimbursementService>();
            //services.AddScoped<IAuthService, AuthService>();


            //Cors
            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //        builder =>
            //        {
            //            builder.AllowAnyOrigin()
            //            .AllowAnyHeader()
            //            .AllowAnyMethod();
            //        });
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
