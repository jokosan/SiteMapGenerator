using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Dal.Serveses;
using System;
using System.Linq;

namespace SiteMapGenerator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly LinkValidator _linkValidator;
        private readonly LoadingPageUrls _loadingPageUrls;
        private readonly LoadingSiteMap _loadingSiteMap;
        private readonly WebRequestServeses _webRequestServeses;
        private readonly TableArchiveOfRequest _tableArchiveOfRequest;
        private readonly TableUrlSiteMap _tableUrlSiteMap;
        private readonly TablePageInfo _pageInfo;
        private readonly TableUrlResult _tableUrlResult;

        public HomeController(
          LinkValidator linkValidator,
          LoadingPageUrls loadingPageUrls,
          LoadingSiteMap loadingSiteMap,
          WebRequestServeses webRequestServeses,
          TableArchiveOfRequest tableArchiveOfRequest,
          TableUrlResult tableUrlResult,
          TablePageInfo pageInfo,
          TableUrlSiteMap tableUrlSiteMap)
        {
            _linkValidator = linkValidator;
            _loadingPageUrls = loadingPageUrls;
            _loadingSiteMap = loadingSiteMap;
            _webRequestServeses = webRequestServeses;
            _tableArchiveOfRequest = tableArchiveOfRequest;
            _tableUrlResult = tableUrlResult;
            _tableUrlSiteMap = tableUrlSiteMap;
            _pageInfo = pageInfo;
        }

        public IActionResult Index()
                 => View(_tableArchiveOfRequest.GetArchiveOfRequest());

        [HttpGet]
        public IActionResult UrlPages(string url)
        {
            if (_linkValidator.CheckURLValid(url))
            {
                int idLink = _tableArchiveOfRequest.SaveUserRequest(url);
                var resultLink = _webRequestServeses.SpeedPageUploads(_loadingPageUrls.ExtractHref(url), _loadingSiteMap.SearchSitemap(url));

                _tableUrlResult.Save(resultLink, _tableUrlSiteMap.RequestToGetMatchesForGiven(idLink), idLink);


                return RedirectToAction("ArxivDetails", "Home", new RouteValueDictionary(new
                {
                    id = idLink
                }));
            }
            else
            {
                ViewBag.Eror = "wrong address";
                return RedirectToAction("Index");
            }
        }

        public IActionResult UserQueryResult(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            return View(_tableUrlResult.JoinTableGroup(_tableUrlResult.JoinTableUrlSiteMapToPageInfo(id.Value)));
        }

        public IActionResult ArxivRequest()
        {
            return View(_tableArchiveOfRequest.GetArchiveOfRequest());
        }

        public IActionResult ArxivDetails(int? id, DateTime? date)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IdOrder = id;

            if (date == null)
            {
                var result = _tableUrlResult.JoinTableUrlSiteMapToPageInfo(id.Value);

                if (result.Count() != 0 || result != null)
                {
                    ViewBag.DateGroup = result.GroupBy(g =>
                        g.PageTestDate.Value.Date)
                        .ToDictionary(x => x.Key);
                }

                return View(result);
            }
            else
            {
                return View(_tableUrlResult.JoinTableUrlSiteMapToPageInfo(id.Value)
                    .Where(x => x.PageTestDate.Value.Date == date.Value.Date));
            }
        }
    }
}
