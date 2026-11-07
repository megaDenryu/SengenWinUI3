using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

internal static class FontIconStyle
{
    internal static readonly IStyleGroup<WinUI.Controls.FontIcon> Font = new StyleGroup<WinUI.Controls.FontIcon>(
        StyleItems.FontSize | StyleItems.FontWeight | StyleItems.FontFamily,
        static (見た目, 本物) =>
        {
            if (見た目.FontSize is { } 大きさ) 本物.FontSize = 大きさ.Value;
            if (見た目.FontWeight is { } 太さ) 本物.FontWeight = 太さ.ToWinUI();
            if (見た目.CachedFontFamily() is { } フォント) 本物.FontFamily = フォント;
        });

    internal static readonly StyleTarget<WinUI.Controls.FontIcon> All = new(FrameworkElementStyle.Layout, IconElementStyle.Foreground, Font);
}
