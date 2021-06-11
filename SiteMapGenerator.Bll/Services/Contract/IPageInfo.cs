using SiteMapGenerator.Bll.Services.Contract.GenericContract;
using SiteMapGenerator.Dal.Models.Dal;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface IPageInfo : IGetFromDatabase<PageInfo>, IDatabaseOperations<PageInfo>
    {
    }
}
