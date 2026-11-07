using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// Panel が持つ項目の当て方。WinUI の Panel が共通に持つのは背景だけであり、余白と枠は StackPanel・Grid がそれぞれ持つ。
internal static class PanelStyle
{
    internal static readonly IStyleGroup<WinUI.Controls.Panel> Background = new StyleGroup<WinUI.Controls.Panel>(
        StyleItems.Background,
        static (style, 本物) =>
        {
            if (style.CachedBackground() is { } 背景) 本物.Background = 背景;
        });
}
