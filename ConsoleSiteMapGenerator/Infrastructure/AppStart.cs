using ConsoleSiteMapGenerator.Infrastructure.Constants;
using SiteMapGenerator.Bll.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSiteMapGenerator.Infrastructure
{
    class AppStart
    {
        public void Start()
        {
            var linkValidator = new LinkValidator();
            var parser = new Parser();
            var loadingPageUrls = new LoadingPageUrls(linkValidator, parser);
            var websiteLoadingSpeed = new WebsiteLoadingSpeed(linkValidator);
            var userInteraction = new UserInteraction();
            var printResult = new PrintResult(userInteraction);

            userInteraction.Info(MessageUsers.Start);
            string userUrl = userInteraction.UserValueInput();

            if (linkValidator.CheckURLValid(userUrl))
            {
                userInteraction.Info(MessageUsers.Waiting);
                printResult.SiteMapPrint(websiteLoadingSpeed.SpeedPageUploads(loadingPageUrls.ExtractHref(userUrl, 100)));
            }
            else
            {
                userInteraction.Info(MessageUsers.IncorrectUrl);
            }
        }
    }
}
