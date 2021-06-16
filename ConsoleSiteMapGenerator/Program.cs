using ConsoleSiteMapGenerator.Infrastructure.DependencyInjection;
using ConsoleSiteMapGenerator.Infrastructure;
using SiteMapGenerator.Bll.BusinessLogic;
using System;

namespace ConsoleSiteMapGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider.RegisterServices();

            var linkCheck = new LinkValidator();
            var loadingPageUrls = new LoadingPageUrls(linkCheck);
            var websiteLoadingSpeed = new WebsiteLoadingSpeed(linkCheck);
            var userInteraction = new UserInteraction();
            var generatingSitemap = new GeneratingSitemap(linkCheck, loadingPageUrls, websiteLoadingSpeed);

            new StartProgram(userInteraction, generatingSitemap).Start();

            Console.ReadLine();
        }
    }
}
