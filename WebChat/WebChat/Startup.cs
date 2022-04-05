using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using WebChat.Bot.Services;
using WebChat.DbContexts;
using WebChat.MessageBroker;
using WebChat.Models;
using WebChat.Services.Auth;
using WebChat.Services.Broker;
using WebChat.Services.Messages;
using WebChat.Services.Stocks;
using WebChat.Services.Users;

namespace WebChat
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
            services.AddControllers();
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("allowSpecificOrigins", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:4200");
                });
            });

            var key = Encoding.ASCII.GetBytes(Settings.Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddHostedService<ProcessMesageConsumer>();
            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMqConfig"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IBroker, Broker>();
            services.AddSingleton<IPassword, Password>();
            services.AddScoped<IMessages, Messages>();
            services.AddScoped<IStocks, Stocks>();
            services.AddScoped<IStocksBotService, StocksBotService>();
            services.AddScoped<IMessagesRepository, MessagesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddDbContext<MessagesLibraryContext>(x => x.UseSqlServer(Configuration.GetConnectionString("ConnStr")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseCors("allowSpecificOrigins");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
