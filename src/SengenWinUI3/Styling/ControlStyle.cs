using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// Control が持つ項目の当て方。本物が Control の葉(Button・ScrollViewer・後の入力の部品)が使う。
internal static class ControlStyle
{
    internal static readonly IStyleGroup<WinUI.Controls.Control> Padding = new StyleGroup<WinUI.Controls.Control>(
        StyleItems.Padding,
        static (見た目, 本物) =>
        {
            if (見た目.Padding is { } 内側の余白) 本物.Padding = 内側の余白.ToWinUI();
        });

    internal static readonly IStyleGroup<WinUI.Controls.Control> Background = new StyleGroup<WinUI.Controls.Control>(
        StyleItems.Background,
        static (見た目, 本物) =>
        {
            if (見た目.CachedBackground() is { } 背景) 本物.Background = 背景;
        });

    internal static readonly IStyleGroup<WinUI.Controls.Control> Font = new StyleGroup<WinUI.Controls.Control>(
        StyleItems.Foreground | StyleItems.FontSize | StyleItems.FontWeight | StyleItems.FontFamily,
        static (見た目, 本物) =>
        {
            if (見た目.CachedForeground() is { } 文字の色) 本物.Foreground = 文字の色;
            if (見た目.FontSize is { } 文字の大きさ) 本物.FontSize = 文字の大きさ.Value;
            if (見た目.FontWeight is { } 文字の太さ) 本物.FontWeight = 文字の太さ.ToWinUI();
            if (見た目.CachedFontFamily() is { } フォント) 本物.FontFamily = フォント;
        });

    internal static readonly IStyleGroup<WinUI.Controls.Control> Border = new StyleGroup<WinUI.Controls.Control>(
        StyleItems.BorderBrush | StyleItems.BorderThickness | StyleItems.CornerRadius,
        static (見た目, 本物) =>
        {
            if (見た目.CachedBorderBrush() is { } 枠線の色) 本物.BorderBrush = 枠線の色;
            if (見た目.BorderThickness is { } 枠線の太さ) 本物.BorderThickness = 枠線の太さ.ToWinUI();
            if (見た目.CornerRadius is { } 丸み) 本物.CornerRadius = new WinUI.CornerRadius(丸み.Value);
        });

    internal static readonly IStyleGroup<WinUI.Controls.Control> ContentAlignment = new StyleGroup<WinUI.Controls.Control>(
        StyleItems.HorizontalContentAlignment | StyleItems.VerticalContentAlignment,
        static (見た目, 本物) =>
        {
            if (見た目.HorizontalContentAlignment is { } 横の寄せ方) 本物.HorizontalContentAlignment = 横の寄せ方.ToWinUI();
            if (見た目.VerticalContentAlignment is { } 縦の寄せ方) 本物.VerticalContentAlignment = 縦の寄せ方.ToWinUI();
        });

    // Control の項目を全部持つ葉が、そのまま使う。
    internal static readonly StyleTarget<WinUI.Controls.Control> All = new(FrameworkElementStyle.Layout, Padding, Background, Font, Border, ContentAlignment);
}
