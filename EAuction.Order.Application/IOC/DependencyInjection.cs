using System.Reflection;
using AutoMapper;
using EAuction.Order.Application.Mapper;
using EAuction.Order.Application.PipelineBehaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EAuction.Order.Application.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(UnhandledExceptionBehaviour<,>));

            #region Configure Mapper

            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<OrderMappingProfile>();
            });
            var mapper = config.CreateMapper();

            #endregion

            return services;
        }
    }
}