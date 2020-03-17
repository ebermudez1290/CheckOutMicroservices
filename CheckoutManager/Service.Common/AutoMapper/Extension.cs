using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Service.Common.AutoMapper
{
    public static class Extension
    {
        public static void AddAutoMapperSupport(this IServiceCollection services, Type type)
        {
            services.AddAutoMapper(type);
        }
    }
}
