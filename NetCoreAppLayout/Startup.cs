using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreAppLayout.Models;
using NetCoreAppLayout.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;


namespace NetCoreAppLayout
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // uygulamam fluent validation desteklesin. ve uygulama i�erisindeki startup dosyas�n�n bulundu�u dizinde ne kadar validator s�n�f� varsa projeye register et diyoruz.
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddFluentValidation(x=> x.RegisterValidatorsFromAssemblyContaining<Startup>()); // mvc application

         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); // wwwroot alt�ndaki dosyalar�n �al��mas�n� sa�lar. statik dosyalar�n tutuldu�u dizini aktif hale getiren servis. built-in net core taraf�ndan yaz�lm�� bir middleware
            app.UseRouting();

            // bu middleware uygulama gelen isteklerin route isteklerin farkl� uygulama templateleri i�in yakalanmas�n� sa�lar.
            app.UseEndpoints(endpoints =>
            {
                // i�erisinde default bir y�nledirme kural� varsa kullan�r�z.
               
                // mapWhen mant��� kulland��� i�in a�a��daki kod blo�unu kesti
                //endpoints.MapGet("/", async context =>
                //{
                //    // anasayfa y�nlenmesinde MapControllerRoute alt sat�r�nda oldu�u i�in bu k�s�m �al���r
                //});

                endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");

                // buda uygulamada area varsa areas k�sm�na y�nlendirmemizi sa�lar.
                //endpoints.MapAreaControllerRoute();

                // endpoints.MapHub(); uygulamam�z i�erisinde signalr ile socket programlama varsa ilgili hub class y�nlendirir. chat uygulamalar� i�in 
                // endpoints.MapRazorPages(); // uygulama razor web uygulamas� olarak a��lm�� ise pages sayfalar�na y�nlendirir. buda page centric bir web uygulamas�

                // endpoints.MapControllers();  // default bir sayfa y�nledirmesi yapmayacak isek mvc ve api uygulamalard�nda uygulaman�n controller y�nlenmesini sa�lar.

                // endpoints.MapBlazorHub(); // blazor net core spa uygulamas�n�n sayfalar�na y�nlendirir.

                // bir ka� sayfadan olu�an basit bir uygulama yaz�cak isek zaten a�a��daki gibi bir route ayar� yapabiliriz.
                endpoints.MapGet("/home", async context =>
                {
                    var pageServices = new PageService();
                    var htmlResult = pageServices.PageResult("Anasayfa", "Anasayfa i�eri�i");
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(htmlResult);
                });


                endpoints.MapGet("/about", async context =>
                {
                    var pageServices = new PageService();
                    var htmlResult = pageServices.PageResult("Hakk�m�zda", "Hakk�m�zda Sayfa");


                    // Karakterleri d�zg�n bir formatta g�sterebilmek i�in charset=utf-8 olarak ayarlad�k yoksa t�rk�e karakter deste�i olmuyordu.
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(htmlResult);
                });


                endpoints.MapGet("/user-json", async context =>
                {
                    var userServices = new UserService();
                    var users = userServices.Users;

                    context.Response.ContentType = "application/json";

                    // JsonSerializer bir c# nesnesini jsonString �evirmek i�in kulland���m�z bir k�t�phane
                    //var usersJson = System.Text.Json.JsonSerializer.Serialize(users);
                    //await context.Response.WriteAsync(usersJson);

                    await context.Response.WriteAsJsonAsync(users);

                });


                endpoints.MapGet("/text-file", async context =>
                {
                    var userServices = new UserService();
                    var users = userServices.Users;


                    context.Response.ContentType = "text/plain; charset=utf-8";
                    await context.Response.WriteAsync("G�nayd�n!");

                });

                // Uygulama ya form �zerinden veri g�nderirken kullanaca��m�z routelar�n tan�mlamas�n� yapabilir
                endpoints.MapPost("create-user", async context =>
                {


                    // async bir func oldu�u i�in await ile �a��r�r�z.
                     var model = await BodyParser<List<UserModel>>.ParseAsync(context.Request.Body, context.Request.ContentType);
                });

              
            });
        }
    }
}
