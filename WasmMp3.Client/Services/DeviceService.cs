using Microsoft.JSInterop;

namespace WasmMp3.Client.Services;

public class DeviceService
{
    private readonly IJSRuntime _js;
    public DeviceService(IJSRuntime js) => _js = js;

    public ValueTask<bool> IsOnlineAsync()
        => _js.InvokeAsync<bool>("device.isOnline");

    public ValueTask VibrateAsync(int ms)
        => _js.InvokeVoidAsync("device.vibrate", ms);

    public ValueTask RegisterOnlineListenerAsync(IJSObjectReference dotNetObjRef)
        => _js.InvokeVoidAsync("device.onOnline", dotNetObjRef);
}
