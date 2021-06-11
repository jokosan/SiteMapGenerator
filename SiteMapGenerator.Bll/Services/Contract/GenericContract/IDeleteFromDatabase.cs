namespace SiteMapGenerator.Bll.Services.Contract.GenericContract
{
    public interface IDeleteFromDatabase<T> where T : class
    {
        void Delete(T elementToDelete);
    }
}
