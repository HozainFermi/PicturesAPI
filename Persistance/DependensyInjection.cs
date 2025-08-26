using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data;
using Persistance.RepoImplementation;

namespace Persistance
{
    public static class DependensyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            //services.AddScoped<IBidRepository, BidRepository>();
            //services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            
           

            services.AddDbContext<PicturesDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Pictures")));

            return services;
        }
    }
}
