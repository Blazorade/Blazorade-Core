using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorade.Core.Services
{
    /// <summary>
    /// Provides localization services.
    /// </summary>
    public class BlazoradeLocalizationService : BlazoradeInteropServiceBase
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="jsRuntime"></param>
        public BlazoradeLocalizationService(IJSRuntime jsRuntime) : base(jsRuntime) { }


        /// <summary>
        /// Returns the timezone offset on the client.
        /// </summary>
        /// <returns></returns>
        public async Task<TimeSpan> GetClientTimezoneOffsetAsync()
        {
            var module = await this.GetBlazoradeCoreModuleAsync();
            var offset = await module.InvokeAsync<int>("getClientTimezoneOffset");
            return TimeSpan.FromMinutes(-1 * offset);
        }

        /// <summary>
        /// Returns the local time on the client.
        /// </summary>
        public async Task<DateTime> GetClientDateTimeAsync()
        {
            var offset = await this.GetClientTimezoneOffsetAsync();
            return DateTime.UtcNow.Add(offset);
        }


        private async Task<IJSObjectReference> GetBlazoradeCoreModuleAsync()
        {
            return await this.ImportJavaScriptModuleAsync("./_content/Blazorade.Core/js/blazoradeCore.js");
        }
    }
}
