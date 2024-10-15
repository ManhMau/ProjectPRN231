using AutoMapper;
using BussinessObject.Mapper;
using BussinessObject.Models;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using Repository.Services;

namespace WebApi
{
    public class Startup
    {
        private IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationMapper());

            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddDbContext<ProjectPRN231Context>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Test"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", build =>
                    build.AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowCredentials()
                         .SetIsOriginAllowed(hostName => true));
            });
            InjectServices(services);
        }



        private void InjectServices(IServiceCollection services)
        {
            services.AddDbContext<ProjectPRN231Context>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Test"));
            });

            services.AddScoped<IDocTypeRepository, DocTypeReponsitory>();
            services.AddScoped<DocTypeDAO>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<DocumentsDAO>();

        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors(build =>
            {
                build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var scope = app.Services.CreateScope();
            /*  var services = scope.ServiceProvider.GetServices<IDataServices>();*/
            /* foreach (var service in services)
             {
                 service.AddData().GetAwaiter().GetResult();
             }*/
            app.Run();

        }

    }
}
