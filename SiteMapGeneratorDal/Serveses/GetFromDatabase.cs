using SiteMapGenerator.Bll.Models;
using SiteMapGenerator.Dal.Models.Dal;
using System.Collections.Generic;
using System.Data;

namespace SiteMapGeneratorDal.Serveses
{
    public class GetFromDatabase 
    {
        private readonly IRepository<ArchiveOfRequest> _repositoryArchiveOfRequest;
        private readonly IRepository<PageInfo> _repositoryPageInfo;
        private readonly IRepository<UrlSiteMap> _repositoryUrlSiteMap;

        public GetFromDatabase(
            IRepository<ArchiveOfRequest> repositoryArchiveOfRequest,
            IRepository<PageInfo> repositoryPageInfo,
            IRepository<UrlSiteMap> repositoryUrlSiteMap)
        {
            _repositoryArchiveOfRequest = repositoryArchiveOfRequest;
            _repositoryPageInfo = repositoryPageInfo;
            _repositoryUrlSiteMap = repositoryUrlSiteMap;
        }

        public IEnumerable<ArchiveOfRequest> GetArchiveOfRequest()
            => _repositoryArchiveOfRequest.GetAll();

        public IEnumerable<UrlSiteMap> GetSiteMap()
            => _repositoryUrlSiteMap.GetAll();

        public IEnumerable<PageInfo> GetPageInfos()
            => _repositoryPageInfo.GetAll();

        public IEnumerable<UrlResult> JoinTableUrlSiteMapToPageInfo()
        {
            return null;
        }
    }
}
