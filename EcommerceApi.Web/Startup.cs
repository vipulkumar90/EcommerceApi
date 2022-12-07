using AutoMapper;
using EcommerceApi.BLL.Mapper;
using EcommerceApi.BLL.Models;
using EcommerceApi.BLL.Services;
using EcommerceApi.BLL.Services.IServices;
using EcommerceApi.DAL.DataContext;
using EcommerceApi.DAL.Entities.UserData;
using EcommerceApi.DAL.Repositories;
using EcommerceApi.DAL.Repositories.IRepositories;
using EcommerceApi.Web.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.AddProfile(new WebMappingProfile());
            });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //EF Core
            services.AddDbContext<EcommerceContext>(
                options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()
            );


            //Identity
            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<EcommerceContext>()
               .AddTokenProvider("ReimbursementPortalApi", typeof(DataProtectorTokenProvider<User>));

            //Configure Password
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 1;
            });

            //JWT
            var jwtSection = Configuration.GetSection("Jwt");
            var jwtOptions = new JwtOptions();
            jwtSection.Bind(jwtOptions);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidIssuer = jwtOptions.Issuer
                };
            });

            //IOption Jwt
            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));

            //Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserAddressRepository, UserAddressRepository>();
            services.AddScoped<IUserPaymentRepository, UserPaymentRepository>();

            //Mapper
            services.AddScoped<IMapper>(sp => mapperConfiguration.CreateMapper());

            //Services
            services.AddScoped<IAuthService, AuthService>();


            //Cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

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
