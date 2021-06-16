namespace SiteMapGenerator.Bll.BusinessLogic.Contract
{
    public interface ILinkValidator
    {
        string AddressHostValidator(string address);
        bool UrlValidation(string address);
    }
}
