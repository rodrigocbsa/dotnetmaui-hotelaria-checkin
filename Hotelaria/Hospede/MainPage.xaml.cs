using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Core;
using Hospede.Methods;
using Hospede.Models;
using Hospede.Pages;
using SharedContent.Methods;
using SharedContent.Paths;

namespace Hospede;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        Save.Clicked += OnSaveClicked;
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

        
        if (File.Exists(SharedApplicationPaths.CsvPath))
            LoadCSVDataToXAML();
    }

    protected async void OnSaveClicked(object sender, EventArgs e)
    {
        Save.IsEnabled = false;

        Ficha ficha = DeXAMLParaFicha();
        if(ficha == null)
            await ToastMethods.ShowToastAsync("O QrCode só poderá ser gerado com todos os campos da ficha preenchidos.", ToastDuration.Long);
        else
        {
            CSVMethods.DeFichaParaCSV(ficha);
            await ToastMethods.ShowToastAsync("Dados salvos localmente. Gerando QrCode...", ToastDuration.Long);
            await Navigation.PushModalAsync(new QrCodePage());
        }

        Save.IsEnabled = true;
    }

    private void LoadCSVDataToXAML()
    {
        Ficha ficha = CSVMethods.DeCSVParaFicha();
        DeFichaParaXAML(ficha);
    }


    private Ficha DeXAMLParaFicha()
    {
        Ficha ficha = null;
        IsStringNullOrEmptyConverter isStringNullOrEmpty = new();

        if (!isStringNullOrEmpty.ConvertFrom(Nome.Text) &&
            !isStringNullOrEmpty.ConvertFrom(CPF.Text) &&
            !isStringNullOrEmpty.ConvertFrom(CEP.Text) &&
            !isStringNullOrEmpty.ConvertFrom(DataNascimento.Date.ToString()) &&
            !isStringNullOrEmpty.ConvertFrom(Sexo.Text) &&
            !isStringNullOrEmpty.ConvertFrom(Email.Text) &&
            !isStringNullOrEmpty.ConvertFrom(Telefone.Text) &&
            TermoAceite.IsChecked)
        {
            ficha = new()
            {
                NomeCompleto = Nome.Text,
                CPF = CPF.Text,
                CEP = CEP.Text,
                DataNascimento = Convert.ToString(DataNascimento.Date),
                Sexo = Sexo.Text,
                Email = Email.Text,
                Telefone = Telefone.Text
            };
            Preferences.Default.Set("Termo", true);
        }
        else
        {
            Preferences.Default.Set("Termo", false);
        }


        return ficha;
    }
    private void DeFichaParaXAML(Ficha ficha)
    {
        Nome.Text = ficha.NomeCompleto;
        CPF.Text = ficha.CPF;
        CEP.Text = ficha.CEP;
        DataNascimento.Date = Convert.ToDateTime(ficha.DataNascimento);
        Sexo.Text = ficha.Sexo;
        Telefone.Text = ficha.Telefone;
        Email.Text = ficha.Email;
        TermoAceite.IsChecked = Preferences.Default.Get("Termo", false);
    }
}