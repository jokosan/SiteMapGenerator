using System;

namespace SiteMapGenerator.Bll.BusinessLogic
{
    public class LinkValidator
    {
        public virtual string GetHost(string address) 
        {
            if (address.Contains("http"))
            {
                var uri = new Uri(address);
                return uri.Scheme + Uri.SchemeDelimiter + uri.Host +
                    (uri.IsDefaultPort ? "" : (":" + uri.Port));
            }
            else
            {
                return string.Empty;
            }
        }

        public virtual bool CheckURLValid(string address)
        {
            Uri uriResult;
            return Uri.TryCreate(address, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
