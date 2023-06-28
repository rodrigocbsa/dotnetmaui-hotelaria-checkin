using Hotel.Pages;

namespace Hotel;

public partial class MainPage : ContentPage
{
	public MainPage()
    {
        InitializeComponent();
        CheckIn_btn.Clicked += OnCheckInClicked;
    }

    private async void OnCheckInClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ScanPage());
    }
}

