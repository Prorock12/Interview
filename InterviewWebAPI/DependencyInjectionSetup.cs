using InterviewWebAPI.Repository;
using InterviewWebAPI.Services;

namespace InterviewWebAPI
{
    public static class DependencyInjectionSetup
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<ISecretMessageService, SecretMessageService>();
            services.AddScoped<IVacancyRepository, VacancyRepository>();
        }
    }
}
