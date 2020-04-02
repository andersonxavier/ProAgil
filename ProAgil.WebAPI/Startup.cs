using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProAgil.Repository;

namespace ProAgil.WebAPI
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
            services.AddDbContext<ProAgilContext>(
                x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
            );

            // Toda vez que alguem precisar de um "IProAgilRepository" ele Injetara "ProAgilRepository";
            // Usado na controler "EventoController-+"
            services.AddScoped<IProAgilRepository, ProAgilRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            //Colocado devido ao erro (Esse erro deu no projeto que tentou consumir ele, dando erro no acesso):
            /*Access to XMLHttpRequest at 'http://localhost:5000/api/values' from origin 'http://localhost:4200' 
            has been blocked by CORS policy: No 'Access-Control-Allow-Origin' header is present on the requested resource.*/
            //Ref: http://www.macoratti.net/18/05/aspcore_cors1.htm
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //retorna erro amigavel do HTTP... Caso seja comentada retornara o erro padrao
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            /*O Cross-Origin Resource Sharing ou 'compartilhamento de recursos de origem cruzada' cujo acrônimo é CORS é um padrão W3C sendo 
            uma especificação de uma tecnologia de navegadores que define meios para um servidor permitir que seus recursos sejam acessados por 
            uma página web de um domínio diferente*/
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            //Permite acessar imagens dentro da pasta "wwwroot" na pasta raiz do seu projeto
            app.UseStaticFiles();
            
            //Comentado para nao dar erro de HTTPS
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
