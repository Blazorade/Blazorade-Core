using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorade.Core.Services
{
    /// <summary>
    /// A base class for service implementations that provide JavaScript interop services.
    /// </summary>
    public abstract class BlazoradeInteropServiceBase
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="jsRuntime"></param>
        protected BlazoradeInteropServiceBase(IJSRuntime jsRuntime)
        {
            this.JSRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        }


        /// <summary>
        /// Returns the JavaScript runtime.
        /// </summary>
        protected IJSRuntime JSRuntime { get; private set; }


        private Dictionary<string, IJSObjectReference> Modules = new Dictionary<string, IJSObjectReference>();
        /// <summary>
        /// Imports the JavaScript module with the given URL.
        /// </summary>
        /// <param name="moduleUrl">
        /// <para>
        /// The relative or absolute path of the module to import.
        /// </para>
        /// <para>
        /// If you are importing a JavaScript file in your component library's <c>wwwroot</c> folder, set the URL to
        /// <c>./content/{Library Name}/yourfile.js</c>
        /// </para>
        /// </param>
        /// <returns>Returns the imported module.</returns>
        protected async Task<IJSObjectReference> ImportJavaScriptModuleAsync(string moduleUrl)
        {
            if(!this.Modules.ContainsKey(moduleUrl))
            {
                var module = await this.JSRuntime.InvokeAsync<IJSObjectReference>("import", moduleUrl).AsTask();
                this.Modules[moduleUrl] = module;
            }

            return this.Modules[moduleUrl];
        }
    }
}
