using Lib.Data.ConnectionString;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Realtime.Host.Entities;
using RealtimeRealtimeDatabaseSubscriptionSubscription.Hubs;
using SignalRCore.Web;
using SignalRCore.Web.Repository;
using System;
using System.Text;
using System.Threading.Tasks;
using static Realtime.Host.Middleware.MiddlewareExtensions;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace StockTickR
{
    public class Startup
    {
        //public IConfiguration Configuration { get; }
        public IConfigurationRoot Configuration { get; set; }

        // [System.Obsolete]
        [System.Obsolete]
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CoreDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CoreDBConnection")));
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.AddSingleton<IConfiguration>(Configuration);
            //services.AddSingleton<TokenController>(Configuration.GetConnectionString("DbConnection"));
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddControllers();
            services.AddRazorPages();
            //services.AddHttpContextAccessor();
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            //Add Token validation Parameters
            var tokenParams = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = Configuration["JWT:issuer"],
                ValidAudience = Configuration["JWT:audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"]))

            };
            ////Add JWT Authentication
            services.AddAuthentication(options =>
            {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtconfig =>
            {
                jwtconfig.TokenValidationParameters = tokenParams;
                jwtconfig.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/stocks")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };

            });

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                //.WithOrigins("https://bitchainnet.com");
                //.WithOrigins("https://chart.bitchainnet.com");
                .WithOrigins("http://localhost:3010,http://localhost:3000,http://localhost:3002,http://localhost:3003,http://localhost:8070,http://localhost:8073")
                .AllowCredentials();
            }));
            //services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
            services.AddSignalR().AddMessagePackProtocol();

            services.AddSingleton<StockTicker>();
            // dependency injection
            //services.AddSqlServerDbContextFactory<InventoryContext>(Configuration.GetConnectionString("DefaultConnection"));
            services.AddScoped<IInventoryRepository, DatabaseRepository>();
            services.AddSingleton<RealtimeDatabaseSubscription, RealtimeDatabaseSubscription>();
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //IWebHostEnvironment
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseFileServer();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseSqlTableDependency<RealtimeDatabaseSubscription>(Configuration.GetConnectionString("DbConnection"));
           
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            // custom jwt auth middleware
            //app.UseMiddleware<JwtMiddleware>();
            var webSocketOptions = new Microsoft.AspNetCore.Builder.WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(180),
                ReceiveBufferSize = 120 * 1024
            };
            //app.UseWebSockets(webSocketOptions);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHub<RealtimeHub>("/stocks");
                endpoints.MapHub<RealtimeHub>("/stocks", options =>
                {
                    options.Transports =
                        HttpTransportType.WebSockets |
                        HttpTransportType.LongPolling;
                });
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Get}/{id?}");
                        endpoints.MapRazorPages();
            });
            

            
        }
    }
}