using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazorade.Core.Components.Server
{
    /// <summary>
    /// Represents a modal dialog that is shown when the application looses connection with the server.
    /// </summary>
    partial class ComponentReconnectModal
    {
        /// <summary>
        /// </summary>
        public ComponentReconnectModal()
        {
            this.BackdropBackgroundColor = "rgba(0, 0, 0, .75)";
            this.ConnectionStateBackgroundColor = "rgba(255, 255, 255, .8)";
            this.ConnectionStatePadding = "64px";

        }


        /// <summary>
        /// The colour of the modal dialog backdrop. Defaults to <c>rgba(0, 0, 0, .75)</c>.
        /// </summary>
        [Parameter]
        public string BackdropBackgroundColor { get; set; }

        /// <summary>
        /// The background colour of the dialog displaying the connection state. Defaults to <c>rgba(255, 255, 255, .8)</c>.
        /// </summary>
        [Parameter]
        public string ConnectionStateBackgroundColor { get; set; }

        /// <summary>
        /// The padding for the modal displaying the connection state. Default to <c>64px</c>.
        /// </summary>
        [Parameter]
        public string ConnectionStatePadding { get; set; }

        /// <summary>
        /// Allows you to customize the message that is shown when the connection is lost and the application is trying to reconnect.
        /// </summary>
        [Parameter]
        public RenderFragment ReconnectingTemplate { get; set; }

        /// <summary>
        /// Allows you to customize the message that is shown when reconnection attempts with the server have failed.
        /// </summary>
        [Parameter]
        public RenderFragment ReconnectFailedTemplate { get; set; }

        /// <summary>
        /// Allows you to customize the message that is shown when the server has rejected the reconnection.
        /// </summary>
        [Parameter]
        public RenderFragment ReconnectRejectedTemplate { get; set; }


        /// <summary>
        /// </summary>
        protected override void OnParametersSet()
        {
            this.AddAttribute("id", "components-reconnect-modal");
            base.OnParametersSet();
        }

    }
}
