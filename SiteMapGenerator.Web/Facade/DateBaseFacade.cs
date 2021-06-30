using SiteMapGenerator.Bll.Models;
using SiteMapGenerator.Dal.Models.Dal;
using SiteMapGenerator.Dal.Serveses;
using System;
using System.Collections.Generic;

namespace SiteMapGenerator.Web.Facade
{
    public class DateBaseFacade
    {
        private readonly TableArchiveOfRequest _tableArchiveOfRequest;
        private readonly TableUrlSiteMap _tableUrlSiteMap;
        private readonly TablePageInfo _pageInfo;
        private readonly TableUrlResult _tableUrlResult;

        public DateBaseFacade(
              TableArchiveOfRequest tableArchiveOfRequest,
              TableUrlResult tableUrlResult,
              TablePageInfo pageInfo,
              TableUrlSiteMap tableUrlSiteMap)
        {
            _tableArchiveOfRequest = tableArchiveOfRequest;
            _tableUrlResult = tableUrlResult;
            _tableUrlSiteMap = tableUrlSiteMap;
            _pageInfo = pageInfo;
        }

        public IEnumerable<UrlResult> ResultGroupJoin(int id)
        {
            var resultJoin = _tableUrlResult.JoinTableUrlSiteMapToPageInfo(id);

            return _tableUrlResult.JoinTableGroup(resultJoin);
        }

        public void Save(IEnumerable<UrlResult> urlResults, int id)
        {
            var getByIdArchiveOfRequests = _tableUrlSiteMap.RequestToGetMatchesForGiven(id);
            _tableUrlResult.Save(urlResults, getByIdArchiveOfRequests, id);
        }

        public int GetId(string url)
            => _tableArchiveOfRequest.SaveUserRequest(url);

        public IEnumerable<ArchiveOfRequest> GetAllTableArchiveOfRequest()
            => _tableArchiveOfRequest.GetArchiveOfRequest();

        public IEnumerable<UrlResult> ResultArxivDetails(int? id, DateTime? date)
        {
            if (date == null)
            {
                return _tableUrlResult.JoinTableUrlSiteMapToPageInfo(id.Value);
            }
            else
            {
                return _tableUrlResult.SerQueryResult(id.Value, date.Value);
            }
        }
    }
}
