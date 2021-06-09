using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Bll.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface IUrlSiteMap : IGetFromDatabase<UrlSiteMapBll>, IDatabaseOperations<UrlSiteMapBll>
    {
    }
}
