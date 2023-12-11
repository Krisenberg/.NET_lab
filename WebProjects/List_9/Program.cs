using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace List_9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var clone = Thread.CurrentThread.CurrentCulture.Clone() as CultureInfo;
            //clone.NumberFormat.NumberDecimalSeparator = ".";

            //Thread.CurrentThread.CurrentCulture = clone;
            //Thread.CurrentThread.CurrentUICulture = clone;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
