using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.Models.Bll
{
    public class ArchiveOfRequestBll
    {
        public ArchiveOfRequestBll()
        {
            this.UrlSiteMap = new HashSet<UrlSiteMapBll>();
        }

        public int IdArchiveOfRequests { get; set; }
        public string NameUrl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrlSiteMapBll> UrlSiteMap { get; set; }
    }
}
