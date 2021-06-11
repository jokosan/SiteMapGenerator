using System;

namespace SiteMapGenerator.Bll.Models.Bll
{
    public class JoinResultBll
    {
        public int IdSitemap { get; set; }
        public Nullable<int> ArchiveOfRequestsId { get; set; }
        public string NameSite { get; set; }
        public int IdPageInfo { get; set; }
        public Nullable<long> WebsiteLoadingSpeed { get; set; }
        public Nullable<int> StatusCode { get; set; }
        public Nullable<System.DateTime> PageTestDate { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
        public Nullable<System.TimeSpan> Elapsed { get; set; }
        public Nullable<System.TimeSpan> ElapsedMin { get; set; }
        public Nullable<System.TimeSpan> ElapsedMax { get; set; }
    }
}
