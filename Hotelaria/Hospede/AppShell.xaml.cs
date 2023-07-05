namespace Hospede;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        About.Clicked += OnAboutClicked;
        Update.Clicked += OnUpdateClicked;
    }

    private async void OnAboutClicked(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new Uri("https://github.com/AIS-BRASIL/Downloads"); /// todo: Página no website da solução
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            // An unexpected error occurred. No browser may be installed on the device.
        }
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new Uri("https://github.com/AIS-BRASIL/Downloads/releases");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            // An unexpected error occurred. No browser may be installed on the device.
        }
    }
}