using Microsoft.JSInterop;
using WasmMP3.Client.Features.Gps;

namespace WasmMP3.Client.Services;

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


    //métodos do CopyText (Clipboard)
    public ValueTask<bool> CopyTextAsync(string txt)
    => _js.InvokeAsync<bool>("clipboard.copyText", txt);

    public ValueTask<string> ReadTextAsync()
    => _js.InvokeAsync<string>("clipboard.readText");

    //métodos do BatteryLevel
    public ValueTask<double> GetBatteryLevelAsync()
        => _js.InvokeAsync<double>("battery.getLevel");


    //GPS

    public ValueTask<Localizacao> GetGeoLocalizationAsync()
        => _js.InvokeAsync<Localizacao>("getGeolocation");

    //câmera
    public ValueTask StartVideoAsync(string videoElementId, bool useFrontCamera)
        => _js.InvokeVoidAsync("camera.startVideo", videoElementId, useFrontCamera);

    public ValueTask<string> TakePictureAsync(string videoElementId, string canvasElementId)
        => _js.InvokeAsync<string>("camera.takePicture", videoElementId, canvasElementId);

    public ValueTask StopVideoAsync(string videoElementId)
        => _js.InvokeVoidAsync("camera.stopVideo", videoElementId);
}
