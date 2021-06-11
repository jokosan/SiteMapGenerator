using System;
using System.ComponentModel.DataAnnotations;

namespace SiteMapGenerator.Dal.Models.Dal
{
    public class UrlSiteMap
    {
        [Key]
        public int IdSitemap { get; set; }
        public int? ArchiveOfRequestsId { get; set; }
        public string NameSite { get; set; }

        public virtual ArchiveOfRequest ArchiveOfRequests { get; set; }
    }
}
