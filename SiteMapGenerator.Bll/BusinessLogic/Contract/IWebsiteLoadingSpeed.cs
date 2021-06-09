using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.Services.Contract
{
    public interface IWebsiteLoadingSpeed
    {
        List<string> SpeedPageUploads(List<string> url, int idUrl);
    }
}
