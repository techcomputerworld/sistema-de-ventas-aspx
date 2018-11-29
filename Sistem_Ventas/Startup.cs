using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistem_Ventas.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sistem_Ventas
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.adddefaultidentity<identityuser>()
            //    .addentityframeworkstores<applicationdbcontext>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //configuración de nuestra cookie de sesión 
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                //el tiempo de expiración sera de 1 dia, porqué le he puesto un 1 si le pongo 3 serian 2 dias
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                //esta dirección será la direccion donde pongamos nuestra cookie 
                options.LoginPath = "/Home/Index";
            });
            //crearemos variables de sesiones 
            services.AddSession(options =>
            {
                //este nombre que le hemos colocado es el nombre que tendra la cookie, puedo poner el que quiera
                options.Cookie.Name = ".SystemVentas.Session";
                options.IdleTimeout = TimeSpan.FromHours(12);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //hay que especificarle a la aplicación de que vamos a usar variables de sesión de esta manera.
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //colocaremos el nombre del controlador y el nombre del método
            //este método nos proporciona el código de error por método Get 
            app.UseStatusCodePagesWithReExecute("/Error/Error", "?statusCode={0}");
            //esto nos proporciona un cógido de error 
            //app.UseStatusCodePages();
            //Con esta configuración cualquier web que no exista nos redireccionara a nuestra web de error.
            //app.UseStatusCodePagesWithRedirects("/Error");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapAreaRoute(
                    name: "Principal", 
                    areaName: "Principal",
                    template: "{controller=Principal}/{action=Index}/{id?}" );
                routes.MapAreaRoute(
                    name: "Usuarios",
                    areaName: "Usuarios",
                    template: "{controller=Usuarios}/{action=Index}/{id?}");
            });
            
        }
        
    }
}
