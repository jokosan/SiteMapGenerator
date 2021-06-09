using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.Models.Bll
{
    public class ArchiveOfRequestBll
    {
        public ArchiveOfRequestBll()
        {
            UrlSiteMaps = new HashSet<UrlSiteMapBll>();
        }

        public int IdArchiveOfRequests { get; set; }
        public string NameUrl { get; set; }

        public virtual ICollection<UrlSiteMapBll> UrlSiteMaps { get; set; }
    }
}
