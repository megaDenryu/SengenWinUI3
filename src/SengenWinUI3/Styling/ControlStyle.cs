using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// Control が持つ項目の当て方。本物が Control の葉(Button・ScrollViewer・後の入力の部品)が使う。
internal static class ControlStyle
{
    internal static readonly IStyleGroup<WinUI.Controls.Control> Padding = new StyleGroup<WinUI.Controls.Control>(
        StyleItems.Padding,
        static (style, 本物) =>
        {
            if (style.Padding is { } 内側の余白) 本物.Padding = 内側の余白.ToWinUI();
        });

    internal static readonly IStyleGroup<WinUI.Controls.Control> Background = new StyleGroup<WinUI.Controls.Control>(
        StyleItems.Background,
        static (style, 本物) =>
        {
            if (style.CachedBackground() is { } 背景) 本物.Background = 背景;
        });

    internal static readonly IStyleGroup<WinUI.Controls.Control> Font = new StyleGroup<WinUI.Controls.Control>(
        StyleItems.Foreground | StyleItems.FontSize | StyleItems.FontWeight,
        static (style, 本物) =>
        {
            if (style.CachedForeground() is { } 文字の色) 本物.Foreground = 文字の色;
            if (style.FontSize is { } 文字の大きさ) 本物.FontSize = 文字の大きさ.Value;
            if (style.FontWeight is { } 文字の太さ) 本物.FontWeight = 文字の太さ.ToWinUI();
        });

    internal static readonly IStyleGroup<WinUI.Controls.Control> Border = new StyleGroup<WinUI.Controls.Control>(
        StyleItems.BorderBrush | StyleItems.BorderThickness | StyleItems.CornerRadius,
        static (style, 本物) =>
        {
            if (style.CachedBorderBrush() is { } 枠線の色) 本物.BorderBrush = 枠線の色;
            if (style.BorderThickness is { } 枠線の太さ) 本物.BorderThickness = 枠線の太さ.ToWinUI();
            if (style.CornerRadius is { } 丸み) 本物.CornerRadius = new WinUI.CornerRadius(丸み.Value);
        });

    // Control の項目を全部持つ葉が、そのまま使う並び。
    internal static readonly StyleTarget<WinUI.Controls.Control> All = new(FrameworkElementStyle.Layout, Padding, Background, Font, Border);
}
