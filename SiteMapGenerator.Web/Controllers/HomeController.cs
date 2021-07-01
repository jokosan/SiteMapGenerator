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
        private readonly TableArchiveOfRequest _tableArchiveOfRequest;
        private readonly TableUrlResult _tableUrlResult;
        private readonly LinkValidator _linkValidator;

        public HomeController(
             TableArchiveOfRequest tableArchiveOfRequest,
             TableUrlResult tableUrlResult,
             LinkValidator linkValidator)
        {
            _tableArchiveOfRequest = tableArchiveOfRequest;
            _tableUrlResult = tableUrlResult;
            _linkValidator = linkValidator;
        }

        public IActionResult Index()
        {
            var result = _tableArchiveOfRequest.GetArchiveOfRequest();

            return View(result);
        }

        [HttpGet]
        public IActionResult ProcessingUserRequest(string url)
        {
            if (_linkValidator.CheckURLValid(url))
            {
                int idLink = _tableUrlResult.SearchLinksAndSaveResult(url);

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

            return View(_tableUrlResult.ResultGroupJoin(id.Value));
        }

        public IActionResult ArxivRequest()
        {
            var result = _tableArchiveOfRequest.GetArchiveOfRequest();

            return View(result);
        }

        public IActionResult ArxivDetails(int? id, DateTime? date)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IdOrder = id;
            var result = _tableUrlResult.ResultArxivDetails(id, date);

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
