using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SiteMapGenerator.Bll.Services.Contract;
using SiteMapGenerator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SiteMapGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoadingSiteAddresses _loadingSite;

        public HomeController(
            ILoadingSiteAddresses loadingSite)
        {
            _loadingSite = loadingSite;
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

            return View(_loadingSite.GetSitemaps(id.Value));
        }

        public IActionResult ArxivRequest()
            => View(_loadingSite.Arxiv());

        public IActionResult ArxivDetails(int? id, DateTime? date)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IdOrder = id;

            if (date == null)
            {
                var result = _loadingSite.Arxiv(id.Value);

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
                return View(_loadingSite.Arxiv(id.Value, date.Value));
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
