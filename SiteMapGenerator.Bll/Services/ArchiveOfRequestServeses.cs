using SiteMapGenerator.Bll.Services.Contract;
using SiteMapGenerator.Dal.Models.Dal;
using System.Collections.Generic;
using System.Data;

namespace SiteMapGenerator.Bll.Services
{
    public class ArchiveOfRequestServeses : IArchiveOfRequest
    {
        private readonly IRepository<ArchiveOfRequest> _repositoryArchiveOfRequest;

        public ArchiveOfRequestServeses(
            IRepository<ArchiveOfRequest> repositoryArchiveOfRequest)
        {
            _repositoryArchiveOfRequest = repositoryArchiveOfRequest;
        }

        public IEnumerable<ArchiveOfRequest> GetTableAll() =>
            _repositoryArchiveOfRequest.GetAll();

        public ArchiveOfRequest SelectId(int? elementId)
            => _repositoryArchiveOfRequest.GetById(elementId.Value);

        public void Insert(ArchiveOfRequest element)
        {
            _repositoryArchiveOfRequest.Add(element);
            _repositoryArchiveOfRequest.SaveChanges();
        }

        public void Update(ArchiveOfRequest elementToUpdate)
        {
            _repositoryArchiveOfRequest.Update(elementToUpdate);
            _repositoryArchiveOfRequest.SaveChanges();
        }
    }
}
