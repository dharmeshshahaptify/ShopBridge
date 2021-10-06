using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopBridge.Service.Interfaces;
using ShopBridge.Service.Services;

namespace ShopBridge.Service.Configuration
{
    public static class ConfigureDependencies
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //database
            services.AddDbContext<ShopbridgedbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

          

            services.AddScoped<DbContext, ShopbridgedbContext>();

            ////repositories
            services.AddScoped<IRepository<Product>, Repository<Product>>();
 

            ////services
            services.AddScoped<IProductService, ProductService>();

        }
    }
}
