using CommunityToolkit.Maui.Core;
using Hospede.Methods;
using Hotel.Pages;
using Hotel.Paths;


namespace Hotel;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Export.Clicked += OnExportClicked;
        About.Clicked += OnAboutClicked;
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

    private void OnAboutClicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new AboutPage());
    }
}
