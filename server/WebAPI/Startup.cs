using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using APICarData.Services;
using APICarData.Domain.Interfaces.Login;
using APICarData.Domain.Interfaces;
using APICarData.Dal;
using APICarData.Domain.Data;
using AutoMapper;
using APICarData.Services.Mapper;
using StackExchange.Redis;
using APICarData.Domain.Interfaces.CompanyCars;

namespace APICarData.Api
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
            // MSSQL connection 
            services.AddDbContext<ApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("testdb")));
            // Interfaces declaration
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginDAL, LoginDAL>();
            services.AddScoped<ICompanyCarsService, CompanyCarsService>();
            services.AddScoped<ICompanyCarsDAL, CompanyCarsDAL>();
            services.AddScoped<IApiContext, ApiContext>();
            // Mapper configuration
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            // Redis connection
            var multiplexer = ConnectionMultiplexer.Connect(Configuration.GetConnectionString("redisdb"));
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            services.AddMvc();

            services.AddHttpContextAccessor();

            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            //.AddGoogle("google", opt =>
            //{
            //    var googleAuth = Configuration.GetSection("Authentication:Google");
            //    opt.ClientId = googleAuth["ClientId"];
            //    opt.ClientSecret = googleAuth["ClientSecret"];
            //    opt.SignInScheme = IdentityConstants.ExternalScheme;
            //});

            services.AddSwaggerGen(option =>
            {

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                        new string[] {}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTAuthDemo v1"));
                app.UseHttpsRedirection();
            }
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
