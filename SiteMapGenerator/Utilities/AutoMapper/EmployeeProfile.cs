using AutoMapper;
using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMapGenerator.Utilities.AutoMapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<JoinResultModel, JoinResultBll>();
        }
    }
}
