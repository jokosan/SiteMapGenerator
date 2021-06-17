using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Dal.Serveses;
using SiteMapGenerator.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMapGenerator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GeneratingSitemap _generatingSitemap;
        private readonly GetFromDatabase _getFromDatabase;
        private readonly SaveDbSiteMap _saveDbSiteMap;

        public HomeController(
          GeneratingSitemap generatingSitemap,
          GetFromDatabase getFromDatabase,
          SaveDbSiteMap saveDbSiteMap)
        {
            _generatingSitemap = generatingSitemap;
            _getFromDatabase = getFromDatabase;
            _saveDbSiteMap = saveDbSiteMap;
        }

        public IActionResult Index()
                 => View();

        [HttpGet]
        public IActionResult UrlPages(string url, int? numberOfLinks)
        {
            if (_generatingSitemap.ValidationAddresses(url) && numberOfLinks != null)
            {
                int idLink = _saveDbSiteMap.SaveUserRequest(url);
                TempData["listError"] = _generatingSitemap.Loading(url, numberOfLinks.Value);

                return RedirectToAction("UserQueryResult", "Home", new RouteValueDictionary(new
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

            if (TempData.ContainsKey("listError"))
            {
                ViewBag.Error = TempData["listError"] as List<string>;
                TempData.Keep("listError");
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
