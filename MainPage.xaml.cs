namespace print_html_dotnet_maui_using_native_apis;

public partial class MainPage : ContentPage
{
	int count = 0;
	IPrintService _printService;
	IIdfaService _idfaService;

	public MainPage(IPrintService printService, IIdfaService idfaService)
	{
		_idfaService = idfaService;
		_printService = printService;
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async void OnPrintNativelyClicked(object sender, EventArgs e)
	{
		try
        {
            string printable = $@"
            <h2>Hello From a Native Print Dialog</h2>
            <p><strong>This is a paragraph:</strong> Look at that!</p>
			<div style='background-color:yellow;color:black;padding:20px;'>And this is a DIV with some inline styling</div>";

            await _printService.PrintAsync($"This Is My Page Title", printable);
        }
        catch (Exception ex)
        {
            string errorMsg = $"{ex.Message} :: {ex.InnerException}";
            await DisplayAlert("Print Error", errorMsg, "OK");
        }
	}

	private async void OnFindIdfaClicked(object sender, EventArgs e)
	{
		var idfa = await _idfaService?.GetIdfaAsync();

		await DisplayAlert("IDFA", idfa ?? "Unavailable", "OK");
	}
}

