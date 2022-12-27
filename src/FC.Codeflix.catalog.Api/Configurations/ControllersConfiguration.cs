using FC.Codeflix.catalog.Api.Configurations.Policies;
using FC.Codeflix.catalog.Api.Filters;

namespace FC.Codeflix.catalog.Api.Configurations
{
    public static class ControllersConfiguration
    {
        public static IServiceCollection AddAndConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(o => o.Filters.Add(typeof(ApiGlobalExceptionFilter)))
                .AddJsonOptions(j => {
                    j.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCasePolicy();
                });
            services.AddDocumentation();
            return services;
        }

        private static IServiceCollection AddDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static WebApplication UseDocumentation(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;
        }
    }
}
