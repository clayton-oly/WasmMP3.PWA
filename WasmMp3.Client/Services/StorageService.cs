using Microsoft.JSInterop;

namespace WasmMp3.Client.Services
{
    public class StorageService
    {
        private readonly IJSRuntime _js;

        public StorageService(IJSRuntime js) => _js = js;

        public ValueTask<string?> GetAsync(string key)
            => _js.InvokeAsync<string?>("deviceStorage.get", key);

        public ValueTask SetAsync(string key, string value)
            => _js.InvokeVoidAsync("deviceStorage.set", key, value);

        public ValueTask RemoveAsync(string key)
            => _js.InvokeVoidAsync("deviceStorage.remove", key);
    }
}
