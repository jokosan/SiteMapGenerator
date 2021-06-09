using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface ILoadingPageUrls
    {
        List<string> ExtractHref(string URL, int countLink);
    }
}
