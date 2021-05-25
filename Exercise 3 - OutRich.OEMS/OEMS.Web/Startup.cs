using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using OEMS.Web.Services.Email;
using OEMS.Data;
using MediatR;
using OEMS.Data.Repositories;
using AutoMapper;
using OEMS.Logger.Extensions.DependencyInjection;
using OEMS.Logger.Extensions.AspNetCore;
using OEMS.Application.Models.User;
using OEMS.Application.Models.Role;
using OEMS.Application.ApplicationServices.Role;
using OEMS.Application.ApplicationServices.User;
using OEMS.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using OEMS.Logger.Filters;
using OEMS.Application.ApplicationServices.Employee;
using OEMS.Application.Models.Employee;


namespace OEMS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {        
            services.AddLogCorrelation();
            services.AddOptions();
            services.Configure<OEMSWebConfig>(Configuration.GetSection("OEMSWebConfig"));
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSession();
            services.AddDbContext<OEMSContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<OEMSContext>();

            services.AddSingleton(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Data.Models.Employee, EmployeeModel>().ReverseMap();
                    cfg.CreateMap<Data.Models.Employee, Core.Models.Employee>().ReverseMap();
                    cfg.CreateMap<Core.Models.Employee, EmployeeModel>().ReverseMap();
                    
                    cfg.CreateMap<Data.Models.OEMSUser, UserModel>().ReverseMap();
                    cfg.CreateMap<Data.Models.OEMSUser, Core.Models.OEMSUser>().ReverseMap();
                    cfg.CreateMap<Core.Models.OEMSUser, UserModel>().ReverseMap();
                    cfg.CreateMap<IdentityUser, Core.Models.IdentityUser>().ReverseMap();
                    cfg.CreateMap<IdentityRole, RoleModel>().ReverseMap();
                }
            ));

            #region Application Services
            services.AddTransient<EmployeeService>();
        
            services.AddTransient<UserService>();
            services.AddTransient<RoleService>();
            #endregion
            
            #region External Logins
            services.AddAuthentication()           
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["AuthenticationConfig:GoogleClientId"];
                googleOptions.ClientSecret = Configuration["AuthenticationConfig:GoogleClientSecret"];
            });
            #endregion

            services.AddTransient<IEmailSender, SMTPEmailService>();
            services.AddRazorPages();
            services.AddHealthChecks().AddDbContextCheck<OEMSContext>();
            services.AddApplicationInsightsTelemetry();
            services.AddMediatR(typeof(Resource).Assembly);
            services.AddTransient<EmployeeRepository>();
            
            services.AddTransient<UserRepository>();
            services.AddHealthChecks().AddSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            #region Api
            services.AddHttpContextAccessor();

            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                config.Filters.Add<SerilogLoggingActionFilter>();
            });
        
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OEMS API",
                    Description = "Web APIs for accessing OEMS resources",
                });
            });
            #endregion
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            #region Api
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OEMS API");
            });
            app.UseLogCorrelation();
            #endregion
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseLogCorrelation();   
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
