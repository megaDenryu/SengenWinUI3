using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// Border は Control でも Panel でもないので、自分が持つ見た目をここで当てる。
internal static class BorderStyle
{
    internal static readonly IStyleGroup<WinUI.Controls.Border> Padding = new StyleGroup<WinUI.Controls.Border>(
        StyleItems.Padding,
        static (style, 本物) =>
        {
            if (style.Padding is { } 内側の余白) 本物.Padding = 内側の余白.ToWinUI();
        });

    internal static readonly IStyleGroup<WinUI.Controls.Border> Background = new StyleGroup<WinUI.Controls.Border>(
        StyleItems.Background,
        static (style, 本物) =>
        {
            if (style.CachedBackground() is { } 背景) 本物.Background = 背景;
        });

    internal static readonly IStyleGroup<WinUI.Controls.Border> Border = new StyleGroup<WinUI.Controls.Border>(
        StyleItems.BorderBrush | StyleItems.BorderThickness | StyleItems.CornerRadius,
        static (style, 本物) =>
        {
            if (style.CachedBorderBrush() is { } 枠線の色) 本物.BorderBrush = 枠線の色;
            if (style.BorderThickness is { } 枠線の太さ) 本物.BorderThickness = 枠線の太さ.ToWinUI();
            if (style.CornerRadius is { } 丸み) 本物.CornerRadius = new WinUI.CornerRadius(丸み.Value);
        });

    internal static readonly StyleTarget<WinUI.Controls.Border> All = new(FrameworkElementStyle.Layout, Padding, Background, Border);
}
