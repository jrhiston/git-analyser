using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace git_analyser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls("http://localhost:1234")
                .Build();

            host.Run();
        }
    }
}
