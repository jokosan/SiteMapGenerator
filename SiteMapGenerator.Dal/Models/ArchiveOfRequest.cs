using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteMapGenerator.Dal.Models.Dal
{
    public class ArchiveOfRequest
    {
        public ArchiveOfRequest()
        {
            UrlSiteMaps = new HashSet<UrlSiteMap>();
        }

        [Key]
        public int IdArchiveOfRequests { get; set; }
        public string NameUrl { get; set; }

        public virtual ICollection<UrlSiteMap> UrlSiteMaps { get; set; }
    }
}
