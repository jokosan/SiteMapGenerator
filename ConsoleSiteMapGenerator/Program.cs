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
            var linkValidator = new LinkValidator();
            var loadingPageUrls = new LoadingPageUrls(linkValidator);
            var websiteLoadingSpeed = new WebsiteLoadingSpeed(linkValidator);
            var userInteraction = new UserInteraction();
            var generatingSitemap = new GeneratingSitemap(linkValidator, loadingPageUrls, websiteLoadingSpeed);
            var printResult = new PrintResult(userInteraction);

            userInteraction.Info(MessageUsers.Start);
            string userUrl = userInteraction.UserValueInput();

            if (generatingSitemap.ValidationAddresses(userUrl))
            {
                userInteraction.Info(MessageUsers.Waiting);
                printResult.SiteMapPrint(generatingSitemap.Loading(userUrl, 10));
            }
            else
            {
                userInteraction.Info(MessageUsers.IncorrectUrl);
                
                Environment.Exit(0);
            }

            Console.ReadLine();
        }
    }
}
