using BarcodeScanner.Mobile.Maui;
using Hotel.Paths;


namespace Hotel.Pages;

public partial class ScanPage : ContentPage
{
    public ScanPage()
    {
        InitializeComponent();
        Camera.OnDetected += OnDetectedCameraView;
    }

    protected async override void OnAppearing()
    {
        if (!await Methods.AskForRequiredPermission())
        {
            Preferences.Default.Set("Permissao", false);
        }
        else
        {
            Preferences.Default.Set("Permissao", true);
        }

        if (Preferences.Default.Get("Permissao", false) == false)
        {
            await DisplayAlert("Aviso", "O aplicativo n�o ir� funcionar sem as devidas permiss�es, por favor, permita o acesso nas configura��es. Ap�s ler este aviso, o aplicativo ir� fechar.", "Ok");
            Application.Current.Quit();
        }
    }

    private void OnDetectedCameraView(object sender, OnDetectedEventArg e)
    {
        SaveDetectedFNRHToCSV(e);
    }



    private async void SaveDetectedFNRHToCSV(OnDetectedEventArg e)
    {
        Camera.IsScanning = false;
        UserFeedbacks.Text = "QrCode capturado.";
        string result = ReadQrCodeContent(e);
        SaveFNRHAsStringToCSV(result);

        await DisplayAlert("Dados salvos", result, "Ok");
        Camera.IsScanning = true;
        UserFeedbacks.Text = "Aguardando QrCode...";
    }

    private string ReadQrCodeContent(OnDetectedEventArg e)
    {
        List<BarcodeScanner.Mobile.Core.BarcodeResult> obj = e.BarcodeResults;

        string result = string.Empty;
        for (var i = 0; i < obj.Count; i++)
        {
            result += obj[i].DisplayValue;
        }
        return result;
    }
    private void SaveFNRHAsStringToCSV(string result)
    {
        if (Preferences.Default.Get("IsHeaderWritten", false) == false)
        {
            WriteHeaderToCsvOnce(result);
        }
        File.AppendAllText(SharedApplicationPaths.CsvPath, result.Split(Environment.NewLine)[1]); // Pula o cabe�alho
    }

    private void WriteHeaderToCsvOnce(string result)
    {
        File.AppendAllText(SharedApplicationPaths.CsvPath, result.Split(Environment.NewLine)[0]);
        Preferences.Default.Set("IsHeaderWritten", true);
    }
}