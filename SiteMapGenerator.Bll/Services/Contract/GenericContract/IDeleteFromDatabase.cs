using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.Services.Contract.GenericContract
{
    public interface IDeleteFromDatabase<T> where T : class
    {
        void Delete(T elementToDelete);
    }
}
