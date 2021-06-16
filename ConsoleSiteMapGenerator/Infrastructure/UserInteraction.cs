using System;

namespace ConsoleSiteMapGenerator.Infrastructure
{
    public class UserInteraction
    {
        public virtual void Info(string message)
        {
            Console.WriteLine(message);
        }

        public virtual string UserValueInput()
            => Console.ReadLine();
    }
}
