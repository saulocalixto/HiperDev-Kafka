using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Ms_HistoricoClientes.Data.Configuracoes;
using Ms_HistoricoClientes.Data.Repositorios;
using Ms_HistoricoClientes.Services;

namespace Ms_HistoricoClientes
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ms_HistoricoClientes", Version = "v1" });
            });

            services.Configure<HistoricoClientesDatabaseSettings>(
                Configuration.GetSection(nameof(HistoricoClientesDatabaseSettings)));

            services.AddSingleton<IHistoricoClientesDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<HistoricoClientesDatabaseSettings>>().Value);

            services.AddSingleton<HistoricoDeClientesRepositorio>();

            services.AddHostedService<PrimeiroConsumer>();
            services.AddHostedService<SegundoConsumer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ms_HistoricoClientes v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}