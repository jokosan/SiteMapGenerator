using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMapGenerator.Models
{
    public class ArchiveOfRequestModel
    {
        public ArchiveOfRequestModel()
        {
            UrlSiteMaps = new HashSet<UrlSiteMapModel>();
        }

        public int IdArchiveOfRequests { get; set; }
        public string NameUrl { get; set; }

        public virtual ICollection<UrlSiteMapModel> UrlSiteMaps { get; set; }
    }
}
