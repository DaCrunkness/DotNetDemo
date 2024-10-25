using DataAccess.Configurations.DbInitializer;
using DataAccess.Data;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Models.IdentityModels;
using Serilog;
using Serilog.Events;
using Web.Configurations.MailJet;
using Web.Configurations.Policies;

namespace Web.Configurations
{
    public static class ServiceExtensions
    {
        public static void ConfigureMyLogging(this IHostBuilder host)
        {
            host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console()
                .WriteTo.File(
                path: "logs\\log-.text",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Information));
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        public static void ConfigurePolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserAndAdmin", policy => policy.RequireRole("Admin").RequireRole("User"));
                options.AddPolicy("Admin_CreateAccess", policy => policy.RequireRole("Admin").RequireClaim("create", "True"));
                options.AddPolicy("Admin_Create_Edit_DeleteAccess", policy => policy.RequireRole("Admin").RequireClaim("create", "True")
                .RequireClaim("edit", "True")
                .RequireClaim("Delete", "True"));

                options.AddPolicy("Admin_Create_Edit_DeleteAccess_OR_SuperAdmin", policy => policy.RequireAssertion(context =>
                AuthorizeAdminWithClaimsOrSuperAdmin(context)));
                options.AddPolicy("OnlySuperAdminChecker", policy => policy.Requirements.Add(new OnlySuperAdminChecker()));
                options.AddPolicy("AdminWithMoreThan1000Days", policy => policy.Requirements.Add(new AdminWithMoreThan1000DaysRequirement(1000)));
                options.AddPolicy("FirstNameAuth", policy => policy.Requirements.Add(new FirstNameAuthRequirement("Rell")));
            });
        }

        static bool AuthorizeAdminWithClaimsOrSuperAdmin(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("Admin") && context.User.HasClaim(c => c.Type == "Create" && c.Value == "True")
                        && context.User.HasClaim(c => c.Type == "Edit" && c.Value == "True")
                        && context.User.HasClaim(c => c.Type == "Delete" && c.Value == "True")
                     || context.User.IsInRole("SuperAdmin");
        }

        public static void ConfigureAppCookies(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
        }

        public static void ConfigureFacebook(this IServiceCollection services)
        {
            //services.AddAuthentication().AddFacebook(options =>
            //{
            //    options.AppId = "596359254795205";
            //    options.AppSecret = "ed0f30be7e32184962b33fd73ef75af1";
            //});
        }

        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, MailJetEmailSender>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

        }


    }
}