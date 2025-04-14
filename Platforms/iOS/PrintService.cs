using UIKit;

namespace print_html_dotnet_maui_using_native_apis.Platforms.iOS;

public class PrintService : IPrintService
{
    public Task PrintAsync(string title, string content)
    {
        var printInfo = UIPrintInfo.PrintInfo;
        printInfo.OutputType = UIPrintInfoOutputType.General;
        printInfo.JobName = title;

        var printController = UIPrintInteractionController.SharedPrintController;
        printController.PrintInfo = printInfo;

        var formatter = new UIMarkupTextPrintFormatter(content);
        printController.PrintFormatter = formatter;

        printController.Present(true, (controller, completed, error) =>
        {
            if (error != null)
                Console.WriteLine($"Print error: {error.LocalizedDescription}");
        });

        return Task.CompletedTask;
    }
}