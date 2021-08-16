using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using EAuction.Products.Api.Data;
using EAuction.Products.Api.Data.Abstractions;
using EAuction.Products.Api.Repositories;
using EAuction.Products.Api.Repositories.Abstractions;
using EAuction.Products.Api.Settings;
using Microsoft.Extensions.Options;

namespace EAuction.Products.Api
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

            #region Configuration Dependencies
            services.Configure<ProductDatabaseSettings>(Configuration.GetSection(nameof(ProductDatabaseSettings)));
            services.AddSingleton<IProductDatabaseSettings, ProductDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);
            #endregion

            #region Project Dependencies
            services.AddTransient<IProductContext, ProductContext>();
            services.AddTransient<IProductRepository, ProductRepository>();
            #endregion

            #region Swagger Dependencies
            services.AddSwaggerGen(c =>
               {
                   c.SwaggerDoc("v1", new OpenApiInfo { Title = "EAuction.Products.Api", Version = "v1" });
               }); 
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EAuction.Products.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
