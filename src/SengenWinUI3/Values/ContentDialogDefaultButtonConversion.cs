namespace SengenWinUI3;

internal static class ContentDialogDefaultButtonConversion
{
    internal static Microsoft.UI.Xaml.Controls.ContentDialogButton ToWinUI(this ContentDialogDefaultButton 既定のボタン) => 既定のボタン switch
    {
        ContentDialogDefaultButton.None => Microsoft.UI.Xaml.Controls.ContentDialogButton.None,
        ContentDialogDefaultButton.Primary => Microsoft.UI.Xaml.Controls.ContentDialogButton.Primary,
        ContentDialogDefaultButton.Secondary => Microsoft.UI.Xaml.Controls.ContentDialogButton.Secondary,
        ContentDialogDefaultButton.Close => Microsoft.UI.Xaml.Controls.ContentDialogButton.Close,
        _ => throw new ArgumentOutOfRangeException(nameof(既定のボタン), 既定のボタン, null),
    };
}
