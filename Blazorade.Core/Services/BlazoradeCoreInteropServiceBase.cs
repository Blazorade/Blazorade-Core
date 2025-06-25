using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorade.Core.Services
{
    /// <summary>
    /// Provides a base class for services that interact with the Blazorade Core JavaScript module.
    /// </summary>
    /// <remarks>This class is designed to facilitate interaction with the Blazorade Core JavaScript module by
    /// providing common functionality for importing and accessing the module. Derived classes can use this base class
    /// to simplify JavaScript interop operations.</remarks>
    public abstract class BlazoradeCoreInteropServiceBase : BlazoradeInteropServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlazoradeCoreInteropServiceBase"/> class.
        /// </summary>
        /// <param name="jsRuntime">The JavaScript runtime instance used for interop calls.</param>
        protected BlazoradeCoreInteropServiceBase(IJSRuntime jsRuntime) : base(jsRuntime) { }



        /// <summary>
        /// Asynchronously imports the Blazorade Core JavaScript module.
        /// </summary>
        /// <remarks>This method loads the JavaScript module located at
        /// <c>./_content/Blazorade.Core/js/blazoradeCore.js</c>. The returned <see cref="IJSObjectReference"/> can be
        /// used to invoke JavaScript functions defined in the module.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see
        /// cref="IJSObjectReference"/>  representing the imported Blazorade Core JavaScript module.</returns>
        protected async Task<IJSObjectReference> GetBlazoradeCoreModuleAsync()
        {
            return await this.ImportJavaScriptModuleAsync("./_content/Blazorade.Core/js/blazoradeCore.js");
        }
    }
}
