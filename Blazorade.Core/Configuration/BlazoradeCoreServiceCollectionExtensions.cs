using Blazorade.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension functions for configuring services required by Blazorade Core.
    /// </summary>
    public static class BlazoradeCoreServiceCollectionExtensions
    {

        /// <summary>
        /// Adds the services needed by Blazorade Core.
        /// </summary>
        public static IServiceCollection AddBlazoradeCore(this IServiceCollection services)
        {
            return services
                .AddScoped<BlazoradeLocalizationService>()
                ;
        }

    }
}
