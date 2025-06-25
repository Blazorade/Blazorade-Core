using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorade.Core.Services
{
    /// <summary>
    /// Provides functionality for managing scroll-related operations in a Blazor application.
    /// </summary>
    /// <remarks>This service is designed to interact with JavaScript via Blazor's interop capabilities,
    /// enabling advanced scroll manipulation and behavior customization. It extends <see
    /// cref="BlazoradeInteropServiceBase"/>, leveraging its base functionality for JavaScript interop.</remarks>
    public class BlazoradeScrollService : BlazoradeCoreInteropServiceBase
    {
        /// <summary>
        /// Provides functionality to interact with scrolling operations in the browser via JavaScript interop.
        /// </summary>
        /// <remarks>This service is designed to facilitate browser scrolling operations by leveraging
        /// JavaScript interop. Ensure that the provided <see cref="IJSRuntime"/> instance is properly configured for
        /// client-side interactions.</remarks>
        /// <param name="jsRuntime">The <see cref="IJSRuntime"/> instance used to invoke JavaScript functions.</param>
        public BlazoradeScrollService(IJSRuntime jsRuntime) : base(jsRuntime) { }



        /// <summary>
        /// Scrolls the specified component into view within the browser window.
        /// </summary>
        /// <remarks>This method uses JavaScript interop to scroll the specified component into view.
        /// Ensure that the  <paramref name="component"/> is a valid reference to a DOM element in the current Blazor
        /// context.</remarks>
        /// <param name="component">A reference to the component to be scrolled into view. This parameter cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ScrollIntoViewAsync(ElementReference component)
        {
            var module = await this.GetBlazoradeCoreModuleAsync();
            await module.InvokeVoidAsync("scrollElementIntoView", component);
        }

        /// <summary>
        /// Scrolls the specified HTML element into view asynchronously.
        /// </summary>
        /// <remarks>This method uses JavaScript interop to scroll the element with the specified ID into
        /// view. Ensure that the element exists in the DOM before calling this method.</remarks>
        /// <param name="elementId">The ID of the HTML element to scroll into view. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ScrollIntoViewAsync(string elementId)
        {
            var module = await this.GetBlazoradeCoreModuleAsync();
            await module.InvokeVoidAsync("scrollElementIntoViewById", elementId);
        }
    }
}
