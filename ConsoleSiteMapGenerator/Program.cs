using ConsoleSiteMapGenerator.Infrastructure;
using ConsoleSiteMapGenerator.Infrastructure.Constants;
using SiteMapGenerator.Bll.BusinessLogic;
using System;

namespace ConsoleSiteMapGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var appStart = new AppStart();
            appStart.Start();

            Console.ReadLine();
        }
    }
}
