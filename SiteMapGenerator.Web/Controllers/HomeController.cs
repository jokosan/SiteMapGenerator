using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SiteMapGenerator.Web.Facade;
using System;
using System.Linq;

namespace SiteMapGenerator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ParserFacade _parserFacade;
        private readonly DateBaseFacade _dateBaseFacade;

        public HomeController(
            ParserFacade parserFacade,
            DateBaseFacade dateBaseFacade)
        {
            _parserFacade = parserFacade;
            _dateBaseFacade = dateBaseFacade;
        }

        public IActionResult Index()
        {
            return View(_dateBaseFacade.GetAllTableArchiveOfRequest());
        }

        [HttpGet]
        public IActionResult UrlPages(string url)
        {
            if (_parserFacade.CheckLink(url))
            {
                var resultLink = _parserFacade.FindLinks(url);
                var idLink = _dateBaseFacade.GetId(url);

                _dateBaseFacade.Save(resultLink, idLink);

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

            return View(_dateBaseFacade.ResultGroupJoin(id.Value));
        }

        public IActionResult ArxivRequest()
        {
            return View(_dateBaseFacade.GetAllTableArchiveOfRequest());
        }

        public IActionResult ArxivDetails(int? id, DateTime? date)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IdOrder = id;
            var result = _dateBaseFacade.ResultArxivDetails(id, date);

            if (result.Count() != 0 || result != null)
            {
                ViewBag.DateGroup = result.GroupBy(g =>
                    g.PageTestDate.Value.Date)
                    .ToDictionary(x => x.Key);
            }

            return View(result);
        }
    }
}
