using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MoneyTrack
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
{
    services.Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = context => false;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });

    services.AddScoped<MoneyTrackRepository>(); // Register the AlunosRepository service

    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    services.AddMemoryCache();
    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromHours(12);
        options.Cookie.Name = "MoneyTrack";
        options.Cookie.IsEssential = true;
    });
}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
       public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseCookiePolicy();
    app.UseSession();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
       
        endpoints.MapControllerRoute(
        name: "CadastroContato",
        pattern: "Cadastro/Contato",
        defaults: new { controller = "Money", action = "CadastroContato" }
     );

        endpoints.MapControllerRoute(
        name: "CadastroFinancas",
        pattern: "Cadastro/Financas",
        defaults: new { controller = "Money", action = "CadastroFinancas" }
    );

        endpoints.MapControllerRoute(
        name: "BTC",
        pattern: "Cadastro/BTC",
        defaults: new { controller = "Crypto", action = "BTC" }
    );

        endpoints.MapControllerRoute(
        name: "Login",
        pattern: "Cadastro/Login",
        defaults: new { controller = "Money", action = "Login" }
    );

        endpoints.MapControllerRoute(
        name: "Lista",
        pattern: "Cadastro/Lista",
        defaults: new { controller = "Money", action = "Lista" }
    );

        endpoints.MapControllerRoute(
        name: "Logout",
        pattern: "Cadastro/Logout",
        defaults: new { controller = "Money", action = "Logout" }
    );

      
        endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");        });
    }
  }
}