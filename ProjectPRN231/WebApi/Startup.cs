using AutoMapper;
using BussinessObject.DTOS.Common;
using BussinessObject.DTOS.User;
using BussinessObject.Mapper;
using BussinessObject.Models;
using DataAccess.DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
using Repository.Services;
using System.Text;

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
            // Register controllers
            services.AddControllers();
            services.AddControllersWithViews();

            // Thêm session
         

            services.AddDistributedMemoryCache();

            // Configure Swagger for API documentation
            services.AddSwaggerGen();

            // AutoMapper configuration
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationMapper());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Configure DbContext with SQL Server
            services.AddDbContext<ProjectPRN231Context>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Test"));
            });

            // Configure CORS to allow any origin, method, and header
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", build =>
                    build.AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowCredentials()
                         .SetIsOriginAllowed(hostName => true)); // Replace with specific origins if needed
            });

            // Register custom services
            InjectServices(services);

            // Configure JWT authentication
            ConfigureJWT(services);
        }

        private void InjectServices(IServiceCollection services)
        {
            // Register services and repositories for dependency injection
            services.AddTransient<IDocTypeRepository, DocTypeReponsitory>();
            services.AddTransient<DocTypeDAO>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<DocumentsDAO>();
            services.AddTransient<UserDao>();
            services.AddTransient<IUserRepository, UserServices>();
            services.AddTransient<IDataServices, RoleDataServices>();

            services.AddTransient<IGroupMemberRepository, GroupMemberRepository>();
            services.AddTransient<GroupMemberDAO>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Enable developer exception page in development mode
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable HTTPS redirection
            app.UseHttpsRedirection();

            // Serve static files
            app.UseStaticFiles();

            // Enable routing
            app.UseRouting();

            // Enable CORS
            app.UseCors("CorsPolicy");

            // Enable Swagger for API documentation
            app.UseSwagger();
            app.UseSwaggerUI();
       

            // Enable authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Map controller routes
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Initialize data with scope and service provider
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider.GetServices<IDataServices>();
            foreach (var service in services)
            {
                try
                {
                    service.AddData().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    // Log the exception (or use a logging framework like Serilog)
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            // Run the application
            app.Run();
        }

        private void ConfigureJWT(IServiceCollection services)
        {
            // Bind JWT settings from configuration
            services.Configure<JwtSetting>(configuration.GetSection("JwtSetting"));
            services.Configure<AdminAccount>(configuration.GetSection("AdminAccount"));

            // Configure Identity with user and role management
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<ProjectPRN231Context>()
                    .AddDefaultTokenProviders();
           
            // Configure identity options (password, lockout, user settings)
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            // Configure JWT authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSetting:Issuer"],
                    ValidAudience = configuration["JwtSetting:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSetting:Key"]))
                };
            });
        }
    }
}
