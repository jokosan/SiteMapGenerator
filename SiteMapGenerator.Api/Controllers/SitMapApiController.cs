using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteMapGenerator.Api.Wrappers;
using SiteMapGenerator.Bll.BusinessLogic;
using SiteMapGenerator.Bll.Models;
using SiteMapGenerator.Dal.Models.Dal;
using SiteMapGenerator.Dal.Serveses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMapGenerator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitMapApiController : ControllerBase
    {
        private readonly TableArchiveOfRequest _tableArchiveOfRequest;
        private readonly TableUrlResult _tableUrlResult;
        private readonly LinkValidator _linkValidator;

        public SitMapApiController(
           TableArchiveOfRequest tableArchiveOfRequest,
           TableUrlResult tableUrlResult,
           LinkValidator linkValidator)
        {
            _tableArchiveOfRequest = tableArchiveOfRequest;
            _tableUrlResult = tableUrlResult;
            _linkValidator = linkValidator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _tableArchiveOfRequest.GetArchiveOfRequest();

            return Ok(new Response<ArchiveOfRequest>(result));
        }

        [HttpGet("{id}")]
        public IActionResult UserQueryResult(int? id)
        {
            var result = _tableUrlResult.ResultGroupJoin(id.Value);

            return Ok(new Response<UrlResult>(result));
        }

        [HttpGet]
        [Route("ArxivRequest")]
        public IActionResult ArxivRequest()
        {
            var result = _tableArchiveOfRequest.GetArchiveOfRequest();

            return Ok(new Response<ArchiveOfRequest>(result));
        }

        [HttpGet]
        [Route("ProcessingUserRequest")]
        public IActionResult ProcessingUserRequest(string url)
        {
            int idLink = _tableUrlResult.SearchLinksAndSaveResult(url);

            return Ok(new Response<UrlResult>(GetUrlResult(idLink, DateTime.Now.Date)));
        }

        [HttpGet]
        [Route("ArxivDetails")]
        public ActionResult<UrlResult> ArxivDetails(int? id, DateTime? date)
        {
            var result = GetUrlResult(id, date);

            return Ok(new Response<UrlResult>(result));
        }

        private IEnumerable<UrlResult> GetUrlResult(int? id, DateTime? date)
             => _tableUrlResult.ResultArxivDetails(id, date);
    }
}
