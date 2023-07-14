namespace Hospede;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        AboutUs.Clicked += OnAboutUsClicked;
        About.Clicked += OnAboutClicked;
    }

    private async void OnAboutUsClicked(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new Uri("https://ais-brasil.github.io");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            // An unexpected error occurred. No browser may be installed on the device.
        }
    }

    private async void OnAboutClicked(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new Uri("https://ais-brasil.github.io/checkin-hospede-brasil");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            // An unexpected error occurred. No browser may be installed on the device.
        }
    }
}