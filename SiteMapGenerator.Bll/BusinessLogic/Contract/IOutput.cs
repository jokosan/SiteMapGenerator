using SiteMapGenerator.Bll.Models.Bll;
using System.Collections.Generic;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface IOutput
    {
        IEnumerable<JoinResultBll> JoinTable(int id);
        IEnumerable<JoinResultBll> JoinTableGroup(IEnumerable<JoinResultBll> joinResultModels);

    }
}
