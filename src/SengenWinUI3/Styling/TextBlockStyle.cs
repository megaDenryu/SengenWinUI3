using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// TextBlock が持つ項目の当て方。WinUI の TextBlock は Control でないので、文字の項目を自分で持つ。
internal static class TextBlockStyle
{
    internal static readonly IStyleGroup<WinUI.Controls.TextBlock> Padding = new StyleGroup<WinUI.Controls.TextBlock>(
        StyleItems.Padding,
        static (style, 本物) =>
        {
            if (style.Padding is { } 内側の余白) 本物.Padding = 内側の余白.ToWinUI();
        });

    internal static readonly IStyleGroup<WinUI.Controls.TextBlock> Font = new StyleGroup<WinUI.Controls.TextBlock>(
        StyleItems.Foreground | StyleItems.FontSize | StyleItems.FontWeight,
        static (style, 本物) =>
        {
            if (style.CachedForeground() is { } 文字の色) 本物.Foreground = 文字の色;
            if (style.FontSize is { } 文字の大きさ) 本物.FontSize = 文字の大きさ.Value;
            if (style.FontWeight is { } 文字の太さ) 本物.FontWeight = 文字の太さ.ToWinUI();
        });

    internal static readonly IStyleGroup<WinUI.Controls.TextBlock> Text = new StyleGroup<WinUI.Controls.TextBlock>(
        StyleItems.TextWrapping | StyleItems.IsTextSelectionEnabled,
        static (style, 本物) =>
        {
            if (style.TextWrapping is { } 折り返し方) 本物.TextWrapping = 折り返し方.ToWinUI();
            if (style.IsTextSelectionEnabled is { } 選べるか) 本物.IsTextSelectionEnabled = 選べるか;
        });
}
