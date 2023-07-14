using BarcodeScanner.Mobile;
using CommunityToolkit.Maui.Core;
using Microsoft.VisualBasic;
using SharedContent.Methods;
using SharedContent.Paths;

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
            await DisplayAlert("Aviso", "O aplicativo não irá funcionar sem as devidas permissões, por favor, permita o acesso nas configurações. Após ler este aviso, o aplicativo irá fechar.", "Ok");
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
        await ToastMethods.ShowToastAsync("QrCode capturado.", ToastDuration.Short);
        string result = ReadQrCodeContent(e);
        SaveFNRHAsStringToCSV(result);

        await DisplayAlert("Dados salvos", result, "Ok");
        Camera.IsScanning = true;
        await ToastMethods.ShowToastAsync("Aguardando QrCode...", ToastDuration.Short);
    }

    private string ReadQrCodeContent(OnDetectedEventArg e)
    {
        List<BarcodeResult> obj = e.BarcodeResults;

        string result = string.Empty;
        for (var i = 0; i < obj.Count; i++)
        {
            result += obj[i].DisplayValue;
        }
        // Transpondo o final de linha e concatenando hora e data do checkin
        return result.Remove(result.Length - 2) + "," + DateAndTime.TimeString + "," + DateAndTime.DateString + "\r\n";
    }
    private void SaveFNRHAsStringToCSV(string result)
    {
        if (Preferences.Default.Get("IsHeaderWritten", false) == false)
        {
            WriteHeaderToCsvOnce(result);
        }
        // Pula o cabeçalho e concatena data hora checkin
        File.AppendAllText(SharedApplicationPaths.CsvPath, result.Split(Environment.NewLine)[1]);
    }

    private void WriteHeaderToCsvOnce(string result)
    {
        File.AppendAllText(SharedApplicationPaths.CsvPath, result.Split(Environment.NewLine)[0].Remove(result.Split(Environment.NewLine)[0].Length - 1) + ",HoraCheckIn,DataCheckIn\r\n");
        Preferences.Default.Set("IsHeaderWritten", true);
    }
}