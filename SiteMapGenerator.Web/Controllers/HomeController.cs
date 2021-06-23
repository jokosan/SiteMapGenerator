using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Dal.Serveses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteMapGenerator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly LinkValidator _linkValidator;
        private readonly GetFromDatabase _getFromDatabase;
        private readonly SaveDbSiteMap _saveDbSiteMap;
        private readonly LoadingPageUrls _loadingPageUrls;
        private readonly LoadingSiteMap _loadingSiteMap;
        private readonly WebRequestServeses _webRequestServeses;

        public HomeController(
          GetFromDatabase getFromDatabase,
          SaveDbSiteMap saveDbSiteMap,
          LinkValidator linkValidator,
          LoadingPageUrls loadingPageUrls,
          LoadingSiteMap loadingSiteMap,
          WebRequestServeses webRequestServeses)
        {
            _getFromDatabase = getFromDatabase;
            _saveDbSiteMap = saveDbSiteMap;
            _linkValidator = linkValidator;
            _loadingPageUrls = loadingPageUrls;
            _loadingSiteMap = loadingSiteMap;
            _webRequestServeses = webRequestServeses;
        }

        public IActionResult Index()
                 => View(_getFromDatabase.GetArchiveOfRequest());

        [HttpGet]
        public IActionResult UrlPages(string url)
        {
            if (_linkValidator.CheckURLValid(url))
            {
                int idLink = _saveDbSiteMap.SaveUserRequest(url);
                var resultLink = _webRequestServeses.SpeedPageUploads(_loadingPageUrls.ExtractHref(url), _loadingSiteMap.SearchSitemap(url));

                _saveDbSiteMap.Save(resultLink, _getFromDatabase.GetSiteMap().Where(x => x.ArchiveOfRequestsId == idLink), idLink);


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

            return View(_getFromDatabase.JoinTableGroup(_getFromDatabase.JoinTableUrlSiteMapToPageInfo(id.Value)));
        }

        public IActionResult ArxivRequest()
            => View(_getFromDatabase.GetArchiveOfRequest());

        public IActionResult ArxivDetails(int? id, DateTime? date)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IdOrder = id;

            if (date == null)
            {
                var result = _getFromDatabase.JoinTableUrlSiteMapToPageInfo(id.Value);

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
                return View(_getFromDatabase.JoinTableUrlSiteMapToPageInfo(id.Value)
                    .Where(x => x.PageTestDate.Value.Date == date.Value.Date));
            }
        }


    }
}
