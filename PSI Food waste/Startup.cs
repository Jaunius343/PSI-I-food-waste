using Microsoft.AspNetCore.Builder;
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
