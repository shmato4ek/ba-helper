using AutoMapper;
using BAHelper.BLL.Services;
using Microsoft.Extensions.FileProviders;

namespace BAHelper.API.Extensions
{
    public static class ServiceExtentions
    {
        public static void RegisterCustomServices (this IServiceCollection services)
        {
            services.AddScoped<DocumentService>();
            services.AddScoped<UserService>();
            services.AddScoped<WordService>();
            services.AddSingleton<IFileProvider>(
                            new PhysicalFileProvider(
                                Path.Combine(Directory.GetCurrentDirectory(),
                                "./LocalFiles")));
            services.AddScoped<DownloadService>();
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps("BAHelper.BLL");
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
