using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

internal static class IconElementStyle
{
    internal static readonly IStyleGroup<WinUI.Controls.IconElement> Foreground = new StyleGroup<WinUI.Controls.IconElement>(
        StyleItems.Foreground,
        static (style, 本物) =>
        {
            if (style.CachedForeground() is { } 色) 本物.Foreground = 色;
        });
}
