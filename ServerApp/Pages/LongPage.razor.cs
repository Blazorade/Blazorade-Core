using Blazorade.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace ServerApp.Pages
{
    partial class LongPage
    {
        private ElementReference BottomP;
        private string BottomElementId = "bottom-paragraph";

        [Inject]
        private BlazoradeScrollService ScrollService { get; set; }

        private async Task ScrollIntoViewByIdAsync(MouseEventArgs args)
        {
            await this.ScrollService.ScrollIntoViewAsync(this.BottomElementId);
        }

        private async Task ScrollIntoViewByElementAsync(MouseEventArgs args)
        {
            await this.ScrollService.ScrollIntoViewAsync(this.BottomP);
        }
    }
}
