using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SiteMapGenerator.Dal.Utilities;

namespace SiteMapGenerator.Dal
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            var startConsole = host.Services.GetService<StartConsole>();
            startConsole.StartMain();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               .ConfigureServices((hostContext, services) =>
               {
                   DbContextServiceCollectionDal.Initialize(services, "Server=LAPTOP-DN88AAG5\\SQLEXPRESS; initial catalog=SitMapDb; integrated security=True;MultipleActiveResultSets=True");
                   ServisesDi.Initialize(services);
               });
    }
}
