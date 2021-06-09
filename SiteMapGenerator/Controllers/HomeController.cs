using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SiteMapGenerator.Bll.Services.Contract;
using SiteMapGenerator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMapGenerator.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ILoadingSiteAddresses _loadingSite;
        private readonly IMapper _mapper;

        public HomeController(
            //ILogger<HomeController> logger,
            ILoadingSiteAddresses loadingSite,
            IMapper mapper)
        {
           // _logger = logger;
            _loadingSite = loadingSite;
            _mapper = mapper;
        }

        public IActionResult Index()
                 => View();


        [HttpGet]
        public IActionResult UrlPages(string url, int? numberOfLinks)
        {
            if (_loadingSite.ValidationAddresses(url) && numberOfLinks != null)
            {
                int idLink = _loadingSite.SaveUserRequest(url);
                TempData["listError"] = _loadingSite.Loading(url, numberOfLinks.Value, idLink);

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

        public ActionResult UserQueryResult(int? id)
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

            return View(_mapper.Map<IEnumerable<UrlSiteMapModel>>(_loadingSite.GetSitemaps(id.Value)));
        }

        public IActionResult ArxivRequest()
            => View(_mapper.Map<IEnumerable<ArchiveOfRequestModel>>(_loadingSite.Arxiv()));

        public IActionResult ArxivDetails(int? id, DateTime? date)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IdOrder = id;

            if (date == null)
            {
                var result = _mapper.Map<IEnumerable<JoinResultModel>>(_loadingSite.Arxiv(id.Value));
                ViewBag.DateGroup = result.GroupBy(g =>
                    g.PageTestDate.Value.Date)
                    .ToDictionary(x => x.Key);

                return View(result);
            }
            else
            {
                return View(_mapper.Map<IEnumerable<JoinResultModel>>(_loadingSite.Arxiv(id.Value, date.Value)));
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
