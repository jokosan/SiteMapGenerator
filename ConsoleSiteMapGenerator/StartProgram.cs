using ConsoleSiteMapGenerator.Infrastructure.Constants;
using ConsoleSiteMapGenerator.Infrastructure.Contract;
using SiteMapGenerator.Bll.BusinessLogic.Contract;
using SiteMapGenerator.Bll.Models.Bll;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleSiteMapGenerator
{
    public class StartProgram
    {
        private readonly IUserInteraction _userInteraction;
        private readonly IGeneratingSitemap _loadingSiteAddresses;

        public StartProgram(
            IUserInteraction userInteraction,
            IGeneratingSitemap loadingSiteAddresses)
        {
            _userInteraction = userInteraction;
            _loadingSiteAddresses = loadingSiteAddresses;
        }

        public void Start()
        {
            _userInteraction.Info(MessageUsers.MessageStart);
            string userUrl = _userInteraction.UserValueInput();

            if (!_loadingSiteAddresses.ValidationAddresses(userUrl))
            {
                _userInteraction.Info(MessageUsers.MessageIncorrectUrl);
                return;
            }

            _userInteraction.Info(MessageUsers.WaitingMessage);
            PrintResult(_loadingSiteAddresses.Loading(userUrl, 50));
        }

        public void PrintResult(IEnumerable<JoinResultBll> resultUrl)
        {
            var statusCode = resultUrl.Where(x => x.StatusCode == 200);

            foreach (var item in statusCode)
            {
                _userInteraction.Info($"Url {item.NameSite} | LoadingSpeed  {item.WebsiteLoadingSpeed}");
            }
        }
    }
}
