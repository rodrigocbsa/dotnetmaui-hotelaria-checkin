using Hospede.Pages;
using System.Diagnostics;

namespace Hospede;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        About.Clicked += OnAboutClicked;
        Routing.RegisterRoute(nameof(MainPage),typeof(MainPage));
    }

    private async void OnAboutClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AboutPage());
    }
}