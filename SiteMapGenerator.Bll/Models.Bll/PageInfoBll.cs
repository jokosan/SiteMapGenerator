using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.Models.Bll
{
    public class PageInfoBll
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

