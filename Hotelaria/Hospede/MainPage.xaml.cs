using Hospede.Methods;
using Hospede.Pages;
using System.Diagnostics;

namespace Hospede;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        CheckIn_btn.Clicked += OnCheckInClicked;
    }

    protected async override void OnAppearing()
    {
        if (!await PermissionMethods.CheckAndRequestFileStoragePermissions().ConfigureAwait(true))
        {
            Preferences.Default.Set("Permissao", false);
        }
        else
        {
            Preferences.Default.Set("Permissao", true);
        }

        if (Preferences.Default.Get("Permissao", false) == false)
        {
            await DisplayAlert("Aviso", "O aplicativo não irá funcionar sem as devidas permissões. Por favor, permita o acesso nas configurações. Após ler este aviso, o aplicativo irá fechar.", "Ok");
            Application.Current.Quit();
        }
    }


    private async void OnCheckInClicked(object sender, EventArgs e)
    {
        bool answer = Preferences.Default.Get("AvisoQrCode", true);

        if (Preferences.Default.Get("CamposVazios", true) == false)
        {
            await Navigation.PushModalAsync(new QrCodePage());
        }
        else
        {
            if (answer)
            {
                answer = await DisplayAlert("Aviso", "O QrCode só poderá ser gerado com todos os campos da ficha preenchidos.", "Entendi", "Não mostrar novamente");
            }

            if (answer == false)
            {
                Preferences.Default.Set("AvisoQrCode", false);
            }
        }

    }
}