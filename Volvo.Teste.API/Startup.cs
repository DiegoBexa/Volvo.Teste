using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volvo.Teste.Repositorio;
using Volvo.Teste.Repositorio.ContextoDb;
using Volvo.Teste.Repositorio.Interface;
using Volvo.Teste.Servico;
using Volvo.Teste.Servico.AutoMapper;
using Volvo.Teste.Servico.Interface;
using Volvo.Teste.Swagger;

namespace Volvo.Teste.API
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
            services.AddControllers();

            var connection = Configuration["ConexaoSqlite:SqliteConnectionString"];

            services.AddDbContext<Contexto>(options =>
                options.UseSqlite(connection)
            );

            services.AddAutoMapper(typeof(AutoMapperSetup));
            services.AddSwaggerConfiguration();

            #region [ Inje��o de independencia]

            services.AddTransient<ICaminhaoRepositorio, CaminhaoRepositorio>();
            services.AddTransient<IMarcaRepositorio, MarcaRepositorio>();
            services.AddTransient<IModeloRepositorio, ModeloRepositorio>();

            services.AddTransient<ICaminhaoServico, CaminhaoServico>();
            services.AddTransient<IMarcaServico, MarcaServico>();
            services.AddTransient<IModeloServico, ModeloServico>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<Contexto>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerConfiguration();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
