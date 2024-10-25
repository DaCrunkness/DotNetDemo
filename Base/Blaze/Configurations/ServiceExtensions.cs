using Blaze.Components.Account;
using Blaze.Configurations.MailJet;
using Blaze.Configurations.Policies;
using DataAccess.Data;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Models.IdentityModels;
using Serilog;
using Serilog.Events;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blaze.Configurations
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
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddCascadingAuthenticationState();
            services.AddScoped<IdentityUserAccessor>();
            services.AddScoped<IdentityRedirectManager>();
            services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
                .AddIdentityCookies();

            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            //services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().AddDefaultUI();
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 5;
            //    options.Password.RequireLowercase = true;
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //});

            services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();
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
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "596359254795205";
                options.AppSecret = "ed0f30be7e32184962b33fd73ef75af1";
            });
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

        public static void ConfigureDependencies(this IServiceCollection services)
        {
            //services.AddScoped<IAuthorizationHandler, AdminWithOver1000DaysHandler>();
            //services.AddScoped<IAuthorizationHandler, FirstNameAuthHandler>();
            //services.AddScoped<INumberOfDaysForAccount, NumberOfDaysForAccount>();
            services.AddTransient<IEmailSender, MailJetEmailSender>();
            //services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

        }

        static bool AuthorizeAdminWithClaimsOrSuperAdmin(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("Admin") && context.User.HasClaim(c => c.Type == "Create" && c.Value == "True")
                        && context.User.HasClaim(c => c.Type == "Edit" && c.Value == "True")
                        && context.User.HasClaim(c => c.Type == "Delete" && c.Value == "True")
                     || context.User.IsInRole("SuperAdmin");
        }
    }
}