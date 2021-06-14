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

            var linkCheck = new LinkCheck();
            var loadingPageUrls = new LoadingPageUrls(linkCheck);
            var websiteLoadingSpeed = new WebsiteLoadingSpeed(linkCheck);

            new StartProgram(new UserInteraction(), new LoadingSiteAddressesConsole(linkCheck, loadingPageUrls, websiteLoadingSpeed)).Start();

            Console.ReadLine();
        }
    }
}
