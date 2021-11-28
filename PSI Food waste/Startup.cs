﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PSI_Food_waste.Services;
using PSI_Food_waste.Models;
using AspNetCoreHero.ToastNotification;
using Microsoft.EntityFrameworkCore;
using PSI_Food_waste.Data;

namespace PSI_Food_waste
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
            services.AddRazorPages();
            services.AddSession();
            services.AddSingleton<IProductRepository, ProductService>();
            services.AddSingleton<IRestaurantRepository, RestaurantServices>();
            services.AddSingleton<IRegisterRepository, RegisterService>();
            services.AddSingleton<IRegistrationEventNotifier, RegistrationEventNotifier>();
            services.AddTransient<INotificationEvent, NotificationEvent>();
            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

            services.AddDbContext<ProductContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ProductContext")));

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ProductContext>();
                context.Database.EnsureCreated();
                DbInitializer.Initialize(context);
            }

            //string path = @"C:\Users\jauta\source\repos\PSI Food waste\text.json";
            //string json = JsonConvert.SerializeObject(ProductService.GetAll(), Formatting.Indented);
            //File.WriteAllText(path, json);



            //string initialData = (Directory.GetCurrentDirectory() + "\\text.json");
            //string json = File.ReadAllText(@initialData);
            //List<Product> myObj = JsonConvert.DeserializeObject<List<Product>>(json);
            //_productRepository.SetAll(myObj);

        }
    }
}
