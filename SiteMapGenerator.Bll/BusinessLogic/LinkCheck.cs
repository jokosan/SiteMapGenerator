using SiteMapGenerator.Bll.BusinessLogic.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class LinkCheck : ILinkCheck
    {
        public string AddressHost(string address)
        {
            if (address.Contains("http"))
            {
                var uri = new Uri(address);
                return uri.Scheme + Uri.SchemeDelimiter + uri.Host +
                    (uri.IsDefaultPort ? "" : (":" + uri.Port));
            }
            else
            {
                return "";
            }
        }

        public bool UrlValidation(string address)
        {
            Uri uriResult;
            return Uri.TryCreate(address, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
