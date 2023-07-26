using Infrastructure;
using Infrastructure.Repositories;
using Ltj.Domain.Interface.Repository;
using Ltj.Domain.Interface.Services;
using Ltj.Domain.Service;
using Ltj.Shared.AutoMapper;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;

namespace Ltj.api
{
    public class Startup : Interfaces. IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddControllersWithViews();

            #region Conexao
            services.AddTransient<AppDb>(_ =>
                new AppDb(Configuration.GetConnectionString("MySqlConnection")));
            #endregion

            #region Repo

            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            #endregion

            #region Services

            services.AddTransient<IProduto, Produto>();
            services.AddTransient<IFuncionario, Funcionario>();

            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tinnova",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                  {
                    new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                      }
                     },
                     Array.Empty<string>()
                   }
                 });

            });

            #endregion


          
            
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GPS API V1");
                //c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Presentation v1"));
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Presentation v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

        }

    }

 
    public static class StartUpExtensions //metodo de extensão
    {
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder WebAppBuilder) where TStartup : Interfaces.IStartup
        {
            var startup = Activator.CreateInstance(typeof(TStartup), WebAppBuilder.Configuration) as Interfaces.IStartup;
            if (startup == null) throw new ArgumentException("Classe Startup.cs inválida!");

            startup.ConfigureServices(WebAppBuilder.Services);

            var app = WebAppBuilder.Build();
            startup.Configure(app, app.Environment);

            app.Run();

            return WebAppBuilder;
        }
    }
}
