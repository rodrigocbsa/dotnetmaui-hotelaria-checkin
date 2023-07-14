using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Core;
using Hospede.Methods;
using Hospede.Models;
using SharedContent.Methods;
using SharedContent.Paths;

namespace Hospede.Pages;

public partial class FichaPage : ContentPage
{
    public FichaPage()
    {
        InitializeComponent();
    }

    protected override void OnDisappearing()
    {
        SaveXAMLDataToCSV();
    }
    protected override void OnAppearing()
    {
        if (File.Exists(SharedApplicationPaths.CsvPath))
            LoadCSVDataToXAML();
    }


    private async void SaveXAMLDataToCSV()
    {
        Ficha ficha = DeXAMLParaFicha();
        if (ficha != null)
        {
            CSVMethods.DeFichaParaCSV(ficha);
            await ToastMethods.ShowToastAsync("Dados salvos localmente.", ToastDuration.Short);
        }            
        else
        {
            await ToastMethods.ShowToastAsync("Salvamento automático: houveram campos vazios na ficha e portanto seus dados não puderam ser salvos.", ToastDuration.Long);
            File.Delete(SharedApplicationPaths.CsvPath);
            File.Delete(SharedApplicationPaths.QrPath);
        }

    }
    private void LoadCSVDataToXAML()
    {
        Ficha ficha = CSVMethods.DeCSVParaFicha();
        if (ficha != null)
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
            Preferences.Default.Set("CamposVazios", false);
            Preferences.Default.Set("Termo", true);
        }
        else
        {
            Preferences.Default.Set("CamposVazios", true);
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