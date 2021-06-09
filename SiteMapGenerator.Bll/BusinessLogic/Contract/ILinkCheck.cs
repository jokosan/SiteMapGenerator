using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.BusinessLogic.Contract
{
    public interface ILinkCheck
    {
        string AddressHost(string address);
        bool UrlValidation(string address);
    }
}
