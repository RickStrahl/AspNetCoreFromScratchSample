using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace ConsoleApplication
{
    public class Program
    {
public static void Main(string[] args)
{
    var host = new WebHostBuilder()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseKestrel()
        .UseStartup<Startup>()                
        .Build();

    host.Run();
}
    }

    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseMvc();

            // this becomes the last handler in the chain
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(
                    "Hello World. The Time is: " +
                    DateTime.Now);
            });
        }
    }


    public class HelloworldController : Controller
    {
        private IHostingEnvironment Environment;
        public HelloworldController(IHostingEnvironment env)
        {
            Environment = env;
        }

        [HttpGet("api/helloworld")]
        public object Helloworld()
        {
            return new
            {
                message = "Hello World",
                time = DateTime.Now
            };
        }

        [HttpGet("helloworld")]
        public ActionResult HelloworldMvc()
        {
            ViewBag.Message = "Hello world!";
            ViewBag.Time = DateTime.Now;

            return View("helloworld");
            //return View("~/helloworld.cshtml");  // from root folder            
        }
    }
}
