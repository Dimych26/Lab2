using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Data;
using Lab2.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lab2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
           

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }

}
//var host = CreateWebHostBuilder(args).Build();

//using (var scope = host.Services.CreateScope())
//{
//    Task t;
//    var services = scope.ServiceProvider;
//    try
//    {
//        var userManager = services.GetRequiredService<UserManager<User>>();
//        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();

//        t = RoleInitializator.InitializeAsync(userManager, rolesManager);
//        t.Wait();

//    }
//    catch (Exception ex)
//    {

//    }
//}

//host.Run();