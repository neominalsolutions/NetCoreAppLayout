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
            // uygulamam fluent validation desteklesin. ve uygulama içerisindeki startup dosyasýnýn bulunduðu dizinde ne kadar validator sýnýfý varsa projeye register et diyoruz.
            services.AddControllersWithViews().AddFluentValidation(x=> x.RegisterValidatorsFromAssemblyContaining<Startup>()); // mvc application
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); // wwwroot altýndaki dosyalarýn çalýþmasýný saðlar. statik dosyalarýn tutulduðu dizini aktif hale getiren servis. built-in net core tarafýndan yazýlmýþ bir middleware
            app.UseRouting();

            // bu middleware uygulama gelen isteklerin route isteklerin farklý uygulama templateleri için yakalanmasýný saðlar.
            app.UseEndpoints(endpoints =>
            {
                // içerisinde default bir yönledirme kuralý varsa kullanýrýz.
               
                // mapWhen mantýðý kullandýðý için aþaðýdaki kod bloðunu kesti
                //endpoints.MapGet("/", async context =>
                //{
                //    // anasayfa yönlenmesinde MapControllerRoute alt satýrýnda olduðu için bu kýsým çalýþýr
                //});

                endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");

                // buda uygulamada area varsa areas kýsmýna yönlendirmemizi saðlar.
                //endpoints.MapAreaControllerRoute();

                // endpoints.MapHub(); uygulamamýz içerisinde signalr ile socket programlama varsa ilgili hub class yönlendirir. chat uygulamalarý için 
                // endpoints.MapRazorPages(); // uygulama razor web uygulamasý olarak açýlmýþ ise pages sayfalarýna yönlendirir. buda page centric bir web uygulamasý

                // endpoints.MapControllers();  // default bir sayfa yönledirmesi yapmayacak isek mvc ve api uygulamalardýnda uygulamanýn controller yönlenmesini saðlar.

                // endpoints.MapBlazorHub(); // blazor net core spa uygulamasýnýn sayfalarýna yönlendirir.

                // bir kaç sayfadan oluþan basit bir uygulama yazýcak isek zaten aþaðýdaki gibi bir route ayarý yapabiliriz.
                endpoints.MapGet("/home", async context =>
                {
                    var pageServices = new PageService();
                    var htmlResult = pageServices.PageResult("Anasayfa", "Anasayfa içeriði");
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(htmlResult);
                });


                endpoints.MapGet("/about", async context =>
                {
                    var pageServices = new PageService();
                    var htmlResult = pageServices.PageResult("Hakkýmýzda", "Hakkýmýzda Sayfa");


                    // Karakterleri düzgün bir formatta gösterebilmek için charset=utf-8 olarak ayarladýk yoksa türkçe karakter desteði olmuyordu.
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(htmlResult);
                });


                endpoints.MapGet("/user-json", async context =>
                {
                    var userServices = new UserService();
                    var users = userServices.Users;

                    context.Response.ContentType = "application/json";

                    // JsonSerializer bir c# nesnesini jsonString çevirmek için kullandýðýmýz bir kütüphane
                    //var usersJson = System.Text.Json.JsonSerializer.Serialize(users);
                    //await context.Response.WriteAsync(usersJson);

                    await context.Response.WriteAsJsonAsync(users);

                });


                endpoints.MapGet("/text-file", async context =>
                {
                    var userServices = new UserService();
                    var users = userServices.Users;


                    context.Response.ContentType = "text/plain; charset=utf-8";



                    await context.Response.WriteAsync("Günaydýn!");

                });

                // Uygulama ya form üzerinden veri gönderirken kullanacaðýmýz routelarýn tanýmlamasýný yapabilir
                endpoints.MapPost("create-user", async context =>
                {


                    // async bir func olduðu için await ile çaðýrýrýz.
                     var model = await BodyParser<List<UserModel>>.ParseAsync(context.Request.Body, context.Request.ContentType);


                    //Formdan gönderilen deðerleri param þeklinde
                    //json olarak formdan gönderilen deðerleri ise jsonString þeklinde okuyabiliyoruz.
                    //using (StreamReader stream = new StreamReader(context.Request.Body))
                    //{

                    //    string body = await stream.ReadToEndAsync();
                    //    // body = "param=somevalue&param2=someothervalue"


                    //    if (context.Request.ContentType == "application/json")
                    //    {



                    //        var userModel = System.Text.Json.JsonSerializer.Deserialize<UserModel>(body);
                    //        var stringJson = System.Text.Json.JsonSerializer.Serialize(userModel);
                    //    }


                    //    else if (context.Request.ContentType == "application/x-www-form-urlencoded")
                    //    {

                    //        // formdaki her bir alanýn arasýna & iþareti koyarak gelecek veri
                    //        //List<string> bodyParams = body.Split("&").ToList();

                    //        //for (int i = 0; i < bodyParams.Count; i++)
                    //        //{
                    //        //   bodyParams[i] = HttpUtility.UrlDecode(bodyParams[i]);   
                    //        //}

                    //        //var model = new UserModel
                    //        //{
                    //        //    Email = bodyParams.Find(x => x.Contains("Email")).Replace("Email=",""),
                    //        //    UserName = bodyParams.Find(x => x.Contains("UserName")).Replace("UserName=","")
                    //        //};

                    //        var model = BodyParser<UserModel>.ParseJSON(body);



                    //    }
                    //}
                });

                endpoints.MapPost("create-category", async context =>
                {
                    // Formdan gönderilen deðerleri param þeklinde 
                    // json olarak formdan gönderilen deðerleri ise jsonString þeklinde okuyabiliyoruz.
                    using (StreamReader stream = new StreamReader(context.Request.Body))
                    {


                        string body = await stream.ReadToEndAsync();
                        // body = "param=somevalue&param2=someothervalue"
                       //var model = BodyParser<CategoryModel>.ParseJSON(body);

                        

                    }
                });
            });
        }
    }
}
