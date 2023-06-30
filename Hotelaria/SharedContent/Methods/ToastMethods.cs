using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace SharedContent.Methods;

public static class ToastMethods
{
    public static async Task ShowToastAsync(string message, ToastDuration duration, double fontSize = 14)
    {
        CancellationTokenSource cancellationTokenSource = new();

        var toast = Toast.Make(message, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }
}
