using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

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
   
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseMvc();

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Pre Processing");

            //    await next();

            //    await context.Response.WriteAsync("Post Processing");
            //});

            // Handler of last Resort
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(
                    "Hello World of the last resort. The Time is: " +
                    DateTime.Now.ToString("hh:mm:ss tt"));

            });
        }
    }

    public class HelloWorldController : Controller
    {
        [HttpGet("api/helloworld")]
        public object HelloWorld()
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
            //return View("~/helloworld.cshtml");
        }
    }
}