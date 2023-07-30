using CleanArchMvc.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanArchMVC.API
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
            services.AddInfrastructureAPI(Configuration);

            //ATIVA A AUTENTICAÇAO E VALIDA O TOKEN
            services.AddInfrastructureJWT(Configuration);
            services.AddInfrastructureSwagger();

            //Para resolver o problema de ciclicidade
            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchMVC.API v1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //UTILIZADO PARA TRATAR AS RESPOSTAS COM UM CÓDIGO DE ESTADO DE 400 A 599.
            //ELE VAI EXPLICITAR O ERRO 401 QUE ESTA SENDO EXIBIDO.
            app.UseStatusCodePages();

            //Trata as resposta com um código de status de 400 a 599
            //Vai explicitar o erro 401 no que está sendo exibido 
            app.UseStatusCodePages();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
