using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using EAuction.Sourcing.Api.Data;
using EAuction.Sourcing.Api.Data.Abstractions;
using EAuction.Sourcing.Api.Hubs;
using EAuction.Sourcing.Api.Repositories;
using EAuction.Sourcing.Api.Repositories.Abstractions;
using EAuction.Sourcing.Api.Settings;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EAuction.Sourcing.Api
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

            #region Configure Dependencies
            services.Configure<SourcingDatabaseSettings>(Configuration.GetSection(nameof(SourcingDatabaseSettings)));
            services.AddSingleton<ISourcingDatabaseSettings, SourcingDatabaseSettings>(sp => sp.GetRequiredService<IOptions<SourcingDatabaseSettings>>().Value);
            #endregion

            #region Project Dependencies

            services.AddTransient<ISourcingContext, SourcingContext>();
            services.AddTransient<IAuctionRepository, AuctionRepository>();
            services.AddTransient<IBidRepository, BidRepository>();

            services.AddAutoMapper(typeof(Startup));

            #endregion

            #region Swagger Dependencies
            services.AddSwaggerGen(c =>
               {
                   c.SwaggerDoc("v1", new OpenApiInfo { Title = "EAuction.Sourcing.Api", Version = "v1" });
               });
            #endregion

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

                return new DefaultRabbitMQPersistentConnection(factory,retryCount,logger);
            });

            services.AddSingleton<EventBusRabbitMQProducer>();

            #endregion

            services.AddCors(o=>o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:35186");
            }));
            
            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EAuction.Sourcing.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AuctionHub>("/auctionhub");
                endpoints.MapControllers();
            });
        }
    }
}
