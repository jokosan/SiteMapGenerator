using SiteMapGenerator.Bll.Models.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface IOutput
    {
        IEnumerable<JoinResultBll> JoinTable(int id);
        IEnumerable<JoinResultBll> JoinTableGroup(IEnumerable<JoinResultBll> joinResultModels);

    }
}
