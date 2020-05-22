using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blazorade.Core.Components
{
    /// <summary>
    /// Allows you to redirect the user to another address.
    /// </summary>
    /// <remarks>
    /// This component handles the redirection when it is rendered. No redirection will occur if your
    /// application logic does not render this component.
    /// </remarks>
    partial class Redirect
    {
        /// <summary>
        /// A delay in milliseconds before redirecting to the specified <c>URL</c>.
        /// </summary>
        [Parameter]
        public int? Delay { get; set; }

        /// <summary>
        /// The URL to redirect to when the component has rendered.
        /// </summary>
        [Parameter]
        public string Url { get; set; }



        /// <summary>
        /// The navigation manager used by the component. This is automatically injected.
        /// </summary>
        [Inject]
        protected NavigationManager NavigationManager { get; set; }


        /// <summary>
        /// </summary>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!string.IsNullOrEmpty(this.Url))
            {
                if (this.Delay.HasValue)
                {
                    await Task.Delay(this.Delay.Value);
                }

                this.NavigationManager.NavigateTo(this.Url, true);
            }

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
