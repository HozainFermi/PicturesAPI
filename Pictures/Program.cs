using Application;
using Infrastracture.Authorization;
using Persistance;

namespace Pictures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policyBuilder =>
                {
                    policyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });

            builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection("AuthorizationOptions"));


            builder.Services
                .AddPersistance(builder.Configuration)
                .AddApplication();

            builder
                .AddSwagger()
                .AddBearerAuthentication();
            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();

            app.Run();
        }
    }
}
