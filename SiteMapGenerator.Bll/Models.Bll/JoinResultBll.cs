using System;

namespace SiteMapGenerator.Bll.Models.Bll
{
    public class JoinResultBll
    {
        public int IdSitemap { get; set; }
        public int? ArchiveOfRequestsId { get; set; }
        public string NameSite { get; set; }
        public int IdPageInfo { get; set; }
        public long? WebsiteLoadingSpeed { get; set; }
        public int? StatusCode { get; set; }
        public DateTime? PageTestDate { get; set; }
        public DateTime? LastModified { get; set; }
        public TimeSpan? Elapsed { get; set; }
        public TimeSpan? ElapsedMin { get; set; }
        public TimeSpan? ElapsedMax { get; set; }
    }
}
