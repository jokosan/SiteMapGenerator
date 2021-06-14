namespace ConsoleSiteMapGenerator.Infrastructure.Contract
{
    public interface IUserInteraction
    {
        void Info(string message);
        string UserValueInput();
    }
}
