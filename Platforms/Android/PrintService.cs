using Android.Content;
using Android.Print;
using Android.Webkit;
using Android.OS;
using Android.Util;

namespace print_html_dotnet_maui_using_native_apis.Platforms.Android;

public class PrintService : IPrintService
{
    public Task PrintAsync(string title, string content)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            var activity = Platform.CurrentActivity;
            var webView = new global::Android.Webkit.WebView(activity!);
            webView.Settings.JavaScriptEnabled = true;

            webView.SetWebViewClient(new DelayedPrintWebViewClient(activity!, title));
            webView.LoadDataWithBaseURL(null, content, "text/HTML", "UTF-8", null);
        });

        return Task.CompletedTask;
    }
}

public class DelayedPrintWebViewClient : WebViewClient
{
    private readonly Context _context;
    private readonly string _jobName;

    public DelayedPrintWebViewClient(Context context, string jobName)
    {
        _context = context;
        _jobName = jobName;
    }

    public override void OnPageFinished(global::Android.Webkit.WebView view, string url)
    {
        base.OnPageFinished(view, url);

        var printManager = (PrintManager?)_context.GetSystemService(Context.PrintService);
        var printAdapter = view.CreatePrintDocumentAdapter(_jobName);
        printManager!.Print(_jobName, printAdapter, null);
    }
}