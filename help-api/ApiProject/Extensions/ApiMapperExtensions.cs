using ApiProject.ApiLogic.Mappers;

namespace ApiProject.Extensions
{
    public static class ApiMapperExtensions
    {
        public static void AddApiMappers(this IServiceCollection services)
        {
            services.AddScoped<IThesisApiMapper, ThesisApiMapper>();
        }
    }
}
