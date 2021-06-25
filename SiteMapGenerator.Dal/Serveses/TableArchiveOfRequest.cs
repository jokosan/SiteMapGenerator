using SiteMapGenerator.Dal.Models.Dal;
using System.Data;
using System.Linq;

namespace SiteMapGenerator.Dal.Serveses
{
    public class TableArchiveOfRequest
    {
        private readonly IRepository<ArchiveOfRequest> _repositoryArchiveOfRequest;

        public TableArchiveOfRequest(
           IRepository<ArchiveOfRequest> repositoryArchiveOfRequest)
        {
            _repositoryArchiveOfRequest = repositoryArchiveOfRequest;
        }

        public IQueryable<ArchiveOfRequest> GetArchiveOfRequest()
          => _repositoryArchiveOfRequest.GetAll();

        public ArchiveOfRequest GetId(int id)
            => _repositoryArchiveOfRequest.GetById(id);

        public virtual int SaveUserRequest(string url)
        {
            var resultArxiv = GetArchiveOfRequest();

            if (resultArxiv.Any(x => x.NameUrl.Contains(url)))
            {
                var resultId = resultArxiv.FirstOrDefault(x => x.NameUrl.Contains(url));
                return resultId.IdArchiveOfRequests;
            }
            else
            {
                var archive = new ArchiveOfRequest() { NameUrl = url };
                return Save(archive);
            }
        }

        private int Save(ArchiveOfRequest archiveOfRequest)
        {
            _repositoryArchiveOfRequest.Add(archiveOfRequest);
            _repositoryArchiveOfRequest.SaveChanges();

            return archiveOfRequest.IdArchiveOfRequests;
        }
    }
}
