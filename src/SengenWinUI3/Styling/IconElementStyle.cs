using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

internal static class IconElementStyle
{
    internal static readonly IStyleGroup<WinUI.Controls.IconElement> Foreground = new StyleGroup<WinUI.Controls.IconElement>(
        StyleItems.Foreground,
        static (見た目, 本物) =>
        {
            if (見た目.CachedForeground() is { } 色) 本物.Foreground = 色;
        });
}
