using Hospede.Methods;
using SharedContent.Paths;

namespace Hospede.Pages;

public partial class QrCodePage : ContentPage
{
    public QrCodePage()
    {
        InitializeComponent();
        Close_btn.Clicked += OnCloseClicked;
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    protected override void OnAppearing()
    {
        CSVMethods.GenerateQrCodeFromCSVData();
        QrCodeImage.Source = SharedApplicationPaths.QrPath;
    }
}