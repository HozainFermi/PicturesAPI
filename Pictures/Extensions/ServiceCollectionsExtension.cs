using System.Text;


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt; 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Npgsql;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Infrastracture;



public static class ServiceCollectionsExtensions
{
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {

        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Orders API",
                Version = "v1"
            });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
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
                    Array.Empty<string>()
                }
            });
        });

        return builder;
    }

    public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("IDK"));
        dataSourceBuilder.EnableDynamicJson();

        builder.Services.AddDbContext<PicturesDbContext>(opt =>
            opt.UseNpgsql(dataSourceBuilder.Build()));

        return builder;
    }

    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        //builder.Services.AddScoped<ICartService, CartService>();
        //builder.Services.AddScoped<IOrderService, OrderService>();
        //builder.Services.AddScoped<IAuthService, AuthService>();
        //builder.Services.AddScoped<IProductService, ProductService>();
        //builder.Services.AddScoped<ICartItemService, CartItemService>();
        return builder;
    }

    public static WebApplicationBuilder AddInfrastractureServices(this WebApplicationBuilder builder)
    {
        return builder;
    }

    public static WebApplicationBuilder AddIntegrationServices(this WebApplicationBuilder builder)
    {
        return builder;
    }

    public static WebApplicationBuilder AddBackgroundService(this WebApplicationBuilder builder)
    {
        // builder.Services.AddHostedService<CreateOrderConsumer>();

        return builder;
    }

    public static WebApplicationBuilder AddBearerAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
             .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidIssuer = builder.Configuration["Jwt:Issuer"],
                     ValidateAudience = true,
                     ValidAudience = builder.Configuration["Jwt:Audience"],
                     ValidateLifetime = true,
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Token"]!)!),
                     ValidateIssuerSigningKey = true,

                     ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha512 }
                 };

                 options.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = ctx =>
                     {
                         Console.WriteLine($"Algorithm used: {ctx.HttpContext.Request.Headers["Authorization"]}");
                         return Task.CompletedTask;
                     }
                 };

             });


        return builder;

    }

    public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
    {
        //   builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Authentication"));
        //   builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMQ"));

        return builder;
    }
}