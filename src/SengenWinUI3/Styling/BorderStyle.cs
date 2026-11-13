using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// Border は Control でも Panel でもないので、自分が持つ見た目をここで当てる。
internal static class BorderStyle
{
    internal static readonly IStyleGroup<WinUI.Controls.Border> Padding = new StyleGroup<WinUI.Controls.Border>(
        StyleItems.Padding,
        static (見た目, 本物) =>
        {
            if (見た目.Padding is { } 内側の余白) 本物.Padding = 内側の余白.ToWinUI();
        });

    internal static readonly IStyleGroup<WinUI.Controls.Border> Background = new StyleGroup<WinUI.Controls.Border>(
        StyleItems.Background,
        static (見た目, 本物) =>
        {
            if (見た目.CachedBackground() is { } 背景) 本物.Background = 背景;
        });

    internal static readonly IStyleGroup<WinUI.Controls.Border> Border = new StyleGroup<WinUI.Controls.Border>(
        StyleItems.BorderBrush | StyleItems.BorderThickness | StyleItems.CornerRadius,
        static (見た目, 本物) =>
        {
            if (見た目.CachedBorderBrush() is { } 枠線の色) 本物.BorderBrush = 枠線の色;
            if (見た目.BorderThickness is { } 枠線の太さ) 本物.BorderThickness = 枠線の太さ.ToWinUI();
            if (見た目.CornerRadius is { } 丸み) 本物.CornerRadius = 丸み.ToWinUI();
        });

    internal static readonly StyleTarget<WinUI.Controls.Border> All = new(FrameworkElementStyle.Layout, Padding, Background, Border);
}
