using System;
using System.Collections.Generic;

#nullable disable

namespace SiteMapGeneratorDal.dbContext
{
    public partial class UrlSiteMap
    {
        public int IdSitemap { get; set; }
        public int? ArchiveOfRequestsId { get; set; }
        public string NameSite { get; set; }

        public virtual ArchiveOfRequest ArchiveOfRequests { get; set; }
    }
}
