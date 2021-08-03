using Microsoft.Extensions.DependencyInjection;
using SiteMapGenerator.Bll.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMapGenerator.Api.Utilites
{
    public class ClassInitialise
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<LoadingSiteMap>();
            services.AddScoped<LinkValidator>();
            services.AddScoped<LoadingPageUrls>();
            services.AddScoped<WebRequestServeses>();
            services.AddScoped<HtmlParser>();
            services.AddScoped<SiteParser>();
        }
    }
}
