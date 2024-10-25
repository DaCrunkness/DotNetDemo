using AspNetCoreRateLimit;
using Api.Configurations.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using EightPI.Configurations.Policies;
using DataAccess.Repository;


// quizTheG@me
namespace Api.Configurations
{
    public static class ServiceExtensions
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
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
            //services.AddCascadingAuthenticationState();
            //services.AddScoped<IdentityUserAccessor>();
            //services.AddScoped<IdentityRedirectManager>();
            //services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = IdentityConstants.ApplicationScheme;
            //    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            //}).AddIdentityCookies();

            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));
            //services.AddDatabaseDeveloperPageExceptionFilter();
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

            //services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddSignInManager()
            //    .AddDefaultTokenProviders();
        }
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            //services.AddScoped<IAuthorizationHandler, AdminWithOver1000DaysHandler>();
            //services.AddScoped<IAuthorizationHandler, FirstNameAuthHandler>();
            //services.AddScoped<INumberOfDaysForAccount, NumberOfDaysForAccount>();
            //services.AddTransient<IEmailSender, MailJetEmailSender>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
        //public static void ConfigureAuthentication(this IServiceCollection services)
        //{
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = "JwtBearer";
        //        options.DefaultChallengeScheme = "JwtBearer";
        //    })
        //    .AddJwtBearer("JwtBearer", jwtBearerOptions =>
        //    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKeyIsSecretSoDoNotTell")),
        //        ValidateIssuer = false,
        //        ValidateAudience = false,
        //        ValidateLifetime = true,
        //        ClockSkew = TimeSpan.FromMinutes(5)
        //    });
        //}




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
        public static void ConfigureSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
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
        
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer schem.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer 1234abcd'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = ".Net 8 Base API",
                    Description = "Base API call list.",
                    TermsOfService = new Uri("https://www.example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Base",
                        Url = new Uri("https://www.example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Product License",
                        Url = new Uri("https://www.example.com/license")
                    }
                });

                //options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference()
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            },
                //            Scheme = "0auth2",
                //            Name = "Bearer",
                //            In = ParameterLocation.Header
                //        },
                //        new List<string>()
                //    }
                //});
            });
        }
        //public static void ConfigureVersioning(this IServiceCollection services)
        //{
        //    services.AddApiVersioning(options =>
        //    {
        //        options.ReportApiVersions = true;
        //        options.AssumeDefaultVersionWhenUnspecified = true;
        //        options.DefaultApiVersion = new ApiVersion(1, 0);
        //        options.ApiVersionReader = new HeaderApiVersionReader("api-version");
        //    });
        //}
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
            {
                builder.WithOrigins("https://www.example.com/");
                builder.WithHeaders("Content-Type", "Authorization");
                builder.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
                builder.AllowCredentials();
            }));
        }
        //public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        //{
        //    services.AddResponseCaching();
        //    services.AddHttpCacheHeaders(
        //        (expirationOption) =>
        //        {
        //            expirationOption.MaxAge = 120;
        //            expirationOption.CacheLocation = CacheLocation.Private;
        //        },
        //    (validationOption) =>
        //    {
        //        validationOption.MustRevalidate = true;
        //    });
        //}
        public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 1,
                    Period = "5s"
                }
            };
            services.Configure<IpRateLimitOptions>(options =>
            {
                options.GeneralRules = rateLimitRules;
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
        static bool AuthorizeAdminWithClaimsOrSuperAdmin(AuthorizationHandlerContext context)
        {
            return (context.User.IsInRole("Admin") && context.User.HasClaim(c => c.Type == "Create" && c.Value == "True")
                        && context.User.HasClaim(c => c.Type == "Edit" && c.Value == "True")
                        && context.User.HasClaim(c => c.Type == "Delete" && c.Value == "True")
                    ) || context.User.IsInRole("SuperAdmin");
        }
    }
}