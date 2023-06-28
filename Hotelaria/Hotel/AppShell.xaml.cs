using Hotel.Pages;
using SharedContent.Paths;

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
        await DisplayAlert("Aviso", "Não há fichas disponíveis para exportação.", "Ok");
    }

    private void OnAboutClicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new AboutPage());
    }
}
