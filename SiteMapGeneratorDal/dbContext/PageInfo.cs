using System;
using System.Collections.Generic;

#nullable disable

namespace SiteMapGeneratorDal.dbContext
{
    public partial class PageInfo
    {
        public int IdPageInfo { get; set; }
        public int? SitemapId { get; set; }
        public long? WebsiteLoadingSpeed { get; set; }
        public int? StatusCode { get; set; }
        public DateTime? PageTestDate { get; set; }
        public DateTime? LastModified { get; set; }
        public TimeSpan? Elapsed { get; set; }
    }
}
