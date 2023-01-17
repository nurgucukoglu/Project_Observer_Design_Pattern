using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project_Observer_Design_Pattern.DAL;
using Project_Observer_Design_Pattern.ObserverDesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Observer_Design_Pattern
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
            //observer'� tan�ml�yoruz
            services.AddDbContext<Context>();
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();

            services.AddSingleton<UserObserverSubject>(sp =>
            {
                UserObserverSubject userObserverSubject = new();
                //her bir s�n�f� subjecte kaydedece�im.
                userObserverSubject.RegisterObserver(new UserObserverWriteToConsole(sp));//her bir observer s�n�f�n� kaydedecek
                userObserverSubject.RegisterObserver(new UserObserverCreateDiscount(sp));
                userObserverSubject.RegisterObserver(new UserObserverSendMail(sp));
                //t�m tan�mlad���m s�n�flar� burada �a��r�yorum
                return userObserverSubject;
            });//observer'la ilgili ana metotlar�m�n oldu�u s�n�f� al�r.

            services.AddControllersWithViews();


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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
