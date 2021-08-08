using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyAspBlog.IRepository;
using MyAspBlog.IService;
using MyAspBlog.Repository;
using MyAspBlog.Service;
using MyAspBlog.WebApi.Utility._AutoMapper;
using SqlSugar.IOC;
using System;
using System.Text;

namespace MyAspBlog.WebApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAspBlog.WebApi", Version = "v1" });
                #region Swagger使用JWT鉴权
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference=new OpenApiReference
              {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
              }
            },
            new string[] {}
          }
        });
                #endregion
            });

            #region SqlSugarIOC 
            SugarIocServices.AddSqlSugar(new IocConfig()
            {
                ConnectionString = this.Configuration["SqlConn"],
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true//自动释放
            });
            #endregion
            #region IOC
            services.AddCustomedIOC();
            #endregion

            #region JWT 
            services.AddCustomedJWT();
            #endregion

            #region AutoMapperDTO
            services.AddAutoMapper(typeof(CustomAutoMapperProfile));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAspBlog.WebApi v1"));
            }

            app.UseRouting();


            app.UseAuthentication(); //鉴权

            app.UseAuthorization(); //授权

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    static class IOCExtend
    {
        public static IServiceCollection AddCustomedIOC(this IServiceCollection services)
        {
            services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
            services.AddScoped<IBlogNewsService, BlogNewsService>();

            services.AddScoped<IAuthorInfoRepository, AuthorInfoRepository>();
            services.AddScoped<IAuthorInfoService, AuthorInfoService>();

            services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
            services.AddScoped<ITypeInfoService, TypeInfoService>();

            return services;
        }

        public static IServiceCollection AddCustomedJWT(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF")),
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:6060",
                    ValidateAudience = true,
                    ValidAudience = "http://localhost:5000",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(60)
                };
            });

            return services;
        }
    }
}
