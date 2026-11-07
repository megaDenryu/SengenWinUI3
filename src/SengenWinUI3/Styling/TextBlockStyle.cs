using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// TextBlock が持つ項目の当て方。WinUI の TextBlock は Control でないので、文字の項目を自分で持つ。
internal static class TextBlockStyle
{
    internal static readonly IStyleGroup<WinUI.Controls.TextBlock> Padding = new StyleGroup<WinUI.Controls.TextBlock>(
        StyleItems.Padding,
        static (見た目, 本物) =>
        {
            if (見た目.Padding is { } 内側の余白) 本物.Padding = 内側の余白.ToWinUI();
        });

    internal static readonly IStyleGroup<WinUI.Controls.TextBlock> Font = new StyleGroup<WinUI.Controls.TextBlock>(
        StyleItems.Foreground | StyleItems.FontSize | StyleItems.FontWeight | StyleItems.FontFamily,
        static (見た目, 本物) =>
        {
            if (見た目.CachedForeground() is { } 文字の色) 本物.Foreground = 文字の色;
            if (見た目.FontSize is { } 文字の大きさ) 本物.FontSize = 文字の大きさ.Value;
            if (見た目.FontWeight is { } 文字の太さ) 本物.FontWeight = 文字の太さ.ToWinUI();
            if (見た目.CachedFontFamily() is { } フォント) 本物.FontFamily = フォント;
        });

    internal static readonly IStyleGroup<WinUI.Controls.TextBlock> Text = new StyleGroup<WinUI.Controls.TextBlock>(
        StyleItems.TextWrapping | StyleItems.IsTextSelectionEnabled,
        static (見た目, 本物) =>
        {
            if (見た目.TextWrapping is { } 折り返し方) 本物.TextWrapping = 折り返し方.ToWinUI();
            if (見た目.IsTextSelectionEnabled is { } 選べるか) 本物.IsTextSelectionEnabled = 選べるか;
        });

    internal static readonly IStyleGroup<WinUI.Controls.TextBlock> Alignment = new StyleGroup<WinUI.Controls.TextBlock>(
        StyleItems.TextAlignment | StyleItems.TextTrimming,
        static (見た目, 本物) =>
        {
            if (見た目.TextAlignment is { } 寄せ方) 本物.TextAlignment = 寄せ方.ToWinUI();
            if (見た目.TextTrimming is { } 切り詰め方) 本物.TextTrimming = 切り詰め方.ToWinUI();
        });
}
