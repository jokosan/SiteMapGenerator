using SiteMapGenerator.Bll.BusinessLogic.Contract;
using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Bll.Services.Contract;
using SiteMapGenerator.Dal.Models.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class LoadingSiteAddresses : ILoadingSiteAddresses
    {
        private readonly ILinkCheck _linkCheck;
        private readonly ILoadingPageUrls _loadingPageUrls;
        private readonly IWebsiteLoadingSpeed _websiteLoadingSpeed;
        private readonly IOutput _output;
        private readonly IArchiveOfRequest _archiveOfRequest;

        public LoadingSiteAddresses(
            ILinkCheck linkCheck,
            ILoadingPageUrls loadingPageUrls,
            IWebsiteLoadingSpeed websiteLoadingSpeed,
            IOutput output,
            IArchiveOfRequest archiveOfRequest)
        {
            _linkCheck = linkCheck;
            _loadingPageUrls = loadingPageUrls;
            _websiteLoadingSpeed = websiteLoadingSpeed;
            _output = output;
            _archiveOfRequest = archiveOfRequest;
        }

        public int SaveUserRequest(string url)
        {
            var resultArxiv = _archiveOfRequest.GetTableAll();

            if (resultArxiv.Any(x => x.NameUrl.Contains(url)))
            {
                var resultId = resultArxiv.FirstOrDefault(x => x.NameUrl.Contains(url));
                return resultId.IdArchiveOfRequests;
            }
            else
            {
                var archive = new ArchiveOfRequest();
                archive.NameUrl = url;

                _archiveOfRequest.Insert(archive);

                return archive.IdArchiveOfRequests;
            }
        }

        public List<JoinResultBll> Loading(string url, int numberOfLinks, int IdUri)
            => _websiteLoadingSpeed.SpeedPageUploads(_loadingPageUrls.ExtractHref(url, numberOfLinks), IdUri);

        public List<JoinResultBll> Loading(string url, int numberOfLinks)
           => _websiteLoadingSpeed.SpeedPageUploads(_loadingPageUrls.ExtractHref(url, numberOfLinks));

        public bool ValidationAddresses(string url)
            => _linkCheck.UrlValidation(_linkCheck.AddressHost(url));

        public IEnumerable<JoinResultBll> GetSitemaps(int id)
            => _output.JoinTableGroup(_output.JoinTable(id));

        public IEnumerable<ArchiveOfRequest> Arxiv()
            => _archiveOfRequest.GetTableAll();

        public IEnumerable<JoinResultBll> Arxiv(int id)
            => _output.JoinTable(id).OrderBy(x => x.PageTestDate);

        public IEnumerable<JoinResultBll> Arxiv(int id, DateTime date)
            => _output.JoinTable(id).Where(w => w.PageTestDate.Value.Date == date.Date).OrderBy(x => x.PageTestDate);
    }
}
