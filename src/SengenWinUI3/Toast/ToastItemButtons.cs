using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 1件のトーストに付けるボタンを作る。ボタンの文字の色は、種類の見た目の文字の色にそろえる。
internal static class ToastItemButtons
{
    internal static WinUI.Controls.Button CreateAction(string text, Action onClick, Action close)
    {
        WinUI.Controls.Button ボタン = new()
        {
            Content = text,
            Margin = new WinUI.Thickness(12, 0, 0, 0),
            VerticalAlignment = WinUI.VerticalAlignment.Center,
        };
        ボタン.Click += (_, _) =>
        {
            onClick();
            close();
        };
        return ボタン;
    }

    internal static WinUI.Controls.Button CreateClose(WinUI.Media.Brush? foreground, Action close)
    {
        WinUI.Controls.Button ボタン = new()
        {
            Content = "×",
            Margin = new WinUI.Thickness(8, 0, 0, 0),
            Padding = new WinUI.Thickness(6, 0, 6, 0),
            VerticalAlignment = WinUI.VerticalAlignment.Top,
            Background = new WinUI.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent),
            BorderThickness = new WinUI.Thickness(0),
        };
        if (foreground is not null)
        {
            ボタン.Foreground = foreground;
        }
        ボタン.Click += (_, _) => close();
        return ボタン;
    }
}
