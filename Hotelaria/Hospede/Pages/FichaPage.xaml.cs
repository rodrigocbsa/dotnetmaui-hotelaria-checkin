using Hospede.Methods;
using Hospede.Models;
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
        if (File.Exists(SharedApplicationPaths.QrPath))
            LoadCSVDataToXAML();
    }


    private async void SaveXAMLDataToCSV()
    {
        bool answer = Preferences.Default.Get("AvisoSalvamento", true);

        Ficha ficha = DeXAMLParaFicha();
        if (ficha != null)
            CsvMethods.DeFichaParaCSV(ficha);
        else
        {
            if (answer)
            {
                answer = await DisplayAlert("Aviso de salvamento autom�tico", "Houveram campos vazios na ficha e portanto seus dados n�o puderam ser salvos. Volte e preencha todos os campos para habilitar a funcionalidade.", "Entendi", "N�o mostrar novamente");
            }
            if (answer == false)
            {
                Preferences.Default.Set("AvisoSalvamento", false);
            }
        }

    }
    private void LoadCSVDataToXAML()
    {
        Ficha ficha = CsvMethods.DeCSVParaFicha();
        if (ficha != null)
            DeFichaParaXAML(ficha);
    }


    private Ficha DeXAMLParaFicha()
    {
        Ficha ficha = null;

        if (Nome.Text != null &&
            CPF.Text != null &&
            CEP.Text != null &&
            DataNascimento.Date.ToString() != null &&
            Email.Text != null &&
            Telefone.Text != null)
        {
            ficha = new()
            {
                NomeCompleto = Nome.Text,
                CPF = CPF.Text,
                CEP = CEP.Text,
                DataNascimento = Convert.ToString(DataNascimento.Date),
                Email = Email.Text,
                Telefone = Telefone.Text
            };
            Preferences.Default.Set("Aceite", Aceite.IsChecked);
            Preferences.Default.Set("CamposVazios", false);
        }
        else
        {
            Preferences.Default.Set("CamposVazios", true);
        }


        return ficha;
    }
    private void DeFichaParaXAML(Ficha ficha)
    {
        Nome.Text = ficha.NomeCompleto;
        CPF.Text = ficha.CPF;
        CEP.Text = ficha.CEP;
        DataNascimento.Date = Convert.ToDateTime(ficha.DataNascimento);
        Telefone.Text = ficha.Telefone;
        Email.Text = ficha.Email;
        Aceite.IsChecked = Preferences.Default.Get("Aceite", false);
    }
}