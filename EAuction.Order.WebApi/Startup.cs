using EAuction.Order.Application.IOC;
using EAuction.Order.Infrastructure.IOC;
using EAuction.Order.WebApi.Consumers;
using EAuction.Order.WebApi.Extensions;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

namespace EAuction.Order.WebApi
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

            #region Infrastructure Dependencies

            services.AddInfrastructure(Configuration);

            #endregion

            #region Application Dependencies

            services.AddApplication();

            #endregion

            services.AddAutoMapper(typeof(Startup));

            #region EventBus Dependencies

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"]
                };
                if (Equals(!string.IsNullOrWhiteSpace(Configuration["EventBus:UserName"])))
                {
                    factory.UserName = Configuration["EventBus:UserName"];
                }
                if (Equals(!string.IsNullOrWhiteSpace(Configuration["EventBus:Password"])))
                {
                    factory.Password = Configuration["EventBus:Password"];
                }

                var retryCount = 5;
                if (Equals(!string.IsNullOrWhiteSpace(Configuration["EventBus:RetryCount"])))
                {
                    retryCount = int.Parse(Configuration["EventBus:RetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, retryCount, logger);
            });

            services.AddSingleton<EventBusOrderCreateConsumer>();

            #endregion

            #region Swagger Dependencies
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EAuction.Order.WebApi", Version = "v1" });
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EAuction.Order.WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRabbitListener();

        }
    }
}
