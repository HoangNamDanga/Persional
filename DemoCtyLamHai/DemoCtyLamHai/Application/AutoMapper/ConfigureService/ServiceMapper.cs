using Microsoft.Extensions.DependencyInjection;

namespace DemoCtyLamHai.Application.AutoMapper.ConfigureService
{
    public static class ServiceMapper
    {

        public static void AddMapper(this IServiceCollection service)
        {
            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
