using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Ms_HistoricoClientes.Data.Configuracoes;
using Ms_HistoricoClientes.Data.Repositorios;
using Ms_HistoricoClientes_KafkaConsumer.Services;

namespace Ms_HistoricoClientes_KafkaConsumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<HistoricoClientesDatabaseSettings>(
            //    Configuration.GetSection(nameof(HistoricoClientesDatabaseSettings)));

            //services.AddScoped<IHistoricoClientesDatabaseSettings>(sp =>
            //    sp.GetRequiredService<IOptions<HistoricoClientesDatabaseSettings>>().Value);

            //services.AddScoped<HistoricoDeClientesRepositorio>();

            //services.AddSingleton<Consumer>()
            //    .AddHostedService<Consumer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}