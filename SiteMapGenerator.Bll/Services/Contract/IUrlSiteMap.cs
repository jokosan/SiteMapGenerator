﻿using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Bll.Services.Contract.GenericContract;
using SiteMapGenerator.Dal.Models.Dal;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface IUrlSiteMap : IGetFromDatabase<UrlSiteMap>, IDatabaseOperations<UrlSiteMap>
    {
    }
}
