using ConsoleSiteMapGenerator.Infrastructure;
using ConsoleSiteMapGenerator.Infrastructure.Constants;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Dal.Serveses;
using System;

namespace SiteMapGenerator.Dal
{
    public class StartConsole
    {
        private readonly SaveDbSiteMap _saveDbSiteMap;
        private readonly GetFromDatabase _getFromDatabase;

        public StartConsole(
            SaveDbSiteMap saveDbSiteMap,
            GetFromDatabase getFromDatabase)
        {
            _saveDbSiteMap = saveDbSiteMap;
            _getFromDatabase = getFromDatabase;
        }

        public void StartMain()
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
                var idArxiv = _saveDbSiteMap.SaveUserRequest(userUrl);
                _saveDbSiteMap.Save(generatingSitemap.Loading(userUrl, 10), _getFromDatabase.GetSiteMap(), idArxiv);
                printResult.SiteMapPrint(_getFromDatabase.JoinTableGroup(_getFromDatabase.JoinTableUrlSiteMapToPageInfo(idArxiv)));
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
