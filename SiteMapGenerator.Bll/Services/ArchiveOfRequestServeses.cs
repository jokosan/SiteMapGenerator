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
    public class ArchiveOfRequestServeses : IArchiveOfRequest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArchiveOfRequestServeses(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<ArchiveOfRequestBll> GetTableAll()
            => _mapper.Map<IEnumerable<ArchiveOfRequestBll>>(_unitOfWork.ArchiveOfRequestsUnitOfWork.Get());

        public ArchiveOfRequestBll SelectId(int? elementId)
            => _mapper.Map<ArchiveOfRequestBll>(_unitOfWork.ArchiveOfRequestsUnitOfWork.GetById(elementId));

        public void Insert(ArchiveOfRequestBll element)
        {
            _unitOfWork.ArchiveOfRequestsUnitOfWork.Insert(EntityTransformation(element));
            _unitOfWork.Save();
        }

        public void Update(ArchiveOfRequestBll elementToUpdate)
        {
            _unitOfWork.ArchiveOfRequestsUnitOfWork.Update(EntityTransformation(elementToUpdate));
            _unitOfWork.Save();
        }

        private ArchiveOfRequest EntityTransformation(ArchiveOfRequestBll entity)
            => _mapper.Map<ArchiveOfRequestBll, ArchiveOfRequest>(entity);
    }
}
