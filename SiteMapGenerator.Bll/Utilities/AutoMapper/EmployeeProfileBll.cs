using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGeneratorDal.dbContext;

namespace SiteMapGenerator.Bll.Utilities.AutoMapper
{
    public class EmployeeProfileBll : Profile
    {
        public EmployeeProfileBll()
        {
            CreateMap<ArchiveOfRequestBll, ArchiveOfRequest>().ReverseMap();
            CreateMap<PageInfoBll, PageInfo>().ReverseMap();
            CreateMap<UrlSiteMapBll, UrlSiteMap>().ReverseMap();
        }
    }
}
