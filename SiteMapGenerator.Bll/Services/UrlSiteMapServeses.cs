using AutoMapper;
using SiteMapGenerator.Bll.Models.Bll;
using SiteMapGenerator.Bll.Services.Contract;
using SiteMapGeneratorDal.dbContext;
using SiteMapGeneratorDal.Infrastructure.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.Services
{
    public class UrlSiteMapServeses : IUrlSiteMap
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UrlSiteMapServeses(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<UrlSiteMapBll> GetTableAll()
            => _mapper.Map<IEnumerable<UrlSiteMapBll>>(_unitOfWork.SitemapUnitOFWork.Get());

        public UrlSiteMapBll SelectId(int? elementId)
            => _mapper.Map<UrlSiteMapBll>(_unitOfWork.SitemapUnitOFWork.GetById(elementId));

        public void Insert(UrlSiteMapBll element)
        {
            _unitOfWork.SitemapUnitOFWork.Insert(EntityTransformation(element));
            _unitOfWork.Save();
        }

        public void Update(UrlSiteMapBll elementToUpdate)
        {
            _unitOfWork.SitemapUnitOFWork.Update(EntityTransformation(elementToUpdate));
            _unitOfWork.Save();
        }

        private UrlSiteMap EntityTransformation(UrlSiteMapBll entity)
          => _mapper.Map<UrlSiteMapBll, UrlSiteMap>(entity);
    }
}
