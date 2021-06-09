using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.Models.Bll
{
    public class UrlSiteMapBll
    {
        public int IdSitemap { get; set; }
        public Nullable<int> ArchiveOfRequestsId { get; set; }
        public string NameSite { get; set; }

        public virtual ArchiveOfRequestBll ArchiveOfRequests { get; set; }
    }
}
