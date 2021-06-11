﻿using System.Collections.Generic;

namespace SiteMapGenerator.Bll.Services.Contract.GenericContract
{
    public interface IGetFromDatabase<T> where T : class
    {
        IEnumerable<T> GetTableAll();
        T SelectId(int? elementId);
    }
}
