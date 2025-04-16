using AdSupport;
using AppTrackingTransparency;

namespace print_html_dotnet_maui_using_native_apis.Platforms.iOS;

public class IdfaService : IIdfaService
{
    public async Task<string?> GetIdfaAsync()
    {
        var status = await ATTrackingManager.RequestTrackingAuthorizationAsync();

        if (status == ATTrackingManagerAuthorizationStatus.Authorized)
        {
            var idfa = ASIdentifierManager.SharedManager.AdvertisingIdentifier.AsString();
            return idfa;
        }

        return "Permission Denied or Not Determined";
    }
}
