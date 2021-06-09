using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMapGenerator.Models
{
    public class UrlSiteMapModel
    {
        public int IdSitemap { get; set; }
        public int? ArchiveOfRequestsId { get; set; }
        public string NameSite { get; set; }

        public virtual ArchiveOfRequestModel ArchiveOfRequests { get; set; }
    }
}
