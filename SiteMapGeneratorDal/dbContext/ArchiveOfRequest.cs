using System;
using System.Collections.Generic;

#nullable disable

namespace SiteMapGeneratorDal.dbContext
{
    public partial class ArchiveOfRequest
    {
        public ArchiveOfRequest()
        {
            UrlSiteMaps = new HashSet<UrlSiteMap>();
        }

        public int IdArchiveOfRequests { get; set; }
        public string NameUrl { get; set; }

        public virtual ICollection<UrlSiteMap> UrlSiteMaps { get; set; }
    }
}
