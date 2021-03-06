using System;
using System.ComponentModel.DataAnnotations;

namespace SiteMapGenerator.Dal.Models.Dal
{
    public class PageInfo
    {
        [Key]
        public int IdPageInfo { get; set; }
        public int? SitemapId { get; set; }
        public long? WebsiteLoadingSpeed { get; set; }
        public int? StatusCode { get; set; }
        public DateTime? PageTestDate { get; set; }
        public TimeSpan? Elapsed { get; set; }
    }
}

