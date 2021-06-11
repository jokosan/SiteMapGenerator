namespace SiteMapGenerator.Bll.BusinessLogic.Contract
{
    public interface ILinkCheck
    {
        string AddressHost(string address);
        bool UrlValidation(string address);
    }
}
