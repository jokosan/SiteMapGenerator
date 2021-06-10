using Microsoft.Extensions.DependencyInjection;
using SiteMapGeneratorDal.Infrastructure.Repository;
using SiteMapGeneratorDal.Infrastructure.Repository.Contract;
using SiteMapGeneratorDal.Infrastructure.UnitOfWork;
using SiteMapGeneratorDal.Infrastructure.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGeneratorDal.Infrastructure.DependencyInjection
{
    public class DependencyInjectionDal
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWorks>();
        }
    }
}
