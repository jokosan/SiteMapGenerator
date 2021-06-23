using System;

namespace SiteMapGenerator.Bll.Models
{
    public class UrlResult
    {
        public int IdUrlResult { get; set; }
        public int? ArchiveOfRequestsId { get; set; }
        public string NameSite { get; set; }
        public long? WebsiteLoadingSpeed { get; set; }
        public int? StatusCode { get; set; }
        public DateTime? PageTestDate { get; set; }
        public int? Elapsed { get; set; }
        public int? ElapsedMin { get; set; }
        public int? ElapsedMax { get; set; }
        public bool parseLink { get; set; }
        public bool sitemapLink { get; set; }
    }
}
