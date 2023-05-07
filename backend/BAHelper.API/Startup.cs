using BAHelper.API.Extensions;
using BAHelper.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace BAHelper.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BAHelperDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.RegisterAutoMapper();
            services.RegisterCustomServices();
            services.AddCors(
                options =>
                {
                    options.AddDefaultPolicy(
                        policy => 
                        {
                            policy.WithOrigins("http://localhost:3000/")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        }
                    );
                }
            );
            services.AddControllers();
            services.ConfigureJwt(Configuration);
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
//             if (env.IsDevelopment())
//             {
//                 app.UseDeveloperExceptionPage();
//                 app.UseSwagger();
//                 app.UseSwaggerUI(options =>
//                 {
//                     options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//                     options.RoutePrefix = string.Empty;
//                 });
//                 // app.UseCors(builder =>
//                 //     builder.WithOrigins("http://localhost:3000").AllowAnyMethod()
// //);
//             }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors(
                // options => options.WithOrigins("http://localhost:3000").AllowAnyMethod()
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InitializeDatabase(app);
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<BAHelperDbContext>();
            context.Database.Migrate();
        }

    }
}