﻿namespace SiteMapGenerator.Bll.Services.Contract.GenericContract
{
    public interface IDatabaseOperations<T> where T : class
    {
        void Insert(T element);
        void Update(T elementToUpdate);
    }
}
