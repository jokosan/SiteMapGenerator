using System;

namespace SiteMapGenerator.Bll.Models.Bll
{
    public class UrlResult
    {
        public int IdSitemap { get; set; }
        public int? ArchiveOfRequestsId { get; set; }
        public string NameSite { get; set; }
        public int IdPageInfo { get; set; }
        public long? WebsiteLoadingSpeed { get; set; }
        public int? StatusCode { get; set; }
        public DateTime? PageTestDate { get; set; }
        public int? Elapsed { get; set; }
    }
}
