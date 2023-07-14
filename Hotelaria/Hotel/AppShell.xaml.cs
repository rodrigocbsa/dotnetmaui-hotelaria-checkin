using CommunityToolkit.Maui.Core;
using Hotel.Pages;
using SharedContent.Methods;
using SharedContent.Paths;


namespace Hotel;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Export.Clicked += OnExportClicked;
        Help.Clicked += OnHelpClicked;
    }

    private async void OnExportClicked(object sender, EventArgs e)
    {
        if (File.Exists(SharedApplicationPaths.CsvPath))
        {
            await Share.Default.RequestAsync(new ShareFileRequest
            {
                File = new ShareFile(SharedApplicationPaths.CsvPath)
            });
            return;
        }
        await ToastMethods.ShowToastAsync("Não há fichas disponíveis para exportação", ToastDuration.Short);
    }

    private void OnHelpClicked(object sender, EventArgs e)
    {
        // Whatsapp Business
    }
}
