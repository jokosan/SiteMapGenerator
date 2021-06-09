using Microsoft.Extensions.DependencyInjection;
using SiteMapGeneratorDal.Infrastructure.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMapGenerator.Bll.Infrastructure.DependencyInjection
{
    public class DependencyResolverBll
    {
        public static void Initialize(IServiceCollection services)
        {
            DependencyResolverBusinessLogic.Initialize(services);
            DependencyResolverServeses.Initialize(services);
            DependencyInjectionDal.Initialize(services);
        }
    }
}
