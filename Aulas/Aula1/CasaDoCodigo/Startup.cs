using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


//Classe de configuração
namespace CasaDoCodigo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //adicionar serviços como sqlserver, serviço de log etc.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            string connectionString = Configuration.GetConnectionString("Default");

            //Serviço para adicionar contexto do bd
            //Método Add.DbContext
            //Classe ApplicationContext
            //Ctrl . using Microsoft.EntityFrameworkCore;
            //Devemos chamar o método AddDbContext dentro do método Startup.ConfigureServices:
            //services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(<< string de conexão >>));
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            //OBJETO:services; MÉTODO:AddTransiente(adiciona uma instancia temporária); TIPO da classe instanciada:DataService
            services.AddTransient<IDataService, DataService>();

            services.AddTransient<IProdutoRepository, ProdutoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Consumir, utilizar os serviços
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    //template: "{controller=Home}/{action=Index}/{id?}");
                    template: "{controller=Pedido}/{action=Carrossel}/{id?}");
            });

            //Com o EnsureCreated uma vez usado não podemos mais usar migration no projeto
            //serviceProvider.GetService<ApplicationContext>().Database.EnsureCreated();

            //Migrate permite usar o migration quantas vezes necessário
            //serviceProvider.GetService<ApplicationContext>().Database.Migrate();

            //System.NullReferenceException: 'Object reference not set to an instance of an object.'
            //serviceProvider.GetService<DataService>().InicializaDB();


            serviceProvider.GetService<IDataService>().InicializaDB();
        }
    }
}
