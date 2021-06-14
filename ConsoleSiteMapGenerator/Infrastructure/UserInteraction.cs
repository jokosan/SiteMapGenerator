using ConsoleSiteMapGenerator.Infrastructure.Contract;
using System;

namespace ConsoleSiteMapGenerator.Infrastructure
{
    public class UserInteraction : IUserInteraction
    {
        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public string UserValueInput()
            => Console.ReadLine();
    }
}
