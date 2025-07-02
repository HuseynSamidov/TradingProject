using Microsoft.Extensions.DependencyInjection;
using TradingProject.Application.Abstracts.Repositories;

namespace TradingProject.Persistence
{
    public static class ServiceRegistration
    {
        public static void RegisterService(this IServiceCollection services)
        {

            #region Repositories

            //services.AddScoped<ICategoryRepository, CategoryRepository>();

            #endregion


            #region Services

            //services.AddScoped<ICategoryServices, CategoryService>();
            //services.AddScoped<IFileUploadServices, FileUploadService>();

            #endregion

        }
    }
}
