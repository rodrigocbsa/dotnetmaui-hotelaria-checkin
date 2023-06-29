using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Hospede.Methods;

static class ToastMethods
{
    internal static async Task ShowToastAsync(string message, ToastDuration duration, double fontSize = 14)
    {
        CancellationTokenSource cancellationTokenSource = new();

        var toast = Toast.Make(message, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }
}
