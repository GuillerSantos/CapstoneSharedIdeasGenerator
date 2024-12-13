using CapstoneIdeaGenerator.Client.Services.Contracts;
using Microsoft.JSInterop;

namespace CapstoneIdeaGenerator.Client.Services
{
    public class ClipboardService : IClipboardService
    {
        private readonly IJSRuntime jsRuntime;

        public ClipboardService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task CopyText(string text)
        {
            await jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
    }
}
