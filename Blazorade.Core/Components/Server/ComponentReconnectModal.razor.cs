using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazorade.Core.Components.Server
{
    partial class ComponentReconnectModal
    {
        public ComponentReconnectModal()
        {
            this.BackdropBackgroundColor = "rgba(0, 0, 0, .75)";
            this.ConnectionStateBackgroundColor = "rgba(255, 255, 255, .8)";
            this.ConnectionStatePadding = "64px";

        }


        [Parameter]
        public string BackdropBackgroundColor { get; set; }

        [Parameter]
        public string ConnectionStateBackgroundColor { get; set; }

        [Parameter]
        public string ConnectionStatePadding { get; set; }

        [Parameter]
        public RenderFragment ReconnectingTemplate { get; set; }

        [Parameter]
        public RenderFragment ReconnectFailedTemplate { get; set; }

        [Parameter]
        public RenderFragment ReconnectRejectedTemplate { get; set; }


        protected override void OnParametersSet()
        {
            this.AddAttribute("id", "components-reconnect-modal");
            base.OnParametersSet();
        }
    }
}
