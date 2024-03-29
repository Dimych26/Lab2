﻿using System;
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
using Lab2.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lab2.Interfaces;
using Lab2.Models;
using Lab2.Services;
using Lab2.Models.Cart;

namespace Lab2
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
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
           
            services.AddDbContext<ApplicationDbContext>(options =>
            
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDefaultIdentity<User>()
            //    .AddRoles<IdentityRole>()
            //    .AddDefaultUI()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddIdentity<User, IdentityRole>()
               .AddRoleManager<RoleManager<IdentityRole>>()
               .AddDefaultUI()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            //.AddEntityFrameworkStores<ApplicationDbContext>();
            //services.AddIdentity<User, IdentityRole>()
            //        .AddRoleManager<RoleManager<IdentityRole>>()
            //        //.AddDefaultUI()
            //        //.AddDefaultTokenProviders()
            //        .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IKitchen, Kitchen>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IStorageService, StorageService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddDistributedMemoryCache();
            
            
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => Cart.GetCart(sp));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddSessionStateTempDataProvider();

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddMemoryCache();
            services.AddSession();
         

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            
            

            app.UseAuthentication();
            
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
