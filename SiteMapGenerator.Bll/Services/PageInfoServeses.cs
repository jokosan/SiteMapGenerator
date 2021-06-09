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
    public class PageInfoServeses : IPageInfo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PageInfoServeses(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<PageInfoBll> GetTableAll()
            => _mapper.Map<IEnumerable<PageInfoBll>>(_unitOfWork.PageInfoUnitOFWork.Get());

        public PageInfoBll SelectId(int? elementId)
           => _mapper.Map<PageInfoBll>(_unitOfWork.PageInfoUnitOFWork.GetById(elementId));

        public void Insert(PageInfoBll element)
        {
            _unitOfWork.PageInfoUnitOFWork.Insert(EntityTransformation(element));
            _unitOfWork.Save();
        }

        public void Update(PageInfoBll elementToUpdate)
        {
            _unitOfWork.PageInfoUnitOFWork.Update(EntityTransformation(elementToUpdate));
            _unitOfWork.Save();
        }

        private PageInfo EntityTransformation(PageInfoBll entity)
          => _mapper.Map<PageInfoBll, PageInfo>(entity);
    }
}
